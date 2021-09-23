using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiBurada_Week1.Model
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Range(0,100)]
        public int DiscountPerc { get; set; }

    }
}
