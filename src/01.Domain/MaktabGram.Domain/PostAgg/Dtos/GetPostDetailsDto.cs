using MaktabGram.Domain.Core.CommentAgg.Entities;

namespace MaktabGram.Domain.Core.PostAgg.Dtos
{
    public class GetPostDetailsDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? ProfileImgUrl { get; set; }
        public string ImgPostUrl { get; set; }
        public int LikeCount { get; set; }
        public string Caption { get; set; }
        public string CreateAt { get; set; }
        public List<Comment> Comments { get; set; }
        public bool UserLikeThisPost { get; set; }
    }
}
