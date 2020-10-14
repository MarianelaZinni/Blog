using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required(ErrorMessage = "El titulo no puede quedar vacio")]
        public string Title { get; set; }
        [Required(ErrorMessage = "El cuerpo del post no puede quedar vacio")]
        public string Body { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
