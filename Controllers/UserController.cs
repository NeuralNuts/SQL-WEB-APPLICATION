﻿using Microsoft.AspNetCore.Mvc;
using SQL_WEB_APPLICATION.Context;
using SQL_WEB_APPLICATION.Models;
using SQL_WEB_APPLICATION.Models.Repository;

namespace SQL_WEB_APPLICATION.Controllers
{
    [Controller]
    [Route("~/api/[controller]")]

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //[HttpGet]
        //public async Task<IActionResult> CheckLoginInfo()
        //{
        //    var add = await _userRepository.CheckLoginInfo();
        //    return Ok(add);
        //}

        [HttpGet]
        [Route("AuthenticateLogin")]
        public async Task<IActionResult> AuthenticateLogin(UserModel? userModel)
        {
            string message;
            var loginStatus = _userRepository.GetUsers().Result.Where(m => m.email.Trim() == userModel.email &&
                                                                                          m.password.Trim() == userModel.password).FirstOrDefault();
            if (loginStatus != null)
            {
                message = "LOGIN VALID";
            }
            else
            {
                message = "LOGIN INVALID";
            }
            return Json(message);
        }

        [HttpGet]
        [Route("CheckLogin")]
        public async Task<IActionResult> CheckLogin(UserModel? userModel)
        {
            string message;
            var checkStatus = _userRepository.CheckUsers().Result.Where(m => m.email.Trim() == userModel.email &&
                                                                                          m.password.Trim() == userModel.password).FirstOrDefault();
            if (checkStatus != null)
            {
                message = "LOGIN VALID";
                return Ok(checkStatus.user_id);
            }
            else
            {
                message = "LOGIN INVALID";
            }
            return Json(message);
        }

        [HttpGet]
        [Route("CheckUser")]
        public async Task<IActionResult> CheckUser(UserModel? userModel)
        {
            string message;
            var checkStatus = _userRepository.CheckUsers().Result.Where(m => m.email.Trim() == userModel.email).FirstOrDefault();
            if (checkStatus == null)
            {
                message = "LOGIN VALID";
                await _userRepository.PostUser(userModel);
            }
            else
            {
                message = "LOGIN INVALID";
            }
            return Json(message);
        }

        //[HttpPost]
        //[Route("PostUser")]
        //public async Task<IActionResult> PostUser([FromBody] UserModel userModel)
        //{
        //    string message;
        //    var checkStatus = _userRepository.CheckUsers().Result.Where(m => m.email.Trim() == userModel.email).FirstOrDefault();
        //    if (checkStatus != null)
        //    {
        //        await _userRepository.PostUser(userModel);
        //        message = "LOGIN VALID";
        //    }
        //    else
        //    {
        //        message = "LOGIN INVALID";
        //    }
        //    return Json(message);
        //}
    }
}
