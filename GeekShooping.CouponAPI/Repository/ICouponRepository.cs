using GeekShooping.CouponAPI.Data.ValueObjects;

namespace GeekShooping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}