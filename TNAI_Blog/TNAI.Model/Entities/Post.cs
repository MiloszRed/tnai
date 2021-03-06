using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI.Model.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [MaxLength(25)]
        public string DateTime { get; set; }
    }
}
