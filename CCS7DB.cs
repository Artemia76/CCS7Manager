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

using System;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CCS7Manager
{
	class CCS7DB
	{
		private SQLiteConnection m_DB;
		private string m_DatabasePath;
		private string m_DatabaseFile;
		private bool m_Initialized;
		public bool IsInit
		{
			get { return m_Initialized; }
		}

		public CCS7DB()
		{
			m_Initialized = false;
			InitSQLite();
		}
		/// <summary>
		/// Initialise la base de donnée 
		/// </summary>
		private void InitSQLite()
		{
			try
			{
				m_DatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
				m_DatabaseFile = m_DatabasePath + "\\CCS7ID.sqlite";
				if (!Directory.Exists(m_DatabasePath))
				{
					Directory.CreateDirectory(m_DatabasePath);
				}
				if (!File.Exists(m_DatabaseFile))
				{
					SQLiteConnection.CreateFile(m_DatabaseFile);
				}
				m_DB = new SQLiteConnection("Data Source=" + m_DatabaseFile + ";Version=3;");
				if (m_DB == null) return;
				m_DB.Open();
				//Test if ccs7id table exist
				SQLiteCommand cmd = m_DB.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='ccs7id';";
				SQLiteDataReader r = cmd.ExecuteReader();
				if (r.StepCount < 1)
				{
					// Create ccs7id table if not exist
					cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "CREATE TABLE IF NOT EXISTS ccs7id ( Id INTEGER PRIMARY KEY, CallSign TEXT nocase, City TEXT, Country TEXT nocase, FName TEXT, Remarks TEXT, State TEXT, Surname TEXT)";
					cmd.ExecuteNonQuery();
				}
				//Test if sources table exist
				cmd = m_DB.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='sources';";
				r = cmd.ExecuteReader();
				if (r.StepCount < 1)
				{
					// Create sources table if not exist
					cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "CREATE TABLE IF NOT EXISTS sources ( Source_Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Description TEXT, URL TEXT)";
					cmd.ExecuteNonQuery();

					AddSource("RadioID", "Official DMR id from RA community", "https://database.radioid.net/static/users.json");
				}
				m_Initialized = true;
				return;
			}
			catch (SQLiteException e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
				return;
			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
				return;
			}
		}

		public bool AddSource(string pName, string pDescription, string pURL)
		{
			try
			{
				if (m_DB == null) return false;
				SQLiteParameter sName = new SQLiteParameter("@Name", pName);
				SQLiteParameter sDescription = new SQLiteParameter("@Description", pDescription);
				SQLiteParameter sURL = new SQLiteParameter("@URL", pURL);
				SQLiteCommand cmd = m_DB.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "INSERT INTO sources (Name,Description,URL) VALUES (@Name, @Description, @URL);";
				cmd.Parameters.Add(sName);
				cmd.Parameters.Add(sDescription);
				cmd.Parameters.Add(sURL);
				cmd.ExecuteNonQuery();
				return true;

			}
			catch (SQLiteException e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
				return false;
			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
				return false;
			}
		}

		public StringDictionary GetSourceList()
		{
			StringDictionary List = new StringDictionary() ;
			try
			{
				if (m_DB != null)
				{
					SQLiteCommand cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "SELECT * FROM sources;";
					SQLiteDataReader r = cmd.ExecuteReader();
					while (r.Read())
					{
						List.Add(r.GetString(1), r.GetString(3));
					}
				}
			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
			return List;
		}

		public User GetUser(int pId)
		{
			User pUser = new User
			{
				Radio_ID = -1
			};
			try
			{
				if (m_DB != null)
				{
					SQLiteParameter sID = new SQLiteParameter("@Id", pId);
					SQLiteCommand cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "SELECT * FROM ccs7id WHERE Id = @Id;";
					cmd.Parameters.Add(sID);
					SQLiteDataReader r = cmd.ExecuteReader();
					if (r.StepCount > 0)
					{
						r.Read();
						pUser.Radio_ID = r.GetInt32(0);
						pUser.Callsign = r.GetString(1);
						pUser.City = r.GetString(2);
						pUser.Country = r.GetString(3);
						pUser.FName = r.GetString(4);
						pUser.Remarks = r.GetString(5);
						pUser.State = r.GetString(6);
						pUser.Surname = r.GetString(7);
					}
				}

			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
			return pUser;
		}

		public List<User> GetUserList()
        {
			List<User> pList = new List<User>();
			try
			{
				if (m_DB != null)
				{
					SQLiteCommand cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "SELECT * FROM ccs7id;";
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							User u = new User
							{
								Radio_ID = Convert.ToInt32(rdr["Id"]),
								Callsign = Convert.ToString(rdr["Callsign"]),
								City = Convert.ToString(rdr["City"]),
								Country = Convert.ToString(rdr["Country"]),
								FName = Convert.ToString(rdr["FName"]),
								Remarks = Convert.ToString(rdr["Remarks"]),
								State = Convert.ToString(rdr["State"]),
								Surname = Convert.ToString(rdr["Surname"])
							};
							pList.Add(u);
						}
					}
				}
			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
			return pList;
		}

		public int GetUserCount()
        {
			try
			{
				if (m_DB != null)
				{
					SQLiteCommand cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "SELECT COUNT(*) FROM ccs7id;";
					return Convert.ToInt32(cmd.ExecuteScalar());
				}

			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
			return 0;
		}

		public bool AddUsers(UserList pUserList)
		{
			try
			{
				if (m_DB == null) return false;
				SQLiteCommand cmd = m_DB.CreateCommand();
				SQLiteTransaction transaction = m_DB.BeginTransaction();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "INSERT OR REPLACE INTO ccs7id (Id,Callsign,City,Country,FName,Remarks,State,Surname) VALUES (@ID, @Callsign, @City, @Country, @FName, @Remarks, @State, @Surname);";
				cmd.Parameters.AddWithValue("@ID", -1);
				cmd.Parameters.AddWithValue("@Callsign", "");
				cmd.Parameters.AddWithValue("@City", "");
				cmd.Parameters.AddWithValue("@Country", "");
				cmd.Parameters.AddWithValue("@FName", "");
				cmd.Parameters.AddWithValue("@Remarks", "");
				cmd.Parameters.AddWithValue("@State", "");
				cmd.Parameters.AddWithValue("@Surname", "");
				foreach (User u in pUserList.users)
				{
					cmd.Parameters["@ID"].Value = u.Radio_ID;
					cmd.Parameters["@Callsign"].Value = u.Callsign;
					cmd.Parameters["@City"].Value = u.City;
					cmd.Parameters["@Country"].Value = u.Country;
					cmd.Parameters["@FName"].Value = u.FName;
					cmd.Parameters["@Remarks"].Value = u.Remarks;
					cmd.Parameters["@State"].Value = u.State;
					cmd.Parameters["@Surname"].Value = u.Surname;
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				cmd.Dispose();
				return true;

			}
			catch (SQLiteException e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
				return false;
			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
				return false;
			}
		}

		public List<String> GetCountryList()
		{
			List<String> pList = new List<string>();
			try
			{
				if (m_DB != null)
				{
					SQLiteCommand cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "SELECT DISTINCT Country FROM ccs7id ORDER BY Country;";
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							pList.Add(Convert.ToString(rdr["Country"]));
						}
					}
				}
			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
			return pList;

		}

		public int GetCountryNumber(string pCountry)
		{
			try
			{
				if (m_DB != null)
				{
					SQLiteCommand cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "SELECT COUNT(*) FROM ccs7id WHERE Country = @Country;";
					cmd.Parameters.AddWithValue("@Country", pCountry);
					return Convert.ToInt32(cmd.ExecuteScalar());
				}

			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
			return 0;
		}

		public List<User> GetUserListByCountry (string pCountry)
        {
			List<User> pList = new List<User>();
			try
			{
				if (m_DB != null)
				{
					SQLiteCommand cmd = m_DB.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "SELECT * FROM ccs7id WHERE Country like @Country;";
					cmd.Parameters.AddWithValue("@Country", pCountry);
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
						while (rdr.Read())
						{
							User u = new User
							{


								Radio_ID = Convert.ToInt32(rdr["Id"]),
								Callsign = Convert.ToString(rdr["Callsign"]),
								City = Convert.ToString(rdr["City"]),
								Country = Convert.ToString(rdr["Country"]),
								FName = Convert.ToString(rdr["FName"]),
								Remarks = Convert.ToString(rdr["Remarks"]),
								State = Convert.ToString(rdr["State"]),
								Surname = Convert.ToString(rdr["Surname"])
							};
							pList.Add(u);
						}
					}
				}

			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
			return pList;
		}
	}
}
