using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.Repository
{
    public class ArticleTagsRepository : BaseRepository<ArticleTags>, IArticleTagsRepository
    {
        public ArticleTagsRepository(InfluencersContext dbContext) : base(dbContext)
        {

        }
    }
}
