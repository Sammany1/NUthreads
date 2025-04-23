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
            .SetSerializer(new DateTimeSerializer(BsonType.DateTime));
      cm.MapMember(c => c.UpdatedAt)
            .SetSerializer(new DateTimeSerializer(BsonType.DateTime));
      cm.SetIsRootClass(true);
    });

    // Map User (inherits from BaseEntity)
    BsonClassMap.RegisterClassMap<User>(cm =>
    {
      cm.AutoMap();
    });

    BsonClassMap.RegisterClassMap<Reply>(cm =>
    {
      cm.AutoMap();
    });

    BsonClassMap.RegisterClassMap<Post>(cm =>
    {
      cm.AutoMap();
    });
  }
}