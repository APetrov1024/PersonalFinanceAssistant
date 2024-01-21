using AutoMapper;
using System;

namespace PersonalFinanceAssistant;

public class PersonalFinanceAssistantApplicationAutoMapperProfile : Profile
{
    public PersonalFinanceAssistantApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }

    protected static T ValueOrDefault<T, E>(E entity, string propPath, T defaultValue = default)
    {
        var properties = propPath.Split(".");
        object value = entity;
        var type = typeof(E);
        foreach (var propName in properties)
        {
            value = type.GetProperty(propName)?.GetValue(value);
            if (value == null)
            {
                return defaultValue == null ? default : (T)defaultValue;
            }
            type = value.GetType();
        }
        if (value is not T)
        {
            throw new ArgumentException($"Property \"{propPath}\" is not \"{typeof(T).FullName}\"");
        }
        return (T)value;
    }
}
