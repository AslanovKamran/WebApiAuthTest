using Microsoft.AspNetCore.Mvc.ModelBinding;
using UdemyCourse.Models.DTOs;
using UdemyCourse.Repos;

namespace UdemyCourse.Validators
{
	public static class WalkValidator
	{
		public static async Task<bool> ValidatePostWalk(PostWalkRequest request, ModelStateDictionary ModelState, IRegionRepository _regionRepository, IWalkDifficultyRepository _wdRepository)
		{
			if (request is null)
			{
				ModelState.AddModelError(nameof(request), "Data is required");
				return false;
			}

			if (String.IsNullOrWhiteSpace(request.Name))
			{
				ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} can not be null or whitespace");
			}

			if (request.Length <= 0)
			{
				ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} can not be less or equal to zero");
			}

			var region = await _regionRepository.GetRegionAsync(request.RegionId);
			var wd = await _wdRepository.GetWalkDifficultyAsync(request.WalkDifficultyId);
			if (region is null)
			{
				ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is not a valid RegionId");
			}
			if (wd is null)
			{
				ModelState.AddModelError(nameof(request.WalkDifficultyId), $"{nameof(request.WalkDifficultyId)} is not a valid WalkDifficultyId");
			}

			return ModelState.ErrorCount <= 0;
		}

		public static async Task<bool> ValidateUpdateWalk(UpdateWalkRequest request, ModelStateDictionary ModelState, IWalkRepository _repos, IRegionRepository _regionRepository, IWalkDifficultyRepository _wdRepository)
		{
			if (request is null)
			{
				ModelState.AddModelError(nameof(request), "Data is required");
				return false;
			}

			var walk = await _repos.GetWalkAsync(request.Id);
			if (walk is null)
			{
				ModelState.AddModelError(nameof(request.Id), $"{nameof(request.Id)} is not a valid Id for Walk");
			}
			if (String.IsNullOrWhiteSpace(request.Name))
			{
				ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} can not be null or whitespace");
			}

			if (request.Length <= 0)
			{
				ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} can not be less or equal to zero");
			}

			var region = await _regionRepository.GetRegionAsync(request.RegionId);
			var wd = await _wdRepository.GetWalkDifficultyAsync(request.WalkDifficultyId);
			if (region is null)
			{
				ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is not a valid RegionId");
			}
			if (wd is null)
			{
				ModelState.AddModelError(nameof(request.WalkDifficultyId), $"{nameof(request.WalkDifficultyId)} is not a valid WalkDifficultyId");
			}

			return ModelState.ErrorCount <= 0;
		}
	}
}
