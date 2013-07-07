namespace SSEPS_II.Synchronisation
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDatabase = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._txtServer = new System.Windows.Forms.TextBox();
            this._txtDatabase = new System.Windows.Forms.TextBox();
            this._txtUserId = new System.Windows.Forms.TextBox();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDatabase);
            this.tabControl1.Location = new System.Drawing.Point(12, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(414, 329);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageDatabase
            // 
            this.tabPageDatabase.Controls.Add(this._txtPassword);
            this.tabPageDatabase.Controls.Add(this._txtUserId);
            this.tabPageDatabase.Controls.Add(this._txtDatabase);
            this.tabPageDatabase.Controls.Add(this._txtServer);
            this.tabPageDatabase.Controls.Add(this.label4);
            this.tabPageDatabase.Controls.Add(this.label3);
            this.tabPageDatabase.Controls.Add(this.label2);
            this.tabPageDatabase.Controls.Add(this.label1);
            this.tabPageDatabase.Location = new System.Drawing.Point(4, 22);
            this.tabPageDatabase.Name = "tabPageDatabase";
            this.tabPageDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDatabase.Size = new System.Drawing.Size(406, 303);
            this.tabPageDatabase.TabIndex = 0;
            this.tabPageDatabase.Text = "Database";
            this.tabPageDatabase.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password";
            // 
            // _txtServer
            // 
            this._txtServer.Location = new System.Drawing.Point(101, 21);
            this._txtServer.Name = "_txtServer";
            this._txtServer.Size = new System.Drawing.Size(246, 20);
            this._txtServer.TabIndex = 4;
            // 
            // _txtDatabase
            // 
            this._txtDatabase.Location = new System.Drawing.Point(101, 71);
            this._txtDatabase.Name = "_txtDatabase";
            this._txtDatabase.Size = new System.Drawing.Size(246, 20);
            this._txtDatabase.TabIndex = 5;
            // 
            // _txtUserId
            // 
            this._txtUserId.Location = new System.Drawing.Point(101, 124);
            this._txtUserId.Name = "_txtUserId";
            this._txtUserId.Size = new System.Drawing.Size(246, 20);
            this._txtUserId.TabIndex = 6;
            // 
            // _txtPassword
            // 
            this._txtPassword.Location = new System.Drawing.Point(101, 177);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.Size = new System.Drawing.Size(246, 20);
            this._txtPassword.TabIndex = 7;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnCancel.Location = new System.Drawing.Point(32, 368);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(96, 31);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // _btnSave
            // 
            this._btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSave.Location = new System.Drawing.Point(157, 368);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(96, 31);
            this._btnSave.TabIndex = 2;
            this._btnSave.Text = "Save";
            this._btnSave.UseVisualStyleBackColor = true;
            this._btnSave.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // _btnClose
            // 
            this._btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnClose.Location = new System.Drawing.Point(277, 368);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(96, 31);
            this._btnClose.TabIndex = 3;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this._btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(446, 420);
            this.Controls.Add(this._btnClose);
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingsForm";
            this.Text = "Settings Form";
            this.tabControl1.ResumeLayout(false);
            this.tabPageDatabase.ResumeLayout(false);
            this.tabPageDatabase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDatabase;
        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.TextBox _txtUserId;
        private System.Windows.Forms.TextBox _txtDatabase;
        private System.Windows.Forms.TextBox _txtServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnClose;
    }
}