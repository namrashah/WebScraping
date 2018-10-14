/*
This class will be used to return nearby transit and Points of interest data from the parser.
This will also be used to save data to the DB.
*/

namespace WebScraperModularized.data{

    public class NTPI{

        public int id {get; set;}

        public string name{get; set;}

        public int drivetime {get; set;}//drive time in minutes

        public int distance {get; set;}//distance in miles

        public string category {get; set;}

        public int property {get; set;}

    }
}