using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Influencers.BusinessLogic.ViewModels.AuthorViewModels
{
    public class AddAuthorViewModel
    {
        [Required]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Please input a valid email")]
        [StringLength(100)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string Email { get; set; }
    }
}
