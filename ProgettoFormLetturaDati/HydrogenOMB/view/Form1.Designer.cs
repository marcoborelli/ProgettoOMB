namespace HydrogenOMB {
    partial class Form1 {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent() {
            this.buttonSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNameValve = new System.Windows.Forms.TextBox();
            this.textBoxModelValve = new System.Windows.Forms.TextBox();
            this.richTextBoxAvvisi = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(12, 12);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(55, 55);
            this.buttonSettings.TabIndex = 4;
            this.buttonSettings.Text = "SETT";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(79, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "VALVE NAME:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "VALVE MODEL:";
            // 
            // textBoxNameValvue
            // 
            this.textBoxNameValve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNameValve.Location = new System.Drawing.Point(188, 8);
            this.textBoxNameValve.Name = "textBoxNameValvue";
            this.textBoxNameValve.Size = new System.Drawing.Size(113, 20);
            this.textBoxNameValve.TabIndex = 0;
            // 
            // textBoxModelValvue
            // 
            this.textBoxModelValve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxModelValve.Location = new System.Drawing.Point(188, 39);
            this.textBoxModelValve.Name = "textBoxModelValvue";
            this.textBoxModelValve.Size = new System.Drawing.Size(113, 20);
            this.textBoxModelValve.TabIndex = 1;
            // 
            // richTextBoxAvvisi
            // 
            this.richTextBoxAvvisi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxAvvisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAvvisi.Location = new System.Drawing.Point(12, 73);
            this.richTextBoxAvvisi.Name = "richTextBoxAvvisi";
            this.richTextBoxAvvisi.ReadOnly = true;
            this.richTextBoxAvvisi.Size = new System.Drawing.Size(289, 94);
            this.richTextBoxAvvisi.TabIndex = 6;
            this.richTextBoxAvvisi.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 179);
            this.Controls.Add(this.richTextBoxAvvisi);
            this.Controls.Add(this.textBoxModelValve);
            this.Controls.Add(this.textBoxNameValve);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSettings);
            this.MinimumSize = new System.Drawing.Size(334, 218);
            this.Name = "Form1";
            this.Text = "OMB";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNameValve;
        private System.Windows.Forms.TextBox textBoxModelValve;
        private System.Windows.Forms.RichTextBox richTextBoxAvvisi;
    }
}

