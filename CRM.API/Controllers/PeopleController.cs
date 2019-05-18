using CRM.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        IPersonService _service;

        public PeopleController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(Int64? version)
        {
            if (version == null)
            {
                var data = _service.GetPeopleAll();
                return Ok(data);
            }
            else
            {
                var data = _service.GetPeopleChanges(version.Value);
                return Ok(data);
            }
        }
    }
}
