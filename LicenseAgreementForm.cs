﻿/*
    Memory Cleaner

    Copyright (C) 2020 Danske

    This file is part of Memory Cleaner

    Memory Cleaner is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Memory Cleaner is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Memory Cleaner.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Memory_Cleaner
{
    public partial class LicenseAgreementForm : Form
    {
        RegistryKey Settings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings", true);

        public LicenseAgreementForm()
        {
            InitializeComponent();
        }

        private void ButtonAgree_Click(object sender, EventArgs e)
        {
            Settings.SetValue("LicenseAccepted", "True");
            this.Close();
        }

        private void ButtonDecline_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
