using System.ComponentModel.DataAnnotations;

namespace OMS.Api.DTOs
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
