using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.ViewModels.ArticleViewModels
{
    public class AddArticleViewModel
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
