using MvcCorePersonajesSeriesApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcCorePersonajesSeriesApi.Services
{
    public class ServiceSeries
    {
        private string url;
        private MediaTypeWithQualityHeaderValue Header;
        public ServiceSeries(string url)
        {
            this.url = url;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series/series/";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/series/personajes/";
            List<Personaje> series = await this.CallApiAsync<List<Personaje>>(request);
            return series;
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/series/findserie/" + idserie;
            Serie serie= await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task<Personaje> FindpersonajeAsync(int idpersonaje)
        {
            string request = "/api/series/findpersonaje/" + idpersonaje;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task<List<Personaje>> PersonajesSeriesAsync(int idserie)
        {
            string request = "/api/series/personajesserie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task CambiarPersonajeSerie(int idpersonaje, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series/cambiarpersonajeserie/" + idpersonaje + "/" + idserie;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
    }
}
