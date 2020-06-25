using System;
using System.Collections.Generic;

namespace Influencers.Models
{
    public partial class ArticleTags
    {
        public int ArticleId { get; set; }
        public int TagId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
