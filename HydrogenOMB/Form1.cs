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

        private void Form1_Load(object sender, EventArgs e) {
            DataGridViewColumn timer = new DataGridViewTextBoxColumn();
            timer.HeaderText = "TIMER";
            dataGridView1.Columns.Add(timer);

            DataGridViewColumn tr1 = new DataGridViewTextBoxColumn();
            tr1.HeaderText="TRIMMER 1";
            dataGridView1.Columns.Add(tr1);

            DataGridViewColumn tr2 = new DataGridViewTextBoxColumn();
            tr2.HeaderText = "TRIMMER 2";
            dataGridView1.Columns.Add(tr2);

        }

        private void startBut_Click(object sender, EventArgs e) {
            dataGridView1.Rows.Insert(0, $"{DateTime.Now.Minute}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}", "aaa","bbb");
        }
    }
}
