using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace realworlddotnet.Api.Models
{
    public class RequestEnvelope<T>
    {
        [FromBody] [Required] public T Body { get; init; }
    }
    
}