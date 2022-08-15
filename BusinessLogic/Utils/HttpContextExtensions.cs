using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public static class HttpContextExtensions
    {
        public async static Task InsertHeaderPaginationParams<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double qty = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalRecords", qty.ToString());
        }
    }
}
