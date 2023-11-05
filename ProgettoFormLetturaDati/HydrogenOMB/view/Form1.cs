using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        //DateTime oraInizio;
        string[] campi = new string[] { "delta", "time", "angle", "pair" };
        const string configurationFileName = "settings.conf", directoryName = "File", templateFileName = "base";
        const char separ = ';';

        string comPorte = "";
        int velocPorta = 0;
        byte gradiMax = 0;
        bool openFileExplorer = true;

        Settings s = new Settings(configurationFileName, directoryName);//form delle impostazioni

        SerialPortReader serialReader;
        DataManager dataMan;
        FileManager fileMan;

        private void Form1_Load(object sender, EventArgs e) {
            CheckFileAndFolder();

            /*timer1.Stop();
            timer1.Enabled = false;

            for (byte i = 0; i < campi.Length; i++) {
                dataGridView1.Columns.Add(campi[i], campi[i].ToUpper());
            }*/

            LeggiImpostazioni();
            InizializzaOggetti();
            serialReader.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            /*TimeSpan deltaTempo = DateTime.Now - oraInizio;
            timerLab.Text = $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}";*/
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

        public void StartMeasure(string mess) {
            //oraInizio = DateTime.Now;
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValvue.Enabled = textBoxNameValvue.Enabled = false;
                    StampaSuRich(Color.Black, DateTime.Now, mess);
                    //timer1.Start();
                    //dataGridView1.Rows.Clear();
                }));
                return;
            }
        }

        public void StopMeasure(string message) {
            if (InvokeRequired) { //se non metto questa parte non funziona. DA CHIEDERE
                this.Invoke(new MethodInvoker(delegate {
                    //timer1.Stop();
                    //MessageBox.Show(message);
                    StampaSuRich(Color.Black, DateTime.Now, message);
                }));
                return;
            }
        }
        public void EndOpen(string message) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    StampaSuRich(Color.Black, DateTime.Now, message);
                }));
                return;
            }
        }
        public void StartWritingExcel(string message) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    StampaSuRich(Color.Black, DateTime.Now, message);
                }));
                return;
            }
        }
        public void StoptWritingExcel(string message) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    StampaSuRich(Color.Green, DateTime.Now, message);

                    RipristinaCampi();//per prepararsi a rifare un'altra misurazione
                    InizializzaOggetti();
                    serialReader.Start();

                    if (openFileExplorer) {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                            ProcessStartInfo psi = new ProcessStartInfo() {
                                FileName = "explorer.exe",
                                Arguments = $"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}"
                            };
                            Process.Start(psi); // Opens the folder using file explorer in Windows
                        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                            Process.Start("xdg-open", $"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}"); // Opens the folder using default file manager in Linux
                        }
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
            using (StreamWriter sw = new StreamWriter(configurationFileName)) {
                sw.Write($"COM3;9600;100;true");
            }
        }
        private void LeggiImpostazioni() {
            using (StreamReader sr = new StreamReader(configurationFileName)) {
                string[] elements = sr.ReadLine().Split(';');
                comPorte = elements[0];
                velocPorta = int.Parse(elements[1]);
                gradiMax = byte.Parse(elements[2]);
                openFileExplorer = bool.Parse(elements[3]);
            }
        }

        private void StampaSuRich(Color col, DateTime ora, string mess) {
            richTextBoxAvvisi.SelectionColor = col;
            richTextBoxAvvisi.AppendText($"{ora}: {mess}\n");
        }
        private void InizializzaOggetti() {
            dataMan = new DataManager(this, separ);
            fileMan = new FileManager($"{AppDomain.CurrentDomain.BaseDirectory}{directoryName}", templateFileName, separ, campi);
            serialReader = new SerialPortReader(comPorte, velocPorta, separ, 2, gradiMax, dataMan, fileMan);
        }
        private void RipristinaCampi() {
            textBoxModelValvue.Enabled = textBoxNameValvue.Enabled = true;
            textBoxModelValvue.Text = textBoxNameValvue.Text = "";
            textBoxNameValvue.Focus();
        }
        public void PrintRow(int rowIndex, List<string> fields) {
            if (InvokeRequired) { // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate {
                    dataGridView1.Rows.Insert(rowIndex, fields.ToArray());
                }));
                return;
            }
        }
    }
}
