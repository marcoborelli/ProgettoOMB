using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HydrogenOMB {
    public class Test {
        private object _instanceId;
        private List<OMBRecord> _data;


        [JsonProperty("instance_id")]
        public object InstanceId {
            get => _instanceId;
            private set {
                if (value is JObject) {
                    _instanceId = ((JObject)value).ToObject<ValveInstance>();
                } else {
                    _instanceId = value.ToString();
                }
            }
        }


        [JsonProperty("data")]
        public List<OMBRecord> Data {
            get => _data;
            set => PublicData.InsertIfObjValid(ref _data, value, "Data (OmbRecord)");
        }


        public Test(object instanceId, List<OMBRecord> data) {
            InstanceId = instanceId;
            Data = data;
        }
    }
}
