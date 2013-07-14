namespace SSEPS_II.Synchronisation
{
    partial class MainForm
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
            this._panelHeader = new System.Windows.Forms.Panel();
            this._btnLogout = new System.Windows.Forms.Button();
            this._lblLoggedInUser = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this._panelMain = new System.Windows.Forms.Panel();
            this._groupBoxFile = new System.Windows.Forms.GroupBox();
            this._txtFeedback = new System.Windows.Forms.TextBox();
            this._rbtnFile = new System.Windows.Forms.RadioButton();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this._btnSelectFlashDisk = new System.Windows.Forms.Button();
            this._lblSelectedFlash = new System.Windows.Forms.Label();
            this._rbtnReceive = new System.Windows.Forms.RadioButton();
            this._rbtnSendApproval = new System.Windows.Forms.RadioButton();
            this._rbtnSendApproved = new System.Windows.Forms.RadioButton();
            this._rbtnNetwork = new System.Windows.Forms.RadioButton();
            this._btnSync = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDatabseSettings = new System.Windows.Forms.ToolStripMenuItem();
            this._btnSelectMDA = new System.Windows.Forms.Button();
            this._panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxLogo)).BeginInit();
            this._panelMain.SuspendLayout();
            this._groupBoxFile.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelHeader
            // 
            this._panelHeader.Controls.Add(this._btnLogout);
            this._panelHeader.Controls.Add(this._lblLoggedInUser);
            this._panelHeader.Controls.Add(this.label2);
            this._panelHeader.Controls.Add(this.label1);
            this._panelHeader.Controls.Add(this._pictureBoxLogo);
            this._panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelHeader.Location = new System.Drawing.Point(0, 24);
            this._panelHeader.Name = "_panelHeader";
            this._panelHeader.Size = new System.Drawing.Size(698, 111);
            this._panelHeader.TabIndex = 0;
            // 
            // _btnLogout
            // 
            this._btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnLogout.Location = new System.Drawing.Point(609, 4);
            this._btnLogout.Name = "_btnLogout";
            this._btnLogout.Size = new System.Drawing.Size(85, 41);
            this._btnLogout.TabIndex = 0;
            this._btnLogout.Text = "Logout";
            this._btnLogout.UseVisualStyleBackColor = true;
            this._btnLogout.Click += new System.EventHandler(this._btnLogout_Click);
            // 
            // _lblLoggedInUser
            // 
            this._lblLoggedInUser.AutoSize = true;
            this._lblLoggedInUser.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this._lblLoggedInUser.Location = new System.Drawing.Point(418, 8);
            this._lblLoggedInUser.Name = "_lblLoggedInUser";
            this._lblLoggedInUser.Size = new System.Drawing.Size(16, 13);
            this._lblLoggedInUser.TabIndex = 3;
            this._lblLoggedInUser.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(134, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(452, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ministry of Labour, Public Service and Human Resource Development";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(133, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "South Sudan Electronic Payroll System - SSEPS II";
            // 
            // _pictureBoxLogo
            // 
            this._pictureBoxLogo.Image = global::SSEPS_II.Synchronisation.Properties.Resources.PubServLogo;
            this._pictureBoxLogo.Location = new System.Drawing.Point(1, 1);
            this._pictureBoxLogo.Name = "_pictureBoxLogo";
            this._pictureBoxLogo.Size = new System.Drawing.Size(126, 85);
            this._pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBoxLogo.TabIndex = 0;
            this._pictureBoxLogo.TabStop = false;
            // 
            // _panelMain
            // 
            this._panelMain.Controls.Add(this._groupBoxFile);
            this._panelMain.Controls.Add(this._btnSync);
            this._panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMain.Location = new System.Drawing.Point(0, 135);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(698, 348);
            this._panelMain.TabIndex = 0;
            // 
            // _groupBoxFile
            // 
            this._groupBoxFile.Controls.Add(this._txtFeedback);
            this._groupBoxFile.Controls.Add(this._rbtnFile);
            this._groupBoxFile.Controls.Add(this.groupBoxFile);
            this._groupBoxFile.Controls.Add(this._rbtnNetwork);
            this._groupBoxFile.Location = new System.Drawing.Point(4, 1);
            this._groupBoxFile.Name = "_groupBoxFile";
            this._groupBoxFile.Size = new System.Drawing.Size(418, 339);
            this._groupBoxFile.TabIndex = 1;
            this._groupBoxFile.TabStop = false;
            // 
            // _txtFeedback
            // 
            this._txtFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtFeedback.Location = new System.Drawing.Point(7, 215);
            this._txtFeedback.Multiline = true;
            this._txtFeedback.Name = "_txtFeedback";
            this._txtFeedback.ReadOnly = true;
            this._txtFeedback.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtFeedback.Size = new System.Drawing.Size(409, 116);
            this._txtFeedback.TabIndex = 6;
            // 
            // _rbtnFile
            // 
            this._rbtnFile.AutoSize = true;
            this._rbtnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rbtnFile.Location = new System.Drawing.Point(7, 38);
            this._rbtnFile.Name = "_rbtnFile";
            this._rbtnFile.Size = new System.Drawing.Size(154, 24);
            this._rbtnFile.TabIndex = 1;
            this._rbtnFile.TabStop = true;
            this._rbtnFile.Text = "Using flash disk";
            this._rbtnFile.UseVisualStyleBackColor = true;
            this._rbtnFile.CheckedChanged += new System.EventHandler(this._rbtnFile_CheckedChanged);
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this._btnSelectMDA);
            this.groupBoxFile.Controls.Add(this._btnSelectFlashDisk);
            this.groupBoxFile.Controls.Add(this._lblSelectedFlash);
            this.groupBoxFile.Controls.Add(this._rbtnReceive);
            this.groupBoxFile.Controls.Add(this._rbtnSendApproval);
            this.groupBoxFile.Controls.Add(this._rbtnSendApproved);
            this.groupBoxFile.Enabled = false;
            this.groupBoxFile.Location = new System.Drawing.Point(8, 69);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(409, 142);
            this.groupBoxFile.TabIndex = 8;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File";
            // 
            // _btnSelectFlashDisk
            // 
            this._btnSelectFlashDisk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSelectFlashDisk.Location = new System.Drawing.Point(6, 101);
            this._btnSelectFlashDisk.Name = "_btnSelectFlashDisk";
            this._btnSelectFlashDisk.Size = new System.Drawing.Size(172, 35);
            this._btnSelectFlashDisk.TabIndex = 4;
            this._btnSelectFlashDisk.Text = "Select Flash Disk";
            this._btnSelectFlashDisk.UseVisualStyleBackColor = true;
            this._btnSelectFlashDisk.Click += new System.EventHandler(this._btnSelectFlashDisk_Click);
            // 
            // _lblSelectedFlash
            // 
            this._lblSelectedFlash.AutoSize = true;
            this._lblSelectedFlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblSelectedFlash.Location = new System.Drawing.Point(191, 107);
            this._lblSelectedFlash.Name = "_lblSelectedFlash";
            this._lblSelectedFlash.Size = new System.Drawing.Size(21, 20);
            this._lblSelectedFlash.TabIndex = 5;
            this._lblSelectedFlash.Text = "...";
            // 
            // _rbtnReceive
            // 
            this._rbtnReceive.AutoSize = true;
            this._rbtnReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rbtnReceive.Location = new System.Drawing.Point(20, 20);
            this._rbtnReceive.Name = "_rbtnReceive";
            this._rbtnReceive.Size = new System.Drawing.Size(179, 20);
            this._rbtnReceive.TabIndex = 0;
            this._rbtnReceive.TabStop = true;
            this._rbtnReceive.Text = "Receive updated records";
            this._rbtnReceive.UseVisualStyleBackColor = true;
            // 
            // _rbtnSendApproval
            // 
            this._rbtnSendApproval.AutoSize = true;
            this._rbtnSendApproval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rbtnSendApproval.Location = new System.Drawing.Point(20, 44);
            this._rbtnSendApproval.Name = "_rbtnSendApproval";
            this._rbtnSendApproval.Size = new System.Drawing.Size(238, 20);
            this._rbtnSendApproval.TabIndex = 1;
            this._rbtnSendApproval.TabStop = true;
            this._rbtnSendApproval.Text = "Send changed records for approval";
            this._rbtnSendApproval.UseVisualStyleBackColor = true;
            // 
            // _rbtnSendApproved
            // 
            this._rbtnSendApproved.AutoSize = true;
            this._rbtnSendApproved.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rbtnSendApproved.Location = new System.Drawing.Point(20, 68);
            this._rbtnSendApproved.Name = "_rbtnSendApproved";
            this._rbtnSendApproved.Size = new System.Drawing.Size(192, 20);
            this._rbtnSendApproved.TabIndex = 2;
            this._rbtnSendApproved.TabStop = true;
            this._rbtnSendApproved.Text = "Send all approved changes";
            this._rbtnSendApproved.UseVisualStyleBackColor = true;
            this._rbtnSendApproved.CheckedChanged += new System.EventHandler(this._rbtnSendApproved_CheckedChanged);
            // 
            // _rbtnNetwork
            // 
            this._rbtnNetwork.AutoSize = true;
            this._rbtnNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rbtnNetwork.Location = new System.Drawing.Point(7, 14);
            this._rbtnNetwork.Name = "_rbtnNetwork";
            this._rbtnNetwork.Size = new System.Drawing.Size(164, 24);
            this._rbtnNetwork.TabIndex = 0;
            this._rbtnNetwork.TabStop = true;
            this._rbtnNetwork.Text = "Network Connect";
            this._rbtnNetwork.UseVisualStyleBackColor = true;
            this._rbtnNetwork.CheckedChanged += new System.EventHandler(this._rbtnNetwork_CheckedChanged);
            // 
            // _btnSync
            // 
            this._btnSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._btnSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSync.Image = global::SSEPS_II.Synchronisation.Properties.Resources.sync;
            this._btnSync.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._btnSync.Location = new System.Drawing.Point(428, 101);
            this._btnSync.Name = "_btnSync";
            this._btnSync.Size = new System.Drawing.Size(266, 133);
            this._btnSync.TabIndex = 0;
            this._btnSync.Text = "Synchronise";
            this._btnSync.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnSync.UseVisualStyleBackColor = true;
            this._btnSync.Click += new System.EventHandler(this._btnSync_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSettings});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(698, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemSettings
            // 
            this.toolStripMenuItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDatabseSettings});
            this.toolStripMenuItemSettings.Name = "toolStripMenuItemSettings";
            this.toolStripMenuItemSettings.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItemSettings.Text = "Settings";
            // 
            // toolStripMenuItemDatabseSettings
            // 
            this.toolStripMenuItemDatabseSettings.Name = "toolStripMenuItemDatabseSettings";
            this.toolStripMenuItemDatabseSettings.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemDatabseSettings.Text = "Databse settings";
            this.toolStripMenuItemDatabseSettings.Click += new System.EventHandler(this.toolStripMenuItemDatabseSettings_Click);
            // 
            // _btnSelectMDA
            // 
            this._btnSelectMDA.Enabled = false;
            this._btnSelectMDA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSelectMDA.Location = new System.Drawing.Point(218, 68);
            this._btnSelectMDA.Name = "_btnSelectMDA";
            this._btnSelectMDA.Size = new System.Drawing.Size(72, 23);
            this._btnSelectMDA.TabIndex = 3;
            this._btnSelectMDA.Text = "Select MDA";
            this._btnSelectMDA.UseVisualStyleBackColor = true;
            this._btnSelectMDA.Click += new System.EventHandler(this._btnSelectMDA_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 483);
            this.Controls.Add(this._panelMain);
            this.Controls.Add(this._panelHeader);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "SSEPS SYNC";
            this._panelHeader.ResumeLayout(false);
            this._panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxLogo)).EndInit();
            this._panelMain.ResumeLayout(false);
            this._groupBoxFile.ResumeLayout(false);
            this._groupBoxFile.PerformLayout();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _panelHeader;
        private System.Windows.Forms.Button _btnLogout;
        private System.Windows.Forms.Label _lblLoggedInUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox _pictureBoxLogo;
        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.GroupBox _groupBoxFile;
        private System.Windows.Forms.RadioButton _rbtnFile;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.TextBox _txtFeedback;
        private System.Windows.Forms.RadioButton _rbtnReceive;
        private System.Windows.Forms.RadioButton _rbtnSendApproval;
        private System.Windows.Forms.RadioButton _rbtnSendApproved;
        private System.Windows.Forms.RadioButton _rbtnNetwork;
        private System.Windows.Forms.Button _btnSync;
        private System.Windows.Forms.Label _lblSelectedFlash;
        private System.Windows.Forms.Button _btnSelectFlashDisk;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatabseSettings;
        private System.Windows.Forms.Button _btnSelectMDA;
    }
}

