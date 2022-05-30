﻿using System.Collections.Generic;

namespace TNAI.Model.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string   Name  { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
