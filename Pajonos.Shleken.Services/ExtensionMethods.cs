using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class ExtensionMethods
    {
        public static List<TResult> Map<TSource, TResult>(this IEnumerable<TSource> data) where TResult : new()
        {
            return data.Select(i => i.Map<TSource, TResult>()).ToList();
        }

        public static TResult Map<TSource, TResult>(this TSource data, TResult result = default(TResult)) where TResult : new()
        {
            var proerties = typeof(TResult).GetProperties();

            if (result == null)
                result = new TResult();

            foreach (var property in proerties)
            {
                try
                {
                    var sourceProperty = data.GetType().GetProperty(property.Name);
                    if (sourceProperty == null) { continue; }
                    var resultProperty = result.GetType().GetProperty(property.Name);
                    var value = sourceProperty.GetValue(data);
                    resultProperty.SetValue(result, value);
                }
                catch { }
            }

            return result;
        }
    }
}
