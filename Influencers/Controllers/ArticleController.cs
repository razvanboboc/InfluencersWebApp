using Influencers.BusinessLogic.Services;
using Influencers.BusinessLogic.ViewModels.ArticleViewModels;
using Influencers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Influencers.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly ArticleService articleService;
        private readonly AuthorService authorService;

        public ArticleController(ILogger<ArticleController> logger, ArticleService articleService, AuthorService authorService)
        {
            _logger = logger;
            this.articleService = articleService;
            this.authorService = authorService;
        }


        public IActionResult Index()
        {
            var articles = articleService.GetAll();

            var previewedArticles = articleService.GetPreviewedArticles(articles);

            return View(new ArticleViewModel { Articles = previewedArticles });
        }
        [HttpGet]
        public IActionResult Top()
        {
            var articles = articleService.GetAll();
            
            var previewedArticles = articleService.GetPreviewedArticles(articles);

            var orderedArticlesByTop = articleService.OrderArticlesDescendinglyByVotes(previewedArticles);

            return View(new ArticleViewModel { Articles = orderedArticlesByTop });
        }

        [HttpGet]
        public IActionResult New()
        {
            var articles = articleService.GetAll();

            var previewedArticles = articleService.GetPreviewedArticles(articles);

            var orderedArticlesByNew = articleService.OrderArticleMostRecent(previewedArticles);

            return View(new ArticleViewModel { Articles = orderedArticlesByNew });
        }

        [HttpGet]
        public IActionResult Old()
        {
            var articles = articleService.GetAll();

            var previewedArticles = articleService.GetPreviewedArticles(articles);

            return View(new ArticleViewModel { Articles = previewedArticles.OrderBy(article => article.AddedTime) });
        }

        [HttpGet]
        public IActionResult Hot()
        {
            var articles = articleService.GetAll();

            var previewedArticles = articleService.GetPreviewedArticles(articles);

            var categorizedArticles = articleService.CategorizeHot(previewedArticles);

            return View(new ArticleViewModel { Articles = categorizedArticles});
        }

        [HttpGet]
        public IActionResult ViewArticle([FromRoute]int id)
        {
            var article = articleService.GetArticleById(id);
            return View(new ViewArticleViewModel { Article = article });
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
                articleService.AddArticle(model.Title, model.Content, model.Email);

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
