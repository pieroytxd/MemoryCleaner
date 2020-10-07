using System;

namespace Memory_Cleaner
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ButtonGitHub = new System.Windows.Forms.Button();
            this.ButtonTwitter = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonMinimize = new System.Windows.Forms.Button();
            this.ButtonCleanupsystemmemory = new System.Windows.Forms.Button();
            this.ButtonSaveSettings = new System.Windows.Forms.Button();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Memory Cleaner v1.0.0";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.Exit);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(66, 292);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Start automatically";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(187, 292);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Start minimized";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(352, 399);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox10);
            this.tabPage1.Controls.Add(this.textBox9);
            this.tabPage1.Controls.Add(this.textBox8);
            this.tabPage1.Controls.Add(this.textBox7);
            this.tabPage1.Controls.Add(this.textBox6);
            this.tabPage1.Controls.Add(this.textBox5);
            this.tabPage1.Controls.Add(this.textBox4);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.checkBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(344, 373);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.Control;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.Location = new System.Drawing.Point(12, 30);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(205, 16);
            this.textBox10.TabIndex = 11;
            this.textBox10.Text = "Current available memory:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(225, 108);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 10;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.Control;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(12, 108);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(205, 16);
            this.textBox8.TabIndex = 9;
            this.textBox8.Text = "Hotkey to clean up the memory:";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(225, 82);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 8;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Control;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(12, 82);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(205, 16);
            this.textBox6.TabIndex = 7;
            this.textBox6.Text = "Desired timer resolution:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(225, 56);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Control;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(12, 57);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(205, 16);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "Current timer resolution:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(225, 30);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ButtonGitHub);
            this.tabPage2.Controls.Add(this.ButtonTwitter);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(344, 373);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "About";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ButtonGitHub
            // 
            this.ButtonGitHub.Location = new System.Drawing.Point(5, 58);
            this.ButtonGitHub.Name = "ButtonGitHub";
            this.ButtonGitHub.Size = new System.Drawing.Size(48, 23);
            this.ButtonGitHub.TabIndex = 2;
            this.ButtonGitHub.Text = "GitHub";
            this.ButtonGitHub.UseVisualStyleBackColor = true;
            this.ButtonGitHub.Click += new System.EventHandler(this.ButtonGitHub_Click);
            // 
            // ButtonTwitter
            // 
            this.ButtonTwitter.Location = new System.Drawing.Point(5, 28);
            this.ButtonTwitter.Name = "ButtonTwitter";
            this.ButtonTwitter.Size = new System.Drawing.Size(47, 23);
            this.ButtonTwitter.TabIndex = 1;
            this.ButtonTwitter.Text = "Twitter";
            this.ButtonTwitter.UseVisualStyleBackColor = true;
            this.ButtonTwitter.Click += new System.EventHandler(this.ButtonTwitter_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(5, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 16);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Program developed by Danske";
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(1, 406);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(37, 23);
            this.ButtonStart.TabIndex = 3;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(40, 406);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(37, 23);
            this.ButtonStop.TabIndex = 4;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonMinimize
            // 
            this.ButtonMinimize.Location = new System.Drawing.Point(79, 406);
            this.ButtonMinimize.Name = "ButtonMinimize";
            this.ButtonMinimize.Size = new System.Drawing.Size(55, 23);
            this.ButtonMinimize.TabIndex = 5;
            this.ButtonMinimize.Text = "Minimize";
            this.ButtonMinimize.UseVisualStyleBackColor = true;
            this.ButtonMinimize.Click += new System.EventHandler(this.ButtonMinimize_Click);
            // 
            // ButtonCleanupsystemmemory
            // 
            this.ButtonCleanupsystemmemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCleanupsystemmemory.Location = new System.Drawing.Point(137, 406);
            this.ButtonCleanupsystemmemory.Name = "ButtonCleanupsystemmemory";
            this.ButtonCleanupsystemmemory.Size = new System.Drawing.Size(131, 23);
            this.ButtonCleanupsystemmemory.TabIndex = 6;
            this.ButtonCleanupsystemmemory.Text = "Clean up system memory";
            this.ButtonCleanupsystemmemory.UseVisualStyleBackColor = true;
            this.ButtonCleanupsystemmemory.Click += new System.EventHandler(this.ButtonCleanupsystemmemory_Click);
            // 
            // ButtonSaveSettings
            // 
            this.ButtonSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSaveSettings.Location = new System.Drawing.Point(271, 406);
            this.ButtonSaveSettings.Name = "ButtonSaveSettings";
            this.ButtonSaveSettings.Size = new System.Drawing.Size(81, 23);
            this.ButtonSaveSettings.TabIndex = 7;
            this.ButtonSaveSettings.Text = "Save Settings";
            this.ButtonSaveSettings.UseVisualStyleBackColor = true;
            this.ButtonSaveSettings.Click += new System.EventHandler(this.ButtonSaveSettings_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 430);
            this.Controls.Add(this.ButtonSaveSettings);
            this.Controls.Add(this.ButtonCleanupsystemmemory);
            this.Controls.Add(this.ButtonMinimize);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Memory Cleaner v1.0.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonMinimize;
        private System.Windows.Forms.Button ButtonCleanupsystemmemory;
        private System.Windows.Forms.Button ButtonSaveSettings;
        private System.Windows.Forms.Button ButtonGitHub;
        private System.Windows.Forms.Button ButtonTwitter;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
    }
}