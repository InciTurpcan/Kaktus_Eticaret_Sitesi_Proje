using Microsoft.EntityFrameworkCore;

namespace iakademi47_proje.Models
{
	public class Cls_Setting
	{
		iakademi47Context context = new iakademi47Context();

		public async Task<List<Setting>> SettingSelect()
		{
			List<Setting> settings = await context.Settings.ToListAsync();
			return settings;
		}

        public async Task<Setting?> SettingDetails(int? id)
        {
            Setting? settings = await context.Settings.FindAsync(id);

            return settings;
        }


        public static bool SettingUpdate(Setting setting)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                try
                {
                    context.Update(setting);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }

            }

        }
    }
}
