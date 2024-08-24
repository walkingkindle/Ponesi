using PonesiWebApi.Interfaces;

namespace PonesiWebApi.Helpers
{
    public class CustomDtoMapper : ICustomDtoMapper
    {
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return Map<TSource, TDestination>(source, Activator.CreateInstance<TDestination>());
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source == null)
            return default(TDestination);

        foreach (var property in typeof(TDestination).GetProperties())
        {
            var sourceProperty = typeof(TSource).GetProperty(property.Name);
            if (sourceProperty != null)
            {
                var value = sourceProperty.GetValue(source);
                property.SetValue(destination, value);
            }
        }

        return destination;
    }
}
 
}
