using project_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_ASP.NET.Logic
{
    public class CustomerHelper
    {
        static public List<Customer> GetCustomersList()
        {
            string virtualFilePath = "~/Storage/Customers.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);
            string[] FileData = System.IO.File.ReadAllLines(physicalFileLocation);

            List<Customer> list1 = new List<Customer>();

            foreach (string LineData in FileData)
            {
                string[] arrCus = LineData.Split(',');
                Customer NewCustomer = new Customer();
                NewCustomer.Name = arrCus[0];
                NewCustomer.Age = arrCus[2];
                NewCustomer.subscription = arrCus[1];
                list1.Add(NewCustomer);
            }
            return list1;
        }

        //מחזיר רשימה עם שמות הלקוחות הקיימים בלבד
        public static List<string> GetCustomersNameOnly()
        {
            List<string> CustomerNameList = new List<string>();

            foreach (var item in GetCustomersList())
            {
                  CustomerNameList.Add(item.Name);
            }
            return CustomerNameList;
        }

        static public bool DoseCustomerExists(string Name) 
        {
            bool exist = false;
            bool ExistInFile = GetCustomersList().Exists(x => (x.Name == Name));

            if (ExistInFile)
            {
                exist = true;
            }
            return exist;
        }
    
        static public void WriteNewCustomer(string userName, string userAge, string subscription)
        {
            string virtualFilePath = "~/Storage/Customers.txt";
            string physicalFileLocation = HttpContext.Current.Server.MapPath(virtualFilePath);

            using (System.IO.StreamWriter sw = System.IO.File.AppendText(physicalFileLocation))
            {
                sw.WriteLine(userName + "," + userAge + "," + subscription);
            }

        }
    }
}