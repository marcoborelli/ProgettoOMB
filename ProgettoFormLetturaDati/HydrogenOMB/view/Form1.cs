using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
/*using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        DateTime oraInizio;
        string[] campi = new string[] { "delta", "time", "angle", "pair" };
        const string configurationFileName = "settings.conf", directoryName = "File", templateFileName = "base";
        const char separ = ';';

        string comPorte = "";
        byte gradiMax = 0;
        bool openFileExplorer = true;

        Settings s = new Settings(configurationFileName, directoryName);//form delle impostazioni

        SerialPortReader serialReader;
        DataManager dataMan;
        FileManager fileMan;

        private void Form1_Load(object sender, EventArgs e) {
            CheckFileAndFolder();

            timer1.Stop();
            timer1.Enabled = false;

            for (byte i = 0; i < campi.Length; i++) {
                dataGridView1.Columns.Add(campi[i], campi[i].ToUpper());
            }

            LeggiImpostazioni();

            dataMan = new DataManager(this, separ);
            fileMan = new FileManager($"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}", templateFileName, separ, campi);
            serialReader = new SerialPortReader(comPorte, separ, 2, gradiMax, dataMan, fileMan);

            serialReader.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            TimeSpan deltaTempo = DateTime.Now - oraInizio;
            timerLab.Text = $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}";
        }

        public void PrintRow(int rowIndex, List<string> fields) {
            if (InvokeRequired) { // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate {
                    dataGridView1.Rows.Insert(rowIndex, fields.ToArray());
                }));
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e) {//settings
            s.Show();
        }

        private void CheckFileAndFolder() {
            if (!File.Exists(configurationFileName)) {
                RicreaFileConf();
            }
            if (!Directory.Exists(directoryName)) {
                Directory.CreateDirectory(directoryName);
            }
        }

        public void StartMeasure() {
            oraInizio = DateTime.Now;

            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValvue.Enabled = textBoxNameValvue.Enabled = false;
                    timer1.Start();
                    dataGridView1.Rows.Clear();
                }));
                return;
            }
        }

        public void StopMeasure(string message) {
            if (InvokeRequired) { //se non metto questa parte non funziona. DA CHIEDERE
                this.Invoke(new MethodInvoker(delegate {
                    timer1.Stop();
                    MessageBox.Show(message);
                    if (openFileExplorer) {
                        Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}");
                    }
                }));
                return;
            }
        }

        private void textBoxNameValvue_TextChanged(object sender, EventArgs e) {
            DatiValvola.NomeValvola = textBoxNameValvue.Text;
        }

        private void textBoxModelValvue_TextChanged(object sender, EventArgs e) {
            DatiValvola.ModelloValvola = textBoxModelValvue.Text;
        }

        private void RicreaFileConf() {//ricreo il file delle configurazioni con dei valori di default
            var p = new FileStream(configurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            p.Seek(0, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(p)) {
                writer.Write("COM3");
                writer.Write(100);
                writer.Write(true);
            }
            p.Close();
        }
        private void LeggiImpostazioni() {
            var p = new FileStream(configurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            p.Seek(0, SeekOrigin.Begin);
            using (BinaryReader reader = new BinaryReader(p)) {
                comPorte = reader.ReadString();
                gradiMax = reader.ReadByte();
                openFileExplorer = reader.ReadBoolean();
            }
            p.Close();
        }
    }
}
