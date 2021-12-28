using System.ComponentModel;
using System.Reflection;

namespace EmiSoft.CleanArchitecture.Application.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum enumValue)
    {
        FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

        return attributes != null &&
            attributes.Length > 0
            ? attributes[0].Description
            : enumValue.ToString();
    }
}
