using Comment = Realworlddotnet.Api.Models.Comment;

namespace Realworlddotnet.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticlesHandler _articlesHandler;

    public ArticlesController(IArticlesHandler articlesHandler)
    {
        _articlesHandler = articlesHandler;
    }

    private string Username => User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpPost]
    [Authorize]
    public async Task<ArticleEnvelope<ArticleResponse>> CreateAsync(
        RequestEnvelope<ArticleEnvelope<NewArticleDto>> request,
        CancellationToken cancellationToken)
    {
        var article =
            await _articlesHandler.CreateArticleAsync(request.Body.Article, Username, cancellationToken);
        var result = ArticlesMapper.MapFromArticleEntity(article);
        return new ArticleEnvelope<ArticleResponse>(result);
    }

    [HttpGet]
    public async Task<ActionResult<ArticlesResponse>> GetAsync(
        [FromQuery] ArticlesQuery query, CancellationToken cancellationToken)
    {
        var response = await _articlesHandler.GetArticlesAsync(query, Username, false, cancellationToken);
        var result = ArticlesMapper.MapFromArticles(response);
        return result;
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetBySlugAsync(string slug,
        CancellationToken cancellationToken)
    {
        var article = await _articlesHandler.GetArticleBySlugAsync(slug, Username, cancellationToken);
        var result = ArticlesMapper.MapFromArticleEntity(article);
        return new ArticleEnvelope<ArticleResponse>(result);
    }

    [Authorize]
    [HttpPut("{slug}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> UpdateBySlugAsync(string slug,
        RequestEnvelope<ArticleEnvelope<ArticleUpdateDto>> request, CancellationToken cancellationToken)
    {
        var article = await _articlesHandler.UpdateArticleAsync(request.Body.Article,
            slug,
            Username,
            cancellationToken);

        var result = ArticlesMapper.MapFromArticleEntity(article);
        return new ArticleEnvelope<ArticleResponse>(result);
    }

    [Authorize]
    [HttpDelete("{slug}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> DeleteBySlugAsync(string slug,
        CancellationToken cancellationToken)
    {
        await _articlesHandler.DeleteArticleAsync(slug, Username, cancellationToken);
        return Ok();
    }

    [Authorize]
    [HttpPost("{slug}/favorite")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> FavoriteBySlugAsync(string slug,
        CancellationToken cancellationToken)
    {
        var article = await _articlesHandler.AddFavoriteAsync(slug, Username, cancellationToken);
        var result = ArticlesMapper.MapFromArticleEntity(article);
        return new ArticleEnvelope<ArticleResponse>(result);
    }

    [Authorize]
    [HttpDelete("{slug}/favorite")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> UnFavoriteBySlugAsync(string slug,
        CancellationToken cancellationToken)
    {
        var article = await _articlesHandler.DeleteFavorite(slug, Username, cancellationToken);
        var result = ArticlesMapper.MapFromArticleEntity(article);
        return new ArticleEnvelope<ArticleResponse>(result);
    }

    [Authorize]
    [HttpGet("feed")]
    public async Task<ActionResult<ArticlesResponse>> GetFeedAsync([FromQuery] FeedQuery query,
        CancellationToken cancellationToken)
    {
        var articlesQuery = new ArticlesQuery(null, null, null, query.Limit, query.Offset);
        var response = await _articlesHandler.GetArticlesAsync(articlesQuery, Username, false, cancellationToken);
        var result = ArticlesMapper.MapFromArticles(response);
        return result;
    }

    [Authorize]
    [HttpPost("{slug}/comments")]
    public async Task<CommentEnvelope<Comment>> AddCommentAsync(string slug,
        RequestEnvelope<CommentEnvelope<CommentDto>> request, CancellationToken cancellationToken)
    {
        var result = await _articlesHandler.AddCommentAsync(slug, Username, request.Body.comment, cancellationToken);
        var comment = CommentMapper.MapFromCommentEntity(result);
        return new CommentEnvelope<Comment>(comment);
    }

    [HttpGet("{slug}/comments")]
    public async Task<ActionResult<CommentsEnvelope<List<Comment>>>> GetCommentAsync(string slug,
        CancellationToken cancellationToken)
    {
        var result = await _articlesHandler.GetCommentsAsync(slug, Username, cancellationToken);
        var comments = result.Select(CommentMapper.MapFromCommentEntity);
        return new CommentsEnvelope<List<Comment>>(comments.ToList());
    }

    [Authorize]
    [HttpDelete("{slug}/comments/{commentId}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetCommentAsync(string slug, int commentId,
        CancellationToken cancellationToken)
    {
        await _articlesHandler.RemoveCommentAsync(slug, commentId, Username, cancellationToken);
        return Ok();
    }
}
