namespace EShoppingModel.Util
{
    using AutoMapper;
    using System;
    public class ObjectMap
    {
        public static IMapper MapperObject(Type source,Type destination)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(source,destination);
            });
            return configuration.CreateMapper();
        }
    }
}
