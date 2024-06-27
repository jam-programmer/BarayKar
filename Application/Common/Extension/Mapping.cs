using Application.Common.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace Application.Common.Extension
{
    public static class Mapping
    {
        public static async Task<PaginatedList<Model>> MappingedAsync<Entity, Model>
            (this IQueryable<Entity> queryable, int curentPage, int pageSize, int count,TypeAdapterConfig? config=null)
        {
            PaginatedList<Model> model = new();
            int skip = (curentPage - 1) * pageSize;
            var list = await queryable.Skip(skip).Take(pageSize).ToListAsync();

            List<Model> Convertedlist = new();
            if (config == null)
            {
                Convertedlist = list.Adapt<List<Model>>();
            }
            else
            {
                Convertedlist = list.Adapt<List<Model>>(config);
            }
            model.List = Convertedlist;
            model.CurentPage = curentPage;
            model.TotalPage = count;
            model.PageSize = pageSize;
            return model;
        }
    }
}
