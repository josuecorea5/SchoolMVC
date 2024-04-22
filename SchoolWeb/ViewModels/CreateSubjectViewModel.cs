using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels
{
    public class CreateSubjectViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(12)]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int TeacherId { get; set; }
        public ICollection<SelectListItem>? TeacherLists { get; set; }
    }
}
