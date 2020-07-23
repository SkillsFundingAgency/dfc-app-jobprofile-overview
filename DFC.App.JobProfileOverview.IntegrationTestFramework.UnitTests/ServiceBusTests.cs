using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.Interface;
using FakeItEasy;
using Microsoft.Azure.ServiceBus;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.IntegrationTestFramework.UnitTests
{
    public class ServiceBusTests
    {
        private AppSettings appSettings;
        private Message message;
        private IServiceBusSupport serviceBus;
        private ITopicClient topicClient;
        private ITopicClientFactory topicClientFactory;

        [SetUp]
        public void Setup()
        {
            this.appSettings = new AppSettings();
            this.topicClient = A.Fake<ITopicClient>();
            this.appSettings.ServiceBusConfig.ConnectionString = "ConnectionString";
            this.message = new Message(Array.Empty<byte>());
            this.topicClientFactory = A.Fake<ITopicClientFactory>();
            A.CallTo(() => this.topicClientFactory.Create(this.appSettings.ServiceBusConfig.ConnectionString)).Returns(this.topicClient);
            this.serviceBus = new ServiceBusSupport(this.topicClientFactory, this.appSettings);
            A.CallTo(() => this.topicClient.SendAsync(this.message)).Returns(Task.CompletedTask);
        }

        [Test]
        public void OneSendMessageCallIsMadeToTheServiceBusTopicClient()
        {
            this.serviceBus.SendMessage(this.message);
            A.CallTo(() => this.topicClient.SendAsync(this.message)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void AppSettingsConnectionStringIsPassedToTheServiceBusTopicClient()
        {
            this.serviceBus.SendMessage(this.message);
            A.CallTo(() => this.topicClientFactory.Create(this.appSettings.ServiceBusConfig.ConnectionString)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CreateANewServiceBusMessage()
        {
            IMessageFactory messageFactory = new MessageFactory();
            Message message = messageFactory.Create("id", Array.Empty<byte>(), "action", "content");
            Assert.AreEqual("id", message.MessageId);
            Assert.AreEqual(Array.Empty<byte>(), message.Body);
            Assert.AreEqual("action", message.UserProperties["ActionType"]);
            Assert.AreEqual("content", message.UserProperties["CType"]);
        }
    }
}