namespace iakademi47_proje.Models
{
    public class Cls_Order
    {
        public int ProductID { get; set; }
        public int Qantity { get; set; }

        public string? MyCart { get; set; }

		public decimal UnitPrice { get; set; }
		public string? ProductName { get; set; }
        public int Kdv { get; set; }
        public string? PhotoPath { get; set; }

		iakademi47Context context = new iakademi47Context();
		public bool AddToMyCart(string id)
        {
            bool exists = false;

            if (MyCart == "") 
            {
                MyCart = id + "=1";
            }
            else 
            {
                string[] MyCartArray = MyCart.Split('&');
                for (int i = 0; i < MyCartArray.Length; i++)
                {
                    string[] MyCartArrayLoop = MyCartArray[i].Split('=');

                    if (MyCartArrayLoop[0]==id)
                    {
                        //bu zaten sepette var
                        exists = true;
                    }

				}

                if (exists==false) 
                {
                    MyCart = MyCart + "&" + id.ToString() + "=1";
                }
            }
            return exists;
        }

        //sepetten sil

        public void DeleteFromMyCart(string scid)
        {
			string[] MyCartArray = MyCart.Split('&');
            string NewMyCart = "";
            int count = 1;

            for (int i = 0; i < MyCartArray.Length; i++)
            {
				string[] MyCartArrayLoop = MyCartArray[i].Split('=');

                if (count==1)
                {
                    //yeni sepetin içine silinmeyecek ürünleri koyacağım
                    //yeni sepete ilk ürünü koyuyorum.
                    if (MyCartArrayLoop[0] != scid)
                    {
                        NewMyCart += MyCartArrayLoop[0] + "=" + Convert.ToInt32(MyCartArrayLoop[1]);
                        count++;
                    }


                }
                else
                {
					//count 1 den büyük ,yeni sepette en az bir ürün var
					if (MyCartArrayLoop[0] != scid)
					{
						NewMyCart += "&" + MyCartArrayLoop[0] + "=" + Convert.ToInt32(MyCartArrayLoop[1]);
						count++;
					}
				}

			}

			MyCart = NewMyCart;

		}

        public List<Cls_Order> SelectMyCart()
        {
            List<Cls_Order> list = new List<Cls_Order>();
			string[] MyCartArray = MyCart.Split('&');

            if (MyCartArray[0] != "")
            {
                for (int i = 0; i < MyCartArray.Length; i++)
                {
					string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                    int sepetid = Convert.ToInt32(MyCartArrayLoop[0]);

                    Product? product = context.Products.FirstOrDefault(p=>p.ProductID == sepetid);

                    Cls_Order pr =new Cls_Order();
                    pr.ProductID = product.ProductID; // databaseden aldım property e gönderdim
                    pr.Qantity = Convert.ToInt32(MyCartArrayLoop[1]);
                    pr.UnitPrice =product.UnitPrice;
                    pr.ProductName = product.ProductName;
                    pr.Kdv = product.Kdv;
                    pr.PhotoPath = product.PhotoPath;
                    list.Add(pr);  // her bir ürünün bütün kolon bilgilerini listeye ekle

				}
            }
            return list;
		}

        public string WriteToOrderTable(string Email)
        {
            string OrderGroupGUID = DateTime.Now.ToString().Replace(":", "").Replace(".", "").Replace(" ", "").Replace(",", "");
            DateTime OrderDate = DateTime.Now;
            try
            {
                
                List<Cls_Order> orders = SelectMyCart();
                

                foreach (var item in orders)
                {
                    Order order = new Order();
                   
                    order.OrderGroupGUID = OrderGroupGUID;
                    order.UserID = context.Users.FirstOrDefault(u => u.Email == Email).UserID;
                    order.ProductID = item.ProductID;
                    order.Qantity = item.Qantity;

                    context.Orders.Add(order);
                    context.SaveChanges();

                }
            }
            catch (Exception)
            {

                OrderGroupGUID = "Error";
            }
            return OrderGroupGUID;
        }

        public List<vw_MyOrders> SelectMyOrders(string Email)
        {
            int UserID = context.Users.FirstOrDefault(u => u.Email == Email).UserID;

            List<vw_MyOrders> myOrders = context.vw_MyOrders.Where(o => o.UserID == UserID).ToList();

            return myOrders;
        }
    }


}
