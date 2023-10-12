using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using System.Net.Mail;
using System.Text;
using XSystem.Security.Cryptography;

namespace iakademi47_proje.Models
{
	public class Cls_User
	{
		iakademi47Context context = new iakademi47Context();
		public async Task<User> loginControl(User user)
		{
			string md5sifrele = MD5Sifrele(user.Password);

			User? usr = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password ==
			md5sifrele && u.IsAdmin == true && u.Active == true);

		return usr;
		}

		public static string MD5Sifrele(string value)
		{
			//using XSystem.Security.Cryptography;
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] btr = Encoding.UTF8.GetBytes(value);
			btr = md5.ComputeHash(btr);

			StringBuilder sb = new StringBuilder();
			foreach (byte item in btr)
			{
				sb.Append(item.ToString("x2").ToLower());
			}
			return sb.ToString();
		}

        public static User? SelectMemberInfo(string email)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                User? user = context.Users.FirstOrDefault(u => u.Email == email);
                return user;
            }
        }

        public static bool loginEmailControl(User user)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                User? usr = context.Users.FirstOrDefault(u => u.Email == user.Email);

                if (usr == null)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool AddUser(User user)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                try
                {
                    user.Active = true;
                    user.IsAdmin = false;
                    user.Password = MD5Sifrele(user.Password);
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static string MemberControl(User user)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                string answer = "";

                try
                {
                    string md5Sifre = MD5Sifrele(user.Password);
                    User? usr = context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == md5Sifre);

                    if (usr == null)
                    {
                        //kullanıcı yanlıs sifre veya email girdi
                        answer = "error";
                    }
                    else
                    {
                        //kullanıcı veritabanında kayıtlı.
                        if (usr.IsAdmin == true)
                        {
                            //admin yetkisi olan personel giriş yapıyor
                            answer = "admin";
                        }
                        else
                        {
                            answer = usr.Email;
                        }
                    }
                }
                catch (Exception)
                {
                    return "HATA";
                }
                return answer;
            }

        }

        public static bool SendSms(string OrderGroupGUID)
        {
            
            try
            {
                using (iakademi47Context context = new iakademi47Context())
                {
                    string ss = "";
                    ss += "<?xml version = '1.0' encoding = 'UTF-8'?> ";
                    ss += "<mainbody>";
                    ss += "<header>";
                    ss += "<company dil='TR'> üyelikte size verilen şirket ismi buraya yazılacak </company>";
                    ss += "<usercode> size verilen usercode </usercode>";
                    ss += "<password> size verilen şifre </password>";
                    ss += "<startdate></startdate>";
                    ss += "<stopdate></stopdate>";
                    ss += "<msgheader></msgheader>";
                    ss += "</header>";
                    ss += "<body>";

                    int userID = context.Orders.FirstOrDefault(o => o.OrderGroupGUID == OrderGroupGUID).UserID;
                    User user = context.Users.FirstOrDefault(u => u.UserID == userID);

                    string content = "Sayın" +user.NameSurname +","+DateTime.Now + " tarihinde " + OrderGroupGUID + " nolu siparişiniz alınmıştır.";
                    ss += "<mp><msg><!CDATA["+ content + "]></msg><no>"+user.Telephone+"</no></mp>";
                    ss += "</body>";
                    ss += "</mainbody>";

                    string result = XmlPost("https://api.netgsm.com.tr/xmlbulpost.asp", ss);
                    
                    if (result != "-1")
                    {
                        //Sms gitti, Order tablosunda sms koluna true bas
                    }
                    else
                    {
                        //Sms gitmedi, Order tablosunda sms koluna false bas
                        //ilgili admin personeline email gönder.
                    }


                    return true;
                }
                   
            }
            catch (Exception)
            {

                return false;
            }
            
            
        }

        public static string XmlPost(string url, string ss)
        {
            try
            {
                WebClient wUploud = new WebClient();
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Method = "POST";
                request.ContentType = "application/x-xxx-form-urlencoded";

                Byte[] bPostArray = Encoding.UTF8.GetBytes(ss);
                Byte[] bResponse = wUploud.UploadData(url, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);

                string sWebpage = new string(sReturnChars);
                return sWebpage;
            }
            catch (Exception)
            {

                return "-1";
            }
        }

        public static void SendEmail(string OrderGroupGUID)
        {
            using (iakademi47Context context = new iakademi47Context())
            {
                Order order = context.Orders.FirstOrDefault(o => o.OrderGroupGUID == OrderGroupGUID);
                User user = context.Users.FirstOrDefault(u => u.UserID == order.UserID);

                string mail = "gonderen email buraya info@inci.com";
                string _mail = user.Email;
                string subject = "";
                string content = "";

                content = "Sayın " + user.NameSurname + "," + DateTime.Now + " tarihinde " + OrderGroupGUID + " nolu siparişiniz alınmıştır.";

                subject = "Sayın " + user.NameSurname + " siparişiniz alınmıştır.";

                string host = "smtp.iakademi.com";
                int port = 587;
                string login = "mailserver a baglanılan login buraya";
                string password = "mailserver a baglanılan şifre buraya";

                MailMessage e_posta = new MailMessage();
                e_posta.From = new MailAddress(mail, "inci bilgi"); //gönderen
                e_posta.To.Add(_mail); //alıcı
                e_posta.Subject = subject;
                e_posta.IsBodyHtml = true;
                e_posta.Body = content;

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(login, password);
                smtp.Port = port;
                smtp.Host = host;

                try
                {
                    smtp.Send(e_posta);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
