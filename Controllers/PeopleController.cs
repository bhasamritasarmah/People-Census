using Microsoft.AspNetCore.Mvc;
using PeopleCensus.Models;
using PeopleCensus.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeopleCensus.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PeopleController : ControllerBase
	{
		private readonly IPeopleCensusService peopleCensusService;

		public PeopleController(IPeopleCensusService peopleCensusService) 
		{
			this.peopleCensusService = peopleCensusService;
		}

		// GET: api/<PeopleController>
		[HttpGet]
		public ActionResult<List<People>> Get()
		{
			return peopleCensusService.Get();
		}

		// GET api/<PeopleController>/5
		[HttpGet("{id}")]
		public ActionResult<People> Get(string id)
		{
			var person = peopleCensusService.Get(id);

			if (person == null) 
			{
				return NotFound($"Person with id = {id} not found.");
			}

			return person;
		}

		// POST api/<PeopleController>
		[HttpPost]
		public ActionResult<People> Post([FromBody] People person)
		{
			peopleCensusService.Create(person);

			return CreatedAtAction(nameof(Get), new {id = person.Id}, person);
		}

		// PUT api/<PeopleController>/5
		[HttpPut("{id}")]
		public ActionResult Put(string id, [FromBody] People person)
		{
			var existingPerson = peopleCensusService.Get(id);

			if (existingPerson == null)
			{
				return NotFound($"Person with id = {id} not found.");
			}

			peopleCensusService.Update(id, person);

			return NoContent();
		}

		// DELETE api/<PeopleController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(string id)
		{
			var person = peopleCensusService.Get(id);

			if(person == null)
			{
				return NotFound($"Person with id = {id} not found.");
			}

			peopleCensusService.Remove(id);

			return Ok($"Person with id = {id} has been deleted.");
		}
	}
}
