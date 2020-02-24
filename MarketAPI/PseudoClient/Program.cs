using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PseudoClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            PseudoHttpClient client = new PseudoHttpClient();
            //await client.Post();
            await client.Get();
        }
    }

    internal class PseudoHttpClient
    {
        private readonly HttpClient _client;

        public PseudoHttpClient()
        {
            /*var authData = string.Format("{0}:{1}", "Vasyan", "parol'");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            */
            _client = new HttpClient();
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            //Console.WriteLine(authHeaderValue);
        }

        public HttpResponseMessage responseMessage { get; set; }

        public async Task Get()
        {
            Uri uri = new Uri(string.Format("https://702dc8f8.ngrok.io/api/Products"));
            responseMessage = await _client.GetAsync(uri);
            Console.WriteLine(responseMessage);

        }

        public async Task Post()
        {
            Uri uri = new Uri(string.Format("https://702dc8f8.ngrok.io/Account/Login"));
            //var response = await _client.GetAsync(uri);
            //Console.WriteLine(response);
            var login = new LoginViewModel();
            login.UserName = "Vasyan";
            login.Password = "parol'";
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            responseMessage = await _client.PostAsync(uri, content) ;
            Console.WriteLine(responseMessage);
            uri = new Uri(string.Format("https://702dc8f8.ngrok.io/api/Products"));
            responseMessage = await _client.GetAsync(uri);
            Console.WriteLine(responseMessage);
        }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}