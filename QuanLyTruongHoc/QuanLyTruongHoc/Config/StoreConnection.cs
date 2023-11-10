using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace QuanLyTruongHoc.Config
{
    public class StoreConnection
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["QuanLyTruongHoc"].ConnectionString;
        }
    }
}