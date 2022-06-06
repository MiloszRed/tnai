using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TNAI.API.Models.InputModels.Category
{
    public class CategoryInputModel
    {
        /// <summary>
        /// Category name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }

}
