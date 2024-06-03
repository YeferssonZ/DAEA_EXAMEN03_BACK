using MongoDB.Driver;

namespace backend.Data
{
    public class MongoDBRepository
    {
        public MongoClient client;

        public IMongoDatabase db;

        public MongoDBRepository()
        {
            client = new MongoClient("mongodb+srv://zaph:lhnnNWCOoC1jqLbQ@cluster0.awt8ftp.mongodb.net/");
            //client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("Recomendacion");

        }
    }
}