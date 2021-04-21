using Air_3550.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_3550.Util
{
    public static class Extensions
    {
        public static string GetFirstError(this ObservableValidator validator)
        {
            var firstError = validator.GetErrors(null).FirstOrDefault();

            if (firstError != null)
            {
                return firstError.ErrorMessage;
            }
            else
            {
                return "";
            }
        }
    }
}
