using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("FindSimilarity");
    }

    [HttpGet]
    public IActionResult FindSimilarity()
    {
        return View(new TextSimilarity());
    }
    
    [HttpPost]
    public IActionResult FindSimilarity(TextSimilarity model)
    {
        ViewBag.ResText = "";

        if (ModelState.IsValid)
        {
            ViewBag.ResText = model.GetSimilarity();
        }

        return View(model);
    }
}