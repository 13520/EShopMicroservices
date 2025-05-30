﻿
using Catalog.API.Products.UpdateProducts;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id) : ICommand<DeleteProductResponse>;

    public record DeleteProductResponse(bool IsSuccess);

    internal class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{id}", async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteProductCommand(id));
                    var response = result.Adapt<DeleteProductResponse>();
                    return Results.Ok(response);
                }
                )
                .WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");

        }
    }
}
