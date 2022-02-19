using System.ComponentModel.DataAnnotations.Schema;

namespace Andreys.Data.Models
{
    internal class ProductUser
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }


        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
