using Newtonsoft.Json;

namespace HydrogenOMB {
    internal class ValveInstance {
        private string _id, _jobNumber;
        private ValveModel _valveModel;


        [JsonProperty("_id")]
        public string Id {
            get => _id;
            private set => PublicData.InsertIfObjValid(ref _id, value, "Id");
        }

        [JsonProperty("job_number")]
        public string JobNumber {
            get => _jobNumber;
            private set => PublicData.InsertIfObjValid(ref _jobNumber, value, "Job Number");
        }

        [JsonProperty("valve_model")]
        public ValveModel ValveModel {
            get => _valveModel;
            private set => PublicData.InsertIfObjValid(ref _valveModel, value, "Valve model");
        }
    }
}
