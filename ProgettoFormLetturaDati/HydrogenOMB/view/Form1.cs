using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.IO.Ports;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        DateTime oraInizio;
        string[] campi = new string[] { "DELTA", "TIME", "TRIMMER 1", "TRIMMER 2" };

        SerialPortReader serialReader;
        DataManager dataMan;
        FileManager fileMan;

        private void Form1_Load(object sender, EventArgs e) {
            string comPorte = "";
            using (StreamReader sr = new StreamReader("settings.conf")) {
                comPorte = sr.ReadLine();
            }
            timer1.Stop();
            stopBut.Enabled = false;
            timer1.Enabled = false;

            dataGridView1.Columns.Add("delta", campi[0]);
            dataGridView1.Columns.Add("timer", campi[1]);
            dataGridView1.Columns.Add("tr1", campi[2]);
            dataGridView1.Columns.Add("tr2", campi[3]);

            dataMan = new DataManager(this);
            fileMan = new FileManager(';', $"{AppDomain.CurrentDomain.BaseDirectory}File", campi);
            serialReader = new SerialPortReader(comPorte, dataMan, fileMan);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            TimeSpan deltaTempo = DateTime.Now - oraInizio;
            timerLab.Text = $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}";
        }

        private void stopBut_Click(object sender, EventArgs e) {/*termina*/
            timer1.Stop();
            serialReader.Stop();

            stopBut.Enabled = false;
            startBut.Enabled = true;

            if (checkOpenExplorer.Checked) {
                Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}File");
            }
        }

        private void startBut_Click(object sender, EventArgs e) { /*inizia*/
            dataGridView1.Rows.Clear();
            oraInizio = DateTime.Now;

            timer1.Start();
            serialReader.Start();

            stopBut.Enabled = true;
            startBut.Enabled = false;
        }

        public void PrintRow(int rowIndex, string[] fields) {
            if (InvokeRequired) { // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate {
                    dataGridView1.Rows.Insert(rowIndex, fields);
                }));
                return;
            }
        }

    }
}
