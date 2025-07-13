using System.ComponentModel.DataAnnotations;

namespace RXC.Client.DTOs.AddAsset
{
    public class AddSite
    {
        [Required(ErrorMessage = "Site name is required")]
        [StringLength(50, ErrorMessage = "Name must be less than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location must be less than 150 characters")]
        public string Location { get; set; }
    }
}
