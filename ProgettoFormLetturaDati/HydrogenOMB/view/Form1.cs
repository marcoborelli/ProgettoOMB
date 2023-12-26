using System;
using System.Windows.Forms;
using System.Drawing;


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        DataManager dataMan;

        private void Form1_Load(object sender, EventArgs e) {
            PublicData.Init();
            Settings.Init();
            InizializzaOggetti();
        }

        private void buttonSettings_Click(object sender, EventArgs e) { //settings
            SettingsForm sForm = new SettingsForm(); //form delle impostazioni
            sForm.Show();
        }

        public void SetStateOfValveDataInput(bool enabled) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValve.Enabled = textBoxNameValve.Enabled = enabled;
                }));
                return;
            }
        }

        public void PrintOn(Color col, DateTime ora, string mess) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    richTextBoxAvvisi.SelectionColor = col;
                    richTextBoxAvvisi.AppendText($"{ora}: {mess}\n");
                }));
                return;
            }
        }

        private void InizializzaOggetti() {
            dataMan = new DataManager(this);
        }

        public void ResetValveFields() {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValve.Text = textBoxNameValve.Text = "";
                    textBoxNameValve.Focus();
                }));
                return;
            }
        }

        public string[] GetValveFields() {
            return new string[] { textBoxNameValve.Text, textBoxModelValve.Text };
        }
    }
}
