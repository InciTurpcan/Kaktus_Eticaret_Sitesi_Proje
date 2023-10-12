using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace iakademi47_proje.Models
{
	public class Status
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StatusID { get; set; }

		[Required]
		[StringLength(50)]
		[DisplayName("Statü")]
		public string? StatusName { get; set; }

		[DisplayName("Aktif")]
		public bool Active { get; set; }
	}
}
