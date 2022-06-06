using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNAI.API.Models.OutputModels.Category
{
    public class CategoryOutputModel
    {
        /// <summary>
        /// Category identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

    }
}