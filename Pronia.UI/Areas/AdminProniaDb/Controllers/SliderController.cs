using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Pronia.Business.ViewModels.SliderViewModels;
using Pronia.Core.Entities;
using Pronia.DataAccess.Contexts;
using Pronia.Business.Utilities;
using Pronia.Business.Services.Interfaces;
using Pronia.Business.Exceptions;

namespace Pronia.UI.Areas.AdminProniaDb.Controllers;

[Area("AdminProniaDb")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileService;

    public SliderController(AppDbContext context, IMapper mapper, IWebHostEnvironment webEnv)
    {
        _context = context;
        _mapper = mapper;
        _webEnv = webEnv;
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
        string fileName = string.Empty;
        try
        {
             fileName = await _fileService.UploadFile(slider.IMageUrl, _webEnv.WebRootPath, "assets", "images", "website-slider");
        }
        catch (FileSizeException ex)
        {
            ModelState.AddModelError("IMageUrl", ex.Message);
            return View(slider);
        }
        catch(FileTypeException ex)
        {
            ModelState.AddModelError("IMageUrl", ex.Message);
            return View(slider);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(slider);
        }
        Slider newslider = _mapper.Map<Slider>(slider);
        newslider.IMageUrl = fileName;
        await _context.Sliders.AddAsync(newslider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return NotFound();
        return View(slider);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return NotFound();
        string fileRoot = Path.Combine(_webEnv.WebRootPath, slider.IMageUrl);
        if(System.IO.File.Exists(fileRoot))
        {
            System.IO.File.Delete(fileRoot);
        }
        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
