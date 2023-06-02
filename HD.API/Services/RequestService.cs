using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HD.API.Services
{
    public class RequestService : IRequestService
    {
        private Uri _uri;
        private HttpClient _httpClient;
        private HttpRequestMessage _httpRequestMessage;
        private HttpResponseMessage _httpResponseMessage;

        public RequestService()
        {
            //Setup Request URI
            _uri = new Uri("https://www.homedepot.com/federation-gateway/graphql?opname=productClientOnlyProduct");

            //Create a new HttpClient and configure
            _httpClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            });

            //Instantiate new Request Message
            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _uri);
            //Add Headers to Request Message
            _httpRequestMessage.Headers.Accept.Clear();
            _httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            _httpRequestMessage.Headers.Add("origin", "https://www.homedepot.com");
            _httpRequestMessage.Headers.Add("x-experience-name", "general-merchandise");

            //Instantiate new Response Message
            _httpResponseMessage = new HttpResponseMessage();
        }

        public RequestService AddPayload(int productId, string storeIdString, string zipCodeString, string queryString)
        {
            //Build Request Payload
            StringContent jsonContent = new(JsonSerializer.Serialize(new
            {
                operationName = "productClientOnlyProduct",
                variables = new
                {
                    skipSpecificationGroup = false,
                    skipSubscribeAndSave = false,
                    skipInstallServices = true,
                    skipKPF = false,
                    itemId = productId,
                    storeId = storeIdString,
                    zipCode = zipCodeString
                },
                query = queryString
            }), Encoding.UTF8, "application/json");

            //Attach Payload to Request Message
            _httpRequestMessage.Content = jsonContent;

            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            //Submit Request and return a Response Message
            _httpResponseMessage = await _httpClient.SendAsync(_httpRequestMessage);

            return _httpResponseMessage;
        }
    }
}
