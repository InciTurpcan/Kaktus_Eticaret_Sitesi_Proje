using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace iakademi47_proje.ViewComponents
{
    public class Degrees :ViewComponent
    {
        public string Invoke()
        {
            string apikey = "52b72dad903d5a0244a91d029fce3686";
            string city = "izmir";

            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&mode=xml&lang=tr&units=metric&appid=" + apikey;

            XDocument weather = XDocument.Load(url);

            var temperature = weather.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            return $"{temperature} Derece";
        }
    }
}
