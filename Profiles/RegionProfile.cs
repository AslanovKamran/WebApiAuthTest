using AutoMapper;
using UdemyCourse.Models.Domain;
using UdemyCourse.Models.DTOs;

namespace UdemyCourse.Profiles
{
	public class RegionProfile : Profile
	{

		public RegionProfile()
		{
			CreateMap<Region, RegionDTO>().ReverseMap();
		}
	}
}
