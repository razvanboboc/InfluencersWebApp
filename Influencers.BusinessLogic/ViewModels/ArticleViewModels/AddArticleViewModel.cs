using Influencers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Influencers.BusinessLogic.ViewModels.ArticleViewModels
{
    public class AddArticleViewModel
    {
        [Required(ErrorMessage ="Please input a valid email")]
        [StringLength(100)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string Email { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required(ErrorMessage ="Please enter the hashtags")]
        public string Tags { get; set; }
    }
}
