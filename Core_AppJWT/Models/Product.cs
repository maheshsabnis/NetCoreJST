using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Core_AppJWT.Models
{
    public class Product
    {
        [Key]
        public int ProductRowId { get; set; }
        [Required(ErrorMessage ="Product Name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Product Price is required")]
        public int Price { get; set; }
    }
}
