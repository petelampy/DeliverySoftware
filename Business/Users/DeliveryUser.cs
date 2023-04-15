using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DeliverySoftware.Business.Users
{
    public class DeliveryUser : IdentityUser
    {
        public string? Address { get; set; }
        public string Forename { get; set; }
        public int? HouseNumber { get; set; }
        public string? PostCode { get; set; }
        public string Surname { get; set; }

        [Required]
        public UserType UserType { get; set; }
    }
}
