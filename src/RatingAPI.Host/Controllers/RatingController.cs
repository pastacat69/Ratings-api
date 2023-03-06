using MediatR;
using Microsoft.AspNetCore.Mvc;
using RatingAPI.Contract.Request;
using RatingAPI.Contract.Response;
using RatingAPI.Core.Domain;
using RatingAPI.Core.Handler.Command;
using RatingAPI.Core.Handler.Query;

namespace RatingAPI.Host.Controllers;

[ApiController]
public class RatingController : ControllerBase
{
    private readonly IMediator _mediator;

    public RatingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("rating/get")]
    public async Task<IActionResult> HttpGetRatings([FromBody] GetRatingsRequest request, CancellationToken token)
        => await ExecuteWithCancellationSupport(async () =>
        {
            var domainRequest = new GetRatings.Request(request.TitleId);
            var domainResponse = await _mediator.Send(domainRequest, token);

            if (!domainResponse.RatingEntities.Any())
            {
                return NotFound();
            }
            
            var apiResponse = new GetRatingsResponse(Map(domainResponse.RatingEntities));

            return Ok(apiResponse);
        });

    [HttpPost("rating/add")]
    public async Task<IActionResult> HttpAddRating([FromBody] AddRatingRequest request)
    {
        var domainRequest = new AddRating.Request(request.TitleId, request.Rating, request.Comment);
        var domainResponse = await _mediator.Send(domainRequest);

        return domainResponse.Match<IActionResult>(
            success => Ok(success.Id),
            validationError => BadRequest(validationError.Message),
            internalError => StatusCode(500));
    }

    [HttpGet("rating/get-average")]
    public async Task<IActionResult> HttpGetAverageRatings()
    {
        var domainRequest = new GetAverageRatings.Request();
        var domainResponse = await _mediator.Send(domainRequest);

        var apiResponse = new GetAverageRatingsResponse(MapAverage(domainResponse.AverageRatingEntities));
        return Ok(apiResponse);
    }

    private async Task<IActionResult> ExecuteWithCancellationSupport(Func<Task<IActionResult>> operation)
    {
        try
        {
            return await operation.Invoke();
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Request was cancelled.");
            return BadRequest();
        }
    }

    private IEnumerable<RatingObject> Map(IEnumerable<RatingEntity> entities) =>
        entities.Select(entity => new RatingObject(entity.Id, entity.TitleId, entity.Rating, entity.Comment));
    private IEnumerable<AverageRatingsObject> MapAverage(IEnumerable<AverageRatingObject> entities) =>
        entities.Select(entity => new AverageRatingsObject(entity.TitleId, entity.Avarage));
}