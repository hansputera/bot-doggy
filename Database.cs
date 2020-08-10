
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

class Database
{
    string dburl = "";
    
   public async void afkset(string userid, string reason)
    {
       
        var cl = new MongoClient(
            dburl
             );

        var db = cl.GetDatabase("db");
        var collect = db.GetCollection<BsonDocument>("afk");
        if (collect == null)
        {
            await db.CreateCollectionAsync("afk");
            Console.WriteLine("[LOG] : Collection AFK tidak ditemukan, membuat collectionnya.");
            Console.WriteLine("[LOG] : Trying to add data.");

            var dataInput = new BsonDocument
            {
                {
                    "userid", userid
                },
                {
                    "reason", reason
                }
            };

            var collectFetch = db.GetCollection<BsonDocument>("afk");
            collectFetch.InsertOne(dataInput);
            
        } else
        {
            
        }
    }
}
