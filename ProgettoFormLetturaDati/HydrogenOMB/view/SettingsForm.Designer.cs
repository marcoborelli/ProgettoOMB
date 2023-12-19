namespace HydrogenOMB {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBoxPorta = new System.Windows.Forms.ComboBox();
            this.trackBarGradi = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkOpenExplorer = new System.Windows.Forms.CheckBox();
            this.comboBoxVelocita = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGradi)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPorta
            // 
            this.comboBoxPorta.FormattingEnabled = true;
            this.comboBoxPorta.Location = new System.Drawing.Point(124, 12);
            this.comboBoxPorta.Name = "comboBoxPorta";
            this.comboBoxPorta.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPorta.TabIndex = 0;
            this.comboBoxPorta.SelectedIndexChanged += new System.EventHandler(this.comboBoxPorta_SelectedIndexChanged);
            // 
            // trackBarGradi
            // 
            this.trackBarGradi.LargeChange = 1;
            this.trackBarGradi.Location = new System.Drawing.Point(124, 85);
            this.trackBarGradi.Maximum = 6;
            this.trackBarGradi.Name = "trackBarGradi";
            this.trackBarGradi.Size = new System.Drawing.Size(121, 45);
            this.trackBarGradi.TabIndex = 1;
            this.trackBarGradi.TickFrequency = 2;
            this.trackBarGradi.Scroll += new System.EventHandler(this.trackBarGradi_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "PORTA COM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MAX GRADI:";
            // 
            // checkOpenExplorer
            // 
            this.checkOpenExplorer.AutoSize = true;
            this.checkOpenExplorer.Location = new System.Drawing.Point(15, 136);
            this.checkOpenExplorer.Name = "checkOpenExplorer";
            this.checkOpenExplorer.Size = new System.Drawing.Size(134, 17);
            this.checkOpenExplorer.TabIndex = 2;
            this.checkOpenExplorer.Text = "OPEN IN EXPLORER ";
            this.checkOpenExplorer.UseVisualStyleBackColor = true;
            this.checkOpenExplorer.CheckedChanged += new System.EventHandler(this.checkOpenExplorer_CheckedChanged);
            // 
            // comboBoxVelocita
            // 
            this.comboBoxVelocita.FormattingEnabled = true;
            this.comboBoxVelocita.Location = new System.Drawing.Point(124, 48);
            this.comboBoxVelocita.Name = "comboBoxVelocita";
            this.comboBoxVelocita.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVelocita.TabIndex = 4;
            this.comboBoxVelocita.SelectedIndexChanged += new System.EventHandler(this.comboBoxVelocita_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "VELOCITA\':";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 167);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxVelocita);
            this.Controls.Add(this.checkOpenExplorer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarGradi);
            this.Controls.Add(this.comboBoxPorta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGradi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPorta;
        private System.Windows.Forms.TrackBar trackBarGradi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkOpenExplorer;
        private System.Windows.Forms.ComboBox comboBoxVelocita;
        private System.Windows.Forms.Label label3;
    }
}