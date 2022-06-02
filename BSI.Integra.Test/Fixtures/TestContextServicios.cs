
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Test.Fixtures
{
    public class TestContextServicios: IDisposable
    {
        public readonly HttpClient client;
        private readonly TestServer server;

        public TestContextServicios()
        {
            AutoMapper.Mapper.Reset();
            server = new TestServer(new WebHostBuilder().UseStartup<Servicios.Startup>());
            client = server.CreateClient();
            //Client.BaseAddress = new Uri("http://localhost:5000");
        }

        public IServiceProvider ServiceProvider => server.Host.Services;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                server.Dispose();
                client.Dispose();
            }
        }

        public Task<HttpResponseMessage> Get (string url)
        {
            return client.GetAsync(url);
        }
        public Task<HttpResponseMessage> PostDiccionario(string url , Dictionary<string, string> datos) {
           
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            foreach (var item in datos)
            {
                var dato = new KeyValuePair<string, string>(item.Key, item.Value);
                data.Add(dato);
            }
            var content = new FormUrlEncodedContent(data);

            return client.PostAsync(url, content);
        }
        public Task<HttpResponseMessage> PostJson(string url, object obj)
        {
           
            var content = AsJson(obj);
            return client.PostAsync(url, content);
        }
        public StringContent  AsJson(object o)
        {
            return  new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
        }
       
    }

}

