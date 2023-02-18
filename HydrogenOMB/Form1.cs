using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        int righeGrid = 0;
        DateTime tempo = DateTime.Now, tempoOld = DateTime.Now, oraInizio;
        TimeSpan deltaTempo;
        private void Form1_Load(object sender, EventArgs e) {
            timer1.Enabled = false;
            DataGridViewColumn delta = new DataGridViewTextBoxColumn();
            delta.HeaderText = "DELTA";
            dataGridView1.Columns.Add(delta);

            DataGridViewColumn timer = new DataGridViewTextBoxColumn();
            timer.HeaderText = "TIMER";
            dataGridView1.Columns.Add(timer);

            DataGridViewColumn tr1 = new DataGridViewTextBoxColumn();
            tr1.HeaderText = "TRIMMER 1";
            dataGridView1.Columns.Add(tr1);

            DataGridViewColumn tr2 = new DataGridViewTextBoxColumn();
            tr2.HeaderText = "TRIMMER 2";
            dataGridView1.Columns.Add(tr2);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            deltaTempo = DateTime.Now - oraInizio;
            timerLab.Text = $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}";
        }

        private void startBut_Click(object sender, EventArgs e) {
            timer1.Enabled = true;
            tempo = DateTime.Now;
            if (righeGrid == 0) {
                oraInizio = tempo;
                dataGridView1.Rows.Insert(righeGrid, "", $"{tempo.Minute}:{tempo.Second}:{tempo.Millisecond}", "TR1", "TR2");
            } else {
                deltaTempo = tempo - tempoOld;
                dataGridView1.Rows.Insert(righeGrid, $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}", $"{tempo.Minute}:{tempo.Second}:{tempo.Millisecond}", "TR1", "TR2");
            }
            tempoOld = tempo;
            righeGrid++;
        }
    }
}
