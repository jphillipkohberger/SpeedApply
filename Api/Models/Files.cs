using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedApply.Api.Models
{
    public class Files
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        // 1. The Foreign Key Property
        public int UserId { get; set; }

        // 2. The Navigation Property
        public Users User { get; set; }
    }
}
