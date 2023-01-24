using GeekShooping.CouponAPI.Data.ValueObjects;
using GeekShooping.CouponAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _repository;
        private readonly ILogger<CouponController> _logger;

        public CouponController(ILogger<CouponController> logger, ICouponRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponVO>> FindByCode(string couponCode)
        {
            CouponVO coupon = await _repository.GetCouponByCouponCode(couponCode);
            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }
    }
}