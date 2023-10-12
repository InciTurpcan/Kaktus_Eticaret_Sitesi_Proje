using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47_proje.Models
{
    public class vw_MyOrders
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderGroupGUID { get; set; }
        public int Qantity { get; set; }
        public int UserID { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string? PhotoPath { get; set; }
    }
}
