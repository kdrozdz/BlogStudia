using BlogProject.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models
{
    public enum BlogPostTag
    {
        Sport,
        Ekonomia,
        Medycyna,
        Informatyka
    }
    public class BlogPostModel
    {
        public string? Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
        [StringLength(100)]
        public string Title {  get; set; }
        public string Content { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? AuthorId { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public  BlogPostTag Tag { get; set; }
    }
}
