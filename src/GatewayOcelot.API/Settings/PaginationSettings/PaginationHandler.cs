using MongoDB.Driver;

namespace GatewayOcelot.API.Settings.PaginationSettings;

public static class PaginationHandler
{
    public static async Task<PageList<TEntity>> PaginateAsync<TEntity>(this IFindFluent<TEntity, TEntity> findFluent, PageParameters pageParameters)
        where TEntity : class
    {
        var count = await findFluent.CountDocumentsAsync();
        var entityPaginatedList = await findFluent.Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize).Limit(pageParameters.PageSize).ToListAsync();

        return new PageList<TEntity>(entityPaginatedList, count, pageParameters);
    }
}
