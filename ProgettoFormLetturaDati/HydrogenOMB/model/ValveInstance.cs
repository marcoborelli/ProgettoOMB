using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HydrogenOMB {
    internal class ValveInstance {
        private string _id, _jobNumber;
        private object _valveModel;


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
        public object ValveModel {
            get => _valveModel;
            private set {
                if (value is JObject) {
                    _valveModel = ((JObject)value).ToObject<ValveModel>();
                } else {
                    _valveModel = value.ToString();
                }
            }
        }
    }
}
