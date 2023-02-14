using Microsoft.AspNetCore.Mvc.ModelBinding;
using UdemyCourse.Models.Domain;
using UdemyCourse.Models.DTOs;
using UdemyCourse.Repos;

namespace UdemyCourse.Validators
{
	public static class WalkDifficultyValidator
	{
		public static bool ValidatePostDifficulty(PostDifficultyRequest request, ModelStateDictionary ModelState)
		{
			if (request is null)
			{
				ModelState.AddModelError(nameof(request), "Data is required");
				return false;
			}

			if (String.IsNullOrWhiteSpace(request.Code))
			{
				ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} can not be null or empty");
			}
			return ModelState.ErrorCount <= 0;
		}

		public static async Task<bool> ValidateUpdateDifficulty(WalkDifficulty request, ModelStateDictionary ModelState, IWalkDifficultyRepository walkDifficultyRepository)
		{
			if (request is null)
			{
				ModelState.AddModelError(nameof(request), "Data is required");
				return false;
			}

			var wd = await walkDifficultyRepository.GetWalkDifficultyAsync(request.Id);
			if(wd is null)
			{

				ModelState.AddModelError(nameof(request.Id), $"{nameof(request.Id)} is not a valid id for WalkDifficulty");
			}

			if (String.IsNullOrWhiteSpace(request.Code))
			{
				ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} can not be null or empty");
			}
			return ModelState.ErrorCount <= 0;
		}


	}
}
