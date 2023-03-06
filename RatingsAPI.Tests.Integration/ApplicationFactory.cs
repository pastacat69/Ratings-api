using System;
using System.Threading;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RatingAPI.Core.Abstraction;

namespace RatingsAPI.Tests.Integration;

public class Host : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // builder.ConfigureServices(svc =>
        // {
        //     var mockedRepository = A.Fake<IRatingsRepository>();
        //     A.CallTo(() => mockedRepository.GetRatings(A.Dummy<string>(), A.Dummy<CancellationToken>()))
        //         .Throws<Exception>();
        //
        //     svc.AddSingleton<IRatingsRepository>(mockedRepository);
        // });
        
        base.ConfigureWebHost(builder);
    }
}