using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Influencers.Repository.Abstractions
{
    public interface ITagRepository : IRepository<Tags>
    {
        MatchCollection FilterHashtags(string tags);

        void AddTags(MatchCollection extractedHashtags);

        bool TagExists(string tag);

        Tags GetTagByName(string tagName);

    }
}
