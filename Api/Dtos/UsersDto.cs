namespace SpeedApply.Api.Dtos
{
    public class UsersDto
    {
        public int Id { get; set; }
        required public string UserName { get; set; }
        required public string Password { get; set; }
        required public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
