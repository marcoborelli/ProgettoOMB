using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HydrogenOMB {
    public class ApiRequester {
        private HttpClient HttpClient { get; set; }
        private static ApiRequester _instance;

        public static ApiRequester Instance {
            get => _instance;
            private set {
                if (value != null && Instance == null) {
                    _instance = value;
                }
            }
        }


        private ApiRequester(string baseAddress) {
            HttpClient = new HttpClient {
                BaseAddress = new Uri(baseAddress)
            };
        }


        public static void Init(string baseAddress) {
            Instance = new ApiRequester(baseAddress);
        }



        public async Task<bool> GetInstanceData(string instanceId) {
            try {
                HttpResponseMessage response = await HttpClient.GetAsync($"/api/instances/get/{instanceId}");
                return response.IsSuccessStatusCode; //TODO: verificare cosa succede se il server non e' raggiungibile
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<string[]> GetAllInstances() {
            try {
                HttpResponseMessage response = await HttpClient.GetAsync($"/api/instances/all?showTests=false&showModel=false");

                if (response.IsSuccessStatusCode) {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    List<ValveInstance> instances = JsonConvert.DeserializeObject<List<ValveInstance>>(jsonString);
                    string[] ids = instances.Select(i => i.Id).ToArray();
                    return ids;
                }

                return null;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddNewTest(Test test) {
            try {
                var requestContent = new StringContent(JsonConvert.SerializeObject(test), Encoding.Unicode, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync($"/api/tests/add", requestContent);
                return response.IsSuccessStatusCode;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
