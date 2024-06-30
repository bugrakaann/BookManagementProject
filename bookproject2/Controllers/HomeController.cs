using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookproject2.Models;

namespace bookproject2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> BookPage(int pageNumber = 1, int pageSize = 8)
    {
        var databaseController = new DatabaseController();
        var books = await databaseController.GetAllBooks(pageNumber,pageSize);
        var BookPageElements = new BookPageElements
        {
            Books = books,
            PageNumber = pageNumber
        };
        return View("bookpage", BookPageElements);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}