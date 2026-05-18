using SpeedApply.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedApply.Api.Dtos
{
    public class AddressDto
    {
        public int UserId { get; set; }
        required public string Street { get; set; }
        required public string City { get; set; }
        required public string State { get; set; }
        required public string Zip { get; set; }
    }
}
