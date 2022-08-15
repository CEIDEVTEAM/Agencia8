using BusinessLogic.DTOs.Generals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO dto)
        {
            return queryable
                .Skip((dto.Page - 1) * dto.RecordsPerPage)
                .Take(dto.RecordsPerPage);
        }
    }
}
