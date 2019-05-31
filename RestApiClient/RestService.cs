using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestApiClient.Configuration;

namespace RestApiClient
{
    public class RestService : IRestService
    {
        private static string _urlApi;
        private readonly string _token;

        public RestService(string urlApi)
        {
            _urlApi = urlApi;
        }
        
        public RestService(string urlApi, string token)
        {
            _urlApi = urlApi;
            _token = token;
        }

        public TResult Login<TResult>(string uri, string userName, string password)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_urlApi);

                    var content = "grant_type=password&username=" + userName + "&password=" + password + "";

                    var httpContent = new StringContent(content, Encoding.UTF8);
                    
                    var response = httpClient.PostAsync(uri, httpContent).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                        throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
                    }

                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TResult>(result);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        public TResult GetData<TResult>(string url)
        {
            try
            {

                var httpClient = new HttpClient { BaseAddress = new Uri(_urlApi) };

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TResult>(result);
                }


                var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<TResult> GetDataAsync<TResult>(string url)
        {
            try
            {

                var httpClient = new HttpClient { BaseAddress = new Uri(_urlApi) };

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                
                var response = await httpClient.GetAsync(url);

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TResult>(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        public bool PostData<TData>(string url, TData data)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = PostData(url, data, httpClient);

                    if (response.IsSuccessStatusCode) return true;

                    var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                    throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public TResult PostData<TData, TResult>(string url, TData data)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = PostData(url, data, httpClient);

                    if (!response.IsSuccessStatusCode)
                    {

                        var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                        throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
                    }


                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<TResult>(result);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }
        public void PostData(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_urlApi);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    var httpContent = new StringContent("", Encoding.UTF8, "application/json");
                    var response = httpClient.PostAsync(url, httpContent).Result;

                    if (response.IsSuccessStatusCode) return;


                    var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                    throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }



        public TResult PutData<TData, TResult>(string url, TData data)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = PutData(url, data, httpClient);

                    if (!response.IsSuccessStatusCode)
                    {
                        var message = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result).Message;
                        throw new InvalidOperationException(message);
                    }

                    var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                    throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }


        public void PutData<TData>(string url, TData data)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = PutData(url, data, httpClient);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                        throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        public TResult PutData<TResult>(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_urlApi);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    var postBody = JsonConvert.SerializeObject("");
                    var httpContent = new StringContent(postBody, Encoding.UTF8, "application/json");
                    var response = httpClient.PutAsync(url, httpContent).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = JsonConvert.DeserializeObject<HttpError>(response.Content.ReadAsStringAsync().Result);
                        throw new InvalidOperationException(error.Message + " " + error.ExceptionMessage);
                    }

                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TResult>(result);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        public bool DeleteData(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_urlApi);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    var response = httpClient.DeleteAsync(url).Result;
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }


        private HttpResponseMessage PostData<TData>(string url, TData data, HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(_urlApi);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var postBody = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            var httpContent = new StringContent(postBody, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(url, httpContent).Result;
            return response;
        }

        private HttpResponseMessage PutData<TData>(string url, TData data, HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(_urlApi);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var postBody = JsonConvert.SerializeObject(data, new JsonSerializerConfiguration());

            var httpContent = new StringContent(postBody, Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync(url, httpContent).Result;
            return response;
        }
    }


}
