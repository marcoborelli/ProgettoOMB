using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HydrogenOMB {
    public partial class Settings : Form {
        string configurationFileName, directoryName;
        bool first = true;
        const string testoLabel = "MAX GRADI: ";
        public Settings(string configurationFileNamee, string directoryNamee) {
            InitializeComponent();
            ConfigurationFileName = configurationFileNamee;
            DirectoryName = directoryNamee;
        }
        private void Settings_Load(object sender, EventArgs e) {
            comboBoxPorta.DropDownStyle = ComboBoxStyle.DropDownList;
            for (byte i = 1; i < 7; i++) {
                comboBoxPorta.Items.Add($"COM{i}");
            }

            var p = new FileStream(ConfigurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (BinaryReader reader = new BinaryReader(p)) {
                comboBoxPorta.Text = reader.ReadString();
                trackBarGradi.Value = (reader.ReadByte() - 90) / 5;
                checkOpenExplorer.Checked = reader.ReadBoolean();
            }
            p.Close();
            trackBarGradi_Scroll(sender, e);
            first = false;
        }

        /*properties*/
        public string ConfigurationFileName {
            get {
                return configurationFileName;
            }
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    configurationFileName = value;
                } else {
                    throw new Exception("Inserire un nome del file di config valido");
                }
            }
        }
        public string DirectoryName {
            get {
                return directoryName;
            }
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    directoryName = value;
                } else {
                    throw new Exception("Inserire un nome della directory valido");
                }
            }
        }
        /*fine properties*/

        private void trackBarGradi_Scroll(object sender, EventArgs e) {
            label2.Text = $"{testoLabel}{(trackBarGradi.Value * 5) + 90}";

            if (first) {
                return;
            }

            var p = new FileStream(ConfigurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            p.Seek(comboBoxPorta.Text.Length+1, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(p)) {
                writer.Write((trackBarGradi.Value * 5) + 90);
            }
            p.Close();
        }

        private void checkOpenExplorer_CheckedChanged(object sender, EventArgs e) {
            if (first) {
                return;
            }

            var p = new FileStream(ConfigurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            p.Seek((comboBoxPorta.Text.Length + 1)+sizeof(byte), SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(p)) {
                writer.Write(checkOpenExplorer.Checked);
            }
            p.Close();
        }

        private void comboBoxPorta_SelectedIndexChanged(object sender, EventArgs e) {
            if (first) {
                return;
            }

            var p = new FileStream(ConfigurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (BinaryWriter writer = new BinaryWriter(p)) {
                writer.Write(comboBoxPorta.Text);
            }
            p.Close();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
