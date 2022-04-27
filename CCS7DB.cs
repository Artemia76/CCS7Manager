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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

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
				SQLiteCommand cmd = m_DB.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "create table if not exists ccs7id ( Id int, CallSign varchar(32) collate nocase, City varchar(32) collate nocase, Country varchar(32) collate nocase, FName varchar(32) collate nocase, Remarks varchar(256) collate nocase, State varchar(32) collate nocase, Surname varchar(32) collate nocase)";
				cmd.ExecuteNonQuery();
				m_Initialized = true;
				return;
			}
			catch (SQLiteException e)
			{
				return;
			}
			catch (Exception e)
			{
				return;
			}
		}


	}
}
