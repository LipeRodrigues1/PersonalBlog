using System.Text.Json;
using PersonalBlog.Models;

namespace PersonalBlog.Service;

public class ArticleService
{
    private string filePath = "Data/Articles.json";

    public List<Article> GetArticles()
    {
        if (!File.Exists(filePath))
        {
            return new List<Article>();
        }

        var json = File.ReadAllText(filePath);

        if (string.IsNullOrEmpty(json))
        {
            return new List<Article>();
        }

        return JsonSerializer.Deserialize<List<Article>>(json) ?? new List<Article>();
    }

    public void SaveArticles(List<Article> articles)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(articles, options);
        File.WriteAllText(filePath, json);
    }

    public void AddArticle(Article article)
    {
        var articles = GetArticles();

        article.Id = articles.Count > 0 ? articles.Max(a => a.Id) + 1 : 1;
        article.CreatedAt = DateTime.Now;
        articles.Add(article);
        SaveArticles(articles);
    }

    public Article GetById(int id)
    {
        return GetArticles().FirstOrDefault(a => a.Id == id);
    }

    public void UpdateArticle(Article updatedArticle)
    {
        var articles = GetArticles();
        var article = articles.FirstOrDefault(a => a.Id == updatedArticle.Id);

        if (article == null)
        {
            return;
        }
        article.Title = updatedArticle.Title;
        article.Content = updatedArticle.Content;
        SaveArticles(articles);
    }
    public void DeleteArticle(int id)
    {
        var articles = GetArticles();
        var article = articles.FirstOrDefault(a => a.Id == id);
        if(article == null)
        {
            return;
        }

        articles.Remove(article);
        SaveArticles(articles);
    }
}
