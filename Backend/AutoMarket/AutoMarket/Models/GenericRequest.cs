namespace AutoMarket.Models
{

    public class GenericRequest<Request>
    {
        public string SystemToken { get; set; }

        public Request Data { get; set; }
    }
}
