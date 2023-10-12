using Microsoft.EntityFrameworkCore;

namespace iakademi47_proje.Models
{
    public class Cls_Supplier
    {
        iakademi47Context context = new iakademi47Context();
        public async Task<List<Supplier>> SupplierSelect()
        {
            List<Supplier> suppliers = await context.Suppliers.ToListAsync();
            return suppliers;
        }

		public static string SupplierInsert(Supplier supplier)
		{
			//method static olduğu için
			using (iakademi47Context context = new iakademi47Context())
			{
				try
				{
					Supplier sup = context.Suppliers.FirstOrDefault(c => c.BrandName.ToLower() == supplier.BrandName.ToLower());
					if (sup == null)
					{
						context.Add(supplier);
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

        public async Task<Supplier?> SupplierDetails(int? id)
        {
            Supplier? supplier = await context.Suppliers.FindAsync(id);
            return supplier;
        }


        public static bool SupplierUpdate(Supplier supplier)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                try
                {
                    context.Update(supplier);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }

            }

        }

		public static bool SupplierDelete(int? id)
		{
			try
			{
				using (iakademi47Context context = new iakademi47Context())
				{
					Supplier supplier = context.Suppliers.FirstOrDefault(c => c.SupplierID == id);
					supplier.Active = false;
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
