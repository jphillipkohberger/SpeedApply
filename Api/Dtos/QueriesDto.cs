using SpeedApply.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedApply.Api.Dtos
{
    public class QueriesDto
    {
        public int Id { get; set; }
        required public string Query { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        //public Users User { get; set; }
    }
}
