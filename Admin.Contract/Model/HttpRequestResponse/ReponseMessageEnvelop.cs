using System.Net;

namespace Admin.Contract.Models.HttpRequestResponse
{
    public class ReponseMessageEnvelop
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
