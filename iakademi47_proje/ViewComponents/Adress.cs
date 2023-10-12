using iakademi47_proje.Models;
using Microsoft.AspNetCore.Mvc;

namespace iakademi47_proje.ViewComponents
{
	public class Adress :ViewComponent
	{
		iakademi47Context context = new iakademi47Context();

		public string Invoke()
		{
			string adress = context.Settings.FirstOrDefault(s => s.SettingID == 1).Address;

			return $"{adress}";
		}
	}
}
