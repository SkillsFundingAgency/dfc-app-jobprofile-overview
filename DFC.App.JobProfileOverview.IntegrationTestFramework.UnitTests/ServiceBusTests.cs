using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.AzureServiceBus;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.Interface;
using FakeItEasy;
using Microsoft.Azure.ServiceBus;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.IntegrationTestFramework.UnitTests
{
    public class ServiceBusTests
    {
        private AppSettings appSettings;
        private Message message;
        private IServiceBus serviceBus;
        private ITopicClient topicClient;
        private ITopicClientFactory topicClientFactory;

        [SetUp]
        public void Setup()
        {
            this.appSettings = new AppSettings();
            this.topicClient = A.Fake<ITopicClient>();
            this.appSettings.ServiceBusConfig.ConnectionString = "ConnectionString";
            this.message = new Message(new byte[] { });
            this.topicClientFactory = A.Fake<ITopicClientFactory>();
            A.CallTo(() => this.topicClientFactory.Create(this.appSettings.ServiceBusConfig.ConnectionString)).Returns(this.topicClient);
            this.serviceBus = new ServiceBus(this.topicClientFactory, this.appSettings);
            A.CallTo(() => this.topicClient.SendAsync(this.message)).Returns(Task.CompletedTask);
        }

        [Test]
        public void OneSendMessageCallIsMadeToTheServiceBusTopicClient()
        {
            this.serviceBus.SendMessage(this.message);
            A.CallTo(() => this.topicClient.SendAsync(this.message)).MustHaveHappenedOnceExactly();
        }
    }
}