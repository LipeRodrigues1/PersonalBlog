using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Service;
using PersonalBlog.Models;
namespace PersonalBlog.Controllers;

public class ArticleController : Controller
{
    private readonly ArticleService _articleService;

    public ArticleController(ArticleService articleService)
    {
        _articleService = articleService;
    }
    public IActionResult Details(int id)
    {
        var article = _articleService.GetById(id);
        if (article == null)
        {
            return NotFound();
        }
        return View(article);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Article article)
    {
        _articleService.AddArticle(article);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Edit(int id)
    {
        var article = _articleService.GetById(id);
        if (article == null)
        {
            return NotFound();
        }
        return View(article);
    }
    [HttpPost]
    public IActionResult Edit(Article article)
    {
        _articleService.UpdateArticle(article);
        return RedirectToAction("Index", "Home");
    }

}
