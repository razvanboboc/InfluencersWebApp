using System;
using System.Collections.Generic;

namespace Influencers.Models
{
    public partial class Article
    {
        public Article()
        {
            Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddedTime { get; set; }
        public int Votes { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }

        public ICollection<ArticleTags> Tags { get; set; } 
    }
}
