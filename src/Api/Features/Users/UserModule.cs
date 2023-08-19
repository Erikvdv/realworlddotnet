using Realworlddotnet.Core.Dto;

namespace Realworlddotnet.Api.Features.Users;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/user",
                [Authorize] async (IUserHandler userHandler, ClaimsPrincipal claimsPrincipal) =>
                {
                    var username = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = await userHandler.GetAsync(username!, new CancellationToken());
                    return new UserEnvelope<UserDto>(user);
                })
            .Produces<UserEnvelope<UserDto>>()
            .WithTags("User")
            .WithName("GetUser")
            .IncludeInOpenApi();

        app.MapPut("/user",
                [Authorize] async (
                    IUserHandler userHandler,
                    ClaimsPrincipal claimsPrincipal,
                    UserEnvelope<UpdatedUserDto> request
                ) =>
                {
                    if (!MiniValidator.TryValidate(request, out var errors))
                        return Results.ValidationProblem(errors);
                    
                    var username = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = await userHandler.UpdateAsync(username!, request.User, new CancellationToken());
                    return Results.Ok(new UserEnvelope<UserDto>(user));
                })
            .Produces<UserEnvelope<UserDto>>()
            .WithTags("User")
            .WithName("UpdateUser")
            .IncludeInOpenApi();

        app.MapPost("/users",
                async (IUserHandler userHandler, UserEnvelope<NewUserDto> request) =>
                {
                    if (!MiniValidator.TryValidate(request, out var errors))
                    {
                        return Results.ValidationProblem(errors);
                    }

                    var user = await userHandler.CreateAsync(request.User, new CancellationToken());
                    return Results.Ok(new UserEnvelope<UserDto>(user));
                })
            .Produces<UserEnvelope<UserDto>>()
            .WithTags("User")
            .WithName("CreateUser")
            .IncludeInOpenApi();

        app.MapPost("/users/login",
                async Task<Results<ValidationProblem, Ok<UserEnvelope<UserDto>>>> (IUserHandler userHandler,
                    UserEnvelope<LoginUserDto> request) =>
                {
                    if (!MiniValidator.TryValidate(request, out var errors))
                    {
                        return TypedResults.ValidationProblem(errors);
                    }

                    var user = await userHandler.LoginAsync(request.User, new CancellationToken());
                    return TypedResults.Ok(new UserEnvelope<UserDto>(user));
                })
            .Produces<UnprocessableEntity<ValidationProblem>>(422)
            .WithTags("User")
            .WithName("LoginUser")
            .IncludeInOpenApi().ProducesValidationProblem();
    }
}
