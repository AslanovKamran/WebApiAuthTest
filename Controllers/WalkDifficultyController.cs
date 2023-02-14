using Microsoft.AspNetCore.Mvc;
using UdemyCourse.Models.Domain;
using UdemyCourse.Models.DTOs;
using UdemyCourse.Repos;
using UdemyCourse.Validators;

namespace UdemyCourse.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class WalkDifficultyController : ControllerBase
	{
		private readonly IWalkDifficultyRepository _repos;
		public WalkDifficultyController(IWalkDifficultyRepository repos)
		{
			_repos = repos;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<IActionResult> GetDifficulties()
		{
			var result = await _repos.GetAllWalkDifficulties();
			return Ok(result);
		}

		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(204)]
		[Route("{id}")]
		public async Task<IActionResult> GetDifficulty(Guid id)
		{
			var result = await _repos.GetWalkDifficultyAsync(id);
			return result is not null ? Ok(result) : NotFound();
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> PostDifficulty(PostDifficultyRequest request)
		{
			if(!WalkDifficultyValidator.ValidatePostDifficulty(request, ModelState)) return BadRequest(ModelState);
			var wdDomain = new WalkDifficulty() { Id = Guid.Empty, Code = request.Code };
			wdDomain = await _repos.AddWalkDifficultyAsync(wdDomain);
			return CreatedAtAction(nameof(GetDifficulty), new { id = wdDomain.Id }, wdDomain);
		}


		[HttpPut]
		[ProducesResponseType(404)]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		
		public async Task<IActionResult> UpdateDifficulty(WalkDifficulty difficulty)
		{
			if (!await WalkDifficultyValidator.ValidateUpdateDifficulty(difficulty, ModelState, _repos)) return BadRequest(ModelState);

			difficulty = await _repos.UpdateWalkDifficultyAsync(difficulty);
			return difficulty == null ? NotFound() : Ok(difficulty);
		}

		[HttpDelete]
		[ProducesResponseType(204)]
		[Route("{id}")]
		public async Task<IActionResult> DeleteDifficulty(Guid id)
		{
			await _repos.DeleteWalkDifficultyAsync(id);
			return NoContent();
		}
	}

}
