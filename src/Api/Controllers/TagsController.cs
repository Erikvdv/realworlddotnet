﻿using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Api.Mappers;
using Realworlddotnet.Api.Models;
using Realworlddotnet.Core.Services.Interfaces;

namespace Realworlddotnet.Api.Controllers;

public class TagsController : Controller
{
    private readonly IArticlesHandler _articlesHandler;
    public TagsController(IArticlesHandler articlesHandler)
    {
        _articlesHandler = articlesHandler;
    }

    [Route("[controller]")]
    public async Task<ActionResult<TagsEnvelope<string[]>>> GetArticlesAsync(CancellationToken cancellationToken)
    {
        var tags = await _articlesHandler.GetTags(cancellationToken);
        return new TagsEnvelope<string[]>(tags);
    }
}