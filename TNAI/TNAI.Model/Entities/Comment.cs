using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI.Model.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Text { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
