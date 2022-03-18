using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    public class ItemRepository
    {
        private const string collectionName = "items";

        // A collection that holds all the instances
        private readonly IMongoCollection<Item> dbCollection;

        // filter builder to build filters for quering items from mongodb
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemRepository()
        {
            // specify how to connect to mongodb
            var mongoClietn = new MongoClient("mongodb://localhost:27017");
            var database = mongoClietn.GetDatabase("Catalog");
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        // Asynchronous methods that return all the items in the collection. It's read only
        // to prevent modification of any items. filterBuilder.Empty - is used to return all the items with 
        // no additional filters
        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Item entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            FilterDefinition<Item> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);

            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}