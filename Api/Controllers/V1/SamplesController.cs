using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Api.Models.V1;
using Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/samples")]
    public class SamplesController : ControllerBase
    {
        private readonly SampleRepository _sampleRepository;

        public SamplesController(SampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Sample>>  GetAll()
        {
            return Ok(_sampleRepository.GetAll());
        }

        [HttpGet("{identifier}")]
        [ProducesResponseType(typeof(Sample), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public Sample GetById(long id)
        {
            return _sampleRepository.GetById(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Sample), 201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public ActionResult<Sample> Create([FromBody, Required] Sample sample, ApiVersion apiVersion)
        {
            var created = _sampleRepository.Create(sample);
            return Ok(created);

            //return CreatedAtAction(nameof(GetById), new { identifier = created.Id, version = apiVersion.ToString() }, created);
        }

        [HttpDelete("{identifier}")]
        [ProducesResponseType(typeof(Sample), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public IActionResult DeleteById(long identifier)
        {
            _sampleRepository.DeleteById(identifier);

            return Ok();
        }

        [HttpPut("{identifier}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Sample), 201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public IActionResult Replace(long identifier, [FromBody] Sample sample)
        {
            _sampleRepository.Replace(identifier, sample);

            return Ok();
            // Or CreatedAt if created newly
        }

        [HttpPatch("{identifier}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public IActionResult Update(long identifier, [FromBody] Sample sample)
        {
            _sampleRepository.Update(identifier, sample);

            return Ok();
        }
    }
}

