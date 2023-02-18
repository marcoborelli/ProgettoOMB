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
            this.components = new System.ComponentModel.Container();
            this.startBut = new System.Windows.Forms.Button();
            this.stopBut = new System.Windows.Forms.Button();
            this.timerLab = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // startBut
            // 
            this.startBut.Location = new System.Drawing.Point(12, 12);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(178, 54);
            this.startBut.TabIndex = 0;
            this.startBut.Text = "INIZIA";
            this.startBut.UseVisualStyleBackColor = true;
            this.startBut.Click += new System.EventHandler(this.startBut_Click);
            // 
            // stopBut
            // 
            this.stopBut.Location = new System.Drawing.Point(12, 136);
            this.stopBut.Name = "stopBut";
            this.stopBut.Size = new System.Drawing.Size(178, 54);
            this.stopBut.TabIndex = 1;
            this.stopBut.Text = "TERMINA";
            this.stopBut.UseVisualStyleBackColor = true;
            // 
            // timerLab
            // 
            this.timerLab.AutoSize = true;
            this.timerLab.Location = new System.Drawing.Point(12, 95);
            this.timerLab.Name = "timerLab";
            this.timerLab.Size = new System.Drawing.Size(41, 13);
            this.timerLab.TabIndex = 2;
            this.timerLab.Text = "TIMER";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(196, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(420, 441);
            this.dataGridView1.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 481);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.timerLab);
            this.Controls.Add(this.stopBut);
            this.Controls.Add(this.startBut);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBut;
        private System.Windows.Forms.Button stopBut;
        private System.Windows.Forms.Label timerLab;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
    }
}

