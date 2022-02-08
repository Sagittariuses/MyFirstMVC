using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMVC.ConnectHelper
{
    public class Helper
    {
        private static AirlineEntities ConObj;

        public static AirlineEntities GetContext()
        {
            if (ConObj == null)
            {
                ConObj = new AirlineEntities();
            }
            return ConObj;
        }
    }
}