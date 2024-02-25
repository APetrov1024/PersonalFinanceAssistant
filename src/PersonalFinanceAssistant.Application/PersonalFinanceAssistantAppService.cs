using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Text.Json;
using PersonalFinanceAssistant.Localization;
using Volo.Abp.Application.Services;
using PersonalFinanceAssistant.CommonDtos;

namespace PersonalFinanceAssistant;

/* Inherit your application services from this class.
 */
public abstract class PersonalFinanceAssistantAppService : ApplicationService
{
    protected PersonalFinanceAssistantAppService()
    {
        LocalizationResource = typeof(PersonalFinanceAssistantResource);
    }

    protected IQueryable<T> FilterBy<T>(List<FilterDto> filters, IQueryable<T> query)
    {
        if (filters != null)
        {
            var propsLookup = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var config = new ParsingConfig { ResolveTypesBySimpleName = true };
            foreach (var filter in filters)
            {
                var propName = filter.Field.ToPascalCase();
                var prop = propsLookup.FirstOrDefault(p => p.Name == propName);
                if (prop != null)
                {
                    if (filter.Type.ToLower() == "like")
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            query = query.Where(propName + " != NULL AND " + propName + ".ToLower().Contains(@0.ToLower())", filter.Value.ToString().ToLower());
                        }
                        else
                        {
                            query = query.Where(propName + " != NULL AND string(object(" + propName + ")).ToLower().Contains(@0.ToLower())", filter.Value.ToString().ToLower());
                        }
                    }
                    if (filter.Type == "=")
                    {
                        if (prop.PropertyType == typeof(bool))
                        {
                            query = query.Where(propName + " != NULL AND " + propName + " == @0", filter.Value);
                        }
                        else
                        {
                            query = query.Where(propName + " != NULL AND string(object(" + propName + ")).ToLower() == @0.ToLower()", filter.Value.ToString().ToLower());
                        }
                    }
                    if (filter.Type.ToLower() == "between")
                    {
                        var min = ((JsonElement)filter.Value).GetProperty("min").GetString();
                        var max = ((JsonElement)filter.Value).GetProperty("max").GetString();
                        if (!min.IsNullOrWhiteSpace())
                        {
                            query = query.Where(propName + " >= @0", min);
                        };
                        if (!max.IsNullOrWhiteSpace())
                        {
                            query = query.Where(propName + " <= @0", max);
                        };
                    }
                }
            }
        }
        return query;
    }


}
