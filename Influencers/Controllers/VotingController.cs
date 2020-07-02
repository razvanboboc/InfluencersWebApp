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
        private readonly AuthorService authorService;
        public VotingController(ArticleService articleService, AuthorService authorService)
        {
            this.articleService = articleService;
            this.authorService = authorService;
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

                var author = authorService.GetAuthorByArticleId(votingDto.ArticleId);

                authorService.UpdateAuthorPostVotes(author, votingDto.Flag);

                return Ok(new { articleid = votingDto.ArticleId });
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}