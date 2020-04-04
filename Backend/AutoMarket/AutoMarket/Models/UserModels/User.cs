using MongoDB.Bson.Serialization.Attributes;

namespace AutoMarket.Models
{
    public class User
    {
        [BsonId]
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public long Phone { get; set; }

        public string CountryCode { get; set; }

        public string Sex { get; set; }

        public string Address { get; set; }

        public int PinCode { get; set; }

        public int AccessLevel { get; set; }
    }
}
