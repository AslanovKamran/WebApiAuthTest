namespace UdemyCourse.Models.DTOs
{
	public class UpdateWalkRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public double Length { get; set; }
		public Guid RegionId { get; set; }
		public Guid WalkDifficultyId { get; set; }
	}
}
