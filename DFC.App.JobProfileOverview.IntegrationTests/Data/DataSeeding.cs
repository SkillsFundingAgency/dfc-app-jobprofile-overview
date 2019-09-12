using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.IntegrationTests.Data
{
    public class DataSeeding
    {
        private const string Segment = "segment";

        public DataSeeding()
        {
            Article1Id = Guid.Parse("63DEA97E-B61C-4C14-15DC-1BD08EA20DC8");
            Article2Id = Guid.Parse("C16B389D-91AD-4F3D-2485-9F7EE953AFE4");
            Article3Id = Guid.Parse("C0103C26-E7C9-4008-3F66-1B2DB192177E");

            Article1Name = "article1";
            Article2Name = "article2";
            Article3Name = "article3";
        }

        public Guid Article1Id { get; private set; }

        public Guid Article2Id { get; private set; }

        public Guid Article3Id { get; private set; }

        public string Article1Name { get; private set; }

        public string Article2Name { get; private set; }

        public string Article3Name { get; private set; }

        public async Task AddData(CustomWebApplicationFactory<Startup> factory)
        {
            var url = $"/{Segment}";
            var models = CreateModels();

            var client = factory?.CreateClient();

            client.DefaultRequestHeaders.Accept.Clear();

            foreach (var model in models)
            {
                await client.PostAsync(url, model, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            }
        }

        public async Task RemoveData(CustomWebApplicationFactory<Startup> factory)
        {
            var models = CreateModels();

            var client = factory?.CreateClient();

            client.DefaultRequestHeaders.Accept.Clear();

            foreach (var model in models)
            {
                var url = string.Concat("/", Segment, "/", model.DocumentId);
                await client.DeleteAsync(url).ConfigureAwait(false);
            }
        }

        private List<JobProfileOverviewSegmentModel> CreateModels()
        {
            var article = "article";
            var created = DateTime.Now;
            var models = new List<JobProfileOverviewSegmentModel>()
            {
                new JobProfileOverviewSegmentModel()
                {
                    Created=created,
                    DocumentId = Article1Id,
                    CanonicalName = Article1Name,
                    Data = new JobProfileOverviewSegmentDataModel
                    {
                    },
                },
                new JobProfileOverviewSegmentModel()
                {
                    Created=created,
                    DocumentId = Article2Id,
                    CanonicalName = Article2Name,
                    Data = new JobProfileOverviewSegmentDataModel
                    {
                    },
                },
                new JobProfileOverviewSegmentModel()
                {
                    Created=created,
                    DocumentId = Article3Id,
                    CanonicalName =Article3Name,
                    Data = new JobProfileOverviewSegmentDataModel
                    {
                    },
                },
            };

            return models;
        }
    }
}
