using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;

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
