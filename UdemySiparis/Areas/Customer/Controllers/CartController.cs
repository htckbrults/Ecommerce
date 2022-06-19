using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UdemySiparis.Data.Repository.IRepository;
using UdemySiparis.Models.ViewModels;

namespace UdemySiparis.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartVM CartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentitiy = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentitiy.FindFirst(ClaimTypes.NameIdentifier);


            CartVM = new CartVM()
            {
                ListCart = _unitOfWork.Cart.GetAll(p => p.AppUserId == claim.Value, includeProperties: "Product"),
                OrderProduct = new()
            };
            foreach (var cart in CartVM.ListCart)
                
            {
                cart.Price = cart.Product.Price * cart.Count;
                CartVM.OrderProduct.OrderPrice += (cart.Price);
            }



            return View(CartVM);
        }
    }
}
