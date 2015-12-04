namespace ORD.WF
{
    partial class RequestList
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
            this.components = new System.ComponentModel.Container();
            this.prescriptionList = new System.Windows.Forms.DataGridView();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medicines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prescriptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // prescriptionList
            // 
            this.prescriptionList.AllowUserToAddRows = false;
            this.prescriptionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prescriptionList.AutoGenerateColumns = false;
            this.prescriptionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.prescriptionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.createdDataGridViewTextBoxColumn,
            this.Medicines});
            this.prescriptionList.DataSource = this.prescriptionBindingSource;
            this.prescriptionList.Location = new System.Drawing.Point(12, 62);
            this.prescriptionList.Name = "prescriptionList";
            this.prescriptionList.ReadOnly = true;
            this.prescriptionList.Size = new System.Drawing.Size(662, 295);
            this.prescriptionList.TabIndex = 0;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Datum";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Medicines
            // 
            this.Medicines.HeaderText = "Léky";
            this.Medicines.Name = "Medicines";
            this.Medicines.ReadOnly = true;
            this.Medicines.Width = 500;
            // 
            // prescriptionBindingSource
            // 
            this.prescriptionBindingSource.DataSource = typeof(ORD.PatientCard.Requests.Prescription);
            // 
            // bCreate
            // 
            this.bCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCreate.Location = new System.Drawing.Point(554, 12);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(120, 44);
            this.bCreate.TabIndex = 1;
            this.bCreate.Text = "Předepsat recept";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // RequestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 369);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.prescriptionList);
            this.Name = "RequestList";
            this.Text = "RequestList";
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView prescriptionList;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.BindingSource prescriptionBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medicines;
    }
}