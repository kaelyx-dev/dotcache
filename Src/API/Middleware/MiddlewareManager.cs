namespace DotCache.API.Middleware
{
    public class MiddlewareManager
    {
        private static readonly List<Type> _registeredMiddlewares = [
            
        ];
        public static void RegisterMiddleware(WebApplication app)
        {
            _registeredMiddlewares.ForEach(Middleware => app.UseMiddleware(Middleware));
        } 
    }
}
