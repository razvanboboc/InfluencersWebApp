using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.ViewModels.ArticleViewModels
{
    public class ViewArticleViewModel
    {
        public Article Article { get; set; }

        public IEnumerable<Tags> Tags { get; set; }
     }
}
