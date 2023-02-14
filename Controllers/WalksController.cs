using AutoMapper;
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
	public class WalksController : ControllerBase
	{
		private readonly IWalkRepository _repos;
		private readonly IMapper _mapper;
		private readonly IRegionRepository _regionRepository;
		private readonly IWalkDifficultyRepository _wdRepository;

		public WalksController(IWalkRepository repos, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository wdRepository)
		{
			_repos = repos;
			_mapper = mapper;
			_regionRepository = regionRepository;
			_wdRepository = wdRepository;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<IActionResult> GetWalks()
		{
			var walksDomain = await _repos.GetAllWalksAsync();
			var walksDto = _mapper.Map <List<WalkDTO>>(walksDomain);
			return Ok(walksDto);
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetWalk(Guid id)
		{
			var walkDomain = await _repos.GetWalkAsync(id);
			if (walkDomain is not null)
			{
				var walkDto = _mapper.Map<WalkDTO>(walkDomain);
				return Ok(walkDto);
			}
			return NotFound();
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> PostWalk(PostWalkRequest request)
		{
			if (!await WalkValidator.ValidatePostWalk(request,ModelState,_regionRepository,_wdRepository)) return BadRequest(ModelState);
			var walk = _mapper.Map<Walk>(request);
			walk = await _repos.AddWalkAsync(walk);
			var walkDto = _mapper.Map<WalkDTO>(walk);

			return CreatedAtAction(nameof(GetWalk), new { id = walkDto.Id }, walkDto);
		}

		[HttpPut]
		[ProducesResponseType(404)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> UpdateWalk(UpdateWalkRequest request)
		{
			if (!await WalkValidator.ValidateUpdateWalk(request,ModelState,_repos,_regionRepository,_wdRepository)) return BadRequest(ModelState);

			Walk walkDomain = new Walk()
			{
				Id = request.Id,
				Name = request.Name,
				Length = request.Length,
				RegionId = request.RegionId,
				WalkDifficultyId = request.WalkDifficultyId
			};

			walkDomain = await _repos.UpdateWalkAsync(walkDomain);
			return walkDomain == null ? NotFound() : Ok(_mapper.Map<WalkDTO>(walkDomain));
		}

		[HttpDelete]
		[ProducesResponseType(204)]
		[Route("{id}")]
		public async Task<IActionResult> DeleteWalk(Guid id)
		{
			await _repos.DeleteWalkAsync(id);
			return Ok();
		}


	}
}
