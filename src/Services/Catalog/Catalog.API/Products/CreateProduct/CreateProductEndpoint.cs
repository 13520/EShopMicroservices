namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", 
                async (CreateProductRequest request, ISender sender) =>
            {
                // Maps request DTO to command object
                var command = request.Adapt<CreateProductCommand>();
                // Goes through MediatR pipeline
                var result = await sender.Send(command);
                // Maps result to response DTO
                var response = result.Adapt<CreateProductResponse>();
                // Returns Created response with location header
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
