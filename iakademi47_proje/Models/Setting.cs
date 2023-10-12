using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47_proje.Models
{
	public class Setting
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SettingID { get; set; }
		[DisplayName ("Telefon")]
        public string? Telephone { get; set; }
        [DisplayName("Adres")]
        public string? Address { get; set; }
       
        public string? Email { get; set; }
        public int MainPageCount { get; set; }
		public int SubPageCount { get; set; }

	}
}
