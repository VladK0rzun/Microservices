using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.MessageBus;
using Services.ShoppingCartAPI.Data;
using Services.ShoppingCartAPI.Models;
using Services.ShoppingCartAPI.Models.Dto;
using Services.ShoppingCartAPI.Service.IService;
using System.Reflection.PortableExecutable;

namespace Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {

        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;
        private ICouponService _couponService;
        private IConfiguration _configuration;
        private readonly IMessageBus _messageBus;

        public CartAPIController( IMapper mapper, AppDbContext db, IProductService productService, ICouponService couponService
            , IMessageBus messageBus, IConfiguration configuration)
        {
            _db = db;
            this._response = new ResponseDTO();
            _mapper = mapper;
            _productService = productService;
            _couponService = couponService;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDTO> GetCart(string userId)
        {
            try
            {
                CartDTO cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDTO>(_db.CartHeaders.First(u => u.UserId == userId))
                };
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDTO>>(_db.CartDetails
                    .Where(u=>u.CartHeaderId == cart.CartHeader.CartHeaderId));

                IEnumerable<ProductDTO> productDTOs = await _productService.GetProducts();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDTOs.FirstOrDefault(u=>u.ProductId==item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }
                //apply coupon
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDTO coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if(coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount) 
                    {
                        cart.CartHeader.CartTotal -= coupon.CouponAmount;
                        cart.CartHeader.Discount = coupon.CouponAmount;
                    }
                }

                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody]CartDTO cartDTO)
        {
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u=>u.UserId == cartDTO.CartHeader.UserId);
                cartFromDb.CouponCode = cartDTO.CartHeader.CouponCode;
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch(Exception ex) 
            {
                _response.IsSuccess=false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }

        [HttpPost("EmailCartRequest")]
        public async Task<object> EmailCartRequest([FromBody] CartDTO cartDTO)
        {
            try
            {
                await _messageBus.PublishMessage(cartDTO, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue"));
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }



        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> CartUpsert(CartDTO cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //if header is not null
                    //check if details has same product
                    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cartDto.CartDetails.First().ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        //create cartdetails
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDTO> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails.First(u=>u.CartDetailsId == cartDetailsId);
                int totalCountCartItem =  _db.CartDetails.Where(u=>u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
               if(totalCountCartItem == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(u=>u.CartHeaderId==cartDetails.CartHeaderId);

                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }
               await _db.SaveChangesAsync();
                
                
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
