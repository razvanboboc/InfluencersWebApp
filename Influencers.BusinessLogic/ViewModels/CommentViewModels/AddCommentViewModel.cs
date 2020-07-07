using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.ViewModels.CommentViewModels
{
    public class AddCommentViewModel
    {
        public int ArticleId { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
       
    }
}
