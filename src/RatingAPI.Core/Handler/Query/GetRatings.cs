using MediatR;
using RatingAPI.Core.Abstraction;
using RatingAPI.Core.Domain;

namespace RatingAPI.Core.Handler.Query;

public static class GetRatings
{
    public class Request : IRequest<Response>
    {
        public string TitleId { get; set; }

        public Request()
        {
        }

        public Request(string titleId)
        {
            TitleId = titleId;
        }
    }

    public class Response
    {
        public IEnumerable<RatingEntity> RatingEntities { get; set; }

        public Response()
        {
        }
        
        public Response(IEnumerable<RatingEntity> ratingEntities)
        {
            RatingEntities = ratingEntities;
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
            // validate request

            await Task.Delay(3000, cancellationToken);
            var results = await _repository.GetRatings(request.TitleId, cancellationToken);

            // additional logic
            
            return new Response(results);
        }

        private Task DownloadFile()
        {
            // download
            
            return Task.CompletedTask;
        }
    }
}