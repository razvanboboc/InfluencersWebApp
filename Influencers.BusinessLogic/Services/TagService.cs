using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Influencers.BusinessLogic.Services
{
    public class TagService
    {
        private ITagRepository tagRepository;
        public TagService(ITagRepository tagRepositoryy)
        {
            this.tagRepository = tagRepositoryy;
        }

        public IEnumerable<Tags> GetAll()
        {
            return tagRepository.GetAll();
        }

        public MatchCollection FilterHashtags(string tags)
        {
            return tagRepository.FilterHashtags(tags);
        }

        public void AddTags(MatchCollection extractedHashtags)
        {
            tagRepository.AddTags(extractedHashtags);
        }

        public Tags GetTagByName(string tagName)
        {
            return tagRepository.GetTagByName(tagName);
        }

    }
}
