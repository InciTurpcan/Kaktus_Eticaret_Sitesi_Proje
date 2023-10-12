using Microsoft.AspNetCore.Mvc;
using iakademi47_proje.Models;

namespace iakademi47_proje.ViewComponents
{
    public class Footers : ViewComponent
    {
        iakademi47Context context = new iakademi47Context();

        public IViewComponentResult Invoke()
        {
            List<Supplier> suppliers = context.Suppliers.ToList();
            return View(suppliers);
        }

    }
}
