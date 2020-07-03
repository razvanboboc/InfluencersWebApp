using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Influencers.BusinessLogic.Services;
using Influencers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Influencers.Controllers
{
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService tagService;
        public TagController(TagService tagService)
        {
            this.tagService = tagService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/[controller]/[action]")]
        public ActionResult SendAvailableTags()
        {
            try
            {
                var articles = tagService.GetAll();

                return Ok(articles);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}