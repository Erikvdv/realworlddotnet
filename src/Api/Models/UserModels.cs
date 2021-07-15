using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace realworlddotnet.Api.Models
{
    
    public class UserEnvelope<T>
    {
        [Required] 
        public T User { get; init; }
    }
    
};