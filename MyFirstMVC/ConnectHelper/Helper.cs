using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMVC.ConnectHelper
{
    public class Helper
    {
        private static AirlineEntities1 ConObj;

        public static AirlineEntities1 GetContext()
        {
            if (ConObj == null)
            {
                ConObj = new AirlineEntities1();
            }
            return ConObj;
        }
    }
}