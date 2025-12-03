namespace MaktabGram.Domain.Core.UserAgg.Dtos
{
    public class GetUserProfileDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Bio { get; set; }
        public string? ImgProfileUrl { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public List<GetUserProfilePostDto> Posts { get; set; }
        public List<GetUserProfilePostDto> SavedPosts { get; set; }
        public List<GetUserProfilePostDto> TagPosts { get; set; }
        public bool IsFollower { get; set; }
    }
}
