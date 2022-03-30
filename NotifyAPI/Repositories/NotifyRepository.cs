using NotifyAPI.DtoModel;
using NotifyDb;
using NotifyDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyAPI.Repositories
{
    public class NotifyRepository : INotifyRepository
    {
        private readonly NotifyDBContext _database;

        public NotifyRepository(NotifyDBContext database)
        {
            _database = database;
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
            return _database.Schedules.Where(s => s.CompanyId == companyId).Select(s => s.NotificationDate.ToString("dd/mm/yyyy")).ToList();
        }

        private async Task AddCompanySchedules(Guid companyId, CompanyDto companyDto)
        {
            var daysDenmark = new[] { 1, 5, 10, 15, 20 };
            var daysNorway = new[] { 1, 5, 10, 20 };
            var daysSweden = new[] { 1, 7, 14, 28 };
            var daysFinland = new[] { 1, 5, 10, 15, 20 };

            switch (companyDto.Market)
            {
                case Market.Denmark:
                    foreach (var days in daysDenmark)
                    {
                        await _database.Schedules.AddAsync(
                            new()
                            {
                                CompanyId = companyId,
                                NotificationDate = companyDto.CallDate.AddDays(days)
                            });
                    }
                    break;
                case Market.Norway:
                    foreach (var days in daysNorway)
                    {
                        await _database.Schedules.AddAsync(
                            new()
                            {
                                CompanyId = companyId,
                                NotificationDate = companyDto.CallDate.AddDays(days)
                            });
                    }
                    break;
                case Market.Sweden:
                    if (companyDto.Type == CompanyType.Small || companyDto.Type == CompanyType.Medium)
                    {
                        foreach (var days in daysSweden)
                        {
                            await _database.Schedules.AddAsync(
                                new()
                                {
                                    CompanyId = companyId,
                                    NotificationDate = companyDto.CallDate.AddDays(days)
                                });
                        }
                    }
                    break;
                case Market.Finland:
                    if (companyDto.Type == CompanyType.Large)
                    {
                        foreach (var days in daysFinland)
                        {
                            await _database.Schedules.AddAsync(
                                new()
                                {
                                    CompanyId = companyId,
                                    NotificationDate = companyDto.CallDate.AddDays(days)
                                });
                        }
                    }
                    break;
                default:
                    break;
            }

            await _database.SaveChangesAsync();
        }
    }
}
