﻿using AutoMapper;
using DFC.App.JobProfileOverview.AutoMapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DFC.App.JobProfileOverview.IntegrationTests.AutoMapperTests
{
    public class AutoMapperProfileTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public AutoMapperProfileTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public void AutoMapperProfileConfigurationForJobProfileModelProfileReturnSuccess()
        {
            // Arrange
            _ = factory.CreateClient();
            var mapper = factory.Server.Host.Services.GetRequiredService<IMapper>();

            // Act
            mapper.ConfigurationProvider.AssertConfigurationIsValid<JobProfileOverviewSegmentProfile>();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void AutoMapperProfileConfigurationForApiModelProfileReturnSuccess()
        {
            // Arrange
            _ = factory.CreateClient();
            var mapper = factory.Server.Host.Services.GetRequiredService<IMapper>();

            // Act
            mapper.ConfigurationProvider.AssertConfigurationIsValid<ApiModelProfile>();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void AutoMapperProfileConfigurationForAllProfilesReturnSuccess()
        {
            // Arrange
            _ = factory.CreateClient();
            var mapper = factory.Server.Host.Services.GetRequiredService<IMapper>();

            // Act
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            // Assert
            Assert.True(true);
        }
    }
}