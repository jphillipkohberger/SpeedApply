using System.ComponentModel.DataAnnotations;

namespace SpeedApply.Api.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        required public string UserName { get; set; }
        required public string Password { get; set; }
        required public string Email { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }

        // Navigation property for 1-to-many
        public ICollection<Queries> Queries { get; set; }
    }
}
