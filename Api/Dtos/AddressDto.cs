using Microsoft.AspNetCore.Http;

namespace SpeedApply.Api.Dtos
{
    public class AddressDto
    {
        public int UserId { get; set; }
        required public string Street { get; set; }
        required public string City { get; set; }
        required public string State { get; set; }
        required public string Zip { get; set; }
        public IFormFile Resume { get; set; } // The file property
    }
}
