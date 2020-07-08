using Influencers.BusinessLogic.Services;
using Influencers.BusinessLogic.ViewModels.ArticleViewModels;
using Influencers.Models;
using Influencers.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly CommentService commentService;

        public ArticleController(ILogger<ArticleController> logger, ArticleService articleService, AuthorService authorService, TagService tagService, ArticleTagsService articleTagsService,CommentService commentService)
        {
            _logger = logger;
            this.articleService = articleService;
            this.authorService = authorService;
            this.tagService = tagService;
            this.articleTagsService = articleTagsService;
            this.commentService = commentService;
        }

        [HttpGet]
        public IActionResult Index(string flag)
        {
            if (flag == null || flag == "top" || flag =="new" || flag =="hot" || flag == "old")
            {
                var articles = articleService.GetAll();

                var previewedArticles = articleService.GetPreviewedArticles(articles);

                List<ViewArticleViewModel> articlesWithTags = new List<ViewArticleViewModel>();

                foreach (var article in previewedArticles)
                {
                    var tags = articleTagsService.GetTagsOfArticleById(article.Id);

                    articlesWithTags.Add(new ViewArticleViewModel { Article = article, Tags = tags });
                }

                switch (flag)
                {
                    case "top":
                        articlesWithTags = articlesWithTags.OrderByDescending(vm => vm.Article.Votes).ToList();
                        break;
                    case "new":
                        articlesWithTags = articlesWithTags.OrderByDescending(vm => vm.Article.AddedTime).ToList();
                        break;
                    case "hot":
                        foreach (ViewArticleViewModel articleWithTags in articlesWithTags.ToList())
                        {
                            TimeSpan diff = DateTime.Now - articleWithTags.Article.AddedTime;
                            double hours = diff.TotalHours;

                            if (!(hours < 24 && articleWithTags.Article.Votes > 5))
                            {
                                articlesWithTags.Remove(articleWithTags);
                            }
                        }
                        break;
                    case "old":
                        articlesWithTags = articlesWithTags.OrderBy(vm => vm.Article.AddedTime).ToList();
                        break;
                };
                return View(new ArticleListViewModel { Articles = articlesWithTags });
            }
            else
            {
                var results = articleService.SearchArticles(flag);

                var previewedArticles = articleService.GetPreviewedArticles(results);

                List<ViewArticleViewModel> articlesWithTags = new List<ViewArticleViewModel>();

                foreach (var article in previewedArticles)
                {
                    var tags = articleTagsService.GetTagsOfArticleById(article.Id);

                    articlesWithTags.Add(new ViewArticleViewModel { Article = article, Tags = tags });
                }

                return View(new ArticleListViewModel { Articles = articlesWithTags });
            }

        }

        [HttpGet]
        public IActionResult ViewArticle([FromRoute]int id)
        {
            var article = articleService.GetArticleById(id);

            var tags = articleTagsService.GetTagsOfArticleById(id);

            var comments = commentService.GetCommentsByArticleId(id);

            return View(new ViewArticleViewModel { Article = article , Tags = tags, Comments = comments});
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
  