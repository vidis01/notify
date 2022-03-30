using NotifyAPI.DtoModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotifyAPI.Repositories
{
    public interface INotifyRepository
    {
        Task<Guid> CreateCompanyAsync(CompanyDto companyDto);
        IEnumerable<string> GetCompanySchedules(Guid companyId);
    }
}
