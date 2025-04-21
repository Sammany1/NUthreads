using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using NUthreads.Domain.Models;

public static class MongoDbMappings
{
    public static void Configure()
    {
        // Map BaseEntity (base class for other entities)
        BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
        {
            cm.MapIdMember(c => c.Id)
              .SetIdGenerator(StringObjectIdGenerator.Instance)
              .SetSerializer(new StringSerializer(BsonType.ObjectId)); 
            cm.MapMember(c => c.CreatedAt)
              .SetSerializer(new DateTimeSerializer(BsonType.DateTime)); // Ensure DateTime is stored correctly
            cm.MapMember(c => c.UpdatedAt)
              .SetSerializer(new DateTimeSerializer(BsonType.DateTime)); // Ensure DateTime is stored correctly
            cm.SetIsRootClass(true); // Indicate that this is a root class in the inheritance hierarchy
        });

        // Map User (inherits from BaseEntity)
        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.AutoMap(); // Automatically map all properties
            // Add any custom mappings for User if needed
        });

        // Map Reply (if it has any special requirements)
        BsonClassMap.RegisterClassMap<Reply>(cm =>
        {
            cm.AutoMap(); // Automatically map all properties
            // Add any custom mappings for Reply if needed
        });
    }
}