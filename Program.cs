using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StaticScraper
{
    class Program
    {
        static void  Main(string[] args)
        {

            getValues();
            Console.ReadLine();
        }

        static async void getValues() {
            Console.WriteLine("here");
            var url = "https://www.apartments.com/the-manhattan-apartments-dallas-tx/wsmpr5k/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);    
            //Console.WriteLine(html.Result);
            //Console.ReadLine();
            var htmlDocument = new HtmlDocument();
            Console.WriteLine("here");
            htmlDocument.LoadHtml(html);
            //Console.WriteLine("Parsing URL {0}", url);
            var response = httpClient.GetAsync(url).Result;
            Console.WriteLine("here");
            string content = response.Content.ReadAsStringAsync().Result;
            var trs = htmlDocument.DocumentNode.SelectNodes("//*[@class=\"availabilityTable  \"]/tbody/tr");
            /*HtmlNode trsnew = htmlDocument.DocumentNode.SelectSingleNode("//*[@class=\"availabilityTable  \"]/tbody");
            //HtmlNode newNode = trsnew.SelectSingleNode(".//tbody");
            var tbold = trsnew.SelectNodes("//*[@class= \"rentalGridRow   bold \"]");
            var thide = trsnew.SelectNodes("//*[@class= \"rentalGridRow  hideOnCollapsed  \"]");
            var thideonnew = trsnew.SelectNodes("//*[@class= \"rentalGridRow  hideOnCollapsed  bold  newAvailability  \"]");
            // Console.WriteLine(tbold);
            foreach (HtmlNode row in tbold)
            {
                Console.WriteLine("Bold");
                var beds = row.Attributes["data-beds"].Value;
                Console.WriteLine("Beds: " + beds);
                var baths = row.Attributes["data-baths"].Value;
                Console.WriteLine("Baths: " + baths);
                var model = row.Attributes["data-model"].Value;
                Console.WriteLine("Name: " + model);


            }

            foreach (HtmlNode row in thide)
            {

                Console.WriteLine("hide");
                var beds = row.Attributes["data-beds"].Value;
                Console.WriteLine("Beds: " + beds);
                var baths = row.Attributes["data-baths"].Value;
                Console.WriteLine("Baths: " + baths);
                var model = row.Attributes["data-model"].Value;
                Console.WriteLine("Name: " + model);


            }
            if (thideonnew != null)
            {
                foreach (HtmlNode row in thideonnew)
                {
                    Console.WriteLine("hide on new");
                    var beds = row.Attributes["data-beds"].Value;
                    Console.WriteLine("Beds: " + beds);
                    var baths = row.Attributes["data-baths"].Value;
                    Console.WriteLine("Baths: " + baths);
                    var model = row.Attributes["data-model"].Value;
                    Console.WriteLine("Name: " + model);


                }

            }*/
            
            //Console.WriteLine(trs);
            Console.WriteLine("here");
            if (trs != null)
            {
                var count = 0;
                Console.WriteLine("in if");
                /*foreach (HtmlNode row in trs)
                {
                    Console.WriteLine(++count);
                    //Console.WriteLine("in foreach");
                    var beds = row.Attributes["data-beds"].Value;
                    Console.WriteLine("Beds: " + beds);
                    var baths = row.Attributes["data-baths"].Value;
                    Console.WriteLine("Baths: " + baths);
                    var model = row.Attributes["data-model"].Value;
                    Console.WriteLine("Name: " + model);
                }*/
            }
            
            var table = htmlDocument.DocumentNode.SelectSingleNode("//table[@class=\"availabilityTable  \"]");
            if (table != null)
            {
                var count = 0;
                Console.WriteLine("tbody");
                var tbody = table.SelectSingleNode("//./tbody");
                var tr = tbody.SelectNodes("//./tr");
                foreach (HtmlNode row in tr) {
                    //Console.WriteLine(++count);
                    //Console.WriteLine(row.InnerHtml);
                    //var td = row.SelectNodes("//./td");

                    //foreach (HtmlNode n in td) {
                    //Console.WriteLine("new");
                    //Console.WriteLine(n.InnerHtml);
                    //Console.WriteLine("over");

                    var baths = row.SelectSingleNode(".//*[@class=\"baths\"]");
                    if (baths != null)
                    {
                        HtmlNode longText = baths.SelectSingleNode(".//*[@class=\"longText\"]");
                        if (longText != null) Console.WriteLine("baths =  " + longText.InnerHtml);
                    }

                    var beds = row.SelectSingleNode(".//*[@class=\"beds\"]");
                    if (baths != null)
                    {
                        HtmlNode longText = beds.SelectSingleNode(".//*[@class=\"longText\"]");
                        if (longText != null) Console.WriteLine("beds =  " + longText.InnerHtml);
                    }

                    HtmlNode rent = row.SelectSingleNode(".//*[@class=\"rent\"]");
                    /*var rent =*/
                    if(rent!=null)Console.WriteLine("rent = "+rent.InnerHtml);

                    HtmlNode deposit = row.SelectSingleNode(".//*[@class=\"deposit\"]");
                    if (deposit != null) Console.WriteLine("deposit = "+deposit.InnerHtml);

                    HtmlNode sqft = row.SelectSingleNode(".//*[@class=\"sqft\"]");
                    if (sqft != null) Console.WriteLine("sqft =  "+sqft.InnerHtml);

                    HtmlNode name = row.SelectSingleNode(".//*[@class=\"name  \"]");
                    if (name != null) Console.WriteLine("name =  " + name.InnerHtml);

                    HtmlNode availability = row.SelectSingleNode(".//*[@class=\"available\"]");
                    if (availability != null) Console.WriteLine("availability =  " + availability.InnerHtml);

                    

                }
                    //Console.WriteLine(td.InnerHtml);
                //}
                
                Console.WriteLine(tr);
            }
        }
    }
}
