using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedApply.Api.Models
{
    public class Queries
    {
        [Key]
        public int Id { get; set; }
        public string Query { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public virtual Users User { get; set; }
    }
}
