using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Influencers.RequestDtos
{
    public class VotingDto
    {
        public bool Flag;
        public int ArticleId;
        public VotingDto(bool flag, int articleId)
        {
            Flag = flag;
            ArticleId = articleId;
        }

    }
}
