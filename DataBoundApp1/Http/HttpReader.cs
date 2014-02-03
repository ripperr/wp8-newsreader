using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataBoundApp1.Http
{
    public static class HttpReader
    {

        public static async Task<string> GetHttpResponse(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("UserAgent", "Windows 8 app client");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);

            if (response.StatusCode == HttpStatusCode.OK)
                return await response.Content.ReadAsStringAsync();
            else
                throw new Exception("Error connecting to " + url +
                    " ! Status: " + response.StatusCode);
        }

    }
}
