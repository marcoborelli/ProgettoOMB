using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HydrogenOMB {
    public class ValveModel {
        private string _id;
        private object _valveFamily;


        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("gear_model")]
        public string GearModel { get; private set; }

        [JsonProperty("ma_gear")]
        public float MAGear { get; private set; }

        [JsonProperty("_id")]
        public string Id {
            get => _id;
            private set => PublicData.InsertIfObjValid(ref _id, value, "Id");
        }

        [JsonProperty("valve_family")]
        public object ValveFamily {
            get => _valveFamily;
            private set {
                if (value is JObject) {
                    _valveFamily = ((JObject)value).ToObject<ValveFamily>();
                } else {
                    _valveFamily = value.ToString();
                }
            }
        }
    }
}
