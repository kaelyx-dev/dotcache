using DotCache.API.Middleware;

namespace DotCache.API.Middleware
{
    public class MiddlewareManager
    {
        private static readonly List<IDotCacheMiddleware> _registeredMiddlewares = [

        ];
        public static void RegisterMiddleware(WebApplication app)
        {
            _registeredMiddlewares.ForEach(Middleware =>
            {
                IDotCacheMiddleware middleware = Middleware;
                app.UseMiddleware((Type)middleware);
            });
        }
    }
}
