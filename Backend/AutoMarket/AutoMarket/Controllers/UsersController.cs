using AutoMarket.Models;
using AutoMarket.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AutoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();


        [HttpPost("login")]
        public ActionResult<GenericReply> Login(GenericRequest<LoginRequest> loginRequest)
        {
            GenericReply loginReply = new GenericReply();

            if (loginRequest.SystemToken != "Ank")
            {
                loginReply.Success = false;
                loginReply.Error = "Token invalid";

                return loginReply;
            }

            var user = _userService.Get(loginRequest.Data.Username);

            if (user != null && PasswordHashService.Verify(loginRequest.Data.Password, user.Password))
            {
                loginReply.Success = true;
            }
            else
            {
                loginReply.Success = false;
                loginReply.Error = "Username/Password incorrect";
            }

            return loginReply;
        }

        [HttpDelete]
        public ActionResult<GenericReply> Delete(GenericRequest<LoginRequest> request)
        {
            GenericReply reply = new GenericReply();

            try
            {
                _userService.Remove(request.Data.Username);
                reply.Success = true;
            }
            catch (Exception e)
            {
                reply.Success = false;
                reply.Error = e.ToString();
            }

            return reply;
        }

        [HttpPost("signUp")]
        public ActionResult<GenericReply> SignUp(GenericRequest<User> signUpRequest)
        {
            GenericReply signUpReply = new GenericReply();

            if (signUpRequest.SystemToken != "Ank")
            {
                signUpReply.Success = false;
                signUpReply.Error = "Token invalid";

                return signUpReply;
            }


            signUpRequest.Data.Password = PasswordHashService.Hash(signUpRequest.Data.Password);

            try
            {
                _userService.Create(signUpRequest.Data);
                signUpReply.Success = true;

                return signUpReply;
            }
            catch (Exception e)
            {
                signUpReply.Success = false;
                signUpReply.Error = e.ToString();

                return signUpReply;
            }
        }
    }
}