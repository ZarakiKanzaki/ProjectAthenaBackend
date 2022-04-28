using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AthenaBackend.Common.DependecyInjection.Middlewares
{
    public class UseWriteDbContextMiddleware<T> where T : DbContext
    {
        private readonly RequestDelegate next;

        public UseWriteDbContextMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext httpContext, T dbContext)
        {
            await next(httpContext);

            await dbContext.SaveChangesAsync();
        }
    }
}
