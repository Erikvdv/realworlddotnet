namespace Realworlddotnet.Core.Services;

public class ArticlesHandler(IConduitRepository repository) : IArticlesHandler
{
    public async Task<Article> CreateArticleAsync(
        NewArticleDto newArticle, string username, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByUsernameAsync(username, cancellationToken);
        var tags = await repository.UpsertTagsAsync(newArticle.TagList, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        var article = new Article(
                newArticle.Title,
                newArticle.Description,
                newArticle.Body
            ) { Author = user, Tags = tags.ToList() };

        repository.AddArticle(article);
        await repository.SaveChangesAsync(cancellationToken);
        return article;
    }

    public async Task<Article> UpdateArticleAsync(
        ArticleUpdateDto update, string slug, string username, CancellationToken cancellationToken)
    {
        var article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "Article not found"
            });
        }

        if (username != article.Author.Username)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 403, Detail = $"{username} is not the author"
            });
        }

        article.UpdateArticle(update);
        await repository.SaveChangesAsync(cancellationToken);
        return article;
    }

    public async Task DeleteArticleAsync(string slug, string username, CancellationToken cancellationToken)
    {
        var article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "Article not found"
            });
        }

        if (username != article.Author.Username)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 403, Detail = $"{username} is not the author"
            });
        }

        repository.DeleteArticle(article);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, string username, bool isFeed,
        CancellationToken cancellationToken)
    {
        return repository.GetArticlesAsync(query, username, false, cancellationToken);
    }


    public async Task<Article> GetArticleBySlugAsync(string slug, string username, CancellationToken cancellationToken)
    {
        var article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }

        var comments = await repository.GetCommentsBySlugAsync(slug, username, cancellationToken);
        article.Comments = comments;

        return article;
    }

    public async Task<Comment> AddCommentAsync(string slug, string username, CommentDto commentDto,
        CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByUsernameAsync(username, cancellationToken);
        var article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }

        var comment = new Comment(commentDto.body, user.Username, article.Id);
        repository.AddArticleComment(comment);

        await repository.SaveChangesAsync(cancellationToken);
        return comment;
    }

    public async Task RemoveCommentAsync(string slug, int commentId, string username,
        CancellationToken cancellationToken)
    {
        var article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "Article not found"
            });
        }

        var comments = await repository.GetCommentsBySlugAsync(slug, username, cancellationToken);
        var comment = comments.FirstOrDefault(x => x.Id == commentId);

        if (comment == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "Comment not found"
            });
        }

        if (comment.Author.Username != username)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422, Detail = "User does not own Article"
            });
        }

        comments.Remove(comment);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Comment>> GetCommentsAsync(string slug, string? username,
        CancellationToken cancellationToken)
    {
        var comments = await repository.GetCommentsBySlugAsync(slug, username, cancellationToken);
        return comments;
    }

    public async Task<Article> AddFavoriteAsync(string slug, string username, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByUsernameAsync(username, cancellationToken);
        var article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }

        var articleFavorite = await repository.GetArticleFavoriteAsync(user.Username, article.Id);

        if (articleFavorite is null)
        {
            repository.AddArticleFavorite(new ArticleFavorite(user.Username, article.Id));
            await repository.SaveChangesAsync(cancellationToken);
        }

        article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);
        return article!;
    }

    public async Task<Article> DeleteFavorite(string slug, string username, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByUsernameAsync(username, cancellationToken);
        var article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);

        if (article == null)
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Article not found",
                Errors = { new KeyValuePair<string, string[]>("slug", new[] { slug }) }
            });
        }

        var articleFavorite = await repository.GetArticleFavoriteAsync(user.Username, article.Id);

        if (articleFavorite is not null)
        {
            repository.RemoveArticleFavorite(articleFavorite);
            await repository.SaveChangesAsync(cancellationToken);
        }

        article = await repository.GetArticleBySlugAsync(slug, false, cancellationToken);
        return article!;
    }

    public async Task<string[]> GetTags(CancellationToken cancellationToken)
    {
        var tags = await repository.GetTagsAsync(cancellationToken);
        return tags.Select(x => x.Id).ToArray();
    }
    
    
}
