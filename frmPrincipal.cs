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
 * MainFrame.cs is part of  CCS7Manager project
 *
 * This class 
 * **************************************************************************/

using CCS7Manager.Properties;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CCS7Manager
{
    /// <summary>
    /// Main Window Class
    /// </summary>
    public partial class frmPrincipal : Form
    {
        private StringDictionary DBList;

        private static UserList ul;

        private CCS7DB m_DB;

        private ApplicationSettingsBase settings;

        // Init state of the window
        private bool windowInitialized;

        private Countries countries_list;

        // Enum list of available output CSV Format
        private enum RadioType
        {
            HD1,
            MOTO,
            TYT,
            GD77,
            AnyTone,
            RT52,
            RT3S,
            BAOFENG,
            DVPI,
            PISTAR
        }

        // String container for JSON File
        private String JsonContent;

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

            // Copy user settings from previous application version if necessary
            settings = Settings.Default;
            if ((bool)settings["UpgradeRequired"] == true)
            {
                // upgrade the settings to the latest version
                settings.Upgrade();

                // clear the upgrade flag
                settings["UpgradeRequired"] = false;
                settings.Save();
            }
            else
            {
                // the settings are up to date
            }

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

            countries_list = new Countries();
            windowInitialized = true;
            //If source database is not initialized, we load default knowns servers
            if (Settings.Default.DBSourcesURL.Length == 0)
            {
                DBList = new StringDictionary();
                DBList.Add("radioid", "https://database.radioid.net/static/users.json");
                DBList.Add("theshield", "http://theshield.site/local_subscriber_ids.json");
                Settings.Default.DBCurrentSource = "radioid";
            }
            else
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Settings.Default.DBSourcesURL)))
                {
                    if (ms.Length != 0)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        DBList = (StringDictionary)bf.Deserialize(ms);
                    }
                }
            }
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
                   Settings.Default.WindowState = WindowState;
                    break;

                default:
                    Settings.Default.WindowState = FormWindowState.Normal;
                    break;
            }
            Settings.Default.CountriesCheckState.Clear();
            for (int i = 0; i < chkBoxCountries.Items.Count; i++)
            {
                if (chkBoxCountries.GetItemChecked(i)) Settings.Default.CountriesCheckState.Add(chkBoxCountries.Items[i].ToString());
            }
            //Check the Database source 
            Settings.Default.DBCurrentSource = cbDatabaseList.SelectedText;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, DBList);
                ms.Position = 0;
                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                Settings.Default.DBSourcesURL = Convert.ToBase64String(buffer);
            }
            Settings.Default.NoCountry = chkEmpty.Checked;
            // Backup configuration
            Settings.Default.Save();
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
            if (Settings.Default.CountriesCheckState == null) Settings.Default.CountriesCheckState = new System.Collections.Specialized.StringCollection();
            foreach (string Country in countries_list.CountryList.Keys)
            {
                chkBoxCountries.Items.Add(Country, Settings.Default.CountriesCheckState.Contains(Country));
            }

            // Populate Radio List ChecBox
            chkListRadios.Items.Add("Ailunce HD1");
            chkListRadios.Items.Add("Motorola DGP-6150+");
            chkListRadios.Items.Add("Retevis RT52");
            chkListRadios.Items.Add("Retevis RT3S / RT90 / RT84 / RT82");
            chkListRadios.Items.Add("AnyTone");
            chkListRadios.Items.Add("Radioddity GD-77");
            chkListRadios.Items.Add("TYT MD-380 / MD-2017 / MD-9600");
            chkListRadios.Items.Add("BOAFENG DM-1701");
            chkListRadios.Items.Add("DVPi");
            chkListRadios.Items.Add("PI-Star");

            //Load the cached JSON File if exist
            LoadJSON(true);

            //Load Database source list
            cbDatabaseList.DataSource = new BindingSource(DBList, null);
            cbDatabaseList.DisplayMember = "Key";
            cbDatabaseList.ValueMember = "Value";
            cbDatabaseList.SelectedIndex = cbDatabaseList.FindString(Settings.Default.DBCurrentSource);

            chkEmpty.Checked = Settings.Default.NoCountry;
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
                    //Uri uri = new Uri("https://database.radioid.net/static/users.json");
                    Uri uri = new Uri((string)cbDatabaseList.SelectedValue);
                    tsState.Text = "USER LIST DOWNLOAD STARTING...";
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
                    webClient.DownloadProgressChanged += DownloadProgressCallBack;
                    webClient.DownloadStringAsync(uri);
                    btnImportWeb.Enabled = false;
                    btnOpenJSON.Enabled = false;
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
                JsonContent = (string)e.Result;
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
                //if it standard Serialized JSON from this app
                if (JsonContent.StartsWith("{\"users\":[{"))
                {
                    frmPrincipal.ul = JsonConvert.DeserializeObject<UserList>(JsonContent);
                }
                // Else we need to parse the unknown Json Format
                else
                {
                    if (frmPrincipal.ul != null)
                    {
                        frmPrincipal.ul.users.Clear();
                    }
                    else
                    {
                        frmPrincipal.ul = new UserList();
                        frmPrincipal.ul.users = new List<User>();
                    }

                    JObject jo = JObject.Parse(JsonContent);
                    JArray ja = (JArray)jo["results"];
                    foreach (JObject o in ja)
                    {
                        User u = new User();
                        u.fname = (string)o["fname"];
                        u.callsign = (string)o["callsign"];
                        u.city = (string)o["city"];
                        u.radio_id = (string)o["id"];
                        u.country = (string)o["country"];
                        u.remarks = (string)o["remarks"];
                        u.surname = (string)o["surname"];
                        u.state = (string)o["state"];
                        frmPrincipal.ul.users.Add(u);
                    }
                }
                chkAllRadios.Enabled = true;
                chkBoxCountries.Enabled = true;
                chkListRadios.Enabled = true;
                chkAllCountries.Enabled = true;
                chkEmpty.Enabled = true;
                btnExport.Enabled = true;
                btnImportWeb.Text = "Update";
                btnOpenJSON.Enabled = true;
                btnImportWeb.Enabled = true;
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
                Export_Contacts(RadioType.BAOFENG);
                Export_Contacts(RadioType.DVPI);
                Export_Contacts(RadioType.PISTAR);
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
                        if (chkListRadios.Items[index].ToString() == "BAOFENG DM-1701")
                            Export_Contacts(RadioType.BAOFENG);
                        if (chkListRadios.Items[index].ToString() == "DVPi")
                            Export_Contacts(RadioType.DVPI);
                        if (chkListRadios.Items[index].ToString() == "PI-Star")
                            Export_Contacts(RadioType.PISTAR);
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
            try
            {
                string ext=".csv";
                if (pRadio == RadioType.PISTAR)
                    ext = ".dat";
                string file = Settings.Default.OutputFolder +
                    Path.DirectorySeparatorChar + "Contacts_" +
                    pRadio.ToString() +
                    ext;
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
                    case RadioType.BAOFENG:
                        streamWriter.WriteLine("Contact Name,Call Type,Call ID,Call Receive Tone");
                        break;
                    default:
                        break;
                }
                if (chkAllCountries.Checked)
                {
                    int num = 1;
                    foreach (User user in ul.users)
                    {
                        streamWriter.WriteLine(GetRadioPattern(num, pRadio, user));
                        ++num;
                    }
                }
                else
                {
                    int num = 1;
                    if (chkEmpty.Checked)
                    {
                        foreach (User user in ul.users.Where(Country => Country.country.Equals("")))
                        {
                            streamWriter.WriteLine(GetRadioPattern(num, pRadio, user));
                            ++num;
                        }
                    }
                    for (int i = 0; i < chkBoxCountries.Items.Count; i++)
                    {
                        if (chkBoxCountries.GetItemChecked(i))
                        {

                            foreach (string Country_Name in countries_list.CountryList[chkBoxCountries.Items[i].ToString()])
                            {
                                foreach (User user in ul.users.Where(Country => Country.country.Equals(Country_Name)))
                                {
                                    streamWriter.WriteLine(GetRadioPattern(num, pRadio, user));
                                    ++num;
                                }
                            }
                        }
                    }
                }
                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message.ToString());
            }
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
            string radio_id = Sanity(pUser.radio_id);
            string callsign = Sanity(pUser.callsign);
            string fname = Sanity(pUser.fname);
            string surname = Sanity(pUser.surname);
            string city = Sanity(pUser.city);
            string state = Sanity(pUser.state);
            string country = Sanity(pUser.country);
            string remarks = Sanity(pUser.remarks);
            switch (pRadio)
            {
                case RadioType.AnyTone:
                    Pattern = pNum.ToString() + "," + radio_id + "," + callsign + "," + fname + " " + surname + "," + city + "," + state + "," + country + "," + remarks + ",0,0,";
                    break;
                case RadioType.GD77:
                    Pattern = radio_id + "," + callsign + "," + fname + " " + surname + "," + "," + city + "," + state + "," + country + "," + "<br/>";
                    break;
                case RadioType.HD1:
                    Pattern = "Private Call," + callsign + " " + fname + "," + city + "," + state + "," + country + "," + radio_id;
                    break;
                case RadioType.MOTO:
                    Pattern = "" + callsign + " " + fname + "," + radio_id;
                    break;
                case RadioType.RT3S:
                    Pattern = radio_id + "," + callsign + "," + fname + " " + surname + "," + "" + "," + city + "," + state + "," + country;
                    break;
                case RadioType.RT52:
                    Pattern = radio_id + "," + fname + " " + surname + "," + "1" + ",0," + callsign + "," + city + "," + state + "," + country + "," + "1";
                    break;
                case RadioType.TYT:
                    Pattern = radio_id + "," + callsign + "," + fname + " " + surname + "," + "," + city + "," + state + "," + country + "," + ",,,,,";
                    break;
                case RadioType.BAOFENG:
                    Pattern = callsign + ",2," + radio_id + ",0";
                    break;
                case RadioType.DVPI:
                    Pattern = radio_id + "," + callsign + "," + fname;
                    break;
                case RadioType.PISTAR:
                    Pattern = radio_id + "\t" + callsign + "\t" + fname;
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
        private void LoadJSON(bool Silent=false, string pPath = "")
        {
            string path;
            if (pPath == "")
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                Path.DirectorySeparatorChar +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                Path.DirectorySeparatorChar +
                "users.json";
            }
            else
            {
                path = pPath;
            }
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                JsonContent = File.ReadAllText(path);
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
            File.WriteAllText(path, JsonConvert.SerializeObject(ul));
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

        /// <summary>
        /// Event Countries Check box Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxCountries_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)(() => UpdateContactSelected()));
        }

        /// <summary>
        /// Update number of contact in current countries selection
        /// </summary>
        private void UpdateContactSelected ()
        {
            int Result=0;
            if (ul == null) return;
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
                        foreach (string Country_Name in countries_list.CountryList[chkBoxCountries.Items[i].ToString()])
                        {
                            foreach (User user in ul.users.Where(Country => Country.country.Equals(Country_Name)))
                            {
                                Result++;
                            }
                        }
                    }
                }
                if (chkEmpty.Checked)
                {
                    foreach (User user in ul.users.Where(Country => Country.country.Equals("")))
                    {
                        Result++;
                    }
                }
            }
            lblContactSelected.Text = string.Format("Contacts Selected : {0}", Result);
        }

        /// <summary>
        /// Replace forbiden char in CSV by space
        /// </summary>
        /// <param name="pString"></param>
        /// <returns></returns>

        private string Sanity(string pString)
        {
            string result = "";
            if (pString != null)
            {
                result = pString.Replace(",", " ");
            }
            return result;
        }

        private void btnOpenJSON_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ofd.Filter = "JSON files (*.json)|*.json|All file (*.*)|*.*";
            ofd.FilterIndex = 0;
            btnImportWeb.Enabled = false;
            btnOpenJSON.Enabled = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadJSON(true, ofd.FileName);
                SaveJSON();
            }
        }

        private void chkEmpty_CheckedChanged(object sender, EventArgs e)
        {
            UpdateContactSelected();
        }
    }
}
