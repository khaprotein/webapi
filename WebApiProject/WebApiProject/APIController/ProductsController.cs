using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;
using System.Data.Entity;


namespace WebApiProject.APIController
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("api/Products/Getproduct")]
        public IHttpActionResult Getproduct()
        {
            Database1Entities1 db = new Database1Entities1();
            var list = db.Products.
                Select(p=>new Shoes{
                    product_id=p.product_id,
                    category_id = p.category_id,
                    product_name = p.product_name,
                    brand_id = p.brand_id,
                    price = p.price,
                    productimage_url =p.Product_image.productimage_url,
                    description = p.description,
                    detail = p.detail   
                }).ToList();
            if (list.Any())
            {
                return Ok(list);
            }
            else
            {
                return BadRequest();
            }
        }   

        [HttpGet]
        [Route("api/Products/Getproductbyid")]
        public IHttpActionResult Getproductbyid(string id_product)
        {
         Database1Entities1 db = new Database1Entities1();
            var p = db.Products.Where(c => c.product_id == id_product).FirstOrDefault();
            Shoes sp = new Shoes {
            product_id = p.product_id,
            category_id = p.category_id,
            product_name = p.product_name,
            brand_id = p.brand_id,
            price = p.price,
            productimage_url = p.Product_image.productimage_url,
            description = p.description,
            detail = p.detail
            };
            if (sp == null)
            {
                return NotFound(); // Trả về mã lỗi 404 Not Found nếu không tìm thấy sản phẩm
            }
            return Ok(sp); // Trả về sản phẩm nếu tìm thấy
        }

        [HttpGet]
        [Route("api/Products/Getproductbyid_category")]
        public IHttpActionResult Getproducttype(int id_category)
        {
            Database1Entities1 db = new Database1Entities1();
            var list = db.Products.Where(c=>c.category_id==id_category).
               Select(p => new Shoes
               {
                   product_id = p.product_id,
                   category_id = p.category_id,
                   product_name = p.product_name,
                   brand_id = p.brand_id,
                   price = p.price,
                   productimage_url = p.Product_image.productimage_url,
                   description = p.description,
                   detail = p.detail
               }).ToList();
            if (list == null)
            {
                return NotFound(); // Trả về mã lỗi 404 Not Found nếu không tìm thấy sản phẩm
            }
            return Ok(list); // Trả về sản phẩm nếu tìm thấy
        }


    }
}
