﻿
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductByCategoryQueryHandler
        (IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByCategoryQuery. Handle called with {@Query}", query);

            var products = await session.Query<Product>()
                .Where(p => p.Categories.Contains(query.Category))
                .ToListAsync(cancellationToken);


            return new GetProductByCategoryResult(products);
        }
    }
}
