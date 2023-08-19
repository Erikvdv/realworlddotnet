using Realworlddotnet.Core.Dto;

namespace Realworlddotnet.Api.Features.Articles;

public class ArticlesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var unAuthorizedGroup = app.MapGroup("articles")
            .WithTags("Articles")
            .IncludeInOpenApi();

        var authorizedGroup = app.MapGroup("articles")
            .RequireAuthorization()
            .WithTags("Articles")
            .IncludeInOpenApi();

        unAuthorizedGroup.MapGet("/",
                async Task<Ok<ArticlesResponse>>
                ([AsParameters] ArticlesQuery query, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var response = await articlesHandler.GetArticlesAsync(query, user, false, new CancellationToken());
                    var result = ArticlesMapper.MapFromArticles(response);
                    return TypedResults.Ok(result);
                })
            .WithName("GetArticles");

        unAuthorizedGroup.MapGet("/{slug}",
                async Task<Ok<ArticleEnvelope<ArticleResponse>>>
                (string slug, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var article = await articlesHandler.GetArticleBySlugAsync(slug, user, new CancellationToken());
                    var result = ArticlesMapper.MapFromArticleEntity(article);
                    return TypedResults.Ok(new ArticleEnvelope<ArticleResponse>(result));
                })
            .WithName("GetArticle");

        unAuthorizedGroup.MapGet("/{slug}/comments",
                async Task<Ok<CommentsEnvelope<List<Comment>>>>
                (string slug, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result = await articlesHandler.GetCommentsAsync(slug, user, new CancellationToken());
                    var comments = result.Select(CommentMapper.MapFromCommentEntity);
                    return TypedResults.Ok(new CommentsEnvelope<List<Comment>>(comments.ToList()));
                })
            .WithName("GetArticleComments");


        authorizedGroup.MapPost("/",
                async Task<Ok<ArticleEnvelope<ArticleResponse>>>
                ([FromBody] ArticleEnvelope<NewArticleDto> request, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var article =
                        await articlesHandler.CreateArticleAsync(request.Article, user!, new CancellationToken());
                    var result = ArticlesMapper.MapFromArticleEntity(article);
                    return TypedResults.Ok(new ArticleEnvelope<ArticleResponse>(result));
                })
            .WithName("CreateArticle");

        authorizedGroup.MapPut("/{slug}",
                async Task<Ok<ArticleEnvelope<ArticleResponse>>>
                (string slug, [FromBody] ArticleEnvelope<ArticleUpdateDto> request, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var article =
                        await articlesHandler.UpdateArticleAsync(request.Article, slug, user!, new CancellationToken());
                    var result = ArticlesMapper.MapFromArticleEntity(article);
                    return TypedResults.Ok(new ArticleEnvelope<ArticleResponse>(result));
                })
            .WithName("UpdateArticle");

        authorizedGroup.MapDelete("/{slug}",
                async Task<Ok>
                (string slug, [FromBody] ArticleEnvelope<ArticleUpdateDto> request, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    await articlesHandler.DeleteArticleAsync(slug, user!, new CancellationToken());
                    return TypedResults.Ok();
                })
            .WithName("DeleteArticle");

        authorizedGroup.MapPost("/{slug}/favorite",
                async Task<Ok<ArticleEnvelope<ArticleResponse>>>
                (string slug, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var article = await articlesHandler.AddFavoriteAsync(slug, user!, new CancellationToken());
                    var result = ArticlesMapper.MapFromArticleEntity(article);
                    return TypedResults.Ok(new ArticleEnvelope<ArticleResponse>(result));
                })
            .WithName("FavoriteBySlug");

        authorizedGroup.MapDelete("/{slug}/favorite",
                async Task<Ok<ArticleEnvelope<ArticleResponse>>>
                (string slug, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var article = await articlesHandler.DeleteFavorite(slug, user!, new CancellationToken());
                    var result = ArticlesMapper.MapFromArticleEntity(article);
                    return TypedResults.Ok(new ArticleEnvelope<ArticleResponse>(result));
                })
            .WithName("UnFavoriteBySlug");

        authorizedGroup.MapGet("/feed",
                async Task<Ok<ArticlesResponse>>
                ([AsParameters] FeedQuery query, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var articlesQuery = new ArticlesQuery(null, null, null, query.Limit, query.Offset);
                    var response =
                        await articlesHandler.GetArticlesAsync(articlesQuery, user, false, new CancellationToken());
                    var result = ArticlesMapper.MapFromArticles(response);
                    return TypedResults.Ok(result);
                })
            .WithName("GetFeed");

        authorizedGroup.MapPost("{slug}/comments",
                async Task<Ok<CommentEnvelope<Comment>>>
                (string slug, [FromBody] CommentEnvelope<CommentDto> request, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result =
                        await articlesHandler.AddCommentAsync(slug, user!, request.comment, new CancellationToken());
                    var comment = CommentMapper.MapFromCommentEntity(result);
                    return TypedResults.Ok(new CommentEnvelope<Comment>(comment));
                })
            .WithName("CreateComment");

        authorizedGroup.MapDelete("{slug}/comments/{commentId}",
                async Task<Ok> (string slug, int commentId, IArticlesHandler articlesHandler,
                    ClaimsPrincipal claimsPrincipal) =>
                {
                    var user = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    await articlesHandler.RemoveCommentAsync(slug, commentId, user!, new CancellationToken());
                    return TypedResults.Ok();
                })
            .WithName("DeleteArticleComment");
    }
}
