﻿namespace EShopping.Controllers
{
    using EShoppingModel.Dto;
    using EShoppingModel.Response;
    using EShoppingRepository.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    [Route("cart")]
    [ApiController]
    [Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class CartController : ControllerBase
    {
        public CartController(ICartService service)
        {
            this.CartService = service;
        }

        public ICartService CartService { get; set; }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
        {
            string CartData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                CartData = await Task.FromResult(CartService.AddToCart(cartDto,userId));
                if (!CartData.Contains("Not") && CartData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, CartData, cartDto, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, CartData, null, ""));
        }

        [HttpGet]
        public async Task<IActionResult> FetchCartBook()
        {
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                var CartData = await Task.FromResult(CartService.FetchCartBook(userId));
                if (CartData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Books Found", CartData, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Books Not Found", null, ""));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFromCartBook(int cartItemId)
        {
            string CartData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                CartData = await Task.FromResult(CartService.DeleteFromCartBook(cartItemId));
                if (!CartData.Contains("Not") && CartData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, CartData, cartItemId, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, CartData, cartItemId, ""));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartBookQuantity(int cartItemId,int quantity = 1)
        {
            string CartData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                CartData = await Task.FromResult(CartService.UpdateCartBookQuantity(cartItemId,quantity));
                if (!CartData.Contains("Not") && CartData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, CartData, cartItemId, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, CartData, cartItemId, ""));
        }
    }
}
