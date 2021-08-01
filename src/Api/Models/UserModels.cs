using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace realworlddotnet.Api.Models
{
    public record UserEnvelope<T>([Required] T User);
    
};