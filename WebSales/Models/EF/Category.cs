using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSales.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {       
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        [StringLength(200)]
        public string SeoTitle { get; set; }
        [StringLength(200)]
        public string SeoDescription { get; set; }
        [StringLength(200)]
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
    }
}