﻿using System;
using WebScraperModularized.helpers;
using System.Net.Http;
using WebScraperModularized.parsers;
using WebScraperModularized.data;
using System.Collections.Generic;
using Z.Dapper.Plus;
using WebScraperModularized.wrappers;

namespace WebScraperModularized
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            //do dapper entitiy mapping to map objects to DB tables
            DapperPlusManager.Entity<URL>().Table("url").Identity(x => x.id);
            DapperPlusManager.Entity<Property>().Table("property").Identity(x => x.id);
            DapperPlusManager.Entity<PropertyType>().Table("propertytype").Identity(x => x.id);

            URL myUrl;//URL to be parsed

            //get url from url helper and do basic null checks
            while((myUrl = URLHelper.getNextURL())!=null && myUrl.url!=null && myUrl.url.Length>0){
                Console.WriteLine("Parsing URL {0}", myUrl.url);//print the current url
                try{
                    var response = client.GetAsync(myUrl.url).Result;//make an HTTP call and get the html for this URL

                    string content = response.Content.ReadAsStringAsync().Result;//save HTML into string

                    if(myUrl.urltype == (int)URL.URLType.PROPERTY_URL){

                        //if the url is of property type, instantiate property parser
                        PropertyParser parser = new PropertyParser(content, myUrl);
                        
                        //parse the html
                        PropertyData propData = parser.parse();
                        
                        //insert into DB
                        new DBHelper().insertParsedProperties(propData);

                        Console.WriteLine("Stored {0} properties", 
                            (propData!=null && propData.urlList!=null)?propData.urlList.Count:0);
                    }
                    else if(myUrl.urltype == (int)URL.URLType.APARTMENT_URL){
                        
                        //if the url is of apartment type, instantiate apartment parser
                        ApartmentParser parser = new ApartmentParser(content, myUrl);

                        //call the parse method
                        ApartmentData apartmentData = parser.parse();
                        Console.WriteLine("Apartment type parser!");
                    }
                    else{
                        Console.WriteLine("Unknown URL Type");
                    }
                    DBHelper.markURLDone(myUrl);//update the status of URL as done in DB
                }
                catch(Exception e){
                    ExceptionHelper.printException(e);
                }
            }
        }
    }
}
