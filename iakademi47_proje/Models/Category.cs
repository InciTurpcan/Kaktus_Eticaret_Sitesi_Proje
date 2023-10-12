
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47_proje.Models
{
	public class Category
	{
		[DisplayName("ID")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryID { get; set; }

		[DisplayName("Üst Kategori Adı")]
		public int ParentID { get; set; }

		[StringLength(50,ErrorMessage ="En fazla 50 karakter")]
		[DisplayName("Kategori Adı")]
		[Required(ErrorMessage ="Kategori Adı Zorunlu")]
		public string? CategoryName { get; set; }

		[DisplayName("Aktif")]
		public bool Active { get; set; }

	}
}
