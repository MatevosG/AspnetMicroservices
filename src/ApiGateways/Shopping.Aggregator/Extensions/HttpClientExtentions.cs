using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Extensions
{
    public static class HttpClientExtentions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Somthing went wrong calling the API");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
