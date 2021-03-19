using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using bouncer.DTO;
using bouncer.DTO.User;
using bouncer.Models;
using bouncer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bouncer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _userRepository;

        public UserController(ILogger<UserController> logger,
        UserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var requestUser = User.FromRegisterRequest(request);
                bool success = await _userRepository.Add(requestUser) != null;
                if (success)
                {
                    return new OkObjectResult(new 
                    { 
                        success = true, 
                        message = "OK"
                    });
                }
                else
                {
                    return new BadRequestObjectResult(new 
                    { 
                        success = false, 
                        message = "Invalid data" 
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Microsoft.AspNetCore.Mvc.StatusCodeResult(500);
            }
        }


        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            try
            {
                var result = await _userRepository.Get(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}