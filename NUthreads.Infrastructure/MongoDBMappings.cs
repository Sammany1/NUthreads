using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using NUthreads.Domain.Models;

public static class MongoDbMappings
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<AuditableEntity>(cm =>
        {
            cm.MapMember(c => c.CreatedAt)
                .SetSerializer(new DateTimeSerializer(BsonType.DateTime)); 
            cm.MapMember(c => c.UpdatedAt)
              .SetSerializer(new DateTimeSerializer(BsonType.DateTime)); 
            cm.SetIsRootClass(true); 
        });
        BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
        {
            cm.MapIdMember(c => c.Id);
            cm.SetIsRootClass(true); 
        });

        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.AutoMap(); 
        });

        BsonClassMap.RegisterClassMap<Reply>(cm =>
        {
            cm.AutoMap(); 
        });
    }
}