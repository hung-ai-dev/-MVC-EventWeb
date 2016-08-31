using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EventWeb.Core.ViewModels
{
    public class FutureDay : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Console.WriteLine(value);
            DateTime dateTime;
            bool isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "d MMMM yyyy",
                CultureInfo.CurrentCulture, 
                DateTimeStyles.None,
                out dateTime);
            return (isValid && dateTime > DateTime.Now);
        }
    }
}