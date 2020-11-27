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

namespace Memory_Cleaner
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.ButtonReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.HotkeyModifierKey = new System.Windows.Forms.ComboBox();
            this.HotkeyToCleanMemory = new System.Windows.Forms.ComboBox();
            this.LabelHotkeyToCleanMemory = new System.Windows.Forms.Label();
            this.LabelHotkeyModifierKey = new System.Windows.Forms.Label();
            this.CheckBoxEnableClearingOfTheStandbyList = new System.Windows.Forms.CheckBox();
            this.CheckBoxEnableEmptyingOfTheWorkingSet = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CheckBoxEnableCustomTimerResolution = new System.Windows.Forms.CheckBox();
            this.DesiredTimerResolution = new System.Windows.Forms.NumericUpDown();
            this.LabelDesiredTimerResolution = new System.Windows.Forms.Label();
            this.CheckBoxEnableTimer = new System.Windows.Forms.CheckBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.TimerPollingInterval = new System.Windows.Forms.ComboBox();
            this.LabelTimerPollingInterval = new System.Windows.Forms.Label();
            this.GroupBoxStartupSettings = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.CheckBoxStartMemoryCleanerOnSystemStartup = new System.Windows.Forms.CheckBox();
            this.CheckBoxStartTimerResolutionAutomatically = new System.Windows.Forms.CheckBox();
            this.CheckBoxStartMinimized = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.TableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesiredTimerResolution)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.TableLayoutPanel3.SuspendLayout();
            this.GroupBoxStartupSettings.SuspendLayout();
            this.TableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonReset
            // 
            this.ButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonReset.Location = new System.Drawing.Point(4, 389);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(75, 23);
            this.ButtonReset.TabIndex = 53;
            this.ButtonReset.Text = "Reset";
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 122);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Memory cleaning settings";
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.72727F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.TableLayoutPanel1.Controls.Add(this.HotkeyModifierKey, 1, 1);
            this.TableLayoutPanel1.Controls.Add(this.HotkeyToCleanMemory, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.LabelHotkeyToCleanMemory, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.LabelHotkeyModifierKey, 0, 1);
            this.TableLayoutPanel1.Controls.Add(this.CheckBoxEnableClearingOfTheStandbyList, 0, 2);
            this.TableLayoutPanel1.Controls.Add(this.CheckBoxEnableEmptyingOfTheWorkingSet, 0, 3);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(6, 16);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 4;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(300, 100);
            this.TableLayoutPanel1.TabIndex = 61;
            // 
            // HotkeyModifierKey
            // 
            this.HotkeyModifierKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.HotkeyModifierKey.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HotkeyModifierKey.FormattingEnabled = true;
            this.HotkeyModifierKey.Items.AddRange(new object[] {
            "None",
            "Alt",
            "Ctrl",
            "Shift"});
            this.HotkeyModifierKey.Location = new System.Drawing.Point(221, 28);
            this.HotkeyModifierKey.Name = "HotkeyModifierKey";
            this.HotkeyModifierKey.Size = new System.Drawing.Size(76, 21);
            this.HotkeyModifierKey.TabIndex = 14;
            this.HotkeyModifierKey.Text = "None";
            this.HotkeyModifierKey.TextChanged += new System.EventHandler(this.HotkeyModifierKey_TextChanged);
            // 
            // HotkeyToCleanMemory
            // 
            this.HotkeyToCleanMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.HotkeyToCleanMemory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HotkeyToCleanMemory.FormattingEnabled = true;
            this.HotkeyToCleanMemory.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "Home",
            "Insert",
            "PageUp",
            "PageDown",
            "CapsLock"});
            this.HotkeyToCleanMemory.Location = new System.Drawing.Point(221, 3);
            this.HotkeyToCleanMemory.Name = "HotkeyToCleanMemory";
            this.HotkeyToCleanMemory.Size = new System.Drawing.Size(76, 21);
            this.HotkeyToCleanMemory.TabIndex = 15;
            this.HotkeyToCleanMemory.Text = "F10";
            this.HotkeyToCleanMemory.TextChanged += new System.EventHandler(this.HotkeyToCleanMemory_TextChanged);
            // 
            // LabelHotkeyToCleanMemory
            // 
            this.LabelHotkeyToCleanMemory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelHotkeyToCleanMemory.AutoSize = true;
            this.LabelHotkeyToCleanMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHotkeyToCleanMemory.Location = new System.Drawing.Point(3, 6);
            this.LabelHotkeyToCleanMemory.Name = "LabelHotkeyToCleanMemory";
            this.LabelHotkeyToCleanMemory.Size = new System.Drawing.Size(124, 13);
            this.LabelHotkeyToCleanMemory.TabIndex = 23;
            this.LabelHotkeyToCleanMemory.Text = "Hotkey to clean memory:";
            this.LabelHotkeyToCleanMemory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelHotkeyModifierKey
            // 
            this.LabelHotkeyModifierKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelHotkeyModifierKey.AutoSize = true;
            this.LabelHotkeyModifierKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHotkeyModifierKey.Location = new System.Drawing.Point(3, 31);
            this.LabelHotkeyModifierKey.Name = "LabelHotkeyModifierKey";
            this.LabelHotkeyModifierKey.Size = new System.Drawing.Size(103, 13);
            this.LabelHotkeyModifierKey.TabIndex = 24;
            this.LabelHotkeyModifierKey.Text = "Hotkey modifier key:";
            this.LabelHotkeyModifierKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckBoxEnableClearingOfTheStandbyList
            // 
            this.CheckBoxEnableClearingOfTheStandbyList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxEnableClearingOfTheStandbyList.AutoSize = true;
            this.CheckBoxEnableClearingOfTheStandbyList.Checked = true;
            this.CheckBoxEnableClearingOfTheStandbyList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxEnableClearingOfTheStandbyList.Location = new System.Drawing.Point(3, 54);
            this.CheckBoxEnableClearingOfTheStandbyList.Name = "CheckBoxEnableClearingOfTheStandbyList";
            this.CheckBoxEnableClearingOfTheStandbyList.Size = new System.Drawing.Size(184, 17);
            this.CheckBoxEnableClearingOfTheStandbyList.TabIndex = 25;
            this.CheckBoxEnableClearingOfTheStandbyList.Text = "Enable clearing of the standby list";
            this.CheckBoxEnableClearingOfTheStandbyList.UseVisualStyleBackColor = true;
            this.CheckBoxEnableClearingOfTheStandbyList.CheckedChanged += new System.EventHandler(this.CheckBoxEnableClearingOfTheStandbyList_CheckedChanged);
            // 
            // CheckBoxEnableEmptyingOfTheWorkingSet
            // 
            this.CheckBoxEnableEmptyingOfTheWorkingSet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxEnableEmptyingOfTheWorkingSet.AutoSize = true;
            this.CheckBoxEnableEmptyingOfTheWorkingSet.Checked = true;
            this.CheckBoxEnableEmptyingOfTheWorkingSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxEnableEmptyingOfTheWorkingSet.Location = new System.Drawing.Point(3, 79);
            this.CheckBoxEnableEmptyingOfTheWorkingSet.Name = "CheckBoxEnableEmptyingOfTheWorkingSet";
            this.CheckBoxEnableEmptyingOfTheWorkingSet.Size = new System.Drawing.Size(191, 17);
            this.CheckBoxEnableEmptyingOfTheWorkingSet.TabIndex = 26;
            this.CheckBoxEnableEmptyingOfTheWorkingSet.Text = "Enable emptying of the working set";
            this.CheckBoxEnableEmptyingOfTheWorkingSet.UseVisualStyleBackColor = true;
            this.CheckBoxEnableEmptyingOfTheWorkingSet.CheckedChanged += new System.EventHandler(this.CheckBoxEnableEmptyingOfTheWorkingSet_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(4, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 72);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timer resolution settings";
            // 
            // TableLayoutPanel2
            // 
            this.TableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TableLayoutPanel2.ColumnCount = 2;
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.72727F));
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.TableLayoutPanel2.Controls.Add(this.CheckBoxEnableCustomTimerResolution, 0, 1);
            this.TableLayoutPanel2.Controls.Add(this.DesiredTimerResolution, 1, 0);
            this.TableLayoutPanel2.Controls.Add(this.LabelDesiredTimerResolution, 0, 0);
            this.TableLayoutPanel2.Location = new System.Drawing.Point(6, 16);
            this.TableLayoutPanel2.Name = "TableLayoutPanel2";
            this.TableLayoutPanel2.RowCount = 2;
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel2.Size = new System.Drawing.Size(300, 50);
            this.TableLayoutPanel2.TabIndex = 34;
            // 
            // CheckBoxEnableCustomTimerResolution
            // 
            this.CheckBoxEnableCustomTimerResolution.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxEnableCustomTimerResolution.AutoSize = true;
            this.CheckBoxEnableCustomTimerResolution.Checked = true;
            this.CheckBoxEnableCustomTimerResolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxEnableCustomTimerResolution.Location = new System.Drawing.Point(3, 29);
            this.CheckBoxEnableCustomTimerResolution.Name = "CheckBoxEnableCustomTimerResolution";
            this.CheckBoxEnableCustomTimerResolution.Size = new System.Drawing.Size(169, 17);
            this.CheckBoxEnableCustomTimerResolution.TabIndex = 33;
            this.CheckBoxEnableCustomTimerResolution.Text = "Enable custom timer resolution";
            this.CheckBoxEnableCustomTimerResolution.UseVisualStyleBackColor = true;
            this.CheckBoxEnableCustomTimerResolution.CheckedChanged += new System.EventHandler(this.CheckBoxEnableCustomTimerResolution_CheckedChanged);
            // 
            // DesiredTimerResolution
            // 
            this.DesiredTimerResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DesiredTimerResolution.Location = new System.Drawing.Point(221, 3);
            this.DesiredTimerResolution.Maximum = new decimal(new int[] {
            156250,
            0,
            0,
            0});
            this.DesiredTimerResolution.Name = "DesiredTimerResolution";
            this.DesiredTimerResolution.Size = new System.Drawing.Size(76, 20);
            this.DesiredTimerResolution.TabIndex = 32;
            this.DesiredTimerResolution.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.DesiredTimerResolution.ValueChanged += new System.EventHandler(this.DesiredTimerResolution_ValueChanged);
            // 
            // LabelDesiredTimerResolution
            // 
            this.LabelDesiredTimerResolution.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelDesiredTimerResolution.AutoSize = true;
            this.LabelDesiredTimerResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDesiredTimerResolution.Location = new System.Drawing.Point(3, 6);
            this.LabelDesiredTimerResolution.Name = "LabelDesiredTimerResolution";
            this.LabelDesiredTimerResolution.Size = new System.Drawing.Size(119, 13);
            this.LabelDesiredTimerResolution.TabIndex = 22;
            this.LabelDesiredTimerResolution.Text = "Desired timer resolution:";
            this.LabelDesiredTimerResolution.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckBoxEnableTimer
            // 
            this.CheckBoxEnableTimer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxEnableTimer.AutoSize = true;
            this.CheckBoxEnableTimer.Checked = true;
            this.CheckBoxEnableTimer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxEnableTimer.Location = new System.Drawing.Point(3, 29);
            this.CheckBoxEnableTimer.Name = "CheckBoxEnableTimer";
            this.CheckBoxEnableTimer.Size = new System.Drawing.Size(84, 17);
            this.CheckBoxEnableTimer.TabIndex = 2;
            this.CheckBoxEnableTimer.Text = "Enable timer";
            this.CheckBoxEnableTimer.UseVisualStyleBackColor = true;
            this.CheckBoxEnableTimer.CheckedChanged += new System.EventHandler(this.CheckBoxEnableTimer_CheckedChanged);
            // 
            // ButtonClose
            // 
            this.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonClose.Location = new System.Drawing.Point(241, 389);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 59;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TableLayoutPanel3);
            this.groupBox3.Location = new System.Drawing.Point(4, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 72);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Timer settings";
            // 
            // TableLayoutPanel3
            // 
            this.TableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TableLayoutPanel3.ColumnCount = 2;
            this.TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.72727F));
            this.TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.TableLayoutPanel3.Controls.Add(this.CheckBoxEnableTimer, 0, 1);
            this.TableLayoutPanel3.Controls.Add(this.TimerPollingInterval, 1, 0);
            this.TableLayoutPanel3.Controls.Add(this.LabelTimerPollingInterval, 0, 0);
            this.TableLayoutPanel3.Location = new System.Drawing.Point(6, 16);
            this.TableLayoutPanel3.Name = "TableLayoutPanel3";
            this.TableLayoutPanel3.RowCount = 2;
            this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel3.Size = new System.Drawing.Size(300, 50);
            this.TableLayoutPanel3.TabIndex = 34;
            // 
            // TimerPollingInterval
            // 
            this.TimerPollingInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TimerPollingInterval.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.TimerPollingInterval.FormattingEnabled = true;
            this.TimerPollingInterval.Items.AddRange(new object[] {
            "0.5 sec",
            "1 sec",
            "2 sec",
            "5 sec",
            "10 sec"});
            this.TimerPollingInterval.Location = new System.Drawing.Point(221, 3);
            this.TimerPollingInterval.Name = "TimerPollingInterval";
            this.TimerPollingInterval.Size = new System.Drawing.Size(76, 21);
            this.TimerPollingInterval.TabIndex = 25;
            this.TimerPollingInterval.Text = "1 sec";
            this.TimerPollingInterval.SelectedIndexChanged += new System.EventHandler(this.TimerPollingInterval_SelectedIndexChanged);
            // 
            // LabelTimerPollingInterval
            // 
            this.LabelTimerPollingInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelTimerPollingInterval.AutoSize = true;
            this.LabelTimerPollingInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTimerPollingInterval.Location = new System.Drawing.Point(3, 6);
            this.LabelTimerPollingInterval.Name = "LabelTimerPollingInterval";
            this.LabelTimerPollingInterval.Size = new System.Drawing.Size(106, 13);
            this.LabelTimerPollingInterval.TabIndex = 23;
            this.LabelTimerPollingInterval.Text = "Timer polling interval:";
            this.LabelTimerPollingInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBoxStartupSettings
            // 
            this.GroupBoxStartupSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GroupBoxStartupSettings.Controls.Add(this.TableLayoutPanel4);
            this.GroupBoxStartupSettings.Location = new System.Drawing.Point(4, 288);
            this.GroupBoxStartupSettings.Name = "GroupBoxStartupSettings";
            this.GroupBoxStartupSettings.Size = new System.Drawing.Size(312, 95);
            this.GroupBoxStartupSettings.TabIndex = 71;
            this.GroupBoxStartupSettings.TabStop = false;
            this.GroupBoxStartupSettings.Text = "Startup settings";
            // 
            // TableLayoutPanel4
            // 
            this.TableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel4.ColumnCount = 2;
            this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.73F));
            this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27F));
            this.TableLayoutPanel4.Controls.Add(this.CheckBoxStartMemoryCleanerOnSystemStartup, 0, 2);
            this.TableLayoutPanel4.Controls.Add(this.CheckBoxStartTimerResolutionAutomatically, 0, 1);
            this.TableLayoutPanel4.Controls.Add(this.CheckBoxStartMinimized, 0, 0);
            this.TableLayoutPanel4.Location = new System.Drawing.Point(6, 16);
            this.TableLayoutPanel4.Name = "TableLayoutPanel4";
            this.TableLayoutPanel4.RowCount = 3;
            this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.TableLayoutPanel4.Size = new System.Drawing.Size(300, 73);
            this.TableLayoutPanel4.TabIndex = 68;
            // 
            // CheckBoxStartMemoryCleanerOnSystemStartup
            // 
            this.CheckBoxStartMemoryCleanerOnSystemStartup.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxStartMemoryCleanerOnSystemStartup.AutoSize = true;
            this.CheckBoxStartMemoryCleanerOnSystemStartup.Location = new System.Drawing.Point(3, 52);
            this.CheckBoxStartMemoryCleanerOnSystemStartup.Name = "CheckBoxStartMemoryCleanerOnSystemStartup";
            this.CheckBoxStartMemoryCleanerOnSystemStartup.Size = new System.Drawing.Size(212, 17);
            this.CheckBoxStartMemoryCleanerOnSystemStartup.TabIndex = 2;
            this.CheckBoxStartMemoryCleanerOnSystemStartup.Text = "Start Memory Cleaner on system startup";
            this.CheckBoxStartMemoryCleanerOnSystemStartup.UseVisualStyleBackColor = true;
            this.CheckBoxStartMemoryCleanerOnSystemStartup.CheckedChanged += new System.EventHandler(this.CheckBoxStartMemoryCleanerOnSystemStartup_CheckedChanged);
            // 
            // CheckBoxStartTimerResolutionAutomatically
            // 
            this.CheckBoxStartTimerResolutionAutomatically.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxStartTimerResolutionAutomatically.AutoSize = true;
            this.CheckBoxStartTimerResolutionAutomatically.Location = new System.Drawing.Point(3, 27);
            this.CheckBoxStartTimerResolutionAutomatically.Name = "CheckBoxStartTimerResolutionAutomatically";
            this.CheckBoxStartTimerResolutionAutomatically.Size = new System.Drawing.Size(185, 17);
            this.CheckBoxStartTimerResolutionAutomatically.TabIndex = 0;
            this.CheckBoxStartTimerResolutionAutomatically.Text = "Start timer resolution automatically";
            this.CheckBoxStartTimerResolutionAutomatically.UseVisualStyleBackColor = true;
            this.CheckBoxStartTimerResolutionAutomatically.CheckedChanged += new System.EventHandler(this.CheckBoxStartTimerResolutionAutomatically_CheckedChanged);
            // 
            // CheckBoxStartMinimized
            // 
            this.CheckBoxStartMinimized.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxStartMinimized.AutoSize = true;
            this.CheckBoxStartMinimized.Location = new System.Drawing.Point(3, 3);
            this.CheckBoxStartMinimized.Name = "CheckBoxStartMinimized";
            this.CheckBoxStartMinimized.Size = new System.Drawing.Size(96, 17);
            this.CheckBoxStartMinimized.TabIndex = 1;
            this.CheckBoxStartMinimized.Text = "Start minimized";
            this.CheckBoxStartMinimized.UseVisualStyleBackColor = true;
            this.CheckBoxStartMinimized.CheckedChanged += new System.EventHandler(this.CheckBoxStartMinimized_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 416);
            this.Controls.Add(this.GroupBoxStartupSettings);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonReset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.TableLayoutPanel2.ResumeLayout(false);
            this.TableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesiredTimerResolution)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.TableLayoutPanel3.ResumeLayout(false);
            this.TableLayoutPanel3.PerformLayout();
            this.GroupBoxStartupSettings.ResumeLayout(false);
            this.TableLayoutPanel4.ResumeLayout(false);
            this.TableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button ButtonReset;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button ButtonClose;
        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        public System.Windows.Forms.ComboBox HotkeyModifierKey;
        public System.Windows.Forms.ComboBox HotkeyToCleanMemory;
        public System.Windows.Forms.Label LabelHotkeyToCleanMemory;
        public System.Windows.Forms.Label LabelHotkeyModifierKey;
        public System.Windows.Forms.CheckBox CheckBoxEnableClearingOfTheStandbyList;
        public System.Windows.Forms.CheckBox CheckBoxEnableEmptyingOfTheWorkingSet;
        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
        public System.Windows.Forms.CheckBox CheckBoxEnableTimer;
        public System.Windows.Forms.NumericUpDown DesiredTimerResolution;
        public System.Windows.Forms.Label LabelDesiredTimerResolution;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel3;
        public System.Windows.Forms.Label LabelTimerPollingInterval;
        public System.Windows.Forms.ComboBox TimerPollingInterval;
        public System.Windows.Forms.CheckBox CheckBoxEnableCustomTimerResolution;
        public System.Windows.Forms.GroupBox GroupBoxStartupSettings;
        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel4;
        public System.Windows.Forms.CheckBox CheckBoxStartMemoryCleanerOnSystemStartup;
        public System.Windows.Forms.CheckBox CheckBoxStartTimerResolutionAutomatically;
        public System.Windows.Forms.CheckBox CheckBoxStartMinimized;
    }
}