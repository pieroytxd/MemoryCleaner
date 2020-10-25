namespace Memory_Cleaner
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.GroupBoxStartupSettings = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CheckBoxStartMinimized = new System.Windows.Forms.CheckBox();
            this.CheckBoxStartAutomatically = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DesiredTimerResolution = new System.Windows.Forms.NumericUpDown();
            this.LabelTimerPollingInterval = new System.Windows.Forms.Label();
            this.TimerPollingInterval = new System.Windows.Forms.ComboBox();
            this.LabelTimerEnabled = new System.Windows.Forms.Label();
            this.TimerEnabled = new System.Windows.Forms.ComboBox();
            this.LabelHotkeyModifierKey = new System.Windows.Forms.Label();
            this.HotkeyModifierKey = new System.Windows.Forms.ComboBox();
            this.LabelHotkeyToCleanMemory = new System.Windows.Forms.Label();
            this.HotkeyToCleanMemory = new System.Windows.Forms.ComboBox();
            this.LabelDesiredTimerResolution = new System.Windows.Forms.Label();
            this.GroupBoxStartupSettings.SuspendLayout();
            this.TableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesiredTimerResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(241, 230);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 52;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonReset
            // 
            this.ButtonReset.Location = new System.Drawing.Point(4, 230);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(75, 23);
            this.ButtonReset.TabIndex = 53;
            this.ButtonReset.Text = "Reset";
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // GroupBoxStartupSettings
            // 
            this.GroupBoxStartupSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GroupBoxStartupSettings.Controls.Add(this.TableLayoutPanel2);
            this.GroupBoxStartupSettings.Location = new System.Drawing.Point(4, 156);
            this.GroupBoxStartupSettings.Name = "GroupBoxStartupSettings";
            this.GroupBoxStartupSettings.Size = new System.Drawing.Size(312, 68);
            this.GroupBoxStartupSettings.TabIndex = 54;
            this.GroupBoxStartupSettings.TabStop = false;
            this.GroupBoxStartupSettings.Text = "Startup settings";
            // 
            // TableLayoutPanel2
            // 
            this.TableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel2.ColumnCount = 1;
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel2.Controls.Add(this.CheckBoxStartMinimized, 0, 1);
            this.TableLayoutPanel2.Controls.Add(this.CheckBoxStartAutomatically, 0, 0);
            this.TableLayoutPanel2.Location = new System.Drawing.Point(6, 14);
            this.TableLayoutPanel2.Name = "TableLayoutPanel2";
            this.TableLayoutPanel2.RowCount = 2;
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.TableLayoutPanel2.Size = new System.Drawing.Size(300, 50);
            this.TableLayoutPanel2.TabIndex = 34;
            // 
            // CheckBoxStartMinimized
            // 
            this.CheckBoxStartMinimized.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxStartMinimized.AutoSize = true;
            this.CheckBoxStartMinimized.Location = new System.Drawing.Point(3, 28);
            this.CheckBoxStartMinimized.Name = "CheckBoxStartMinimized";
            this.CheckBoxStartMinimized.Size = new System.Drawing.Size(96, 17);
            this.CheckBoxStartMinimized.TabIndex = 1;
            this.CheckBoxStartMinimized.Text = "Start minimized";
            this.CheckBoxStartMinimized.UseVisualStyleBackColor = true;
            this.CheckBoxStartMinimized.CheckedChanged += new System.EventHandler(this.CheckBoxStartMinimized_CheckedChanged);
            // 
            // CheckBoxStartAutomatically
            // 
            this.CheckBoxStartAutomatically.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CheckBoxStartAutomatically.AutoSize = true;
            this.CheckBoxStartAutomatically.Location = new System.Drawing.Point(3, 3);
            this.CheckBoxStartAutomatically.Name = "CheckBoxStartAutomatically";
            this.CheckBoxStartAutomatically.Size = new System.Drawing.Size(185, 17);
            this.CheckBoxStartAutomatically.TabIndex = 0;
            this.CheckBoxStartAutomatically.Text = "Start timer resolution automatically";
            this.CheckBoxStartAutomatically.UseVisualStyleBackColor = true;
            this.CheckBoxStartAutomatically.CheckedChanged += new System.EventHandler(this.CheckBoxStartAutomatically_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 145);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General settings";
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.72727F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.TableLayoutPanel1.Controls.Add(this.DesiredTimerResolution, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.LabelTimerPollingInterval, 0, 4);
            this.TableLayoutPanel1.Controls.Add(this.TimerPollingInterval, 1, 4);
            this.TableLayoutPanel1.Controls.Add(this.LabelTimerEnabled, 0, 3);
            this.TableLayoutPanel1.Controls.Add(this.TimerEnabled, 1, 3);
            this.TableLayoutPanel1.Controls.Add(this.LabelHotkeyModifierKey, 0, 2);
            this.TableLayoutPanel1.Controls.Add(this.HotkeyModifierKey, 1, 2);
            this.TableLayoutPanel1.Controls.Add(this.LabelHotkeyToCleanMemory, 0, 1);
            this.TableLayoutPanel1.Controls.Add(this.HotkeyToCleanMemory, 1, 1);
            this.TableLayoutPanel1.Controls.Add(this.LabelDesiredTimerResolution, 0, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(6, 16);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 5;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(300, 125);
            this.TableLayoutPanel1.TabIndex = 30;
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
            // LabelTimerPollingInterval
            // 
            this.LabelTimerPollingInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelTimerPollingInterval.AutoSize = true;
            this.LabelTimerPollingInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTimerPollingInterval.Location = new System.Drawing.Point(3, 106);
            this.LabelTimerPollingInterval.Name = "LabelTimerPollingInterval";
            this.LabelTimerPollingInterval.Size = new System.Drawing.Size(106, 13);
            this.LabelTimerPollingInterval.TabIndex = 26;
            this.LabelTimerPollingInterval.Text = "Timer polling interval:";
            this.LabelTimerPollingInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimerPollingInterval
            // 
            this.TimerPollingInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TimerPollingInterval.FormattingEnabled = true;
            this.TimerPollingInterval.Items.AddRange(new object[] {
            "0.5 sec",
            "1 sec",
            "2 sec",
            "5 sec",
            "10 sec"});
            this.TimerPollingInterval.Location = new System.Drawing.Point(221, 103);
            this.TimerPollingInterval.Name = "TimerPollingInterval";
            this.TimerPollingInterval.Size = new System.Drawing.Size(76, 21);
            this.TimerPollingInterval.TabIndex = 17;
            this.TimerPollingInterval.Text = "1 sec";
            this.TimerPollingInterval.TextChanged += new System.EventHandler(this.TimerPollingInterval_TextChanged);
            // 
            // LabelTimerEnabled
            // 
            this.LabelTimerEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelTimerEnabled.AutoSize = true;
            this.LabelTimerEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTimerEnabled.Location = new System.Drawing.Point(3, 81);
            this.LabelTimerEnabled.Name = "LabelTimerEnabled";
            this.LabelTimerEnabled.Size = new System.Drawing.Size(77, 13);
            this.LabelTimerEnabled.TabIndex = 25;
            this.LabelTimerEnabled.Text = "Timer enabled:";
            this.LabelTimerEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimerEnabled
            // 
            this.TimerEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TimerEnabled.FormattingEnabled = true;
            this.TimerEnabled.Items.AddRange(new object[] {
            "True",
            "False"});
            this.TimerEnabled.Location = new System.Drawing.Point(221, 78);
            this.TimerEnabled.Name = "TimerEnabled";
            this.TimerEnabled.Size = new System.Drawing.Size(76, 21);
            this.TimerEnabled.TabIndex = 19;
            this.TimerEnabled.Text = "True";
            this.TimerEnabled.TextChanged += new System.EventHandler(this.TimerEnabled_TextChanged);
            // 
            // LabelHotkeyModifierKey
            // 
            this.LabelHotkeyModifierKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelHotkeyModifierKey.AutoSize = true;
            this.LabelHotkeyModifierKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHotkeyModifierKey.Location = new System.Drawing.Point(3, 56);
            this.LabelHotkeyModifierKey.Name = "LabelHotkeyModifierKey";
            this.LabelHotkeyModifierKey.Size = new System.Drawing.Size(103, 13);
            this.LabelHotkeyModifierKey.TabIndex = 24;
            this.LabelHotkeyModifierKey.Text = "Hotkey modifier key:";
            this.LabelHotkeyModifierKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HotkeyModifierKey
            // 
            this.HotkeyModifierKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.HotkeyModifierKey.FormattingEnabled = true;
            this.HotkeyModifierKey.Items.AddRange(new object[] {
            "None",
            "Alt",
            "Ctrl",
            "Shift"});
            this.HotkeyModifierKey.Location = new System.Drawing.Point(221, 53);
            this.HotkeyModifierKey.Name = "HotkeyModifierKey";
            this.HotkeyModifierKey.Size = new System.Drawing.Size(76, 21);
            this.HotkeyModifierKey.TabIndex = 14;
            this.HotkeyModifierKey.Text = "None";
            this.HotkeyModifierKey.TextChanged += new System.EventHandler(this.HotkeyModifierKey_TextChanged);
            // 
            // LabelHotkeyToCleanMemory
            // 
            this.LabelHotkeyToCleanMemory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelHotkeyToCleanMemory.AutoSize = true;
            this.LabelHotkeyToCleanMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHotkeyToCleanMemory.Location = new System.Drawing.Point(3, 31);
            this.LabelHotkeyToCleanMemory.Name = "LabelHotkeyToCleanMemory";
            this.LabelHotkeyToCleanMemory.Size = new System.Drawing.Size(124, 13);
            this.LabelHotkeyToCleanMemory.TabIndex = 23;
            this.LabelHotkeyToCleanMemory.Text = "Hotkey to clean memory:";
            this.LabelHotkeyToCleanMemory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HotkeyToCleanMemory
            // 
            this.HotkeyToCleanMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
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
            this.HotkeyToCleanMemory.Location = new System.Drawing.Point(221, 28);
            this.HotkeyToCleanMemory.Name = "HotkeyToCleanMemory";
            this.HotkeyToCleanMemory.Size = new System.Drawing.Size(76, 21);
            this.HotkeyToCleanMemory.TabIndex = 15;
            this.HotkeyToCleanMemory.Text = "None";
            this.HotkeyToCleanMemory.TextChanged += new System.EventHandler(this.HotkeyToCleanMemory_TextChanged);
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
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 256);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBoxStartupSettings);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.ButtonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.GroupBoxStartupSettings.ResumeLayout(false);
            this.TableLayoutPanel2.ResumeLayout(false);
            this.TableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesiredTimerResolution)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonReset;
        public System.Windows.Forms.GroupBox GroupBoxStartupSettings;
        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
        public System.Windows.Forms.CheckBox CheckBoxStartAutomatically;
        public System.Windows.Forms.CheckBox CheckBoxStartMinimized;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        public System.Windows.Forms.NumericUpDown DesiredTimerResolution;
        public System.Windows.Forms.Label LabelTimerPollingInterval;
        public System.Windows.Forms.ComboBox TimerPollingInterval;
        public System.Windows.Forms.Label LabelTimerEnabled;
        public System.Windows.Forms.ComboBox TimerEnabled;
        public System.Windows.Forms.Label LabelHotkeyModifierKey;
        public System.Windows.Forms.ComboBox HotkeyModifierKey;
        public System.Windows.Forms.Label LabelHotkeyToCleanMemory;
        public System.Windows.Forms.ComboBox HotkeyToCleanMemory;
        public System.Windows.Forms.Label LabelDesiredTimerResolution;
    }
}