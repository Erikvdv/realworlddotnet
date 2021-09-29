using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using realworlddotnet.Api.Mappers;
using realworlddotnet.Api.Models;
using realworlddotnet.Core.Dto;
using realworlddotnet.Core.Services.Interfaces;

namespace realworlddotnet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesInteractor _articlesInteractor;

        public ArticlesController(IArticlesInteractor articlesInteractor)
        {
            _articlesInteractor = articlesInteractor;
        }

        [HttpPost]
        [Authorize]
        public async Task<ArticleEnvelope<ArticleResponse>> CreateAsync(RequestEnvelope<ArticleEnvelope<NewArticleDto>> request,
            CancellationToken cancellationToken)
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var article =
                await _articlesInteractor.CreateArticleAsync(request.Body.Article, username, cancellationToken);
            var result = ArticlesMapper.MapFromArticleEntity(article);
            return new ArticleEnvelope<ArticleResponse>(result);
        }

        [HttpGet]
        public async Task<ActionResult<ArticlesResponse>> GetAsync([FromQuery] ArticlesQuery query, CancellationToken cancellationToken)
        {
            var response = await _articlesInteractor.GetArticlesAsync(query, cancellationToken);
            var result = ArticlesMapper.MapFromArticles(response);
            return result;
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPut("{slug}")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> UpdateBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpDelete("{slug}")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> DeleteBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost("{slug}/favorite")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> FavoriteBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpDelete("{slug}/favorite")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> UnFavoriteBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpGet("feed")]
        public async Task<ActionResult<ArticlesResponseDto>> GetFeedAsync([FromQuery] FeedQuery query)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost("{slug}/comments")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> AddCommentAsync(string slug)
        {
            throw new NotImplementedException();
        }


        [HttpGet("{slug}/comments")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetCommentAsync(string slug)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpDelete("{slug}/comments/{commentId}")]
        public async Task<ActionResult<ArticleEnvelope<ArticleResponse>>> GetCommentAsync(string slug, string commentId)
        {
            throw new NotImplementedException();
        }
    }
}