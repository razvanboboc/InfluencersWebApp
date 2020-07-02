using Influencers.BusinessLogic.Services;
using Influencers.BusinessLogic.ViewModels.ArticleViewModels;
using Influencers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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

            articleService.OrderArticlesDescendinglyByVotes(previewedArticles);

            return View(new ArticleViewModel { Articles = previewedArticles });
        }

        [HttpGet]
        public IActionResult ViewArticle(int id)
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

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
