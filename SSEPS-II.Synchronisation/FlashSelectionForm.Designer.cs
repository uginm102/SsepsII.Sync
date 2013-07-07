namespace SSEPS_II.Synchronisation
{
    partial class FlashSelectionForm
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
            this.groupBoxSseps = new System.Windows.Forms.GroupBox();
            this._panelSsepsMedium = new System.Windows.Forms.Panel();
            this.groupBoxPrevious = new System.Windows.Forms.GroupBox();
            this._panelPreviousMedium = new System.Windows.Forms.Panel();
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this._panelOtherMedium = new System.Windows.Forms.Panel();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOK = new System.Windows.Forms.Button();
            this.groupBoxSseps.SuspendLayout();
            this.groupBoxPrevious.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSseps
            // 
            this.groupBoxSseps.Controls.Add(this._panelSsepsMedium);
            this.groupBoxSseps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSseps.Location = new System.Drawing.Point(9, 8);
            this.groupBoxSseps.Name = "groupBoxSseps";
            this.groupBoxSseps.Size = new System.Drawing.Size(400, 100);
            this.groupBoxSseps.TabIndex = 1;
            this.groupBoxSseps.TabStop = false;
            this.groupBoxSseps.Text = "SSEPS Medium";
            // 
            // _panelSsepsMedium
            // 
            this._panelSsepsMedium.AutoScroll = true;
            this._panelSsepsMedium.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelSsepsMedium.Location = new System.Drawing.Point(3, 18);
            this._panelSsepsMedium.Name = "_panelSsepsMedium";
            this._panelSsepsMedium.Size = new System.Drawing.Size(394, 79);
            this._panelSsepsMedium.TabIndex = 0;
            // 
            // groupBoxPrevious
            // 
            this.groupBoxPrevious.Controls.Add(this._panelPreviousMedium);
            this.groupBoxPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPrevious.Location = new System.Drawing.Point(9, 120);
            this.groupBoxPrevious.Name = "groupBoxPrevious";
            this.groupBoxPrevious.Size = new System.Drawing.Size(400, 100);
            this.groupBoxPrevious.TabIndex = 2;
            this.groupBoxPrevious.TabStop = false;
            this.groupBoxPrevious.Text = "Previously used medium";
            // 
            // _panelPreviousMedium
            // 
            this._panelPreviousMedium.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelPreviousMedium.Location = new System.Drawing.Point(3, 18);
            this._panelPreviousMedium.Name = "_panelPreviousMedium";
            this._panelPreviousMedium.Size = new System.Drawing.Size(394, 79);
            this._panelPreviousMedium.TabIndex = 0;
            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Controls.Add(this._panelOtherMedium);
            this.groupBoxOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxOther.Location = new System.Drawing.Point(9, 232);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(400, 100);
            this.groupBoxOther.TabIndex = 3;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "Other medium";
            // 
            // _panelOtherMedium
            // 
            this._panelOtherMedium.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelOtherMedium.Location = new System.Drawing.Point(3, 18);
            this._panelOtherMedium.Name = "_panelOtherMedium";
            this._panelOtherMedium.Size = new System.Drawing.Size(394, 79);
            this._panelOtherMedium.TabIndex = 0;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnCancel.Location = new System.Drawing.Point(85, 349);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 41);
            this._btnCancel.TabIndex = 4;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // _btnOK
            // 
            this._btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnOK.Location = new System.Drawing.Point(228, 349);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 41);
            this._btnOK.TabIndex = 5;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // FlashSelectionForm
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(418, 403);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this.groupBoxOther);
            this.Controls.Add(this.groupBoxPrevious);
            this.Controls.Add(this.groupBoxSseps);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlashSelectionForm";
            this.Text = "Select medium to use";
            this.groupBoxSseps.ResumeLayout(false);
            this.groupBoxPrevious.ResumeLayout(false);
            this.groupBoxOther.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSseps;
        private System.Windows.Forms.GroupBox groupBoxPrevious;
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Panel _panelSsepsMedium;
        private System.Windows.Forms.Panel _panelPreviousMedium;
        private System.Windows.Forms.Panel _panelOtherMedium;
    }
}