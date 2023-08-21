using Microsoft.AspNetCore.Mvc;
using Pronia.Core.Entities;
using Pronia.DataAccess.Contexts;

namespace Pronia.UI.Areas.AdminProniaDb.Controllers;

[Area("AdminProniaDb")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;

    public SliderController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Sliders);
    }
    public async Task<IActionResult> Detail(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if(slider == null) return NotFound();
        return View(slider);
    }
}
