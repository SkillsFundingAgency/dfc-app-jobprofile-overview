using DFC.App.JobProfileOverview.Data.Contracts;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Extensions;
using DFC.App.JobProfileOverview.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Controllers
{
    public class SegmentController : Controller
    {
        private readonly ILogger<SegmentController> logger;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;
        private readonly AutoMapper.IMapper mapper;

        public SegmentController(ILogger<SegmentController> logger, IJobProfileOverviewSegmentService jobProfileOverviewSegmentService, AutoMapper.IMapper mapper)
        {
            this.logger = logger;
            this.jobProfileOverviewSegmentService = jobProfileOverviewSegmentService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("/")]
        [Route("{controller}")]
        public async Task<IActionResult> Index()
        {
            logger.LogInformation($"{nameof(Index)} has been called");

            var viewModel = new IndexViewModel();
            var segmentModels = await jobProfileOverviewSegmentService.GetAllAsync().ConfigureAwait(false);

            if (segmentModels != null)
            {
                viewModel.Documents = segmentModels
                    .OrderBy(x => x.CanonicalName)
                    .Select(x => mapper.Map<IndexDocumentViewModel>(x))
                    .ToList();

                logger.LogInformation($"{nameof(Index)} has succeeded");
            }
            else
            {
                logger.LogWarning($"{nameof(Index)} has returned with no results");
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("{controller}/{article}")]
        public async Task<IActionResult> Document(string article)
        {
            logger.LogInformation($"{nameof(Document)} has been called with: {article}");

            var model = await jobProfileOverviewSegmentService.GetByNameAsync(article, Request.IsDraftRequest()).ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = mapper.Map<DocumentViewModel>(model);

                logger.LogInformation($"{nameof(Document)} has succeeded for: {article}");

                return View(viewModel);
            }

            logger.LogWarning($"{nameof(Document)} has returned no content for: {article}");

            return NoContent();
        }

        [HttpGet]
        [Route("{controller}/{article}/contents")]
        public async Task<IActionResult> Body(string article)
        {
            logger.LogInformation($"{nameof(Body)} has been called with: {article}");

            var model = await jobProfileOverviewSegmentService.GetByNameAsync(article, Request.IsDraftRequest()).ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = mapper.Map<BodyViewModel>(model);

                logger.LogInformation($"{nameof(Body)} has succeeded for: {article}");

                return this.NegotiateContentResult(viewModel, model.Data);
            }

            logger.LogWarning($"{nameof(Body)} has returned no content for: {article}");

            return NoContent();
        }

        [HttpPut]
        [HttpPost]
        [Route("{controller}")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]JobProfileOverviewSegmentModel createOrUpdateJobProfileOverviewModel)
        {
            logger.LogInformation($"{nameof(CreateOrUpdate)} has been called");

            if (createOrUpdateJobProfileOverviewModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCareerPathSegmentModel = await jobProfileOverviewSegmentService.GetByIdAsync(createOrUpdateJobProfileOverviewModel.DocumentId).ConfigureAwait(false);

            if (existingCareerPathSegmentModel == null)
            {
                var createdResponse = await jobProfileOverviewSegmentService.CreateAsync(createOrUpdateJobProfileOverviewModel).ConfigureAwait(false);

                logger.LogInformation($"{nameof(CreateOrUpdate)} has created content for: {createOrUpdateJobProfileOverviewModel.CanonicalName}");

                return new CreatedAtActionResult(nameof(Document), "Segment", new { article = createdResponse.CanonicalName }, createdResponse);
            }
            else
            {
                var updatedResponse = await jobProfileOverviewSegmentService.ReplaceAsync(createOrUpdateJobProfileOverviewModel).ConfigureAwait(false);

                logger.LogInformation($"{nameof(CreateOrUpdate)} has updated content for: {createOrUpdateJobProfileOverviewModel.CanonicalName}");

                return new OkObjectResult(updatedResponse);
            }
        }

        [HttpDelete]
        [Route("{controller}/{documentId}")]
        public async Task<IActionResult> Delete(Guid documentId)
        {
            logger.LogInformation($"{nameof(Delete)} has been called");

            var jobProfileOverviewSegmentModel = await jobProfileOverviewSegmentService.GetByIdAsync(documentId).ConfigureAwait(false);

            if (jobProfileOverviewSegmentModel == null)
            {
                logger.LogWarning($"{nameof(Document)} has returned no content for: {documentId}");

                return NotFound();
            }

            await jobProfileOverviewSegmentService.DeleteAsync(documentId, jobProfileOverviewSegmentModel.PartitionKey).ConfigureAwait(false);

            logger.LogInformation($"{nameof(Delete)} has deleted content for: {jobProfileOverviewSegmentModel.CanonicalName}");

            return Ok();
        }
    }
}
