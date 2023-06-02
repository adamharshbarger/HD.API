using HD.API.Services;

namespace HD.API
{
    public static class ProductAPI
    {
        public static void RegisterProductAPI(this WebApplication app)
        {
            app.MapGet("/GetProductByID/{productId}", GetProductByID);
        }

        public static async Task<IResult> GetProductByID(int productId, IRequestService product, IConfiguration configuration)
        {
            string? storeId = configuration["SearchSettings:StoreID"];
            string? zipCode = configuration["SearchSettings:ZipCode"];
            string? query = configuration["SearchSettings:Query"];

            HttpResponseMessage response = product
                                            .AddPayload(productId, storeId, zipCode, query)
                                            .SendAsync()
                                            .Result;

            var message = await response.Content.ReadAsStringAsync();

            return Results.Ok(message);
        }
    }
}
