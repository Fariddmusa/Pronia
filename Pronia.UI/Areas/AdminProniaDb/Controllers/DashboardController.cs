using Microsoft.AspNetCore.Mvc;

namespace Pronia.UI.Areas.AdminProniaDb.Controllers;

[Area("AdminProniaDb")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
