using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Influencers.BusinessLogic.Services;
using Influencers.RequestDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Influencers.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class VotingController : ControllerBase
    {
        private readonly ArticleService articleService;
        public VotingController(ArticleService articleService)
        {
            this.articleService = articleService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/[controller]/[action]")]
        public ActionResult AddVote(VotingDto votingDto)
        {
            try
            {
                articleService.UpdateArticleVotes(votingDto.ArticleId, votingDto.Flag);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}