using Microsoft.EntityFrameworkCore;
using NotifyAPI;
using NotifyAPI.DtoModel;
using NotifyAPI.Repositories;
using NotifyDb;
using NotifyDB.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace NotifyTests
{
    public class NotifyRepositoryTests
    {
        private readonly DbContextOptions<NotifyDBContext> _dbContextOptions;
        private readonly NotificationSettings _notificationSettings;

        public NotifyRepositoryTests()
        {
            var dbName = $"TestDb_{DateTime.Now}";
            _dbContextOptions = new DbContextOptionsBuilder<NotifyDBContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            _notificationSettings = new NotificationSettings
            {
                Settings = new NotificationSetting[]
                {
                    new()
                    {
                        Market = "Denmark",
                        CompanyTypes = new[]{ 0, 1, 2},
                        NotificationDays = new[]{  1, 5, 10, 15, 20 }
                    },
                    new()
                    {
                        Market = "Norway",
                        CompanyTypes = new[]{ 0, 1, 2},
                        NotificationDays = new[]{ 1, 5, 10, 20 }
                    },
                    new()
                    {
                        Market = "Sweden",
                        CompanyTypes = new[]{ 0, 1 },
                        NotificationDays = new[]{ 1, 7, 14, 28 }
                    },
                    new()
                    {
                        Market = "Finland",
                        CompanyTypes = new[]{ 2 },
                        NotificationDays = new[]{ 1, 5, 10, 15, 20 }
                    }
                }
            };
        }        

        [Fact]
        public async void CreateCompanyWithSchedulesTest()
        {
            var repository = CreateRepository();

            var callDate = DateTime.Now;

            var companyDto = new CompanyDto
            {
                Name = "CompanyName",
                Number = "1234567890",
                Type = CompanyType.Small,
                Market = Market.Denmark,
                CallDate = callDate
            };

            var companyId = await repository.CreateCompanyAsync(companyDto);
            var schedules = repository.GetCompanySchedules(companyId) as List<string>;

            Assert.True(companyId != Guid.Empty);
            Assert.Equal(5, schedules.Count);

            for (int i = 0; i < schedules.Count; i++)
            {
                Assert.Equal(
                    callDate.AddDays(_notificationSettings.Settings[0].NotificationDays[i]).ToString("dd/MM/yyyy"), schedules[i]);
            }
        }

        [Fact]
        public async void CreateCompanyWithNoSchedules()
        {
            var repository = CreateRepository();

            var callDate = DateTime.Now;

            var companyDto = new CompanyDto
            {
                Name = "CompanyName",
                Number = "1234567890",
                Type = CompanyType.Small,
                Market = Market.Finland,
                CallDate = callDate
            };

            var companyId = await repository.CreateCompanyAsync(companyDto);
            var schedules = repository.GetCompanySchedules(companyId) as List<string>;

            Assert.True(companyId != Guid.Empty);
            Assert.Empty(schedules);
        }

        private NotifyRepository CreateRepository()
        {
            var context = new NotifyDBContext(_dbContextOptions);
            return new NotifyRepository(context, _notificationSettings);
        }
    }
}
