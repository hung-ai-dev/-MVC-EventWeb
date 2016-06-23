using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EventWeb.ViewModels
{
    public class FutureDay : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Console.WriteLine(value);
            DateTime dateTime;
            bool isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "d MMM yyyy",
                CultureInfo.CurrentCulture, 
                DateTimeStyles.None,
                out dateTime);
            return (isValid && dateTime > DateTime.Now);
        }
    }
}