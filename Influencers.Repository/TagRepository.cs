using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.Repository
{
    public class TagRepository : BaseRepository<Tags>, ITagRepository
    {
        public TagRepository(InfluencersContext dbContext) : base(dbContext)
        {

        }
    }
}
