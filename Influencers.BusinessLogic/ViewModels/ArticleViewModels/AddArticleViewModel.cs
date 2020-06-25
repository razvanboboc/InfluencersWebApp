using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.ViewModels.ArticleViewModels
{
    public class AddArticleViewModel
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? Date { get; set; }

        public virtual Author Author { get; set; }
    }
}
