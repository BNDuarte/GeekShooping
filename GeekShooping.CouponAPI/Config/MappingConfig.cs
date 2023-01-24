using AutoMapper;
using GeekShooping.CouponAPI.Data.ValueObjects;
using GeekShooping.CouponAPI.Models;

namespace GeekShooping.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
