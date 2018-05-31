namespace Core.App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Core.App.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {
        public async Task<Response> CheckConnection(string urlServidor = "")
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Verifique su conexión a internet"
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(urlServidor);
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "El servidor no se encuentra disponible"
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok"
            };
                    
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string IdUsuario)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}/{1}", servicePrefix, controller) + (IdUsuario == "" ? "" : ("?IdUsuario="+IdUsuario));
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
