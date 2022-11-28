using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LatHtaukBayDin20221120GraphQL.Api.Models
{
    [Table("Tbl_Blog")]
    public class BlogModel
    {
        [Key]
        public int Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Author { get; set; }
        public string Blog_Content { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
