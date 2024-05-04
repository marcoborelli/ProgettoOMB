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
            this.richTextBoxAvvisi = new System.Windows.Forms.RichTextBox();
            this.cbValveInstance = new System.Windows.Forms.ComboBox();
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
            this.label1.Location = new System.Drawing.Point(71, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "MODEL SERIAL NUMBER:";
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
            this.richTextBoxAvvisi.Size = new System.Drawing.Size(412, 165);
            this.richTextBoxAvvisi.TabIndex = 6;
            this.richTextBoxAvvisi.Text = "";
            // 
            // cbValveInstance
            // 
            this.cbValveInstance.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbValveInstance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValveInstance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValveInstance.FormattingEnabled = true;
            this.cbValveInstance.Location = new System.Drawing.Point(73, 46);
            this.cbValveInstance.Name = "cbValveInstance";
            this.cbValveInstance.Size = new System.Drawing.Size(351, 21);
            this.cbValveInstance.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 250);
            this.Controls.Add(this.cbValveInstance);
            this.Controls.Add(this.richTextBoxAvvisi);
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
        private System.Windows.Forms.RichTextBox richTextBoxAvvisi;
        private System.Windows.Forms.ComboBox cbValveInstance;
    }
}

