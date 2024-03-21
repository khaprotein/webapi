using store_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace store_api.Controllers
{
    public class apiStoreController : ApiController
    {
        Store_apiEntities db = new Store_apiEntities();

        private static int cartCount = 1; // Biến tĩnh để đếm số lượng giỏ hàng
        private static int cartCount2 = 1;


        [HttpGet]
        [Route("api/apiStore/listProduct")]
        public List<Product> getallproduct()
        {
            return db.Products.ToList();
        }

        [HttpGet]
        [Route("api/apiStore/Product")]
        public Product getproduct(string product_id)
        {
            return db.Products.FirstOrDefault(c => c.product_id == product_id);
        }


        [HttpPost]
        [Route("api/apiStore/updateProduct")]
        public IHttpActionResult updateproduct(string product_id)
        {
            var sp = db.Products.SingleOrDefault(c => c.product_id == product_id);

            return Ok();
        }


        [HttpPost]
        [Route("api/apiStore/register")]
        public IHttpActionResult Register(User newuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Users.Add(newuser);
            db.SaveChanges();

            return Ok("Đăng ký tài khoản thành công!");
        }

        [HttpPost]
        [Route("api/apiStore/login")]
        public IHttpActionResult Login(Dangnhap tk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra xác thực người dùng dựa trên thông tin đăng nhập
            var user = db.Users.SingleOrDefault(u => u.email == tk.email && u.password == tk.pass);
            if (user == null)
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác.");
                return BadRequest(ModelState);
            }

            // Nếu thông tin đăng nhập chính xác, bạn có thể thực hiện các thao tác khác tại đây, chẳng hạn như tạo token, lưu trạng thái đăng nhập, vv.

            return Ok("Đăng nhập thành công!");
        }

        [HttpGet]
        [Route("api/apiStore/userbyid")]
        public User getuser(string iduser)
        {
            return db.Users.SingleOrDefault(c => c.user_id == iduser);
        }

        [HttpGet]
        [Route("api/apiStore/userbyid")]
        public Shopping_Cart get_cart(string iduser)
        {
            return db.Shopping_Cart.FirstOrDefault(c => c.user_id == iduser);
        }


        [HttpPut]
        [Route("api/apiStore/updateuser")]
        public IHttpActionResult UpdateUser(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Find the user in the database by user ID
            var userInDb = db.Users.SingleOrDefault(u => u.user_id == updatedUser.user_id);
            if (userInDb == null)
            {
                return NotFound(); // User not found
            }

            // Update the user information
            userInDb.name = updatedUser.name;
            userInDb.email = updatedUser.email;
            userInDb.phonenumber = updatedUser.phonenumber;
            userInDb.password = updatedUser.password;
            userInDb.address = updatedUser.address;
            // Update other fields as needed

            // Save changes to the database
            db.SaveChanges();

            return Ok("Cập nhật thông tin tài khoản thành công!");
        }

        [HttpPost]
        [Route("api/apiStore/AddToCart")]
        public IHttpActionResult AddToCart(AddToCartRequest request)
        {
            var sp = db.Products.SingleOrDefault(c => c.product_id == request.ProductId);
            if (sp != null)
            {
                // Kiểm tra xem số lượng sản phẩm có vượt quá số lượng tồn kho không
                if (sp.stockQuantity < request.Quantity)
                {
                    // Trả về lỗi khi số lượng sản phẩm trong giỏ hàng vượt quá số lượng tồn kho
                    return BadRequest("Số lượng sản phẩm trong giỏ hàng vượt quá số lượng tồn kho.");
                }
                var cart = db.Shopping_Cart.FirstOrDefault(c => c.product_id == request.ProductId && c.user_id == request.UserId);
                if (cart == null)
                {
                    // Tạo đối tượng Shopping_Cart để thêm vào cơ sở dữ liệu
                    var cartItem = new Shopping_Cart
                    {
                        cart_id = GenerateUniqueCartId(), // Tạo mã cart_id duy nhất
                        user_id = request.UserId,
                        product_id = request.ProductId,
                        quantity = request.Quantity
                    };

                    // Thêm sản phẩm vào giỏ hàng
                    db.Shopping_Cart.Add(cartItem);
                }
                else
                {
                    // Xử lý trường hợp sản phẩm đã có trong giỏ hàng
                    cart.quantity += request.Quantity;
                }
            }

            db.SaveChanges();
            return Ok();
        }


        [HttpGet]
        [Route("api/apiStore/GetCart")]
        public List<Shopping_Cart> GetCart(string userId)
        {
            return db.Shopping_Cart.Where(c => c.user_id == userId).ToList();
        }


        // Phương thức để tạo cart_id duy nhất
        public static string GenerateUniqueCartId()
        {

            string cartId = "Cart" + cartCount.ToString();

            cartCount++;

            return cartId;
        }
        

        [HttpPost]
        [Route("api/apiStore/UpdateCart")]
        public IHttpActionResult UpdateCart(AddToCartRequest request)
        {
            var cartItem = db.Shopping_Cart.FirstOrDefault(c => c.product_id == request.ProductId && c.user_id == request.UserId);
            if (cartItem == null)
            {
                return NotFound();
            }

            var product = db.Products.FirstOrDefault(p => p.product_id == request.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            // Kiểm tra xem số lượng yêu cầu có hợp lệ không
            if (request.Quantity <= 0)
            {
                return BadRequest("Invalid quantity.");
            }

            // Kiểm tra số lượng tồn kho
            if (request.Quantity > product.stockQuantity)
            {
                return BadRequest("Requested quantity exceeds stock quantity.");
            }

            // Cập nhật số lượng sản phẩm trong giỏ hàng
            cartItem.quantity = request.Quantity;

            db.SaveChanges();

            return Ok("Cart updated successfully.");
        }


        [HttpDelete]
        [Route("api/apiStore/deletecart")]
        public IHttpActionResult deleteCart(string cartid)
        {
            var cart = db.Shopping_Cart.FirstOrDefault(c => c.cart_id == cartid);

            db.Shopping_Cart.Remove(cart);
            db.SaveChanges();
            return Ok();
        }

       

        [HttpPost]
        [Route("api/apiStore/createOrder")]
        public IHttpActionResult CreateOrder(Hoadon dl)
        {
            var cart = db.Shopping_Cart.Where(c => c.user_id == dl.userID).ToList();
            
            var neworder = new Order
            {
                orders_id = CreateOrderID(),
                user_id = dl.userID,
                total_amount = dl.Tongtien,
                orders_date = DateTime.Now,
                shipping_address = dl.Diachi,
                user_phone = dl.SDT,
                oderstatus_id = 1
            };
            db.Orders.Add(neworder);
            db.SaveChanges();
            return Ok();
        }



        [HttpPost]
        [Route("api/apiStore/createOrderAndDetail")]
        public IHttpActionResult CreateOrderAndDetail(Hoadon dl)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Tạo đơn hàng
                    var newOrder = new Order
                    {
                        orders_id = CreateOrderID(),
                        user_id = dl.userID,
                        total_amount = dl.Tongtien,
                        orders_date = DateTime.Now,
                        shipping_address = dl.Diachi,
                        user_phone = dl.SDT,
                        oderstatus_id = 1
                    };
                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    List<Shopping_Cart> cartItems = db.Shopping_Cart.Where(c => c.user_id == dl.userID).ToList();
                   
                    // Tạo chi tiết đơn hàng cho từng sản phẩm trong giỏ hàng
                    foreach (var item in cartItems)
                    {
                        Order_Details newOD = new Order_Details
                        {
                            order_details_id = CreateOrderDetailId(),
                            order_id = newOrder.orders_id,
                            product_id = item.product_id,
                            quantity = item.quantity,
                            price_oder = item.Product.price
                        };
                        db.Order_Details.Add(newOD);
                        db.SaveChanges();
                    }
                   

                    // Xóa các sản phẩm trong giỏ hàng sau khi tạo đơn hàng thành công
                     db.Shopping_Cart.RemoveRange(cartItems);
                     db.SaveChanges();

                    dbContextTransaction.Commit();
                    return Ok("Đơn hàng và chi tiết đơn hàng đã được tạo thành công.");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return InternalServerError(ex);
                }
            }
        }



        // Hàm tạo mã đơn hàng tự động
        public string CreateOrderID()
        {
            var latestOrder = db.Orders.OrderByDescending(o => o.orders_id).FirstOrDefault();

            int orderNumber = 1; // Giá trị mặc định nếu không có đơn hàng nào trong cơ sở dữ liệu

            if (latestOrder != null)
            {
                // Lấy số thứ tự từ mã đơn hàng lớn nhất và tăng lên một đơn vị
                var match = Regex.Match(latestOrder.orders_id, @"Or_(?<number>\d*)");
                if (match.Success)
                {
                    if (int.TryParse(match.Groups["number"].Value, out orderNumber))
                    {
                        orderNumber++;
                    }
                }
            }

            string newOrderCode = "Or_" + orderNumber.ToString("D3"); // Tạo mã đơn hàng mới
            return newOrderCode;
        }

        // Hàm tạo mã đơn hàng chi tiết tự động
        public string CreateOrderDetailId()
        {
            try
            {
                var latestOrder = db.Order_Details.OrderByDescending(o => o.order_details_id).FirstOrDefault();

                int orderNumber = 1;

                if (latestOrder != null)
                {
                    var match = Regex.Match(latestOrder.order_details_id, @"OD_(?<number>\d*)");
                    if (match.Success)
                    {
                        if (int.TryParse(match.Groups["number"].Value, out orderNumber))
                        {
                            orderNumber++;
                        }
                    }
                }

                string newOrderCode = "OD_" + orderNumber.ToString("D4");
                return newOrderCode;
            }
            catch (Exception ex)
            {
                // Ghi nhật ký lỗi và xử lý ngoại lệ
                throw ex;
            }
        }

        [HttpGet]
        [Route("api/apiStore/getOrder")]
        public List<Order> getOrder(string id_user)
        {
            return db.Orders.Where(c => c.user_id == id_user).ToList();
        }

    }
}
