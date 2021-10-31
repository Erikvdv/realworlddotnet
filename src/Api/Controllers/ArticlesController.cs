using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Api.Mappers;
using Realworlddotnet.Api.Models;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Services.Interfaces;

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

    [HttpPost]
    [Authorize]
    public async Task<ArticleEnvelope<ArticleResponse>> CreateAsync(
        RequestEnvelope<ArticleEnvelope<NewArticleDto>> request,
        CancellationToken cancellationToken)
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var article =
            await _articlesHandler.CreateArticleAsync(request.Body.Article, username, cancellationToken);
        var result = ArticlesMapper.MapFromArticleEntity(article);
        return new ArticleEnvelope<ArticleResponse>(result);
    }

    [HttpGet]
    public async Task<ActionResult<ArticlesResponse>> GetAsync(
        [FromQuery] ArticlesQuery query, CancellationToken cancellationToken)
    {
        var response = await _articlesHandler.GetArticlesAsync(query, cancellationToken);
        var result = ArticlesMapper.MapFromArticles(response);
        return result;
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        var article = await _articlesHandler.GetArticleBySlugAsync(slug, cancellationToken);
        var result = ArticlesMapper.MapFromArticleEntity(article);
        return new ArticleEnvelope<ArticleResponse>(result);
    }

    [Authorize]
    [HttpPut("{slug}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> UpdateBySlugAsync(string slug)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpDelete("{slug}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> DeleteBySlugAsync(string slug)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{slug}/favorite")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> FavoriteBySlugAsync(string slug)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpDelete("{slug}/favorite")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> UnFavoriteBySlugAsync(string slug)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("feed")]
    public async Task<ActionResult<ArticlesResponseDto>> GetFeedAsync([FromQuery] FeedQuery query)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{slug}/comments")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> AddCommentAsync(string slug)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpGet("{slug}/comments")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetCommentAsync(string slug)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpDelete("{slug}/comments/{commentId}")]
    public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetCommentAsync(string slug, string commentId)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
