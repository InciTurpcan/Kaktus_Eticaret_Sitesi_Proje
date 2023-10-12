using Microsoft.EntityFrameworkCore;

namespace iakademi47_proje.Models
{
    public class Cls_Status
    {
        iakademi47Context context = new iakademi47Context();
        public async Task<List<Status>> StatusSelect()
        {
            List<Status> statuses = await context.Statuses.ToListAsync();
            return statuses;
        }

		public static string StatusInsert(Status status)
		{
			//method static olduğu için
			using (iakademi47Context context = new iakademi47Context())
			{
				try
				{
					Status st = context.Statuses.FirstOrDefault(s => s.StatusName.ToLower() == status.StatusName.ToLower());
					if (st == null)
					{
						context.Add(status);
						context.SaveChanges();
						return "başarılı";
					}
					else
					{
						return "zaten var";
					}

				}
				catch (Exception)
				{

					return "başarısız";
				}
			}
		}

        public async Task<Status?> StatusDetails(int? id)
        {
            Status? status = await context.Statuses.FindAsync(id);
            return status;
        }


        public static bool StatusUpdate(Status status)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                try
                {
                    context.Update(status);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }

            }

        }

        public static bool StatusDelete(int? id)
        {
            try
            {
                using (iakademi47Context context = new iakademi47Context())
                {
                    Status status = context.Statuses.FirstOrDefault(c => c.StatusID == id);
                    status.Active = false;
                    context.SaveChanges();
                    return true;
                }


            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
