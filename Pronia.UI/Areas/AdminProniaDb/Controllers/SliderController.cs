using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Business.ViewModels.SliderViewModels;
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
        return View(_context.Sliders.AsNoTracking());
    }
    public async Task<IActionResult> Detail(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        slider.Title = "Test";
        if(slider == null) return NotFound();
        return View(slider);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Create(SliderPostVM slider)
    {
        if(!ModelState.IsValid) return View(slider);
        //await _context.Sliders.AddAsync(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
