using System;

namespace HydrogenOMB {
    public class OMBRecord {
        private string _delta;
        private string _time;
        private short _angle;
        private float _pair;
        private const byte InitialParamaterNumber = 2;


        public string Delta {
            get => _delta;
            private set => PublicData.InsertIfObjValid(ref _delta, value, "Delta");
        }

        public string Time {
            get => _time;
            private set => PublicData.InsertIfObjValid(ref _time, value, "Time");
        }

        public short Angle {
            get => _angle;
            private set => PublicData.InsertIfObjValid(ref _angle, value, "Angle");
        }

        public float Pair {
            get => _pair;
            private set => PublicData.InsertIfObjValid(ref _pair, value, "Pair");
        }


        public OMBRecord(string row, char separator, DateTime before) {
            string[] fields = row.Split(separator); //in caso ci siano più campi
            if (fields.Length != InitialParamaterNumber) {
                CampiDefault(ref fields);
            }

            if (int.Parse(fields[0]) > Settings.Instance.MaxDegrees) { //nel caso in cui i gradi (presenti all'indice 0 per il momento) siano maggiori del limite imposto via software
                return;
            }

            Angle = short.Parse(fields[0]);
            Pair = float.Parse(fields[1]);

            DateTime now = DateTime.Now;
            Time = $"{now.Hour}:{now.Minute}:{now.Second}:{now.Millisecond}";

            if (before != DateTime.MinValue) { //se e' uguale e' perche' e' al primo
                TimeSpan delta = now - before;
                Delta = $"{delta.Minutes}:{delta.Seconds}:{delta.Milliseconds}";
            } else {
                Delta = "-";
            }
        }


        private void CampiDefault(ref string[] fields) {
            fields = new string[InitialParamaterNumber];
            for (byte i = 0; i < InitialParamaterNumber; i++) {
                fields[i] = "-";
            }
        }
    }
}
