using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;
using Realworlddotnet.Data.Contexts;
using Realworlddotnet.Data.Services;
using Xunit;

namespace E2e.Tests;

public class DataTests
{
    [Fact(DisplayName = "User management")]
    public async Task UserManagementTest()
    {
        const string username1 = "EvdV";
        const string username2 = "ACM";

        var connectionString = "Filename=:memory:";
        var connection = new SqliteConnection(connectionString);
        connection.Open();

        try
        {
            var contextOptions = new DbContextOptionsBuilder<ConduitContext>()
                .LogTo(Console.WriteLine).EnableSensitiveDataLogging().EnableDetailedErrors()
                .UseSqlite(connection)
                .Options;

            await using (var context = new ConduitContext(contextOptions))
            {
                await context.Database.EnsureCreatedAsync();
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                await repo.AddUserAsync(new User(new NewUserDto(username1, "test1@test.com", "Test1234")));
                await repo.SaveChangesAsync(CancellationToken.None);
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                await repo.AddUserAsync(new User(new NewUserDto(username2, "test2@test.com", "Test1234")));
                await repo.SaveChangesAsync(CancellationToken.None);
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var usr = await repo.GetUserByUsernameAsync(username1, CancellationToken.None);
                usr.Username.Should().Be(username1);
            }
        }
        finally
        {
            connection.Close();
        }
    }

    [Fact(DisplayName = "Follow User")]
    public async Task FollowUserTest()
    {
        const string username1 = "EvdV";
        const string username2 = "ACM";

        var user1 = new User(new NewUserDto(username1, "test1@test.com", "Test1234"));
        var user2 = new User(new NewUserDto(username2, "test2@test.com", "Test1234"));


        var connectionString = "Filename=:memory:";
        var connection = new SqliteConnection(connectionString);
        connection.Open();

        try
        {
            var contextOptions = new DbContextOptionsBuilder<ConduitContext>()
                .LogTo(Console.WriteLine).EnableSensitiveDataLogging().EnableDetailedErrors()
                .UseSqlite(connection)
                .Options;

            await using (var context = new ConduitContext(contextOptions))
            {
                await context.Database.EnsureCreatedAsync();
                context.Users.AddRange(user1, user2);
                await context.SaveChangesAsync(CancellationToken.None);
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                repo.Follow(username1, username2);
                await repo.SaveChangesAsync(CancellationToken.None);
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var isFollowing = await repo.IsFollowingAsync(username1, username2, CancellationToken.None);
                isFollowing.Should().BeTrue();
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                repo.UnFollow(username1, username2);
                await repo.SaveChangesAsync(CancellationToken.None);
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var isFollowing = await repo.IsFollowingAsync(username1, username2, CancellationToken.None);
                isFollowing.Should().BeFalse();
            }
        }
        finally
        {
            connection.Close();
        }
    }

    [Fact(DisplayName = "Article Management")]
    public async Task ArticleManagementTest()
    {
        const string username1 = "EvdV";
        const string username2 = "ACM";

        var user1 = new User(new NewUserDto(username1, "test1@test.com", "Test1234"));
        var user2 = new User(new NewUserDto(username2, "test2@test.com", "Test1234"));

        var article1 = new Article("title1", "description1", "body1");


        var connectionString = "Filename=:memory:";
        var connection = new SqliteConnection(connectionString);
        connection.Open();

        try
        {
            var contextOptions = new DbContextOptionsBuilder<ConduitContext>()
                .LogTo(Console.WriteLine).EnableSensitiveDataLogging().EnableDetailedErrors()
                .UseSqlite(connection)
                .Options;

            await using (var context = new ConduitContext(contextOptions))
            {
                await context.Database.EnsureCreatedAsync();
                context.Users.AddRange(user1, user2);
                await context.SaveChangesAsync(CancellationToken.None);

                var repo = new ConduitRepository(context);
                repo.Follow(username1, username2);
                await repo.SaveChangesAsync(CancellationToken.None);
            }

            string slug;

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var usr1 = await repo.GetUserByUsernameAsync(username1, CancellationToken.None);
                article1.Author = usr1;
                repo.AddArticle(article1);
                await repo.SaveChangesAsync(CancellationToken.None);
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var articles = await repo.GetArticlesAsync(new ArticlesQuery(null, null, null),
                    username1,
                    false,
                    CancellationToken.None);
                articles.ArticlesCount.Should().Be(1);
                slug = articles.Articles.First().Slug;
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var article = await repo.GetArticleBySlugAsync(slug, true, CancellationToken.None);
                article!.Author.Username.Should().Be(username1);
            }

            Comment comment1;

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var article = await repo.GetArticleBySlugAsync(slug, true, CancellationToken.None);
                comment1 = new Comment("commentbody1", username1, article!.Id);
                repo.AddArticleComment(comment1);
                await repo.SaveChangesAsync(CancellationToken.None);
            }


            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var article = await repo.GetArticleBySlugAsync(slug, false, CancellationToken.None);
                var comments = await repo.GetCommentsBySlugAsync(slug, username2, CancellationToken.None);
                article!.Comments = comments;
                article.Comments.Count.Should().Be(1);
                var firstComment = article.Comments.First();
                firstComment.Username.Should().Be(username1);
                firstComment.Author.Followers.Should().HaveCount(1);
                repo.RemoveArticleComment(firstComment);
                await repo.SaveChangesAsync(CancellationToken.None);
            }

            await using (var context = new ConduitContext(contextOptions))
            {
                var repo = new ConduitRepository(context);
                var article = await repo.GetArticleBySlugAsync(slug, false, CancellationToken.None);
                article!.Comments.Count.Should().Be(0);
            }
        }
        finally
        {
            connection.Close();
        }
    }
}
