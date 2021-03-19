using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bouncer.Models;
using bouncer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bouncer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformController
    {
        private readonly ILogger<PlatformController> _logger;
        private readonly PlatformRepository _platformRepository;

        public PlatformController(ILogger<PlatformController> logger, PlatformRepository platformRepository)
        {
            _logger = logger;
            _platformRepository = platformRepository;
        }

        /// <summary>
        /// Endpoint to be called when creating a new platform
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<bool> Create([FromBody] Platform newPlatform)
        {
            try
            {
                await _platformRepository.Add(newPlatform);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        [HttpGet("{id}")]
        public async Task<Platform> Get(int id)
        {
            try
            {
                var result = await _platformRepository.Get(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("user/{id}")]
        public async Task<dynamic> GetByUser(int id)
        {
            try
            {
                var result = await _platformRepository.GetByOwnerId(id);
                return new { platforms = result };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}