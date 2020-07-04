using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
