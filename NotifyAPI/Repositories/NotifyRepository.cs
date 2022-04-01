using NotifyAPI.DtoModel;
using NotifyDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyAPI.Repositories
{
    public class NotifyRepository : INotifyRepository
    {
        private readonly NotifyDBContext _database;
        private readonly NotificationSettings _notificationSettings;

        public NotifyRepository(NotifyDBContext database, NotificationSettings notificationSettings)
        {
            _database = database;
            _notificationSettings = notificationSettings;
        }

        public async Task<Guid> CreateCompanyAsync(CompanyDto companyDto)
        {
             var newCompany = await _database.Companies.AddAsync(
                new()
                {
                    Name = companyDto.Name,
                    Number = companyDto.Number,
                    Type = companyDto.Type,
                    Market  = companyDto.Market,
                    CallDate = companyDto.CallDate
                }
            );

            await _database.SaveChangesAsync();

            await AddCompanySchedules(newCompany.Entity.Id, companyDto);

            return newCompany.Entity.Id;
        }

        public IEnumerable<string> GetCompanySchedules(Guid companyId)
        {
            return _database.Schedules
                .Where(s => s.CompanyId == companyId)
                .Select(s => s.NotificationDate.ToString("dd/MM/yyyy")).ToList();
        }

        private async Task AddCompanySchedules(Guid companyId, CompanyDto companyDto)
        {
            var settings = _notificationSettings.Settings
                .SingleOrDefault(a => a.Market.ToUpper() == companyDto.Market.ToString().ToUpper());

            if (settings == null) return;
            
            if (settings.CompanyTypes.Contains((int)companyDto.Type))
            {
                foreach (var days in settings.NotificationDays)
                {
                    await _database.Schedules.AddAsync(
                        new()
                        {
                            CompanyId = companyId,
                            NotificationDate = companyDto.CallDate.AddDays(days)
                        });
                }
                await _database.SaveChangesAsync();
            }
        }        
    }
}
