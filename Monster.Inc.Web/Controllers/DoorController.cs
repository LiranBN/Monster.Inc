using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Monster.Inc.Web.Models;
using Monster.Inc.Web.Repository.Interfaces;

namespace Monster.Inc.Web.Controllers
{
    [Authorize(Roles = Role.DoorsMang)]
    [Route("api/Door")]
    [ApiController]
    public class DoorController : ControllerBase
    {
        private readonly IDoorRepository doorRepository;
        private readonly ILogger logger;

        public DoorController(IDoorRepository doorRepository, ILoggerFactory loggerFactory)
        {
            this.doorRepository = doorRepository;
            logger = loggerFactory.CreateLogger("DoorController");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoor(int id)
        {
            try
            {
                var item = await doorRepository.GetDoorAsync(id);
                return Ok(item);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return StatusCode(500);
            }
        }

        [Authorize(Roles = Role.DoorsMang + "," + Role.Intimidiator)]
        [HttpGet]
        public async Task<IActionResult> GetDoors()
        {
            try
            {
                var items = await doorRepository.GetDoorsAsync();
                return Ok(items);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Door item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newItem = await doorRepository.CreateDoorAsync(item);

                return StatusCode(201, item);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Door item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newItem = await doorRepository.UpdateDoorAsync(item);

                return Ok(item);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await doorRepository.DeleteDoorAsync(id);
                return Ok();
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return StatusCode(500);
            }
        }
    }
}
