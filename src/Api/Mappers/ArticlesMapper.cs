using Realworlddotnet.Api.Models;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Api.Mappers;

public static class ArticlesMapper
{
    public static ArticleResponse MapFromArticleEntity(Article article)
    {
        var tags = article.Tags.Select(tag => tag.Id);
        var author = article.Author;
        var result = new ArticleResponse(
            article.Slug,
            article.Title,
            article.Description,
            article.Body,
            article.CreatedAt,
            article.UpdatedAt,
            tags,
            new Author(
                author.Username,
                author.Image,
                author.Bio,
                false),
            article.Favorited,
            article.FavoritesCount);
        return result;
    }

    public static ArticlesResponse MapFromArticles(ArticlesResponseDto articlesResponseDto)
    {
        var articles = articlesResponseDto.Articles
            .Select(articleEntity => MapFromArticleEntity(articleEntity))
            .ToList();
        return new ArticlesResponse(articles, articlesResponseDto.ArticlesCount);
    }
}
