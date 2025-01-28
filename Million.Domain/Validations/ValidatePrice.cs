using System.Text.RegularExpressions;

namespace Million.Domain.Validations
{
    public static class ValidatePrice
    {
        public static bool IsValid(this decimal price) 
        {
            Regex decimalRegex = new Regex("^[0-9]([.,][0-9]{1,3})?$");

            return decimalRegex.IsMatch(price.ToString()) ? price < 999999999999 : false;
        }

    }
}
