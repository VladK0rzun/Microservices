using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.CouponAPI.Data;
using Services.CouponAPI.Models;
using Services.CouponAPI.Models.Dto;

namespace Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);
            }
            catch(Exception ex)
            {
               _response.IsSuccess = false;
               _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u=>u.CouponId == id);
                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDTO Post([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);
                _db.Coupons.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Put([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);
                _db.Coupons.Update(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u=>u.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
