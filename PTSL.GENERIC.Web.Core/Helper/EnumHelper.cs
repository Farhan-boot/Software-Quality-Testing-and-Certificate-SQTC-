using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PTSL.GENERIC.Web.Core.Helper
{
    public static class EnumHelper
    {
        public static string GetEnumDisplayName(this System.Enum? enumValue)
        {
            var displayAttribute = enumValue
                ?.GetType()
                .GetField(enumValue.ToString())
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .Cast<DisplayAttribute>()
                .FirstOrDefault();

            return displayAttribute?.Name ?? enumValue?.ToString() ?? string.Empty;
        }

        public static IEnumerable<DropdownVM> GetEnumDropdowns<T>() where T : System.Enum
        {
            var list = System.Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new DropdownVM
                {
                    Id = (int)(object)x,
                    Name = x.GetEnumDisplayName(),
                });

            return list;
        }
    }
}