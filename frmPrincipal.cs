/****************************************************************************
**
** Copyright (C) 2019 Gianni Peschiutta (F4IKZ).
** Contact: neophile76@gmail.com
**
** CCS7Manager is free software; you can redistribute it and/or modify
** it under the terms of the GNU General Public License as published by
** the Free Software Foundation; either version 3 of the License, or
** (at your option) any later version.
**
** CCS7Manager is distributed in the hope that it will be useful,
** but WITHOUT ANY WARRANTY; without even the implied warranty of
** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
** GNU General Public License for more details.
**
** The license is as published by the Free Software
** Foundation and appearing in the file LICENSE.GPL3
** included in the packaging of this software. Please review the following
** information to ensure the GNU General Public License requirements will
** be met: https://www.gnu.org/licenses/gpl-3.0.html.
****************************************************************************/

/****************************************************************************
 * MainFrame.cs is part of FF2Play project
 *
 * This class 
 * **************************************************************************/

using CCS7Manager.Properties;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CCS7Manager
{
    /// <summary>
    /// Main Window Class
    /// </summary>
    public partial class frmPrincipal : Form
    {
        private static UserList ul;

        private ApplicationSettingsBase settings;

        // Init state of the window
        private bool windowInitialized;

        // Enum list of available output CSV Format
        private enum RadioType
        {
            HD1,
            MOTO,
            TYT,
            GD77,
            AnyTone,
            RT52,
            RT3S
        }

        // String container for JSON File
        private string CCS7UserList;

        /// <summary>
        /// Constructor for main window
        /// </summary>
        public frmPrincipal()
        {
            InitializeComponent();
            // Window Default Setting
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            SetAllowUnsafeHeaderParsing20();

            // Copy user settings from previous application version if necessary
            settings = Settings.Default;

            // On vérifie des les données de taille et de position de la fenêtre ne sont pas vides
            // Et qu'elles sont toujours valides avec la configuration d'écran actuelle
            // Ceci pour éviter une fenêtre en dehors des limites
            if (Settings.Default.WindowPosition != Rectangle.Empty &&
                IsVisibleOnAnyScreen(Settings.Default.WindowPosition))
            {
                // Définition de la position et de la taille de la fenêtre
                StartPosition = FormStartPosition.Manual;
                DesktopBounds = Settings.Default.WindowPosition;

                // Définition de l'état de la fenêtre pour être maximisée
                // ou réduite selon la sauvegarde
                WindowState = Settings.Default.WindowState;
            }
            else
            {
                // Réinitialisation de la position à la valeur par défaut
                StartPosition = FormStartPosition.WindowsDefaultLocation;

                // Nous pouvons alors définir la taille enrégistrée si celle ci 
                // Existe
                if (Settings.Default.WindowPosition != Rectangle.Empty)
                {
                    Size = Settings.Default.WindowPosition.Size;
                }
            }
            if (Settings.Default.OutputFolder.Length == 0)
            {
                Settings.Default.OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                Settings.Default.Save();

            }
            tb_OutputFolder.Text = Settings.Default.OutputFolder;
            windowInitialized = true;
        }

        /// <summary>
        /// Check if another instance of this application is already launched
        /// </summary>
        /// <returns></returns>
        public bool SetAllowUnsafeHeaderParsing20()
        {
            //Get the assembly that contains the internal class
            Assembly aNetAssembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
            if (aNetAssembly != null)
            {
                //Use the assembly in order to get the internal type for the internal class
                Type aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
                if (aSettingsType != null)
                {
                    //Use the internal static property to get an instance of the internal settings class.
                    //If the static instance isn't created allready the property will create it for us.
                    object anInstance = aSettingsType.InvokeMember("Section",
                      BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });

                    if (anInstance != null)
                    {
                        //Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
                        FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (aUseUnsafeHeaderParsing != null)
                        {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, true);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if this application is visible on all screen of the desktop
        /// </summary>
        /// <param name="rect">Area to test</param>
        /// <returns>true if visible on all area surface</returns>
        private bool IsVisibleOnAnyScreen(Rectangle rect)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(rect))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Event once window is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
        }

        /// <summary>
        /// Event on resize the current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResize(object sender, EventArgs e)
        {
            TrackWindowState();
        }

        /// <summary>
        /// Event on move the current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMove(object sender, EventArgs e)
        {
            TrackWindowState();
        }

        /// <summary>
        /// Save the last known window state
        /// </summary>
        private void TrackWindowState()
        {
            // Don't record the window setup, otherwise we lose the persistent values!
            if (!windowInitialized) { return; }

            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.WindowPosition = this.DesktopBounds;
            }

            if (WindowState == FormWindowState.Minimized)
            {
            }
        }

        /// <summary>
        /// Closing application event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void OnClosing(object sender, FormClosingEventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.WindowState = WindowState;
                    break;

                default:
                    Properties.Settings.Default.WindowState = FormWindowState.Normal;
                    break;
            }
            // Backup configuration
            settings.Save();
        }
        /// <summary>
        /// After Window Creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
          InitContent();
        }
    
        /// <summary>
        /// Init all contenair on main window
        /// </summary>
        private void InitContent()
        {
            // Populate Countries Check Box List
            this.chkBoxCountries.Items.Add((object) "Afghanistan");
            this.chkBoxCountries.Items.Add((object) "Albania");
            this.chkBoxCountries.Items.Add((object) "Algeria");
            this.chkBoxCountries.Items.Add((object) "American Samoa");
            this.chkBoxCountries.Items.Add((object) "Andorra");
            this.chkBoxCountries.Items.Add((object) "Angola");
            this.chkBoxCountries.Items.Add((object) "Anguilla");
            this.chkBoxCountries.Items.Add((object) "Antigua and Barbuda");
            this.chkBoxCountries.Items.Add((object) "Argentina Republic");
            this.chkBoxCountries.Items.Add((object) "Armenia");
            this.chkBoxCountries.Items.Add((object) "Aruba");
            this.chkBoxCountries.Items.Add((object) "Australia");
            this.chkBoxCountries.Items.Add((object) "Austria");
            this.chkBoxCountries.Items.Add((object) "Azerbaijan");
            this.chkBoxCountries.Items.Add((object) "Bahamas");
            this.chkBoxCountries.Items.Add((object) "Bahrain");
            this.chkBoxCountries.Items.Add((object) "Bangladesh");
            this.chkBoxCountries.Items.Add((object) "Barbados");
            this.chkBoxCountries.Items.Add((object) "Belarus");
            this.chkBoxCountries.Items.Add((object) "Belgium");
            this.chkBoxCountries.Items.Add((object) "Belize");
            this.chkBoxCountries.Items.Add((object) "Benin");
            this.chkBoxCountries.Items.Add((object) "Bermuda");
            this.chkBoxCountries.Items.Add((object) "Bhutan");
            this.chkBoxCountries.Items.Add((object) "Bolivia");
            this.chkBoxCountries.Items.Add((object) "Bosnia And Herzegovin");
            this.chkBoxCountries.Items.Add((object) "Botswana");
            this.chkBoxCountries.Items.Add((object) "Brazil");
            this.chkBoxCountries.Items.Add((object) "British Virgin Islands");
            this.chkBoxCountries.Items.Add((object) "Brunei Darussalam");
            this.chkBoxCountries.Items.Add((object) "Bulgaria");
            this.chkBoxCountries.Items.Add((object) "Burkina Faso");
            this.chkBoxCountries.Items.Add((object) "Burma");
            this.chkBoxCountries.Items.Add((object) "Burundi");
            this.chkBoxCountries.Items.Add((object) "Cambodia");
            this.chkBoxCountries.Items.Add((object) "Canada");
            this.chkBoxCountries.Items.Add((object) "Cayman Islands");
            this.chkBoxCountries.Items.Add((object) "Chile");
            this.chkBoxCountries.Items.Add((object) "China");
            this.chkBoxCountries.Items.Add((object) "Colombia");
            this.chkBoxCountries.Items.Add((object) "Comoros");
            this.chkBoxCountries.Items.Add((object) "Congo, Dem. Rep.");
            this.chkBoxCountries.Items.Add((object) "Cook Islands");
            this.chkBoxCountries.Items.Add((object) "Corse");
            this.chkBoxCountries.Items.Add((object) "Costa Rica");
            this.chkBoxCountries.Items.Add((object) "Croatia");
            this.chkBoxCountries.Items.Add((object) "Cuba");
            this.chkBoxCountries.Items.Add((object) "Cyprus");
            this.chkBoxCountries.Items.Add((object) "Czech Republic");
            this.chkBoxCountries.Items.Add((object) "Denmark");
            this.chkBoxCountries.Items.Add((object) "Djibouti");
            this.chkBoxCountries.Items.Add((object) "Dominica");
            this.chkBoxCountries.Items.Add((object) "Dominican Republic");
            this.chkBoxCountries.Items.Add((object) "Ecuador");
            this.chkBoxCountries.Items.Add((object) "Egypt");
            this.chkBoxCountries.Items.Add((object) "El Salvador");
            this.chkBoxCountries.Items.Add((object) "Eritrea");
            this.chkBoxCountries.Items.Add((object) "Estonia");
            this.chkBoxCountries.Items.Add((object) "Ethiopia");
            this.chkBoxCountries.Items.Add((object) "Faroe Islands");
            this.chkBoxCountries.Items.Add((object) "Fiji");
            this.chkBoxCountries.Items.Add((object) "Finland");
            this.chkBoxCountries.Items.Add((object) "France");
            this.chkBoxCountries.Items.Add((object) "French Guiana");
            this.chkBoxCountries.Items.Add((object) "French Polynesia");
            this.chkBoxCountries.Items.Add((object) "Gambia");
            this.chkBoxCountries.Items.Add((object) "Georgia");
            this.chkBoxCountries.Items.Add((object) "Germany");
            this.chkBoxCountries.Items.Add((object) "Gibraltar");
            this.chkBoxCountries.Items.Add((object) "Greece");
            this.chkBoxCountries.Items.Add((object) "Greenland");
            this.chkBoxCountries.Items.Add((object) "Grenada");
            this.chkBoxCountries.Items.Add((object) "Guam");
            this.chkBoxCountries.Items.Add((object) "Guatemala");
            this.chkBoxCountries.Items.Add((object) "Guinea");
            this.chkBoxCountries.Items.Add((object) "Guinea-Bissau");
            this.chkBoxCountries.Items.Add((object) "Guyana");
            this.chkBoxCountries.Items.Add((object) "Haiti");
            this.chkBoxCountries.Items.Add((object) "Honduras");
            this.chkBoxCountries.Items.Add((object) "Hong Kong");
            this.chkBoxCountries.Items.Add((object) "Hungary");
            this.chkBoxCountries.Items.Add((object) "Iceland");
            this.chkBoxCountries.Items.Add((object) "India");
            this.chkBoxCountries.Items.Add((object) "Indonesia");
            this.chkBoxCountries.Items.Add((object) "Iran");
            this.chkBoxCountries.Items.Add((object) "Iraq");
            this.chkBoxCountries.Items.Add((object) "Ireland");
            this.chkBoxCountries.Items.Add((object) "Israel");
            this.chkBoxCountries.Items.Add((object) "Italy");
            this.chkBoxCountries.Items.Add((object) "Ivory Coast");
            this.chkBoxCountries.Items.Add((object) "Jamaica");
            this.chkBoxCountries.Items.Add((object) "Japan");
            this.chkBoxCountries.Items.Add((object) "Jordan");
            this.chkBoxCountries.Items.Add((object) "Kazakhstan");
            this.chkBoxCountries.Items.Add((object) "Kenya");
            this.chkBoxCountries.Items.Add((object) "Kiribati");
            this.chkBoxCountries.Items.Add((object) "Korea S, Republic of");
            this.chkBoxCountries.Items.Add((object) "Kuwait");
            this.chkBoxCountries.Items.Add((object) "Kyrgyzstan");
            this.chkBoxCountries.Items.Add((object) "Laos P.D.R.");
            this.chkBoxCountries.Items.Add((object) "Latvia");
            this.chkBoxCountries.Items.Add((object) "Lebanon");
            this.chkBoxCountries.Items.Add((object) "Lesotho");
            this.chkBoxCountries.Items.Add((object) "Liberia");
            this.chkBoxCountries.Items.Add((object) "Libya");
            this.chkBoxCountries.Items.Add((object) "Liechtenstein");
            this.chkBoxCountries.Items.Add((object) "Lithuania");
            this.chkBoxCountries.Items.Add((object) "Luxembourg");
            this.chkBoxCountries.Items.Add((object) "Macao, China");
            this.chkBoxCountries.Items.Add((object) "Macedonia");
            this.chkBoxCountries.Items.Add((object) "Madagascar");
            this.chkBoxCountries.Items.Add((object) "Malawi");
            this.chkBoxCountries.Items.Add((object) "Malaysia");
            this.chkBoxCountries.Items.Add((object) "Maldives");
            this.chkBoxCountries.Items.Add((object) "Mali");
            this.chkBoxCountries.Items.Add((object) "Malta");
            this.chkBoxCountries.Items.Add((object) "Mauritania");
            this.chkBoxCountries.Items.Add((object) "Mauritius");
            this.chkBoxCountries.Items.Add((object) "Mexico");
            this.chkBoxCountries.Items.Add((object) "Micronesia");
            this.chkBoxCountries.Items.Add((object) "Moldova");
            this.chkBoxCountries.Items.Add((object) "Monaco");
            this.chkBoxCountries.Items.Add((object) "Mongolia");
            this.chkBoxCountries.Items.Add((object) "Montenegro");
            this.chkBoxCountries.Items.Add((object) "Montserrat");
            this.chkBoxCountries.Items.Add((object) "Morocco");
            this.chkBoxCountries.Items.Add((object) "Mozambique");
            this.chkBoxCountries.Items.Add((object) "Namibia");
            this.chkBoxCountries.Items.Add((object) "Nepal");
            this.chkBoxCountries.Items.Add((object) "Netherlands");
            this.chkBoxCountries.Items.Add((object) "Netherlands Antilles");
            this.chkBoxCountries.Items.Add((object) "New Caledonia");
            this.chkBoxCountries.Items.Add((object) "New Zealand");
            this.chkBoxCountries.Items.Add((object) "Nicaragua");
            this.chkBoxCountries.Items.Add((object) "Niger");
            this.chkBoxCountries.Items.Add((object) "Norway");
            this.chkBoxCountries.Items.Add((object) "Oman");
            this.chkBoxCountries.Items.Add((object) "Pakistan");
            this.chkBoxCountries.Items.Add((object) "Panama");
            this.chkBoxCountries.Items.Add((object) "Papua New Guinea");
            this.chkBoxCountries.Items.Add((object) "Paraguay");
            this.chkBoxCountries.Items.Add((object) "Peru");
            this.chkBoxCountries.Items.Add((object) "Philippines");
            this.chkBoxCountries.Items.Add((object) "Poland");
            this.chkBoxCountries.Items.Add((object) "Portugal");
            this.chkBoxCountries.Items.Add((object) "Puerto Rico");
            this.chkBoxCountries.Items.Add((object) "Qatar");
            this.chkBoxCountries.Items.Add((object) "Reunion");
            this.chkBoxCountries.Items.Add((object) "Romania");
            this.chkBoxCountries.Items.Add((object) "Russia");
            this.chkBoxCountries.Items.Add((object) "Rwanda");
            this.chkBoxCountries.Items.Add((object) "Saint Kitts and Nevis");
            this.chkBoxCountries.Items.Add((object) "Saint Lucia");
            this.chkBoxCountries.Items.Add((object) "San Marino");
            this.chkBoxCountries.Items.Add((object) "Samoa");
            this.chkBoxCountries.Items.Add((object) "Satellite Networks");
            this.chkBoxCountries.Items.Add((object) "Saudi Arabia");
            this.chkBoxCountries.Items.Add((object) "Senegal");
            this.chkBoxCountries.Items.Add((object) "Serbia");
            this.chkBoxCountries.Items.Add((object) "Seychelles");
            this.chkBoxCountries.Items.Add((object) "Sierra Leone");
            this.chkBoxCountries.Items.Add((object) "Singapore");
            this.chkBoxCountries.Items.Add((object) "Slovakia");
            this.chkBoxCountries.Items.Add((object) "Slovenia");
            this.chkBoxCountries.Items.Add((object) "Solomon Islands");
            this.chkBoxCountries.Items.Add((object) "Somalia");
            this.chkBoxCountries.Items.Add((object) "South Africa");
            this.chkBoxCountries.Items.Add((object) "Spain");
            this.chkBoxCountries.Items.Add((object) "Sri Lanka");
            this.chkBoxCountries.Items.Add((object) "St. Pierre and Miquelon");
            this.chkBoxCountries.Items.Add((object) "St. Vincent and Gren.");
            this.chkBoxCountries.Items.Add((object) "Sudan");
            this.chkBoxCountries.Items.Add((object) "Suriname");
            this.chkBoxCountries.Items.Add((object) "Swaziland");
            this.chkBoxCountries.Items.Add((object) "Sweden");
            this.chkBoxCountries.Items.Add((object) "Switzerland");
            this.chkBoxCountries.Items.Add((object) "Syrian Arab Republic");
            this.chkBoxCountries.Items.Add((object) "Taiwan");
            this.chkBoxCountries.Items.Add((object) "Tajikistan");
            this.chkBoxCountries.Items.Add((object) "Tanzania");
            this.chkBoxCountries.Items.Add((object) "Thailand");
            this.chkBoxCountries.Items.Add((object) "Timor-Leste");
            this.chkBoxCountries.Items.Add((object) "Togo");
            this.chkBoxCountries.Items.Add((object) "Tonga");
            this.chkBoxCountries.Items.Add((object) "Trinidad and Tobago");
            this.chkBoxCountries.Items.Add((object) "Tunisia");
            this.chkBoxCountries.Items.Add((object) "Turkey");
            this.chkBoxCountries.Items.Add((object) "Turkmenistan");
            this.chkBoxCountries.Items.Add((object) "Uganda");
            this.chkBoxCountries.Items.Add((object) "Ukraine");
            this.chkBoxCountries.Items.Add((object) "United Arab Emirates");
            this.chkBoxCountries.Items.Add((object) "United Kingdom");
            this.chkBoxCountries.Items.Add((object) "United States");
            this.chkBoxCountries.Items.Add((object) "Uruguay");
            this.chkBoxCountries.Items.Add((object) "Uzbekistan");
            this.chkBoxCountries.Items.Add((object) "Vanuatu");
            this.chkBoxCountries.Items.Add((object) "Venezuela");
            this.chkBoxCountries.Items.Add((object) "Viet Nam");
            this.chkBoxCountries.Items.Add((object) "Virgin Islands, U.S.");
            this.chkBoxCountries.Items.Add((object) "Yemen");
            this.chkBoxCountries.Items.Add((object) "Zambia");
            this.chkBoxCountries.Items.Add((object) "Zimbabwe");

            // Populate Radio List ChecBox
            this.chkListRadios.Items.Add((object)"Ailunce HD1");
            this.chkListRadios.Items.Add((object)"Retevis RT52");
            this.chkListRadios.Items.Add((object)"Retevis RT3S / RT90 / RT84 / RT82");
            this.chkListRadios.Items.Add((object)"AnyTone");
            this.chkListRadios.Items.Add((object)"Radioddity GD-77");
            this.chkListRadios.Items.Add((object)"TYT MD-380 / MD-2017 / MD-9600");

            //Load the cached JSON File if exist
            LoadJSON(true);
        }
    
        /// <summary>
        /// Get CCS7 Contacts List form WEB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportWeb_Click(object sender, EventArgs e)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    Uri uri = new Uri("https://www.radioid.net/static/users.json");
                    tsState.Text = "USER LIST DOWNLOAD STARTING...";
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
                    webClient.DownloadProgressChanged += DownloadProgressCallBack;
                    webClient.DownloadStringAsync(uri);
                    btnImportWeb.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                int num = (int) MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// The JSON File as been downloaded or aborted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                CCS7UserList = (string)e.Result;
                if (ReadCCS7UserList())
                {
                    tsState.Text = "DOWNLOAD COMPLETED";
                    lblVersionDate.Text = "Version Date : " + DateTime.Now.ToShortDateString();
                    SaveJSON();
                }
            }
            else
            {
                tsState.Text = "DOWNLOAD ERROR - PLEASE TRY AGAIN";
            }
            btnImportWeb.Enabled = true;
        }

        /// <summary>
        /// Decode the JSON file to the user list
        /// </summary>
        /// <returns></returns>
        private bool ReadCCS7UserList()
        {
            try
            {
                frmPrincipal.ul = JsonConvert.DeserializeObject<UserList>(CCS7UserList);
                chkAllRadios.Enabled = true;
                chkBoxCountries.Enabled = true;
                chkListRadios.Enabled = true;
                chkAllCountries.Enabled = true;
                btnExport.Enabled = true;
                btnImportWeb.Text = "Update";
                UpdateContactSelected();
                return true;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }

        /// <summary>
        /// Download progression udpate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadProgressCallBack(object sender, DownloadProgressChangedEventArgs e)
        {
            tsState.Text = string.Format("DOWNLOADING : {0} % COMPLETE", e.ProgressPercentage);
        }

        /// <summary>
        /// CheckBox All Radio changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkTodos_CheckedChanged(object sender, EventArgs e)
        {
          if (this.chkAllCountries.Checked)
            this.chkBoxCountries.Enabled = false;
          else
            this.chkBoxCountries.Enabled = true;
        }
    
        /// <summary>
        /// Button Export Contact Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (chkAllRadios.Checked)
            {
                Export_Contacts(RadioType.HD1);
                Export_Contacts(RadioType.MOTO);
                Export_Contacts(RadioType.TYT);
                Export_Contacts(RadioType.GD77);
                Export_Contacts(RadioType.AnyTone);
                Export_Contacts(RadioType.RT52);
                Export_Contacts(RadioType.RT3S);
            }
            else
            {
                for (int index = 0; index < chkListRadios.Items.Count; ++index)
                {
                    if (chkListRadios.GetItemChecked(index))
                    {
                        if (chkListRadios.Items[index].ToString() == "Ailunce HD1")
                            Export_Contacts(RadioType.HD1);
                        if (chkListRadios.Items[index].ToString() == "Motorola DGP-6150+")
                            Export_Contacts(RadioType.MOTO);
                        if (chkListRadios.Items[index].ToString() == "Retevis RT52")
                            Export_Contacts(RadioType.RT52);
                        if (chkListRadios.Items[index].ToString() == "Retevis RT3S / RT90 / RT84 / RT82")
                            Export_Contacts(RadioType.RT3S);
                        if (chkListRadios.Items[index].ToString() == "TYT MD-380 / MD-2017 / MD-9600")
                            Export_Contacts(RadioType.TYT);
                        if (chkListRadios.Items[index].ToString() == "AnyTone")
                            Export_Contacts(RadioType.AnyTone);
                        if (chkListRadios.Items[index].ToString() == "Radioddity GD-77")
                            Export_Contacts(RadioType.GD77);
                    }
                }
            }
            tsState.Text = "FINISHED PROCESS";
        }

        /// <summary>
        /// Export Contact List for the selected Radio Model
        /// </summary>
        /// <param name="pRadio"></param>
        private void Export_Contacts(RadioType pRadio)
        {
            string file = Settings.Default.OutputFolder +
                Path.DirectorySeparatorChar +"Contacts_" +
                pRadio.ToString() +
                ".csv";
            FileInfo fileInfo = new FileInfo(file);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
            tsState.Text = "GENERATING " + pRadio.ToString() + " FILE";
            switch (pRadio)
            {
                case RadioType.AnyTone:
                    streamWriter.WriteLine("No.,Radio ID,Callsign,Name,City,State,Country,Remarks,Call Type,Call Alert");
                    break;
                case RadioType.GD77:
                    streamWriter.WriteLine("Radio ID,Callsign,Name,NickName,City,State,Country,Remarks<br/>");
                    break;
                case RadioType.HD1:
                    streamWriter.WriteLine("Call Type,Contacts Alias,City,Province,Country,Call ID");
                    break;
                case RadioType.MOTO:
                    streamWriter.WriteLine("Name,radio_id");
                    break;
                case RadioType.RT3S:
                    streamWriter.WriteLine("Radio ID,CallSign,Name,Nickname,City,State,Country");
                    break;
                case RadioType.RT52:
                    streamWriter.WriteLine("Id,Name,Call Type,Call Alert,Call Sign,City,Province,Country,Remarks");
                    break;
                case RadioType.TYT:
                    streamWriter.WriteLine("Radio ID,Callsign,Name,NickName,City,State,Country,,,,,,");
                    break;
            }
            if (chkAllCountries.Checked)
            {
                int num = 1;
                foreach (User user in ul.users)
                {
                    streamWriter.WriteLine(GetRadioPattern(num, pRadio,user));
                    ++num;
                }
            }
            else
            {
                int num = 1;
                for (int i = 0; i < chkBoxCountries.Items.Count; i++)
                {
                    if (chkBoxCountries.GetItemChecked(i))
                    {
                        
                        foreach (User user in ul.users.Where(Country => Country.country.Equals(chkBoxCountries.Items[i].ToString())))
                        {
                            streamWriter.WriteLine(GetRadioPattern(num, pRadio, user));
                            ++num;
                        }
                    }
                }
            }
            streamWriter.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Format CSV Line with given Radio Pattern
        /// </summary>
        /// <param name="pNum"></param>
        /// <param name="pRadio"></param>
        /// <param name="pUser"></param>
        /// <returns>CSV Line to add on CSV File</returns>
        private string GetRadioPattern(int pNum, RadioType pRadio, User pUser)
        {
            string Pattern="";
            switch (pRadio)
            {
                case RadioType.AnyTone:
                    Pattern = pNum.ToString() + "," + pUser.radio_id + "," + pUser.callsign + "," + pUser.fname + " " + pUser.surname + "," + pUser.city + "," + pUser.state + "," + pUser.country + "," + pUser.remarks + ",0,0,";
                    break;
                case RadioType.GD77:
                    Pattern = pUser.radio_id + "," + pUser.callsign + "," + pUser.fname + " " + pUser.surname + "," + "," + pUser.city + "," + pUser.state + "," + pUser.country + "," + "<br/>";
                    break;
                case RadioType.HD1:
                    Pattern = "Private Call," + pUser.callsign + " " + pUser.fname + "," + pUser.city + "," + pUser.state + "," + pUser.country + "," + pUser.radio_id;
                    break;
                case RadioType.MOTO:
                    Pattern = "" + (pUser.callsign + " ") + pUser.fname + "," + pUser.radio_id;
                    break;
                case RadioType.RT3S:
                    Pattern = pUser.radio_id + "," + pUser.callsign + "," + pUser.fname + " " + pUser.surname + "," + "" + "," + pUser.city + "," + pUser.state + "," + pUser.country;
                    break;
                case RadioType.RT52:
                    Pattern = pUser.radio_id + "," + pUser.fname + " " + pUser.surname + "," + "1" + ",0," + pUser.callsign + "," + pUser.city + "," + pUser.state + "," + pUser.country + "," + "1";
                    break;
                case RadioType.TYT:
                    Pattern = pUser.radio_id + "," + pUser.callsign + "," + pUser.fname + " " + pUser.surname + "," + "," + pUser.city + "," + pUser.state + "," + pUser.country + "," + ",,,,,";
                    break;
            }
            return Pattern;
        }

        /// <summary>
        /// Check if Radio List are changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkAllRadios_CheckedChanged(object sender, EventArgs e)
        {
            chkListRadios.Enabled = !chkAllRadios.Checked;
        }

        /// <summary>
        /// Read the cache JSON File
        /// </summary>
        private void LoadJSON(bool Silent=false)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                Path.DirectorySeparatorChar +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                Path.DirectorySeparatorChar +
                "users.json";
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                CCS7UserList = File.ReadAllText(path);
                if (ReadCCS7UserList())
                {
                    tsState.Text = "LOADING COMPLETED";
                }
                lblVersionDate.Text = "Version Date : " + fileInfo.LastWriteTime.ToShortDateString();
            }
            else
                if (!Silent) MessageBox.Show("Fail to open users.json file");
        }

        /// <summary>
        /// Save JSON File in cache folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveJSON()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                Path.DirectorySeparatorChar +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                Path.DirectorySeparatorChar +
                "users.json";
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            if (fileInfo.Exists)
            {
                File.Delete(path);
            }
            File.WriteAllText(path, CCS7UserList, Encoding.UTF8);
        }

        /// <summary>
        /// Checkbox select all countries change state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAllCountries_CheckedChanged(object sender, EventArgs e)
        {
            chkBoxCountries.Enabled = !chkAllCountries.Checked;
            UpdateContactSelected();
        }

        /// <summary>
        /// Choose Export Folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            using (var sfd = new FolderBrowserDialog())
            {
                sfd.SelectedPath = Settings.Default.OutputFolder;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if(Settings.Default.OutputFolder != sfd.SelectedPath)
                    {
                        Settings.Default.OutputFolder = sfd.SelectedPath;
                        Settings.Default.Save();
                        tb_OutputFolder.Text = Settings.Default.OutputFolder;
                    }
                }
            }
        }

        private void chkBoxCountries_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)(() => UpdateContactSelected()));
        }

        private void UpdateContactSelected ()
        {
            int Result=0;
            if (chkAllCountries.Checked)
            {
                Result = ul.users.Count;
            }
            else
            {
                for (int i = 0; i < chkBoxCountries.Items.Count; i++)
                {
                    if (chkBoxCountries.GetItemChecked(i))
                    {
                        foreach (User user in ul.users.Where(Country => Country.country.Equals(chkBoxCountries.Items[i].ToString())))
                        {
                            Result++;
                        }
                    }
                }
            }
            lblContactSelected.Text = string.Format("Contacts Selected : {0}", Result);
        }
    }
}
