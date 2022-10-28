/****************************************************************************
**
** Copyright (C) 2017 FSFranceSimulateur team.
** Contact: https://github.com/ffs2/ffs2play
**
** FFS2Play is free software; you can redistribute it and/or modify
** it under the terms of the GNU General Public License as published by
** the Free Software Foundation; either version 3 of the License, or
** (at your option) any later version.
**
** FFS2Play is distributed in the hope that it will be useful,
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
 * dlgConServeur.cs is part of FF2Play project
 *
 * This class purpose a dialog interface to manage account profils
 * to connect severals FFS2Play networks servers
 * **************************************************************************/

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Specialized;
using System.Collections;

namespace CCS7Manager
{
	public partial class dlgConServeur : Form
	{
		private int position;
        private StringDictionary DBList = new StringDictionary();
        private readonly CCS7DB m_DB;
		private string CurrentProfil;
		private string CurrentURL;

        public dlgConServeur(bool Direct=false)
		{
			InitializeComponent();
			m_DB = new CCS7DB();
            DBList = m_DB.GetSourceList();
			foreach (DictionaryEntry  item in DBList)
			{
				ListSources.SelectedNode = ListSources.Nodes[0];
                ListSources.SelectedNode = ListSources.SelectedNode.Nodes.Add(item.Key.ToString());
            }
			if (ListSources.Nodes[0].GetNodeCount(false) > 0)
			{
                string DBCurrent = Properties.Settings.Default.DBCurrentSource;
				foreach (TreeNode node in ListSources.Nodes[0].Nodes)
				{
					if (DBCurrent ==  node.Text )
					{
                        ListSources.SelectedNode = node;
						position = node.Index;
						break;
					}
				}
                ListSources.SelectedNode = ListSources.Nodes[0].Nodes[0];
				position = 0;
			}
			else
			{
				position = -1;
                ListSources.SelectedNode = ListSources.Nodes[0];
			}
		}

		private void btnAnnuler_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void OnNewSource_Click(object sender, EventArgs e)
		{
			int num = GetNumNouveau ();
			CurrentProfil = "Nouveau";
			if (num > 0)
			{
				CurrentProfil += " " + num;
			}
            ListSources.SelectedNode= ListSources.Nodes[0];

            ListSources.SelectedNode = ListSources.SelectedNode.Nodes.Add(CurrentProfil);
            tbProfil.Text = CurrentProfil;
            tbURL.Text = "Enter url of new source here";
            ListSources.ExpandAll();
			btnSupprimer.Enabled = true;
			btnSupprimer.BackColor = Color.LightGray;
			btnSupprimer.ForeColor = Color.Black;
			SaveSource();
		}

		private int GetNumNouveau()
		{
			int result = 0;
			TreeNodeCollection nodes = ListSources.Nodes[0].Nodes;
			foreach (TreeNode n in nodes)
			{
				if (n.Text.StartsWith("Nouveau"))
				{
					result++;
				}
			}
			return result;
		}

		private void UpdateSource()
		{
			if (position < 0) return;
			tbURL.Text = tbURL.Text.ToLower();
			DBList.Remove(CurrentProfil);
			CurrentProfil = tbProfil.Text;
			CurrentURL = tbURL.Text;
            DBList[CurrentProfil] = CurrentURL;
			ListSources.SelectedNode.Text = CurrentProfil;
		}

		private void SaveSource()
		{
			m_DB.UpdateSourceList(DBList);
        }

		private void btnAppliquer_Click(object sender, EventArgs e)
		{
            UpdateSource();
            SaveSource();
			btnAppliquer.Enabled = false;
			btnAppliquer.BackColor = Color.LightGray;
			btnAppliquer.ForeColor = Color.Black;
		}

		private void ListSources_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Parent != null)
			{
				CurrentProfil = e.Node.Text;
				position = e.Node.Index;

				btnAppliquer.Enabled = false;
				btnAppliquer.BackColor = Color.LightGray;
				btnAppliquer.ForeColor = Color.Black;
				btnSupprimer.Enabled = true;
				btnSupprimer.BackColor = Color.DodgerBlue;
				btnSupprimer.ForeColor = Color.White;
				tbProfil.Enabled = true;
				tbProfil.Text = CurrentProfil;
				tbURL.Enabled = true;
				tbURL.Text = DBList[CurrentProfil];
			}
			else
			{
				position = -1;
				btnAppliquer.Enabled = false;
				btnAppliquer.BackColor = Color.LightGray;
				btnAppliquer.ForeColor = Color.Black;
				btnSupprimer.Enabled = false;
				btnSupprimer.BackColor = Color.LightGray;
				btnSupprimer.ForeColor = Color.Black;
				tbProfil.Enabled = false;
				tbProfil.Text = "";
				tbURL.Text = "";
				tbURL.Enabled = false;
			}
			btnAppliquer.Enabled = false;
			btnAppliquer.BackColor = Color.LightGray;
			btnAppliquer.ForeColor = Color.Black;

		}

		private void btnSupprimer_Click(object sender, EventArgs e)
		{
			if (position != -1)
			{
				DBList.Remove(CurrentProfil);
                ListSources.Nodes[0].Nodes[position].Remove();
				if (DBList.Count == 0)
				{
                    ListSources.SelectedNode = ListSources.Nodes[0];
				}
				else
				{
					if (position > (DBList.Count - 1)) ListSources.SelectedNode = ListSources.Nodes[0].Nodes[DBList.Count - 1];
				}

			}
            UpdateSource();
            SaveSource();
            btnAppliquer.Enabled = false;
			btnAppliquer.BackColor = Color.LightGray;
			btnAppliquer.ForeColor = Color.Black;
		}

		private DialogResult MessageModifications()
		{
			DialogResult dialogResult = MessageBox.Show("Do you want save changes ?", "Change Saving", MessageBoxButtons.YesNoCancel);
			if (dialogResult == DialogResult.Yes)
			{
				UpdateSource();
				SaveSource();
			}
			return dialogResult;
		}
		private void tbURL_TextChanged(object sender, EventArgs e)
		{
			if (position < 0) return;
            CheckChangesPending();
        }

		private void tbProfil_TextChanged(object sender, EventArgs e)
		{
			if (position < 0) return;
			CheckChangesPending();
        }

		private void ListSources_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
            if (btnAppliquer.Enabled)
            {
                DialogResult dialogResult = MessageModifications();
                if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

		private void CheckChangesPending ()
		{
            if ((tbURL.Text != DBList[CurrentProfil]) || (tbProfil.Text != CurrentProfil))
			{
                btnAppliquer.Enabled = true;
                btnAppliquer.BackColor = Color.DodgerBlue;
                btnAppliquer.ForeColor = Color.White;
            }
			else
			{
                btnAppliquer.Enabled = false;
                btnAppliquer.BackColor = Color.LightGray;
                btnAppliquer.ForeColor = Color.Black;
            }
        }
	}
}
