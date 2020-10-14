using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "El nombre no puede quedar vacio")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Dirección de email inválida")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "La clave no puede quedar vacia")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }
}
