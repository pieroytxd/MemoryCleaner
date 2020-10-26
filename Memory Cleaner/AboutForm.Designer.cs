namespace Memory_Cleaner
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Info = new System.Windows.Forms.Label();
            this.LinkGitHub = new System.Windows.Forms.LinkLabel();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonDiscord = new System.Windows.Forms.Button();
            this.ButtonDonate = new System.Windows.Forms.Button();
            this.ButtonTwitter = new System.Windows.Forms.Button();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.ColumnCount = 1;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel1.Controls.Add(this.Info, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.LinkGitHub, 0, 1);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(10, 5);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 2;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(215, 71);
            this.TableLayoutPanel1.TabIndex = 0;
            // 
            // Info
            // 
            this.Info.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Info.AutoSize = true;
            this.Info.Location = new System.Drawing.Point(3, 2);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(113, 52);
            this.Info.TabIndex = 0;
            this.Info.Text = "Memory Cleaner\r\nVersion 1.5.0\r\n\r\nDeveloped by Danske";
            // 
            // LinkGitHub
            // 
            this.LinkGitHub.ActiveLinkColor = System.Drawing.Color.White;
            this.LinkGitHub.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LinkGitHub.AutoSize = true;
            this.LinkGitHub.LinkColor = System.Drawing.Color.White;
            this.LinkGitHub.Location = new System.Drawing.Point(3, 57);
            this.LinkGitHub.Name = "LinkGitHub";
            this.LinkGitHub.Size = new System.Drawing.Size(105, 13);
            this.LinkGitHub.TabIndex = 1;
            this.LinkGitHub.TabStop = true;
            this.LinkGitHub.Text = "github.com/danskee";
            this.LinkGitHub.VisitedLinkColor = System.Drawing.Color.White;
            this.LinkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkGitHub_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonClose.AutoSize = true;
            this.ButtonClose.Location = new System.Drawing.Point(182, 87);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(43, 23);
            this.ButtonClose.TabIndex = 53;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonDiscord
            // 
            this.ButtonDiscord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonDiscord.AutoSize = true;
            this.ButtonDiscord.Location = new System.Drawing.Point(123, 87);
            this.ButtonDiscord.Name = "ButtonDiscord";
            this.ButtonDiscord.Size = new System.Drawing.Size(53, 23);
            this.ButtonDiscord.TabIndex = 54;
            this.ButtonDiscord.Text = "Discord";
            this.ButtonDiscord.UseVisualStyleBackColor = true;
            this.ButtonDiscord.Click += new System.EventHandler(this.ButtonDiscord_Click);
            // 
            // ButtonDonate
            // 
            this.ButtonDonate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonDonate.AutoSize = true;
            this.ButtonDonate.Location = new System.Drawing.Point(65, 87);
            this.ButtonDonate.Name = "ButtonDonate";
            this.ButtonDonate.Size = new System.Drawing.Size(52, 23);
            this.ButtonDonate.TabIndex = 55;
            this.ButtonDonate.Text = "Donate";
            this.ButtonDonate.UseVisualStyleBackColor = true;
            this.ButtonDonate.Click += new System.EventHandler(this.ButtonDonate_Click);
            // 
            // ButtonTwitter
            // 
            this.ButtonTwitter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonTwitter.AutoSize = true;
            this.ButtonTwitter.Location = new System.Drawing.Point(10, 87);
            this.ButtonTwitter.Name = "ButtonTwitter";
            this.ButtonTwitter.Size = new System.Drawing.Size(49, 23);
            this.ButtonTwitter.TabIndex = 56;
            this.ButtonTwitter.Text = "Twitter";
            this.ButtonTwitter.UseVisualStyleBackColor = true;
            this.ButtonTwitter.Click += new System.EventHandler(this.ButtonTwitter_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 118);
            this.Controls.Add(this.ButtonTwitter);
            this.Controls.Add(this.ButtonDonate);
            this.Controls.Add(this.ButtonDiscord);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.TableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.LinkLabel LinkGitHub;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonDiscord;
        private System.Windows.Forms.Button ButtonDonate;
        private System.Windows.Forms.Button ButtonTwitter;
    }
}