
using System.Text.Json;
using RedeSocial.API.Models;

namespace RedeSocial.API.Extensions {
    public static class HttpExtensions {

        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header) {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Add("pagination", JsonSerializer.Serialize(header, jsonOptions));
            response.Headers.Add("Acess-Control-Expose-Header", "Pagination");
        }
    }
}
