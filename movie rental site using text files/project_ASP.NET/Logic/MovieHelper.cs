using project_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_ASP.NET.Logic
{
    public class MovieHelper
    {
        static public List<Movie> GetMoviesList()
        {
            string virtualFilePath = "~/Storage/Movies.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);
            string[] FileData = System.IO.File.ReadAllLines(physicalFileLocation);

            List<Movie> list = new List<Movie>();

            foreach (string LineData in FileData)
            {
                string[] arr = LineData.Split(',');
                Movie NewMovie = new Movie();
                NewMovie.Name = arr[0];
                NewMovie.Category = arr[1];

                list.Add(NewMovie);
            }
            return list;
        }

        static public bool DoseMovieExists(string Name)
        {
            bool exist = false;
            bool ExistInFile = GetMoviesList().Exists(x => (x.Name == Name));

            if (ExistInFile)
            {
                exist = true;
            }
            return exist;
        }

        static public void WriteNewMovie(string Name, string Category)
        {
            string virtualFilePath = "~/Storage/Movies.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);

            using (System.IO.StreamWriter sw = System.IO.File.AppendText(physicalFileLocation))
            {
                sw.WriteLine(Name + "," + Category);
            }
        }

        //מחזיר את מספר העותקים הכולל של הספר בקובץ טקסט
        static public int CountingMovieDuplicates(string Name, string Category)
        {
            int count = 0;
            bool ExistInFile = GetMoviesList().Exists(x => (x.Name == Name));
            var movieList = GetMoviesList();

            foreach (var item in movieList)
            {
                foreach (var movie in movieList.GroupBy(x => x))
                {
                    if (item.Name == Name && item.Category == Category)
                    {
                        count = movie.Count();
                    }
                }
            }
            return count;
        }
    }
}