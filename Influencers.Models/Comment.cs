using System;
using System.Collections.Generic;

namespace Influencers.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public int? ArticleId { get; set; }
        public string Content { get; set; }
        public DateTime? AddedTime { get; set; }
        public int Votes { get; set; }

        public virtual Article Article { get; set; }
        public virtual Author Author { get; set; }
    }
}
