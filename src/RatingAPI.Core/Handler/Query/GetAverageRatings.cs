using MediatR;
using RatingAPI.Core.Abstraction;
using RatingAPI.Core.Domain;

namespace RatingAPI.Core.Handler.Query
{
    public static class GetAverageRatings
    {
        public class Request : IRequest<Response>
        {
            public Request()
            { }
        }

        public class Response
        {
            public IEnumerable<AverageRatingObject> AverageRatingEntities { get; set; }
            public Response(IEnumerable<AverageRatingObject> ratingEntities)
            {
                AverageRatingEntities = ratingEntities;
            }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRatingsRepository _repository;

            public Handler(IRatingsRepository repository)
            {
                _repository = repository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var results = await _repository.GetAvarageRatings();
                return new Response(results);
            }
        }
    }
}
