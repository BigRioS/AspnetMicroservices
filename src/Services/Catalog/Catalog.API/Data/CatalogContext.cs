using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var constring = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var collectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
            var client = new MongoClient(constring);
            var database = client.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(collectionName);

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
