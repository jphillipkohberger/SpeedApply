using SpeedApply.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedApply.Api.Dtos
{
    public class FilesDto
    {
        public int Id { get; set; }
        required public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
