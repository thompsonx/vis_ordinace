namespace ORD.WF
{
    partial class RequestForm
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
            this.lDate = new System.Windows.Forms.Label();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.bAddMedicines = new System.Windows.Forms.Button();
            this.bCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lDate
            // 
            this.lDate.AutoSize = true;
            this.lDate.Location = new System.Drawing.Point(12, 18);
            this.lDate.Name = "lDate";
            this.lDate.Size = new System.Drawing.Size(41, 13);
            this.lDate.TabIndex = 0;
            this.lDate.Text = "Datum:";
            // 
            // tbDate
            // 
            this.tbDate.Enabled = false;
            this.tbDate.Location = new System.Drawing.Point(59, 15);
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(117, 20);
            this.tbDate.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(15, 62);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(430, 249);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // bAddMedicines
            // 
            this.bAddMedicines.Location = new System.Drawing.Point(328, 12);
            this.bAddMedicines.Name = "bAddMedicines";
            this.bAddMedicines.Size = new System.Drawing.Size(114, 44);
            this.bAddMedicines.TabIndex = 3;
            this.bAddMedicines.Text = "Přidat léky";
            this.bAddMedicines.UseVisualStyleBackColor = true;
            // 
            // bCreate
            // 
            this.bCreate.Location = new System.Drawing.Point(333, 317);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(112, 36);
            this.bCreate.TabIndex = 4;
            this.bCreate.Text = "Potvrdit";
            this.bCreate.UseVisualStyleBackColor = true;
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 359);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.bAddMedicines);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.tbDate);
            this.Controls.Add(this.lDate);
            this.Name = "RequestForm";
            this.Text = "RequestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lDate;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button bAddMedicines;
        private System.Windows.Forms.Button bCreate;
    }
}