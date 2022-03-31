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

        [HttpGet("{id}")]
        public IEnumerable<string> Get(Guid id)
        {
            return _notifyRepository.GetCompanySchedules(id);
        }

        [HttpPost]
        public async Task<Guid> Post([FromBody] CompanyDto companyDto)
        {
            return await _notifyRepository.CreateCompanyAsync(companyDto);
        }
    }
}
