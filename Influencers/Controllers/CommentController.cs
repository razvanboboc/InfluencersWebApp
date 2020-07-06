using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Influencers.BusinessLogic.Services;
using Influencers.BusinessLogic.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Influencers.Controllers
{
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly CommentService commentService;
        private readonly ArticleService articleService;
        private readonly AuthorService authorService;

        public CommentController(ILogger<CommentController> logger, CommentService commmentService, ArticleService articleService, AuthorService authorService)
        {
            _logger = logger;
            this.commentService = commmentService;
            this.articleService = articleService;
            this.authorService = authorService;
        }
        [HttpGet]
        public IActionResult AddComment(int articleId)
        {
            return View(new AddCommentViewModel { ArticleId = articleId });
        }

        [HttpPost]
        public IActionResult AddComment([FromForm]AddCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var authorExists = authorService.VerifyIfAuthorExistsByEmail(model.Email);

            if (authorExists)
            {
                var article = articleService.GetArticleById(model.ArticleId);

                var author = authorService.GetAuthorByArticleId(model.ArticleId);

                commentService.Add(article, author, model.Content);

            }

            return Redirect(Url.Action("ViewArticle", "Article", new { id = model.ArticleId }));
        }
    }
}