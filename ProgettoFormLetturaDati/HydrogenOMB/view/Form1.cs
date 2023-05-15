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
        string[] campi = new string[] { "DELTA", "TIME", "ANGLE", "TRIMMER" };
        const string configurationFileName = "settings.conf", directoryName = "File";
        const char separ = ';';

        Settings s = new Settings(configurationFileName, directoryName);

        SerialPortReader serialReader;
        DataManager dataMan;
        FileManager fileMan;

        private void Form1_Load(object sender, EventArgs e) {
            CheckFileAndFolder();

            string comPorte = "";
            byte gradiMax = 0;

            var p = new FileStream(configurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (BinaryReader reader = new BinaryReader(p)) {
                comPorte = reader.ReadString();
                gradiMax = reader.ReadByte();
            }
            p.Close();

            timer1.Stop();

            stopBut.Enabled = false;
            timer1.Enabled = false;
            checkOpenExplorer.Checked = true;

            dataGridView1.Columns.Add("delta", campi[0]);
            dataGridView1.Columns.Add("timer", campi[1]);
            dataGridView1.Columns.Add("ang", campi[2]);
            dataGridView1.Columns.Add("tr2", campi[3]);

            dataMan = new DataManager(this, separ, 1);
            fileMan = new FileManager(separ, $"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}", campi);
            serialReader = new SerialPortReader(comPorte, separ, 1, gradiMax, dataMan, fileMan);

            serialReader.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            TimeSpan deltaTempo = DateTime.Now - oraInizio;
            timerLab.Text = $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}";
        }

        private void stopBut_Click(object sender, EventArgs e) {/*termina*/
            /*timer1.Stop();
            serialReader.Stop();

            stopBut.Enabled = false;
            startBut.Enabled = true;

            if (checkOpenExplorer.Checked) {
                Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}");
            }*/
        }

        private void startBut_Click(object sender, EventArgs e) { /*inizia*/
            /*dataGridView1.Rows.Clear();
            oraInizio = DateTime.Now;
            timer1.Start();
            serialReader.Start();

            stopBut.Enabled = true;
            startBut.Enabled = false;*/
        }

        public void PrintRow(int rowIndex, string[] fields) {
            if (InvokeRequired) { // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate {
                    dataGridView1.Rows.Insert(rowIndex, fields);
                }));
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e) {/*settings*/
            s.Show();
        }

        private void CheckFileAndFolder() {
            if (!File.Exists(configurationFileName)) {
                using (StreamWriter sw = new StreamWriter(configurationFileName)) {
                    sw.WriteLine("COM3");
                }
            }
            if (!Directory.Exists(directoryName)) {
                Directory.CreateDirectory(directoryName);
            }
        }

        public void StartMeasure() {
            dataGridView1.Rows.Clear();
            oraInizio = DateTime.Now;
            timer1.Start();

            stopBut.Enabled = true;
            startBut.Enabled = false;
        }

        public void StopMeasure() {
            timer1.Stop();

            stopBut.Enabled = false;
            startBut.Enabled = true;

            if (checkOpenExplorer.Checked) {
                Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}");
            }
        }
    }
}
