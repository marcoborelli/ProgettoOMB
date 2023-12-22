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

        string[] campi = new string[] { "delta", "time", "angle", "pair" };
        const char separ = ';';

        SerialPortReader serialReader;
        DataManager dataMan;
        FileManager fileMan;

        private void Form1_Load(object sender, EventArgs e) {
            CheckFileAndFolder();
            Settings.Init();
            InizializzaOggetti();
            serialReader.StartPort();
        }

        private void buttonSettings_Click(object sender, EventArgs e) { //settings
            SettingsForm sForm = new SettingsForm(); //form delle impostazioni
            sForm.Show();
        }

        private void CheckFileAndFolder() {
            if (!Directory.Exists(PublicData.OutpDirectory)) {
                Directory.CreateDirectory(PublicData.OutpDirectory);
            }
        }

        public void StartMeasure(string mess) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValve.Enabled = textBoxNameValve.Enabled = false;
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
                    serialReader.StartPort();

                    if (Settings.Instance.OpenInExplorer) {
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
            PublicData.InfoValve.NomeValvola = textBoxNameValve.Text;
        }

        private void textBoxModelValvue_TextChanged(object sender, EventArgs e) {
            PublicData.InfoValve.ModelloValvola = textBoxModelValve.Text;
        }


        private void StampaSuRich(Color col, DateTime ora, string mess) {
            richTextBoxAvvisi.SelectionColor = col;
            richTextBoxAvvisi.AppendText($"{ora}: {mess}\n");
        }
        private void InizializzaOggetti() {
            dataMan = new DataManager(this, separ);
            fileMan = new FileManager($"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.OutpDirectory}", PublicData.TemplateFileName, separ, campi);
            serialReader = new SerialPortReader(Settings.Instance.PortName, Settings.Instance.PortBaud, separ, 2, Settings.Instance.MaxDegrees, dataMan, fileMan);
        }
        private void RipristinaCampi() {
            textBoxModelValve.Enabled = textBoxNameValve.Enabled = true;
            textBoxModelValve.Text = textBoxNameValve.Text = "";
            textBoxNameValve.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
        }
    }
}
