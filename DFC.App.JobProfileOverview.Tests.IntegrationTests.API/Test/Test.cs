using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support;
using NUnit.Framework;
using System.Threading.Tasks;
using static DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.EnumLibrary;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Test
{
    public class Test : SetUpAndTearDown
    {
        [Test]
        public async Task ProofOfConcept()
        {
            SOCCodeContentType socCodeContentType = CommonAction.GenerateSOCCodeContentType();
            byte[] messageBody = CommonAction.ConvertObjectToByteArray(socCodeContentType);
            CommonAction.CreateServiceBusMessage(socCodeContentType.Id, messageBody, ContentType.HTML, ActionType.Published, CType.JobProfileSoc);

        }
    }
}