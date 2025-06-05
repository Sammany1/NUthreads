using MongoDB.Driver;
using NUthreads.Infrastructure.Repositories.Common;

public class RevokedTokenRepository : BaseRepository<RevokedToken>, IRevokedTokenRepository
{
    private readonly IMongoCollection<RevokedToken> _collection;

    public RevokedTokenRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<RevokedToken>("RevokedTokens");
    }

    public async Task AddAsync(RevokedToken token)
    {
        await _collection.InsertOneAsync(token);
    }

    public async Task<bool> IsTokenRevokedAsync(string token)
    {
        var filter = Builders<RevokedToken>.Filter.Eq(t => t.Token, token);
        var result = await _collection.Find(filter).FirstOrDefaultAsync();
        return result != null;
    }
}
