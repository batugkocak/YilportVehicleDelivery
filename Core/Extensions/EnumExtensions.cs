using System.ComponentModel;

namespace Core.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }
        throw new ArgumentException("Item not found.", nameof(enumValue));
    }

    
    public static T GetEnumValueByDescription<T>(this string description) where T : Enum
    {
        foreach (Enum enumItem in Enum.GetValues(typeof(T)))
        {
            if (enumItem.GetEnumDescription() == description)
            {
                return (T)enumItem;
            }
        }
        throw new ArgumentException("Not found.", nameof(description));
    }
    
    
    public static T ToEnum<T>(this string enumString)
    {
        return (T) Enum.Parse(typeof (T), enumString);
    }

    public static string IntToString<T>(this int val)
    {
        // ((VehicleStatus)vehicle.Status).ToString(),
        return Enum.GetNames(typeof(T))[val];
    }
}