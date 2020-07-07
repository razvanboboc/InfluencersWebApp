using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Influencers.RequestDtos
{
    public class CommentVoteDto
    {
        public int Flag { get; set; }
        public int CommentId { get; set; }
    }
}
