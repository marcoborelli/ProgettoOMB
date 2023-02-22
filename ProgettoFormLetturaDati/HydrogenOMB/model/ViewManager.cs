using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydrogenOMB {
    public class ViewManager {
        private DataGridView _gridView;
        private DateTime _startTime, _now, _oldTime;
        private TimeSpan _deltaTime;
        private bool _first;
        private Form _associatedForm;
        public ViewManager(DataGridView GridVieww, Form form) {
            this.GridView = GridVieww;
            AssociatedForm = form;
            First = true;
        }

        /*properties*/
        public DataGridView GridView {
            get {
                return _gridView;
            }
            set {
                if (value != null) {
                    _gridView = value;
                } else {
                    throw new Exception("Inserire un GridView valida");
                }
            }
        }
        public DateTime StartTime {
            get {
                return _startTime;
            }
            private set {
                _startTime = value;
            }
        }
        public DateTime Now {
            get {
                return _now;
            }
            set {
                _now = value;
            }
        }
        private DateTime OldTime {
            get {
                return _oldTime;
            }
            set {
                _oldTime = value;
            }
        }
        private TimeSpan DeltaTime {
            get {
                return _deltaTime;
            }
            set {
                _deltaTime = value;
            }
        }
        private bool First {
            get {
                return _first;
            }
            set {
                _first = value;
            }
        }
        private Form AssociatedForm {
            get {
                return _associatedForm;
            }
            set {
                if (value != null) {
                    _associatedForm = value;
                } else {
                    throw new Exception("Inserire una form valida da associare");
                }
            }
        }
        /*fine properties*/

        public void PrintOnForm(string row) {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Now = DateTime.Now;
            string[] fields = row.Split(';');

            if (fields.Length != 2) {
                fields = new string[] { "-", "-" };
            }

            if (AssociatedForm.InvokeRequired) { // after we've done all the processing, 
                AssociatedForm.Invoke(new MethodInvoker(delegate {
                    string MinSecMilTime = $"{Now.Minute}:{Now.Second}:{Now.Millisecond}";
                    if (First) {
                        this.GridView.Rows.Insert(0, "", MinSecMilTime, fields[0], fields[1]);
                    } else {
                        DeltaTime = Now - OldTime;
                        this.GridView.Rows.Insert(0, $"{DeltaTime.Minutes}:{DeltaTime.Seconds}:{DeltaTime.Milliseconds}", MinSecMilTime, fields[0], fields[1]);
                    }
                    OldTime = Now;
                    First = false;
                }));
                return;
            }
        }
    }
}
