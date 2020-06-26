using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Influencers.BusinessLogic.Services;
using Influencers.BusinessLogic.ViewModels.AuthorViewModels;
using Influencers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Influencers.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly ArticleService articleService;
        private readonly AuthorService authorService;

        public AuthorController(ILogger<ArticleController> logger, ArticleService articleService, AuthorService authorService)
        {
            _logger = logger;
            this.articleService = articleService;
            this.authorService = authorService;
        }

        [HttpGet]
        public IActionResult JoinInfluencers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult JoinInfluencers([FromForm]AddAuthorViewModel model)
        {
            authorService.Add(model.Nickname, model.Email);

            return Redirect(Url.Action("Index", "Article"));
        }
    }
}