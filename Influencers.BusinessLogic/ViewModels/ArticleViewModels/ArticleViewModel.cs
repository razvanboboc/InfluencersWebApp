using System;
using System.Collections.Generic;
using System.Text;
using Influencers.Models;
namespace Influencers.BusinessLogic.ViewModels.ArticleViewModels
{
    public class ArticleViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
    }
}
