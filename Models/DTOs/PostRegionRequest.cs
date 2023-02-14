using System.ComponentModel.DataAnnotations;

namespace UdemyCourse.Models.DTOs
{
	public class PostRegionRequest
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Data is requiredcan not be empty or white space.")]
		public string Code { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false, ErrorMessage = "Data is requiredcan not be empty or white space.")]
		public string Name { get; set; } = string.Empty;

		[Required]
		public double Area { get; set; }

		[Required]
		public double Lat { get; set; }

		[Required]
		public double Long { get; set; }

		[Required]
		[Range(0, Double.PositiveInfinity)]
		public double Population { get; set; }

		public PostRegionRequest()
		{
			this.Area = default(double) + 1;
		}

	}
}
