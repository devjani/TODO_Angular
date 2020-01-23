using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDB.Data.Generic
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database();
        IMongoClient Client();
        TResult RunCommand<TResult>(string command);
        TResult RunCommand<TResult>(string command, ReadPreference readpreference);
        BsonValue RunScript(string command, CancellationToken cancellationToken);
        Task<BsonValue> RunScriptAsync(string command, CancellationToken cancellationToken);
    }
}
