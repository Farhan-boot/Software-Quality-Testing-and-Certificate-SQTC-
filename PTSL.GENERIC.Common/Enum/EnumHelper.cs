using PTSL.GENERIC.Common.Model.BaseModels;

using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PTSL.GENERIC.Common.Enum;

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
                Name = x.GetEnumDisplayName() ?? x.ToString(),
            });

        return list;
    }
}

