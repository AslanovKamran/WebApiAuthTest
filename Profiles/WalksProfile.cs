using AutoMapper;
using UdemyCourse.Models.Domain;
using UdemyCourse.Models.DTOs;

namespace UdemyCourse.Profiles
{
	public class WalksProfile : Profile
	{
		public WalksProfile()
		{
			CreateMap<Walk, WalkDTO>().ReverseMap();
			CreateMap<WalkDifficulty, WalkDifficultyDTO>().ReverseMap();
			CreateMap<PostWalkRequest, Walk>().ReverseMap();
		}
	}
}
