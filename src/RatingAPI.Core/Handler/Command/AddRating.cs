using MediatR;
using OneOf;
using RatingAPI.Core.Abstraction;
using RatingAPI.Core.Domain;
using RatingAPI.Core.Domain.Error;

namespace RatingAPI.Core.Handler.Command;

public static class AddRating
{
    public class Request : IRequest<OneOf<Response, ValidationError, InternalError>>
    {
        public string TitleId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public Request()
        {
        }
        
        public Request(string titleId, int rating, string? comment)
        {
            TitleId = titleId;
            Rating = rating;
            Comment = comment;
        }
    }

    public class Response
    {
        public string Id { get; set; }

        public Response()
        {
        }
        
        public Response(string id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Request, OneOf<Response, ValidationError, InternalError>>
    {
        private readonly IRatingsRepository _repository;
        private readonly ITextAnalyzer _textAnalyzer;

        public Handler(IRatingsRepository repository,
            ITextAnalyzer textAnalyzer)
        {
            _repository = repository;
            _textAnalyzer = textAnalyzer;
        }

        public async Task<OneOf<Response, ValidationError, InternalError>> Handle(Request request,
            CancellationToken cancellationToken)
        {
            try
            {
                var isTextRestricted = _textAnalyzer.DoesTextContainRestrictedContent(request.Comment);
                if (isTextRestricted)
                {
                    return new ValidationError("Text contains restricted content");
                }

                var entityId = Guid.NewGuid().ToString();
                var entity = new RatingEntity(entityId, request.TitleId, request.Rating, request.Comment);

                await _repository.AddRating(entity);
                return new Response(entityId);
            }
            catch (Exception)
            {
                return new InternalError();
            }
        }
    }
}