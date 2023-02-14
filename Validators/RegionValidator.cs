using Microsoft.AspNetCore.Mvc.ModelBinding;
using UdemyCourse.Models.DTOs;

namespace UdemyCourse.Validators
{
	public static class RegionValidator
	{
		public static bool ValidatePostRegion(PostRegionRequest request, ModelStateDictionary ModelState)
		{
			if (request is null)
			{
				ModelState.AddModelError(nameof(request), $"Data is required");
				return false;
			}
			if (String.IsNullOrWhiteSpace(request.Code))
			{
				ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} can not be empty or white space.");
			}
			if (String.IsNullOrWhiteSpace(request.Name))
			{
				ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} can not be empty or white space.");
			}
			if (request.Area <= 0)
			{
				ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} can not be less or equal to zero.");
			}
			if (request.Population < 0)
			{
				ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} can not be less than zero.");
			}
			return ModelState.ErrorCount <= 0;
		}
	}
}
