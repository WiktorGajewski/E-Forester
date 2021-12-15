﻿using E_Forester.Application.Content.Subareas.Commands.CreateSubareaCommand;
using E_Forester.Application.Content.Subareas.Queries.GetSubareasQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/subareas")]
    public class SubareasController : BaseController
    {
        public SubareasController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            var result = await _mediator.Send(new GetSubareasQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] CreateSubareaCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
