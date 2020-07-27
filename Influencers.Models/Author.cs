using System;
using System.Collections.Generic;

namespace Influencers.Models
{
    public partial class Author
    {
        public Author()
        {
            Article = new HashSet<Article>();
            Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public int? Votes { get; set; }
        public string ImageSource { get; set; }


        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
