namespace Realworlddotnet.Api.Controllers;

public class TagsController(IArticlesHandler articlesHandler) : Controller
{
    [Route("[controller]")]
    [HttpGet]
    public async Task<ActionResult<TagsEnvelope<string[]>>> GetArticlesAsync(CancellationToken cancellationToken)
    {
        var tags = await articlesHandler.GetTags(cancellationToken);
        return new TagsEnvelope<string[]>(tags);
    }
}
