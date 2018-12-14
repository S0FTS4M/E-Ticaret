using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int Amount { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public double Price { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        
        [StringLength(255)]
        public string Category { get; set; }

        [StringLength(255)]
        public string mainPageUrl { get; set; }

        public List<Comment> comment { get; set; }
    }
}