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
        const char separ = ';';

        string comPorte = "";
        int velocPorta = 0;
        byte gradiMax = 0;
        bool openFileExplorer = true;

        SettingsForm s = new SettingsForm(); //form delle impostazioni

        SerialPortReader serialReader;
        DataManager dataMan;
        FileManager fileMan;

        private void Form1_Load(object sender, EventArgs e) {
            CheckFileAndFolder();
            LeggiImpostazioni();
            InizializzaOggetti();
            serialReader.Start();
        }

        private void button1_Click(object sender, EventArgs e) { //settings
            s.Show();
        }

        private void CheckFileAndFolder() {
            if (!File.Exists(PublicData.ConfigFileName)) {
                RicreaFileConf();
            }
            if (!Directory.Exists(PublicData.OutpDirectory)) {
                Directory.CreateDirectory(PublicData.OutpDirectory);
            }
        }

        public void StartMeasure(string mess) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValvue.Enabled = textBoxNameValvue.Enabled = false;
                    StampaSuRich(Color.Black, DateTime.Now, mess);
                }));
                return;
            }
        }

        public void StopMeasure(string message) {
            if (InvokeRequired) { //se non metto questa parte non funziona. DA CHIEDERE
                this.Invoke(new MethodInvoker(delegate {
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
                                Arguments = $"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.OutpDirectory}"
                            };
                            Process.Start(psi); // Opens the folder using file explorer in Windows
                        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                            Process.Start("xdg-open", $"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.OutpDirectory}"); // Opens the folder using default file manager in Linux
                        }
                    }
                }));
                return;
            }
        }

        private void textBoxNameValvue_TextChanged(object sender, EventArgs e) {
            PublicData.InfoValve.NomeValvola = textBoxNameValvue.Text;
        }

        private void textBoxModelValvue_TextChanged(object sender, EventArgs e) {
            PublicData.InfoValve.ModelloValvola = textBoxModelValvue.Text;
        }

        private void RicreaFileConf() {//ricreo il file delle configurazioni con dei valori di default
            using (StreamWriter sw = new StreamWriter(PublicData.ConfigFileName)) {
                sw.Write($"COM3;9600;100;true");
            }
        }
        private void LeggiImpostazioni() {
            using (StreamReader sr = new StreamReader(PublicData.ConfigFileName)) {
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
            fileMan = new FileManager($"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.OutpDirectory}", PublicData.TemplateFileName, separ, campi);
            serialReader = new SerialPortReader(comPorte, velocPorta, separ, 2, gradiMax, dataMan, fileMan);
        }
        private void RipristinaCampi() {
            textBoxModelValvue.Enabled = textBoxNameValvue.Enabled = true;
            textBoxModelValvue.Text = textBoxNameValvue.Text = "";
            textBoxNameValvue.Focus();
        }
    }
}
