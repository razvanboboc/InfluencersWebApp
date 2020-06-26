using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Influencers.Models;
using Influencers.BusinessLogic.Services;
using Influencers.BusinessLogic.ViewModels.ArticleViewModels;

namespace Influencers.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly ArticleService articleService;

        public ArticleController(ILogger<ArticleController> logger, ArticleService articleService)
        {
            _logger = logger;
            this.articleService = articleService;
        }

        public IActionResult Index()
        {
            var articles = articleService.GetAll();
            return View(new ArticleViewModel { Articles = articles });
        }

        public IActionResult AddArticle([FromForm]AddArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
            articleService.AddArticle(model.Title, model.Content);
            return View();
        }

        public IActionResult JoinInfluencers()
        {
            return View();
        }

        public IActionResult Ranking()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
