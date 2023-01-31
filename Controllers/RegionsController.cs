using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
	}
}
