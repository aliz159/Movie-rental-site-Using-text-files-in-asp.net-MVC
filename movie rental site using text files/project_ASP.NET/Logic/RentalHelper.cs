using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using project_ASP.NET.Models;
using System.Text;

namespace project_ASP.NET.Logic
{
    public class RentalHelper
    {
        static public List<Rental> GetRentalList()
        {
            string virtualFilePath = "~/Storage/Rentals.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);
            string[] FileData = System.IO.File.ReadAllLines(physicalFileLocation);

            List<Rental> list = new List<Rental>();

            foreach (string LineData in FileData)
            {
                if (LineData != "-")
                {
                    string[] arr = LineData.Split(',');
                    Rental RentedMovie = new Rental();

                    RentedMovie.NameOfcustomer = arr[0];
                    RentedMovie.NameOfMovie = arr[1];

                    list.Add(RentedMovie);
                }
            }
            return list;
        }

        static public void WriteNewRental(string Name, string Movie)
        {
            string virtualFilePath = "~/Storage/Rentals.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);

            using (System.IO.StreamWriter sw = System.IO.File.AppendText(physicalFileLocation))
            {
                sw.WriteLine(Name + "," + Movie);
            }
        }

        static public bool DoseRentalSucceeded(string RentName)
        {
            string virtualFilePath = "~/Storage/Rentals.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);
            string[] FileData = System.IO.File.ReadAllLines(physicalFileLocation);

            string[] arr;

            bool exist = false;

            foreach (string LineData in FileData)
            {
                arr = LineData.Split(',');
                if (arr[1] == RentName)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        //מוחק את הספר מקובץ הטקסט של ההשכרה
        static public void DeleteRentedMovie(string Name, string Movie)
        {
            string virtualFilePath = "~/Storage/Rentals.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);

            System.IO.File.WriteAllLines(physicalFileLocation, System.IO.File.ReadLines(physicalFileLocation).Where(l => l != (Name + "," + Movie)).ToList());
        }

        //בודק האם הסרט שהוחזר באמת נמחק מקובץ הטקסט
        static public bool DoesMovieReturned(string customerName, string movieRented)
        {
            bool IsMovieReturned = false;
            bool ExistInFile = GetRentalList().Exists(x => (x.NameOfcustomer == customerName && x.NameOfMovie == movieRented));

            if (!ExistInFile)
            {
                IsMovieReturned = true;
            }
            return IsMovieReturned;
        }

        //מחזיר רשימה עם שמות הסרטים שהושכרו
        public static List<string> GetMoviesRentedNames()
        {
            List<string> RentalMoviesList = new List<string>();

            foreach (var item1 in GetRentalList())
            {

                RentalMoviesList.Add(item1.NameOfMovie);
            }
            return RentalMoviesList;
        }

        public static List<string> GetCustomersWhoRented()
        {
            List<string> CustomerNameList = new List<string>();

            foreach (var item in GetRentalList())
            {
                foreach (var item1 in CustomerHelper.GetCustomersList())
                {
                    if (item.NameOfcustomer == item1.Name)
                    {
                        CustomerNameList.Add(item1.Name);
                    }
                }
            }
            return CustomerNameList;
        }

        //מחזיר את מספר ההשכרות הכולל של הספר בקובץ טקסט
        static public int CountingMovieDuplicates(string Name, string Movie)
        {
            int count = 0;
            bool ExistInFile = GetRentalList().Exists(x => (x.NameOfcustomer == Name));

            var movieList = GetRentalList();

            foreach (var item in movieList)
            {
                foreach (var movie in movieList.GroupBy(x => x))
                {
                    if (item.NameOfcustomer == Name && item.NameOfMovie == Movie)
                    {
                        count = movie.Count();
                    }
                }
            }
            return count;
        }
    }
}