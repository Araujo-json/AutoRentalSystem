namespace ClientApp
{
    partial class deleteForm
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
            this.cardToDelete = new System.Windows.Forms.TextBox();
            this.applybtn = new System.Windows.Forms.Button();
            this.fieldLabel = new System.Windows.Forms.Label();
            this.subTitle = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cardToDelete
            // 
            this.cardToDelete.Location = new System.Drawing.Point(166, 136);
            this.cardToDelete.Name = "cardToDelete";
            this.cardToDelete.Size = new System.Drawing.Size(153, 20);
            this.cardToDelete.TabIndex = 70;
            // 
            // applybtn
            // 
            this.applybtn.Location = new System.Drawing.Point(361, 134);
            this.applybtn.Name = "applybtn";
            this.applybtn.Size = new System.Drawing.Size(75, 23);
            this.applybtn.TabIndex = 69;
            this.applybtn.Text = "Apply";
            this.applybtn.UseVisualStyleBackColor = true;
            this.applybtn.Click += new System.EventHandler(this.apply_Click);
            // 
            // fieldLabel
            // 
            this.fieldLabel.AutoSize = true;
            this.fieldLabel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLabel.Location = new System.Drawing.Point(95, 138);
            this.fieldLabel.Name = "fieldLabel";
            this.fieldLabel.Size = new System.Drawing.Size(65, 15);
            this.fieldLabel.TabIndex = 59;
            this.fieldLabel.Text = "ID Number";
            // 
            // subTitle
            // 
            this.subTitle.AutoSize = true;
            this.subTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subTitle.Location = new System.Drawing.Point(95, 102);
            this.subTitle.Name = "subTitle";
            this.subTitle.Size = new System.Drawing.Size(351, 16);
            this.subTitle.TabIndex = 58;
            this.subTitle.Text = "Enter Card Number of Credit Card to Delete and Click Delete:";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(111, 46);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(244, 24);
            this.title.TabIndex = 57;
            this.title.Text = "Delete Credit Card Screen";
            // 
            // cancelbtn
            // 
            this.cancelbtn.Location = new System.Drawing.Point(340, 185);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(96, 23);
            this.cancelbtn.TabIndex = 56;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // deleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 259);
            this.Controls.Add(this.cardToDelete);
            this.Controls.Add(this.applybtn);
            this.Controls.Add(this.fieldLabel);
            this.Controls.Add(this.subTitle);
            this.Controls.Add(this.title);
            this.Controls.Add(this.cancelbtn);
            this.Name = "deleteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Credit Card";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cardToDelete;
        private System.Windows.Forms.Button applybtn;
        private System.Windows.Forms.Label fieldLabel;
        private System.Windows.Forms.Label subTitle;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button cancelbtn;
    }
}