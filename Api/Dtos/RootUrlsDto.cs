namespace SpeedApply.Api.Dtos
{
    public class RootUrlsDto
    {
        public int Id { get; set; }
        required public string Domain { get; set; }
        required public string SearchPath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
