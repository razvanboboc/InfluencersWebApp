using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Influencers.BusinessLogic.ViewModels.ArticleViewModels
{
    public class EditArticleViewModel
    {
        public int ArticleId { get; set; }
        [Required(ErrorMessage = "Please input a valid email")]
        [StringLength(100)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string Email { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
