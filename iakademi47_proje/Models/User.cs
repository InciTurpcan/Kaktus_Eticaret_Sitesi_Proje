using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47_proje.Models
{
	public class User
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserID { get; set; }

		[Required]
		[StringLength(50)]
		[DisplayName(" Ad Soyad")]
		public string? NameSurname { get; set; }

		[StringLength(100)]
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[StringLength(100)]
		[Required]
		[DataType(DataType.Password)] //Şifrenin yıldızlı görünmesini sağlar.
		[DisplayName(" Şifre ")]
		public string? Password { get; set; }


		[DisplayName("Telefon")]
		public string? Telephone { get; set; }

		[DisplayName("Fatura Adresi")]
		public string? InvoiceAddress { get; set; }

        public bool IsAdmin { get; set; }

		[DisplayName("Aktif")]
		public bool Active { get; set; }

	}
}
