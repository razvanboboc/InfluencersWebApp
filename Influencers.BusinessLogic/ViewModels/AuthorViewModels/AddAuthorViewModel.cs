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

        [Required]
        public string Email { get; set; }
    }
}
