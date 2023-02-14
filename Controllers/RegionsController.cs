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
	public class RegionsController : ControllerBase
	{

		private readonly IRegionRepository _repos;
		private readonly IMapper _mapper;

		public RegionsController(IRegionRepository repos, IMapper mapper)
		{
			_repos = repos;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<IActionResult> GetRegions()
		{
			var regions = await _repos.GetAllRegionsAsync();

		
			var result = _mapper.Map<List<RegionDTO>>(regions);
			return Ok(result);
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetRegion(Guid id)
		{
			var region = await _repos.GetRegionAsync(id);
			return region == null
						? NotFound("Wrong id")
						: Ok(_mapper.Map<RegionDTO>(region));
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> PostRegion(PostRegionRequest request)
		{
			//Validate the request
			if (!RegionValidator.ValidatePostRegion(request,ModelState)) return BadRequest(ModelState);

			var region = _mapper.Map<Region>(request);
			region = await _repos.AddRegionAsync(region);
			var regionDTO = _mapper.Map<RegionDTO>(region);

			return CreatedAtAction(nameof(GetRegion), new { id = regionDTO.Id }, regionDTO);

		}

		[HttpPut]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> UpdateRegion(Region region)
		{
			if (ModelState.IsValid)
			{
				region = await _repos.UpdateRegionAsync(region);
				return region == null ? NotFound() : Ok(_mapper.Map<RegionDTO>(region));
			}
			else return BadRequest(ModelState);
		}

		[HttpDelete]
		[ProducesResponseType(204)]
		[Route("{id}")]
		public async Task<IActionResult> DeleteRegion(Guid id)
		{
			await _repos.DeleteRegionAsync(id);
			return NoContent();
		}




		
	}
}
