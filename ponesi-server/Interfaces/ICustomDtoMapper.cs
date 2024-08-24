namespace PonesiWebApi.Interfaces
{
    public interface ICustomDtoMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }

}
