using RatingAPI.Core.Domain;

namespace RatingAPI.Core.Abstraction;

public interface IRatingsRepository
{
    Task<IEnumerable<RatingEntity>> GetRatings(string titleId, CancellationToken token);
    Task AddRating(RatingEntity entity);
    public Task<IEnumerable<AverageRatingObject>> GetAvarageRatings();
}