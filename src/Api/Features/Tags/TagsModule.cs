namespace Realworlddotnet.Api.Features.Tags;

public class TagsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/tags",
                async (ITagsHandler articlesHandler) =>
                {
                    var tags = await articlesHandler.GetTagsAsync(new CancellationToken());
                    return new TagsEnvelope<string[]>(tags);
                })
            .Produces<TagsEnvelope<string[]>>()
            .WithTags("Tags")
            .WithName("GetTags")
            .IncludeInOpenApi();
    }
}
