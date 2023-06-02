namespace HD.API.Services
{
    public interface IRequestService
    {
        public RequestService AddPayload(int productId, string storeIdString, string zipCodeString, string queryString);
        public Task<HttpResponseMessage> SendAsync();
    }
}
