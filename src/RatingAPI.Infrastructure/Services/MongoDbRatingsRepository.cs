using MongoDB.Driver;
using RatingAPI.Core.Abstraction;
using RatingAPI.Core.Domain;

namespace RatingAPI.Infrastructure.Services;

public class MongoDbRatingsRepository : IRatingsRepository
{
    private readonly IMongoCollection<RatingEntity> _mongoCollection;

    public MongoDbRatingsRepository(IMongoCollection<RatingEntity> mongoCollection)
    {
        _mongoCollection = mongoCollection;
    }

    public async Task<IEnumerable<RatingEntity>> GetRatings(string titleId, CancellationToken token)
    {
        return await _mongoCollection.Find(i => i.TitleId == titleId).ToListAsync(token);
    }

    public async Task AddRating(RatingEntity entity)
    {
        await _mongoCollection.InsertOneAsync(entity);
    }

    public async Task<IEnumerable<AverageRatingObject>> GetAvarageRatings()
    {
        return await _mongoCollection.Aggregate()
             .Group(rating => rating.TitleId, groupRating => new AverageRatingObject
             {
                 TitleId = groupRating.Key,
                 Avarage = groupRating.Average(rating => rating.Rating)
             }).ToListAsync();
    }
}