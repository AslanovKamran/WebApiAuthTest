namespace UdemyCourse.Models.DTOs
{
	public class PostRegionRequest
	{
		public string Code { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public string Area { get; set; } = string.Empty;

		public double Lat { get; set; }

		public double Long { get; set; }

		public double Population { get; set; }

	}
}
