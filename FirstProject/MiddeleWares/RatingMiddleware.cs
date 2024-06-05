using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Service;
using System.Threading.Tasks;

namespace FirstProject.middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;


        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public Task Invoke(HttpContext httpContext,IRatingService _ratingService)
        {

            Rating rating = new Rating
            {
                Host=httpContext.Request.Host.Host,
                Method=httpContext.Request.Method,
                Path=httpContext.Request.Path,
                Referer = httpContext.Request.Headers["Referer"],
                UserAgent = httpContext.Request.Headers["User-Agent"],
                RecordDate=DateTime.Now
            };
            var newRating= _ratingService.AddRating(rating);
            if (newRating==0)
            {
                return null;
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
