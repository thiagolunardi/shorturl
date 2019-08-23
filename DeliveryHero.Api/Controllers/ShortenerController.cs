using System;
using System.Collections.Generic;
using DeliveryHero.Api.Model;
using DeliveryHero.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryHero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly IShortenerService _shortenerService;

        public ShortenerController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ShortUrl>> Get()
        {
            var urls = _shortenerService.GetAll();

            return urls;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            var url = _shortenerService.Get(id);

            if (url is null)
                return NotFound();

            return RedirectPermanent(url.FullUrl);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] ShortUrl value)
        {
            try
            {
                var @new = _shortenerService.Generate(value.FullUrl);

                return Created($"/shortener/{@new.Id}", @new);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
