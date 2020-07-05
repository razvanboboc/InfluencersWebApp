using Influencers.BusinessLogic.Services;
using Influencers.BusinessLogic.ViewModels.ArticleViewModels;
using Influencers.Models;
using Influencers.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Influencers.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly ArticleService articleService;
        private readonly AuthorService authorService;
        private readonly TagService tagService;
        private readonly ArticleTagsService articleTagsService;

        public ArticleController(ILogger<ArticleController> logger, ArticleService articleService, AuthorService authorService, TagService tagService, ArticleTagsService articleTagsService)
        {
            _logger = logger;
            this.articleService = articleService;
            this.authorService = authorService;
            this.tagService = tagService;
            this.articleTagsService = articleTagsService;
        }

        [HttpGet]
        public IActionResult Index(string flag)
        {
            var articles = articleTagsService.GetArticlesIncludingTags();

            var previewedArticles = articleService.GetPreviewedArticles(articles);

            var articleTags = articleTagsService.GetAll();
            
            switch (flag)
            {
                case "top":
                    previewedArticles = articleService.OrderArticlesDescendinglyByVotes(previewedArticles);
                    break;
                case "new":
                    previewedArticles = articleService.OrderArticleMostRecent(previewedArticles);
                    break;
                case "hot":
                    previewedArticles = articleService.CategorizeHot(previewedArticles);
                    break;
                case "old":
                    previewedArticles = previewedArticles.OrderBy(article => article.AddedTime);
                    break;
            };

            //var articlesWithTags = articleTags.Join(
            //    articles,
            //    articleTags => articleTags.ArticleId,
            //    previewedArticles => previewedArticles.Id,
            //    (articleTags, previewedArticles) => new
            //    {
            //        Id = previewedArticles.Id,
            //        Title = previewedArticles.Title,
            //        Content = previewedArticles.Content,
            //        AddedTime = previewedArticles.AddedTime,
            //        Votes = previewedArticles.Votes,
            //        Tag = articleTags.Tag
            //    });

            return View(new ArticleViewModel { Articles = previewedArticles});
        }


        [HttpGet]
        public IActionResult ViewArticle([FromRoute]int id)
        {
            var article = articleService.GetArticleById(id);

            var tags = articleTagsService.GetTagsOfArticleById(id);

            return View(new ViewArticleViewModel { Article = article , Tags = tags});
        }

        [HttpGet]
        public IActionResult AddArticle()
        {
            return View();
        }

        [HttpGet]
        public IActionResult JoinInfluencers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditArticle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddArticle([FromForm]AddArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var authorExists = authorService.VerifyIfAuthorExistsByEmail(model.Email);

            if (authorExists)
            {

                MatchCollection extractedHashtags = tagService.FilterHashtags(model.Tags);

                tagService.AddTags(extractedHashtags);
  
                articleService.AddArticle(model.Title, model.Content, model.Email);

                var recentlyCreatedArticle = articleService.GetNewestAddedArticle(model.Title, model.Content, model.Email);

                foreach(var hashtag in extractedHashtags)
                {
                    var tag = tagService.GetTagByName(hashtag.ToString());

                    articleTagsService.Add(tag, recentlyCreatedArticle);
                }
            }

            var newestArticle = articleService.GetNewestAddedArticle(model.Title, model.Content, model.Email);

            return Redirect(Url.Action("ViewArticle", "Article", new { id = newestArticle.Id}));
        }

        [HttpPost]
        public IActionResult EditArticle([FromForm]EditArticleViewModel model,int id)
        {
            //if (!ModelState.IsValid)
            //{

            //    return View(model);
            //}

            model.ArticleId = id;

            var authorExists = authorService.VerifyIfAuthorExistsByEmail(model.Email);

            if (authorExists)
            {
                articleService.UpdateArticle(model.ArticleId, model.Content);
            }

            return Redirect(Url.Action("ViewArticle", "Article", new { id = model.ArticleId }));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
  