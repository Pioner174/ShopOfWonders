using System.ComponentModel.DataAnnotations;

namespace SOW.DataModel
{
    public class Comment
    {
        public long CommentId { get; set; }

        public string? CommentText { get; set; }

        [Range(0,5)]
        public decimal Raiting { get; set; }

        public Product Product { get; set; }

        public User  Author { get; set; }

    }
}
