using Realworlddotnet.Core.Repositories;

namespace Realworlddotnet.Api.Features.Tags;

public class TagsHandler : ITagsHandler
{
    private readonly IConduitRepository _repository;

    public TagsHandler(IConduitRepository repository)
    {
        _repository = repository;
    }


    public async Task<string[]> GetTagsAsync(CancellationToken cancellationToken)
    {
        var tags = await _repository.GetTagsAsync(cancellationToken);
        return tags.Select(x => x.Id).ToArray();
    }
}
