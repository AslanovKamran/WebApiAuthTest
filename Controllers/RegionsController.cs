using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UdemyCourse.Models.Domain;
using UdemyCourse.Models.DTOs;
using UdemyCourse.Repos;

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

			//return DTO Regions
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
		public async Task<IActionResult> PostRegion(PostRegionRequest request)
		{
			var region = _mapper.Map<Region>(request);
			region = await _repos.AddRegionAsync(region);
			var regionDTO = _mapper.Map<RegionDTO>(region);

			return CreatedAtAction(nameof(GetRegion), new { id = regionDTO.Id }, regionDTO);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteRegion(Guid id)
		{
			await _repos.DeleteRegionAsync(id);
			return NoContent();
		}

		[HttpPut]
		[ProducesResponseType(404)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> UpdateRegion( Region region)
		{
			region = await _repos.UpdateRegionAsync(region);
			return region == null ? NotFound() : Ok(_mapper.Map<RegionDTO>(region));
		}
	}
}
