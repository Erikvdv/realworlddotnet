using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using realworlddotnet.Api.Models;
using realworlddotnet.Domain.Dto;
using realworlddotnet.Domain.Entities;
using Article = realworlddotnet.Api.Models.Article;

namespace realworlddotnet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateAsync(RequestEnvelope<ArticleEnvelope<NewArticleDto>> request, CancellationToken cancellationToken)
        {

            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult<ArticlesEnvelope<List<Article>>>> GetAsync([FromQuery] ArticlesQuery query)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("{slug}")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> GetBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpPut("{slug}")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> UpdateBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpPut("{slug}")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> DeleteBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpPost("{slug}/favorite")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> FavoriteBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpDelete("{slug}/favorite")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> UnFavoriteBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpGet("feed")]
        public async Task<ActionResult<ArticlesEnvelope<List<Article>>>> GetFeedAsync([FromQuery] FeedQuery query)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpPost("{slug}/comments")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> AddCommentAsync(string slug)
        {
            throw new NotImplementedException();
        }
        
 
        [HttpGet("{slug}/comments")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> GetCommentAsync(string slug)
        {
            throw new NotImplementedException();
        }
        
        [Authorize]
        [HttpDelete("{slug}/comments/{commentId}")]
        public async Task<ActionResult<ArticleEnvelope<Article>>> GetCommentAsync(string slug, string commentId)
        {
            throw new NotImplementedException();
        }

    }
}