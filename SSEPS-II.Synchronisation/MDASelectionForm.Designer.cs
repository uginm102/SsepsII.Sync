namespace SSEPS_II.Synchronisation
{
    partial class MDASelectionForm
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGovernmentLevel = new System.Windows.Forms.ComboBox();
            this.cmbGovernment = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this._dgvMDAs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._dgvMDAs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(162, 354);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(82, 34);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Government level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Government";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "MDA";
            // 
            // cmbGovernmentLevel
            // 
            this.cmbGovernmentLevel.FormattingEnabled = true;
            this.cmbGovernmentLevel.Location = new System.Drawing.Point(119, 6);
            this.cmbGovernmentLevel.Name = "cmbGovernmentLevel";
            this.cmbGovernmentLevel.Size = new System.Drawing.Size(156, 21);
            this.cmbGovernmentLevel.TabIndex = 5;
            this.cmbGovernmentLevel.SelectedIndexChanged += new System.EventHandler(this.cmbGovernmentLevel_SelectedIndexChanged);
            // 
            // cmbGovernment
            // 
            this.cmbGovernment.FormattingEnabled = true;
            this.cmbGovernment.Location = new System.Drawing.Point(119, 33);
            this.cmbGovernment.Name = "cmbGovernment";
            this.cmbGovernment.Size = new System.Drawing.Size(401, 21);
            this.cmbGovernment.TabIndex = 6;
            this.cmbGovernment.SelectedIndexChanged += new System.EventHandler(this.cmbGovernment_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(323, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 34);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // _dgvMDAs
            // 
            this._dgvMDAs.AllowUserToAddRows = false;
            this._dgvMDAs.AllowUserToDeleteRows = false;
            this._dgvMDAs.AllowUserToResizeColumns = false;
            this._dgvMDAs.AllowUserToResizeRows = false;
            this._dgvMDAs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvMDAs.Location = new System.Drawing.Point(116, 69);
            this._dgvMDAs.Name = "_dgvMDAs";
            this._dgvMDAs.RowHeadersVisible = false;
            this._dgvMDAs.Size = new System.Drawing.Size(401, 279);
            this._dgvMDAs.TabIndex = 9;
            this._dgvMDAs.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._dgvMDAs_CellMouseUp);
            this._dgvMDAs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvMDAs_CellValueChanged);
            // 
            // MDASelectionForm
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 396);
            this.Controls.Add(this._dgvMDAs);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbGovernment);
            this.Controls.Add(this.cmbGovernmentLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MDASelectionForm";
            this.Text = "MDA Selection Form";
            ((System.ComponentModel.ISupportInitialize)(this._dgvMDAs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGovernmentLevel;
        private System.Windows.Forms.ComboBox cmbGovernment;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView _dgvMDAs;
    }
}