using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Hellang.Middleware.ProblemDetails;
using Moq;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;
using Realworlddotnet.Core.Services;
using Realworlddotnet.Core.Services.Interfaces;
using Xunit;

namespace Unit.Tests.Core.Services;

public class ProfilesHandlerTests
{
    [Fact(DisplayName = "Get Profile")]
    public async Task GetProfileTest()
    {
        const string username1 = "EvdV";
        const string username2 = "AcM";

        var profileUser = new User(new NewUserDto(username1, "email1", "pw1")) { Bio = "Bio1", Image = "image1" };

        var repo = new Mock<IConduitRepository>();
        repo.Setup(x => x.GetUserByUsernameAsync(
            It.Is<string>(username => username == username1),
            It.IsAny<CancellationToken>())).ReturnsAsync(profileUser);

        repo.Setup(x =>
            x.IsFollowingAsync(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var handler = new ProfilesHandler(repo.Object);

        var result1 = await handler.GetAsync(username1, null, CancellationToken.None);
        result1.Following.Should().BeFalse();
        result1.Username.Should().Be(profileUser.Username);
        result1.Bio.Should().Be(profileUser.Bio);
        result1.Image.Should().Be(profileUser.Image);

        var result2 = await handler.GetAsync(username1, username2, CancellationToken.None);
        result2.Following.Should().BeTrue();
        result2.Username.Should().Be(profileUser.Username);
        result2.Bio.Should().Be(profileUser.Bio);
        result2.Image.Should().Be(profileUser.Image);

        var act = () => handler.GetAsync(username2, username1, CancellationToken.None);
        var result3 = await act.Should().ThrowAsync<ProblemDetailsException>();
        result3.Subject.First().Details.Status.Should().Be(422);
        result3.Subject.First().Details.Detail.Should().Be("Profile not found");
    }
}
