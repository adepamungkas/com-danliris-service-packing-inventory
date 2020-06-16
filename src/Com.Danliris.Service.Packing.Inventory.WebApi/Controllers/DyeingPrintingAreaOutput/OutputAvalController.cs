﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaOutput
{
    [Produces("application/json")]
    [Route("v1/output-aval")]
    [Authorize]
    public class OutputAvalController : ControllerBase
    {
        private readonly IOutputAvalService _service;
        private readonly IIdentityProvider _identityProvider;

        public OutputAvalController(IOutputAvalService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutputAvalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var excpetion = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(excpetion);
            }
            try
            {
                VerifyUser();
                var result = await _service.Create(viewModel);

                return Created("/", result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {

                var data = await _service.ReadById(id);
                return Ok(new
                {
                    data
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}",
            [FromQuery] string filter = "{}")
        {
            try
            {

                var data = _service.Read(page, size, filter, order, keyword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpGet("available-aval")]
        public IActionResult GetAvailableAval([FromQuery] DateTimeOffset searchDate,
                                              [FromQuery] string searchShift,
                                              [FromQuery] string searchGroup,
                                              [FromQuery] string keyword = null,
                                              [FromQuery] int page = 1,
                                              [FromQuery] int size = 25,
                                              [FromQuery] string order = "{}",
                                              [FromQuery] string filter = "{}")
        {
            var data = _service.ReadAvailableAval(searchDate, searchShift, searchGroup, page, size, filter, order, keyword);
            if (data == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else
            {
                return Ok(data);
            }
        }
        [HttpGet("available-aval/all")]
        public IActionResult GetAvailableAllAval([FromQuery] DateTimeOffset searchDate,
                                              [FromQuery] string searchShift,
                                              [FromQuery] string searchGroup,
                                              [FromQuery] string keyword = null,
                                              [FromQuery] int page = 1,
                                              [FromQuery] int size = 25,
                                              [FromQuery] string order = "{}",
                                              [FromQuery] string filter = "{}")
        {
            try
            {
                var data = _service.ReadAllAvailableAval(page, size, filter, order, keyword);
                if (data == null)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);

            }
        }

        [HttpGet("available-aval/{id}")]
        public IActionResult GetAvailableByBonAval([FromRoute]int id,
                                              [FromQuery] string keyword = null,
                                              [FromQuery] int page = 1,
                                              [FromQuery] int size = 25,
                                              [FromQuery] string order = "{}",
                                              [FromQuery] string filter = "{}")
        {
            try
            {


                var data = _service.ReadByBonAvailableAval(id, page, size, filter, order, keyword);
                if (data == null)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
                else
                {
                    return Ok(data);
                }
            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);

            }
        }

        [HttpGet("aval-summary-by-type")]
        public IActionResult GetSummaryAvalByType([FromQuery]string avalType,
                                              [FromQuery] string keyword = null,
                                              [FromQuery] int page = 1,
                                              [FromQuery] int size = 25,
                                              [FromQuery] string order = "{}",
                                              [FromQuery] string filter = "{}")
        {
            try
            {
                var data = _service.ReadByTypeAvailableAval(avalType, page, size, filter, order, keyword);
                if (data == null)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
                else
                {
                    return Ok(data);
                }
            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);

            }
        }

        [HttpGet("xls/{id}")]
        public async Task<IActionResult> GetExcel(int id)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = await _service.GenerateExcel(id);
                string filename = "Bon Keluar Aval Dyeing/Printing.xlsx";
                xlsInBytes = Result.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
