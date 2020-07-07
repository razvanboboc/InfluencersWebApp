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
        private readonly CommentService commentService;
        public VotingController(ArticleService articleService, AuthorService authorService, CommentService commentService)
        {
            this.articleService = articleService;
            this.authorService = authorService;
            this.commentService = commentService;
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

                //authorService.UpdateAuthorPostVotes(author, votingDto.Flag);

                authorService.UpdateAuthorVotes(author.Id);

                return Ok(new { articleid = votingDto.ArticleId });
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/[controller]/[action]")]
        public ActionResult AddCommentVote(CommentVoteDto commentVoteDto)
        {
            try
            {
                commentService.UpdateCommentVotes(commentVoteDto.CommentId, commentVoteDto.Flag);

                var author = authorService.GetAuthorByCommentId(commentVoteDto.CommentId);

                //authorService.UpdateAuthorPostVotes(author, commentVoteDto.Flag);
                
                authorService.UpdateAuthorVotes(author.Id);

                return Ok(new { commentId = commentVoteDto.CommentId });
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}