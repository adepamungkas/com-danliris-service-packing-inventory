﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Transit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/transit-area-note")]
    [Authorize]
    public class TransitAreaNoteController : ControllerBase
    {
        private readonly ITransitAreaNoteService _service;
        private readonly IIdentityProvider _identityProvider;

        public TransitAreaNoteController(ITransitAreaNoteService service, IIdentityProvider identityProvider)
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

        [HttpGet]
        public IActionResult GetTransitAreaNote(DateTimeOffset? searchDate, string zone, string group, string mutation)
        {
            try
            {
                VerifyUser();
                int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GetReport(searchDate, zone, group, mutation, clientTimeZoneOffset);

                return Ok(new
                {
                    data = Result
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("xls")]
        public IActionResult GetTransitAreaNoteExcel(DateTimeOffset? searchDate, string zone, string group, string mutation)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GenerateExcel(searchDate, zone, group, mutation, clientTimeZoneOffset);
                string filename = "Transit Area Note Dyeing/Printing.xlsx";
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
