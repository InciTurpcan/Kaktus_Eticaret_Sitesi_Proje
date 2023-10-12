using Microsoft.AspNetCore.Mvc;
using iakademi47_proje.Models;
using XAct;
using PagedList.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Specialized;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;

namespace iakademi47_proje.Controllers
{
    
    public class HomeController : Controller
    {

        Cls_Product p = new Cls_Product();
        MainPageModel mpm = new MainPageModel();
		Cls_Order cls_order = new Cls_Order();
		iakademi47Context context = new iakademi47Context();
        public IActionResult Index()
        {
            mpm.SliderProducts = p.ProductSelect("Slider","",0);
            mpm.NewProducts = p.ProductSelect("New", "",0); //"New"=home/ındexe giden parametre, ""=alt sayfaya giden parametre , 0=AJAX İÇİN
            mpm.Productofday = p.ProductDetails();
            mpm.SpecialProducts = p.ProductSelect("Special", "",0);//özel
            mpm.DiscountedProducts = p.ProductSelect("Discounted", "",0);//indirimli
            mpm.HighLightedProducts = p.ProductSelect("HighLighted", "", 0);//öne çıkanlar
            mpm.TopsellerProducts = p.ProductSelect("TopSeller", "", 0);//çok satanlar
            mpm.StarProducts = p.ProductSelect("Star", "", 0);//yıldızlı
            mpm.FeaturedProducts = p.ProductSelect("Featured", "",0);//fırsat
            mpm.NotableProducts = p.ProductSelect("Notable", "",0);//dikkat çeken

			return View(mpm);
        }
        public IActionResult CartProcess(int id)
        {
            Cls_Product.Highlighted_Increase(id);
            
            cls_order.ProductID = id;
            cls_order.Qantity = 1;
            
            var cookieOptions = new CookieOptions();
            //tarayıcıdan okuma
            var cookie = Request.Cookies["sepetim"];
            if (cookie == null) 
            {
                //sepet boşsa
              cookieOptions= new CookieOptions();
                cookieOptions.Expires=DateTime.Now.AddDays(7);// 7 günlük çerez süresi
                cookieOptions.Path = "/";
                cls_order.MyCart = "";
                cls_order.AddToMyCart(id.ToString());
                Response.Cookies.Append("sepetim", cls_order.MyCart, cookieOptions);
                HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
                TempData["Message"] = "Ürün Sepetinize Eklendi";
				

			}

            else
            {
                //sepet doluysa
                cls_order.MyCart = cookie;

                if (cls_order.AddToMyCart(id.ToString())==false)
                {
                    //sepet dolu ,aynı ürün değil
					Response.Cookies.Append("sepetim", cls_order.MyCart, cookieOptions);
					cookieOptions.Expires = DateTime.Now.AddDays(7);
					HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
					TempData["Message"] = "Ürün Sepetinize Eklendi";
                    //o an hangi sayfadaysam sayfanın linkini yakalıyorum
                    
				}
                else 
                {
					HttpContext.Session.SetString("Message", "Ürün Sepetinizde Zaten Var");
					TempData["Message"] = "Ürün Sepetinizde Zaten Var";
					

				}
            }

			string url = Request.Headers["Referer"].ToString();
			return Redirect(url);
		}
		
		public IActionResult Cart()
		{
            List<Cls_Order> MyCart;

            if (HttpContext.Request.Query["scid"].ToString() != "")
            {
                int scid = Convert.ToInt32(HttpContext.Request.Query["scid"].ToString());
                cls_order.MyCart = Request.Cookies["sepetim"];
                cls_order.DeleteFromMyCart(scid.ToString());

                var cookieOptions = new CookieOptions();
				Response.Cookies.Append("sepetim", cls_order.MyCart, cookieOptions);
				cookieOptions.Expires = DateTime.Now.AddDays(7);
				TempData["Message"] = "Ürün Sepetinizden Silindi";
                 MyCart = cls_order.SelectMyCart();
                ViewBag.MyCart = MyCart;
                ViewBag.MyCart_Table_Details = MyCart;
			}
            else 
            {
                var cookie = Request.Cookies["sepetim"];
              

                if (cookie== null)
                {
                    //SEPETTE HİÇ ÜRÜN OLMAYABİLİR
                    var cookieOptions = new CookieOptions();
                    cls_order.MyCart = "";
                    MyCart =cls_order.SelectMyCart();
					ViewBag.MyCart = MyCart;
					ViewBag.MyCart_Table_Details = MyCart;

				}
                else 
                {
                    //SEPETTE ÜRÜN VAR
					var cookieOptions = new CookieOptions();
					cls_order.MyCart = Request.Cookies["sepetim"];
					MyCart = cls_order.SelectMyCart();
					ViewBag.MyCart = MyCart;
					ViewBag.MyCart_Table_Details = MyCart;

				}
            
            }

            if (MyCart.Count==0)
            {
                ViewBag.MyCart= null;
            }

			return View();
		}

        public IActionResult Details(int id)
        {
            //efcore
            //mpm.ProductDetails = context.Products.FirstOrDefault(p => p.ProductID == id);

            //select * from Products where ProductID = id  ado.net , dapper

            //linq  - 4 nolu ürünün bütün kolon (sütün) bilgileri elimde
            mpm.ProductDetails = (from p in context.Products where p.ProductID == id select p).FirstOrDefault();

            //linq
            mpm.CategoryName = (from p in context.Products
                                join c in context.Categories
                              on p.CategoryID equals c.CategoryID
                                where p.ProductID == id
                                select c.CategoryName).FirstOrDefault();

            //linq
            mpm.BrandName = (from p in context.Products
                             join s in context.Suppliers
                           on p.SupplierID equals s.SupplierID
                             where p.ProductID == id
                             select s.BrandName).FirstOrDefault();

            //select * from Products where Related = 2 and ProductID != 4
            mpm.RelatedProducts = context.Products.Where(p => p.Related == mpm.ProductDetails!.Related && p.ProductID != id).ToList();

            Cls_Product.Highlighted_Increase(id);

            return View(mpm);
        }

        public PartialViewResult gettingProducts(string id)
        {
            id = id.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
            List<sp_arama> ulist = Cls_Product.gettingSearchProducts (id);
            string json = JsonConvert.SerializeObject(ulist);
            var response = JsonConvert.DeserializeObject<List<Search>>(json);
            return PartialView(response);
        }

        public IActionResult Order()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                User? user = Cls_User.SelectMemberInfo(HttpContext.Session.GetString("Email").ToString());


                return View(user);
            }
            else 
            {
             return RedirectToAction("Login");
            }
        
        }

        [HttpPost]
        public IActionResult Order(IFormCollection frm)
        {
            //string? kredikartno = Request.Form("kredikartno"); //string parametre olsaydı bu şekilde request yakalardı.
            string kredikartno = frm["kredikartno"].ToString(); //form collection olunca böyle yakalıyoruz
            string kredikartay = frm["kredikartay"].ToString();
            string kredikartyıl = frm["kredikartyıl"].ToString();
            string kredikartcvc = frm["kredikartcvc"].ToString();

            //bankaya git eger true gelirse(onay gelirse)
            //order tablosuna kayıt atacağız
            // digital-planet e(e-fatura ) bilgilerini gönder

            //payu
            //iyzico

            string txt_tckimlikno = frm["txt_tckimlikno"].ToString();
            string txt_vergino = frm["txt_vergino"].ToString();

            if (txt_tckimlikno != "")
            {
                WebServiceController.tckimlikno = txt_tckimlikno;
                //fatura bilgilerini digital planet şirketine vb gönderirsiniz(xml dosyası)
            }
            else
            {
                WebServiceController.vergino = txt_vergino;
            }

            NameValueCollection data = new NameValueCollection();
            string url = "https://www.inciturpcan.com/backref";

            data.Add("BACK_REF", url);
            data.Add("CC_CVC", kredikartcvc);
            data.Add("CC_NUMBER", kredikartno);
            data.Add("EXP_MONTH", kredikartay);
            data.Add("EXP_YEAR", kredikartyıl);

            var deger = "";
            foreach (var item in data)
            {
                var value = item as string;
                var byteCount  = Encoding.UTF8.GetByteCount(data.Get(value));
                deger += byteCount + data.Get(value);
            }

            var signatureKey = "payu üyeliğinde size verilen SECRET_KEY burada yazılacak";
            var hash = HashWithSignature(deger, signatureKey);


            data.Add("ORDER_HASH", hash);

            var x = POSTFormPAYU("https://secure.payu.tr/order/...", data);

            //sanal kredi kartı
            if (x.Contains("<STATUS>SUCCESS</STATUS>")&& x.Contains("<RETURN_CODE>3DS_ENROLLED</RETURN_CODE>"))
            {
                // sanal kart ok
            }
            else
            {
                //gerçek kredi kartı
            }

            return RedirectToAction("backref");
        }

        public IActionResult MyOrders()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                List<vw_MyOrders> orders = cls_order.SelectMyOrders(HttpContext.Session.GetString("Email").ToString());
                return View(orders);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult DetailedSearch()
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Suppliers = context.Suppliers.ToList();
            return View();
        }


        public IActionResult DProducts(int CategoryID, string[] SupplierID, string price, string IsInStock)
        {
            price = price.Replace(" ", "");
            string[] PriceArray = price.Split('-');
            string startprice = PriceArray[0];
            string endprice = PriceArray[1];

            string sign = ">";
            if (IsInStock == "0")
            {
                sign = ">=";
            }

            int count = 0;
            string suppliervalue = ""; //1,2,4
            for (int i = 0; i < SupplierID.Length; i++)
            {
                if (count == 0)
                {
                    suppliervalue = "SupplierID =" + SupplierID[i];
                    count++;
                }
                else
                {
                    suppliervalue += " or SupplierID =" + SupplierID[i];
                }
            }

            string query = "select * from Products where  CategoryID = " + CategoryID + " and (" + suppliervalue + ") and (UnitPrice > " + startprice + " and UnitPrice < " + endprice + ") and Stock " + sign + " 0 order by ProductName";

            ViewBag.Products = p.SelectProductsByDetails(query);
            return View();
        }

        public static string HashWithSignature(string deger, string signatureKey)
        {
            return "";
        }

        public IActionResult backref()
        {
            Confirm_Order();
            return RedirectToAction("Confirm");
        }

        public static string OrderGroupGUID = "";
         
        
        public IActionResult Confirm_Order()
        {
            //sipariş tablosuna kaydet
            //cookie sepetini sileceğiz
            //e-fatura oluşturacağız, e fatura oluşturan xml methodu cağıracağız.

            var cookieOptions = new CookieOptions();
            var cookie = Request.Cookies["sepetim"];
            if (cookie != null)
            {
                cls_order.MyCart = cookie; // tarayıcada ki sepet bilgilerini property e koydum.
                OrderGroupGUID = cls_order.WriteToOrderTable(HttpContext.Session.GetString("Email"));

                cookieOptions.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Delete("sepetim");

                bool result = Cls_User.SendSms(OrderGroupGUID);
                if (result==false)
                {
                    //Orders tablosunda sms koluna false değeri basılır,admin panele menü yapılır.
                    //Orders tablosunda sms kolonu = false olan siparişleri getir
                }
                
                //Cls_User.SendEmail(OrderGroupGUID);

                HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
                TempData["Message"] = "Ürün Sepetinize Eklendi";

                
            }



            return RedirectToAction("Confirm");
        }

        public IActionResult Confirm()
        {
            ViewBag.OrderGroupGUID = OrderGroupGUID;
            return View();
        }
        public static string POSTFormPAYU(string url, NameValueCollection data)
        {
            return "";
        }

        public IActionResult Login()
        { 

            return View();
        
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            string answer = Cls_User.MemberControl(user);

            if (answer == "error")
            {
                TempData["Message"] = "Email ve/veya Şifre yanlış";
            }
            else if (answer== "admin")
            {
                HttpContext.Session.SetString("Admin", "Admin");
                HttpContext.Session.SetString("Email", answer);
                return RedirectToAction("Login", "Admin");

            }
            else
            {
               
                HttpContext.Session.SetString("Email", answer);
                return RedirectToAction("Index", "Home");

            }
            return View();

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            if (Cls_User.loginEmailControl(user) == false)
            {
                bool answer = Cls_User.AddUser(user);

                if (answer)
                {
                    TempData["Message"] = "Kaydedildi.";
                    return RedirectToAction("Login");
                }
                TempData["Message"] = "Hata.Tekrar deneyiniz.";
            }
            else
            {
                TempData["Message"] = "Bu Email Zaten mevcut.Başka Deneyiniz.";
            }
            return View();
        }


        public IActionResult NewProducts()
        {
            mpm.NewProducts = p.ProductSelect("New","New",0);
            return View(mpm);
        }

        public PartialViewResult _partialNewProducts(string nextpagenumber)
        {
            int pagenumber = Convert.ToInt32(nextpagenumber);
            mpm.NewProducts = p.ProductSelect("New", "New", pagenumber);
            return PartialView(mpm);
        }

        public IActionResult SpecialProducts()
        {
            mpm.SpecialProducts = p.ProductSelect("Special", "Special", 0);
            return View(mpm);
        }

        public PartialViewResult _partialSpecialProducts(string nextpagenumber)
        {
            int pagenumber = Convert.ToInt32(nextpagenumber);
            mpm.SpecialProducts = p.ProductSelect("Special", "Special", pagenumber);
            return PartialView(mpm);
        }

        public IActionResult DiscountedProducts()
        {
            mpm.DiscountedProducts = p.ProductSelect("Discounted", "Discounted", 0);
            return View(mpm);
        }

        public PartialViewResult _partialDiscountedProducts(string nextpagenumber)
        {
            int pagenumber = Convert.ToInt32(nextpagenumber);
            mpm.DiscountedProducts = p.ProductSelect("Discounted", "Discounted", pagenumber);
            return PartialView(mpm);
        }

        public IActionResult HighLightedProducts()
        {
            mpm.HighLightedProducts = p.ProductSelect("HighLighted", "HighLighted", 0);

            return View(mpm);

        }

        public PartialViewResult _partialHighlighted(string nextpagenumber)
        {

            int pagenumber = Convert.ToInt32(nextpagenumber);
            mpm.HighLightedProducts = p.ProductSelect("HighLighted", "HighLighted", pagenumber);

            return PartialView(mpm);

        }

        public IActionResult TopsellerProducts(int page = 1, int pagesize = 4)
        {
            PagedList<Product> model = new PagedList<Product>(context.Products.OrderByDescending(p => p.TopSeller), page, pagesize);
            return View("TopsellerProducts", model);

        }

        public IActionResult CategoryPage(int id)
        {
            List<Product> products = p.ProductSelectWithCategoryID(id);
            return View(products);
        }

        public IActionResult SupplierPage(int id)
        {
            List<Product> products = p.ProductSelectWithSupplierID(id);
            return View(products);
        }

        public IActionResult ContactUs() 
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        //******* API ********

        public IActionResult PharmacyOnDuty()
        {
            /*
            https://openfiles.izmir.bel.tr/111324/docs/ibbapi-WebServisKullanimDokumani_1.0.pdf
            https://openapi.izmir.bel.tr/api/ibb/cbs/wizmirnetnoktalari 
            https://acikveri.bizizmir.com/dataset/kablosuz-internet-baglanti-noktalari/resource/982875a4-2bb6-4178-8ee2-3f07641156bb
            https://acikveri.bizizmir.com/dataset/izban-banliyo-hareket-saatleri
            */

            //https://openapi.izmir.bel.tr/api/ibb/nobetcieczaneler

            string json = new WebClient().DownloadString("https://openapi.izmir.bel.tr/api/ibb/nobetcieczaneler");

            var pharmacy = JsonConvert.DeserializeObject<List<Pharmacy>>(json);

            return View(pharmacy);
        }

        public IActionResult ArtAndCulture()
        {
            //https://openapi.izmir.bel.tr/api/ibb/kultursanat/etkinlikler

            string json = new WebClient().DownloadString("https://openapi.izmir.bel.tr/api/ibb/kultursanat/etkinlikler");

            var activite = JsonConvert.DeserializeObject<List<Activite>>(json);

            return View(activite);
        }
    }
}
