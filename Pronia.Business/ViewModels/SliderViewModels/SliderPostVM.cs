using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Business.ViewModels.SliderViewModels;

public class SliderPostVM
{
    [Required,MaxLength(20),MinLength(7)]
    public string Title { get; set; } = null!;
    [Required,MaxLength(110)]
    public string Description { get; set; } = null!;
    public int Discount { get; set; }
    [Required]
    public IFormFile IMageUrl { get; set; } = null!;
}
