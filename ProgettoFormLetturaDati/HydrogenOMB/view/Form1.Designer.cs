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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNameValvue = new System.Windows.Forms.TextBox();
            this.textBoxModelValvue = new System.Windows.Forms.TextBox();
            this.richTextBoxAvvisi = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 55);
            this.button1.TabIndex = 4;
            this.button1.Text = "SETT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.textBoxNameValvue.Location = new System.Drawing.Point(188, 8);
            this.textBoxNameValvue.Name = "textBoxNameValvue";
            this.textBoxNameValvue.Size = new System.Drawing.Size(113, 20);
            this.textBoxNameValvue.TabIndex = 0;
            this.textBoxNameValvue.TextChanged += new System.EventHandler(this.textBoxNameValvue_TextChanged);
            // 
            // textBoxModelValvue
            // 
            this.textBoxModelValvue.Location = new System.Drawing.Point(188, 39);
            this.textBoxModelValvue.Name = "textBoxModelValvue";
            this.textBoxModelValvue.Size = new System.Drawing.Size(113, 20);
            this.textBoxModelValvue.TabIndex = 1;
            this.textBoxModelValvue.TextChanged += new System.EventHandler(this.textBoxModelValvue_TextChanged);
            // 
            // richTextBoxAvvisi
            // 
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
            this.Controls.Add(this.textBoxModelValvue);
            this.Controls.Add(this.textBoxNameValvue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "OMB";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNameValvue;
        private System.Windows.Forms.TextBox textBoxModelValvue;
        private System.Windows.Forms.RichTextBox richTextBoxAvvisi;
    }
}

