using Microsoft.AspNetCore.Mvc;
using NotifyAPI.DtoModel;
using NotifyAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotityController : ControllerBase
    {
        private readonly INotifyRepository _notifyRepository;

        public NotityController(INotifyRepository notifyRepository)
        {
            _notifyRepository = notifyRepository;
        }

        // GET api/<NotityController>/5
        [HttpGet("{id}")]
        public IEnumerable<string> Get(Guid id)
        {
            return _notifyRepository.GetCompanySchedules(id);
        }

        // POST api/<NotityController>
        [HttpPost]
        public async Task<Guid> Post([FromBody] CompanyDto companyDto)
        {
            return await _notifyRepository.CreateCompanyAsync(companyDto);
        }
    }
}
