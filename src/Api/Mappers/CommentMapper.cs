using CommentEntity = Realworlddotnet.Core.Entities.Comment;
using CommentModel = Realworlddotnet.Api.Models.Comment;

namespace Realworlddotnet.Api.Mappers;

public static class CommentMapper
{
    public static CommentModel MapFromCommentEntity(CommentEntity commentEntity)
    {
        var author = new Author(
            commentEntity.Author.Username,
            commentEntity.Author.Image,
            commentEntity.Author.Bio,
            commentEntity.Author.Followers.Any());
        return new CommentModel(commentEntity.Id,
            commentEntity.CreatedAt,
            commentEntity.UpdatedAt,
            commentEntity.Body,
            author);
    }
}
