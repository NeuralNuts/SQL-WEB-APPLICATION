﻿#region Imports
using Microsoft.AspNetCore.Mvc;
using SQL_WEB_APPLICATION.Context;
using SQL_WEB_APPLICATION.Models;
using SQL_WEB_APPLICATION.Models.Dto;
using SQL_WEB_APPLICATION.Models.Repository;
#endregion

#region Comment controller
namespace SQL_WEB_APPLICATION.Controllers
{
    [Controller]
    [Route("~/api/[controller]")]
    public class CommentController : Controller
    {
        #region Dapper intalized
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        #endregion

        #region Gets all comments 
        [HttpGet]
        [Route("GetComments")]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            
            return Ok(comments);
        }
        #endregion

        #region Creates new user comment 
        [HttpPost]
        [Route("PostComments")]
        public async Task<IActionResult> PostComments(CommentModel commentModel)
        {

            commentModel.created_date = DateTime.Now;
            await _commentRepository.PostUserComments(commentModel);
            return Ok();
        }
        #endregion

        #region Gets users comments 
        [HttpGet]
        [Route("GetUserComments")]
        public async Task<IActionResult> GetUserComments(CommentModel commentModel)
        {
            var user_comments = await _commentRepository.GetUserComments(commentModel);
            return Ok(user_comments);
        }
        #endregion
    }
}
#endregion