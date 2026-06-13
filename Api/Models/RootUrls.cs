using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace SpeedApply.Api.Models
{
    public class RootUrls
    {
        [Key]
        public int Id { get; set; }
        required public string Domain { get; set; }
        required public string SearchPath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
