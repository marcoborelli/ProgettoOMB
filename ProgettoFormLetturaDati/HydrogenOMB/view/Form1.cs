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
            ApiRequester.Init($"{Settings.Instance.BackendURL}");
            dataMan = new DataManager(this);
        }

        private void buttonSettings_Click(object sender, EventArgs e) { //settings
            SettingsForm sForm = new SettingsForm(); //form delle impostazioni
            sForm.Show();
        }

        public void SetStateOfValveDataInput(bool enabled) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    cbValveInstance.Enabled = enabled;
                }));
                return;
            }
        }

        public void PrintOn(Color col, string mess) {
            DateTime now = DateTime.Now;
            this.Invoke(new MethodInvoker(delegate {
                richTextBoxAvvisi.SelectionColor = col;
                richTextBoxAvvisi.AppendText($"{now.TimeOfDay.Hours}:{now.TimeOfDay.Minutes}:{now.TimeOfDay.Seconds}: {mess}\n");
            }));
        }

        public void ResetValveIdField() {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    cbValveInstance.SelectedIndex = -1;
                    cbValveInstance.Focus();
                }));
                return;
            }
        }

        public string GetValveId() {
            string text = "";

            this.Invoke(new MethodInvoker(delegate {
                text = cbValveInstance.Text;
            }));

            return text;
        }

        public void SetItemsCombo(string[] items) {
            cbValveInstance.Items.AddRange(items);
        }
    }
}
