namespace Admin.Contract.Models.HttpRequestResponse
{
    public class ResponseDataEnvelop<T> : ReponseMessageEnvelop
    {
        public T? Data { get; set; }
    }
}
