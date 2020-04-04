namespace AutoMarket.Models
{
    public class AutoMarketDatabaseSettings : IAutoMarketDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAutoMarketDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}