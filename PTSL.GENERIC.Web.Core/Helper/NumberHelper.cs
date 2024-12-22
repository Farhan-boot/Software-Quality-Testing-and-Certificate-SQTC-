using System.Globalization;

namespace PTSL.GENERIC.Web.Core.Helper;

public static class NumberHelper
{
    public static string ToBDTCurrency(this double number)
    {
        return number.ToString("C", new CultureInfo("bn-BD", false).NumberFormat);
    }
}
