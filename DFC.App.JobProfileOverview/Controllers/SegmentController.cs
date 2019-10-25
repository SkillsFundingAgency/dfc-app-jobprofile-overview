using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.Extensions;
using DFC.App.JobProfileOverview.SegmentService;
using DFC.App.JobProfileOverview.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Controllers
{
    public class SegmentController : Controller
    {
        public const string SegmentRoutePrefix = "segment";
        public const string JobProfileRoutePrefix = "job-profiles";
        private const string IndexActionName = nameof(Index);
        private const string DocumentActionName = nameof(Document);
        private const string BodyActionName = nameof(Body);
        private const string PostActionName = nameof(Post);
        private const string PutActionName = nameof(Put);
        private const string DeleteActionName = nameof(Delete);
        private const string PatchWorkingPatternActionName = nameof(PatchWorkingPattern);
        private const string PatchHiddenAlternativeTitleActionName = nameof(PatchHiddenAlternativeTitle);
        private const string PatchJobProfileSpecialismActionName = nameof(PatchJobProfileSpecialism);
        private const string PatchWorkingHoursDetailActionName = nameof(PatchWorkingHoursDetail);
        private const string PatchWorkingPatternDetailActionName = nameof(PatchWorkingPatternDetail);
        private const string PatchSocCodeDataActionName = nameof(PatchSocCodeData);

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
            logger.LogInformation($"{IndexActionName} has been called");

            var viewModel = new IndexViewModel();
            var segmentModels = await jobProfileOverviewSegmentService.GetAllAsync().ConfigureAwait(false);

            if (segmentModels != null)
            {
                viewModel.Documents = segmentModels
                    .OrderBy(x => x.CanonicalName)
                    .Select(x => mapper.Map<IndexDocumentViewModel>(x))
                    .ToList();

                logger.LogInformation($"{IndexActionName} has succeeded");
            }
            else
            {
                logger.LogWarning($"{IndexActionName} has returned with no results");
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("{controller}/{article}")]
        public async Task<IActionResult> Document(string article)
        {
            logger.LogInformation($"{DocumentActionName} has been called with: {article}");

            var model = await jobProfileOverviewSegmentService.GetByNameAsync(article, Request.IsDraftRequest()).ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = mapper.Map<DocumentViewModel>(model);

                viewModel.Data.Breadcrumb = BuildBreadcrumb(model, SegmentRoutePrefix);

                logger.LogInformation($"{DocumentActionName} has succeeded for: {article}");

                return View(viewModel);
            }

            logger.LogWarning($"{DocumentActionName} has returned no content for: {article}");

            return NoContent();
        }

        [HttpGet]
        [Route("{controller}/{documentId}/contents")]
        public async Task<IActionResult> Body(Guid documentId)
        {
            logger.LogInformation($"{BodyActionName} has been called with: {documentId}");

            var model = await jobProfileOverviewSegmentService.GetByIdAsync(documentId).ConfigureAwait(false);
            if (model != null)
            {
                var viewModel = mapper.Map<BodyViewModel>(model);

                viewModel.Data.Breadcrumb = BuildBreadcrumb(model, JobProfileRoutePrefix);

                logger.LogInformation($"{BodyActionName} has succeeded for: {documentId}");

                var test = this.NegotiateContentResult(viewModel, model.Data);
                return test;
            }

            logger.LogWarning($"{BodyActionName} has returned no content for: {documentId}");

            return NoContent();
        }

        [HttpPost]
        [Route("segment")]
        public async Task<IActionResult> Post([FromBody]JobProfileOverviewSegmentModel jobProfileOverviewSegmentModel)
        {
            logger.LogInformation($"{PostActionName} has been called");

            if (jobProfileOverviewSegmentModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDocument = await jobProfileOverviewSegmentService.GetByIdAsync(jobProfileOverviewSegmentModel.DocumentId).ConfigureAwait(false);
            if (existingDocument != null)
            {
                return new StatusCodeResult((int)HttpStatusCode.AlreadyReported);
            }

            var response = await jobProfileOverviewSegmentService.UpsertAsync(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            logger.LogInformation($"{PostActionName} has upserted content for: {jobProfileOverviewSegmentModel.CanonicalName}");

            return new StatusCodeResult((int)response);
        }

        [HttpPut]
        [Route("segment")]
        public async Task<IActionResult> Put([FromBody]JobProfileOverviewSegmentModel jobProfileOverviewSegmentModel)
        {
            logger.LogInformation($"{PutActionName} has been called");

            if (jobProfileOverviewSegmentModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDocument = await jobProfileOverviewSegmentService.GetByIdAsync(jobProfileOverviewSegmentModel.DocumentId).ConfigureAwait(false);
            if (existingDocument == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            if (jobProfileOverviewSegmentModel.SequenceNumber <= existingDocument.SequenceNumber)
            {
                return new StatusCodeResult((int)HttpStatusCode.AlreadyReported);
            }

            jobProfileOverviewSegmentModel.Etag = existingDocument.Etag;
            jobProfileOverviewSegmentModel.SocLevelTwo = existingDocument.SocLevelTwo;

            var response = await jobProfileOverviewSegmentService.UpsertAsync(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            return new StatusCodeResult((int)response);
        }

        [HttpPatch]
        [Route("segment/{documentId}/workingPattern")]
        public async Task<IActionResult> PatchWorkingPattern([FromBody]PatchWorkingPatternModel patchWorkingPatternModel, Guid documentId)
        {
            logger.LogInformation($"{PatchWorkingPatternActionName} has been called");

            if (patchWorkingPatternModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await jobProfileOverviewSegmentService.PatchWorkingPatternAsync(patchWorkingPatternModel, documentId).ConfigureAwait(false);
            if (response != HttpStatusCode.OK && response != HttpStatusCode.Created)
            {
                logger.LogError($"{PatchWorkingPatternActionName}: Error while patching Working Pattern content for Job Profile with Id: {patchWorkingPatternModel.JobProfileId} for the {patchWorkingPatternModel.Title} pattern");
            }

            return new StatusCodeResult((int)response);
        }

        [HttpPatch]
        [Route("segment/{documentId}/hiddenAlternativeTitle")]
        public async Task<IActionResult> PatchHiddenAlternativeTitle([FromBody]PatchHiddenAlternativeTitleModel patchHiddenAlternativeTitleModel, Guid documentId)
        {
            logger.LogInformation($"{PatchHiddenAlternativeTitleActionName} has been called");

            if (patchHiddenAlternativeTitleModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await jobProfileOverviewSegmentService.PatchHiddenAlternativeTitleAsync(patchHiddenAlternativeTitleModel, documentId).ConfigureAwait(false);
            if (response != HttpStatusCode.OK && response != HttpStatusCode.Created)
            {
                logger.LogError($"{PatchHiddenAlternativeTitleActionName}: Error while patching Hidden Alternative Title content for Job Profile with Id: {patchHiddenAlternativeTitleModel.JobProfileId} for the {patchHiddenAlternativeTitleModel.Title} title");
            }

            return new StatusCodeResult((int)response);
        }

        [HttpPatch]
        [Route("segment/{documentId}/jobProfileSpecialism")]
        public async Task<IActionResult> PatchJobProfileSpecialism([FromBody]PatchJobProfileSpecialismModel patchJobProfileSpecialismModel, Guid documentId)
        {
            logger.LogInformation($"{PatchJobProfileSpecialismActionName} has been called");

            if (patchJobProfileSpecialismModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await jobProfileOverviewSegmentService.PatchJobProfileSpecialismAsync(patchJobProfileSpecialismModel, documentId).ConfigureAwait(false);
            if (response != HttpStatusCode.OK && response != HttpStatusCode.Created)
            {
                logger.LogError($"{PatchJobProfileSpecialismActionName}: Error while patching Specialism content for Job Profile with Id: {patchJobProfileSpecialismModel.JobProfileId} for the {patchJobProfileSpecialismModel.Title} specialism");
            }

            return new StatusCodeResult((int)response);
        }

        [HttpPatch]
        [Route("segment/{documentId}/workingHoursDetail")]
        public async Task<IActionResult> PatchWorkingHoursDetail([FromBody]PatchWorkingHoursDetailModel patchWorkingHoursDetailModel, Guid documentId)
        {
            logger.LogInformation($"{PatchWorkingHoursDetailActionName} has been called");

            if (patchWorkingHoursDetailModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchWorkingHoursDetailModel, documentId).ConfigureAwait(false);
            if (response != HttpStatusCode.OK && response != HttpStatusCode.Created)
            {
                logger.LogError($"{PatchWorkingHoursDetailActionName}: Error while patching Working Hours Detail content for Job Profile with Id: {patchWorkingHoursDetailModel.JobProfileId} for the {patchWorkingHoursDetailModel.Title} hours detail");
            }

            return new StatusCodeResult((int)response);
        }

        [HttpPatch]
        [Route("segment/{documentId}/workingPatternDetail")]
        public async Task<IActionResult> PatchWorkingPatternDetail([FromBody]PatchWorkingPatternDetailModel patchWorkingPatternDetailModel, Guid documentId)
        {
            logger.LogInformation($"{PatchWorkingPatternDetailActionName} has been called");

            if (patchWorkingPatternDetailModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchWorkingPatternDetailModel, documentId).ConfigureAwait(false);
            if (response != HttpStatusCode.OK && response != HttpStatusCode.Created)
            {
                logger.LogError($"{PatchWorkingPatternDetailActionName}: Error while patching Working Pattern Detail content for Job Profile with Id: {patchWorkingPatternDetailModel.JobProfileId} for the {patchWorkingPatternDetailModel.Title} pattern detail");
            }

            return new StatusCodeResult((int)response);
        }

        [HttpPatch]
        [Route("segment/{documentId}/socCodeData")]
        public async Task<IActionResult> PatchSocCodeData([FromBody]PatchSocDataModel patchSocDataModel, Guid documentId)
        {
            logger.LogInformation($"{PatchSocCodeDataActionName} has been called");

            if (patchSocDataModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await jobProfileOverviewSegmentService.PatchSocCodeDataAsync(patchSocDataModel, documentId).ConfigureAwait(false);
            if (response != HttpStatusCode.OK && response != HttpStatusCode.Created)
            {
                logger.LogError($"{PatchSocCodeDataActionName}: Error while patching Soc Data content for Job Profile with Id: {patchSocDataModel.JobProfileId} for the {patchSocDataModel.SocCode} soc code");
            }

            return new StatusCodeResult((int)response);
        }

        [HttpDelete]
        [Route("{controller}/{documentId}")]
        public async Task<IActionResult> Delete(Guid documentId)
        {
            logger.LogInformation($"{DeleteActionName} has been called");

            var isDeleted = await jobProfileOverviewSegmentService.DeleteAsync(documentId).ConfigureAwait(false);
            if (isDeleted)
            {
                logger.LogInformation($"{DeleteActionName} has deleted content for document Id: {documentId}");
                return Ok();
            }
            else
            {
                logger.LogWarning($"{DeleteActionName} has returned no content for: {documentId}");
                return NotFound();
            }
        }

        #region Define helper methods

        private static BreadcrumbViewModel BuildBreadcrumb(JobProfileOverviewSegmentModel model, string routePrefix)
        {
            var viewModel = new BreadcrumbViewModel
            {
                Paths = new List<BreadcrumbPathViewModel>()
                {
                    new BreadcrumbPathViewModel()
                    {
                        Route = $"/explore-careers",
                        Title = "Home: Explore careers",
                    },
                },
            };

            if (model != null)
            {
                var breadcrumbPathViewModel = new BreadcrumbPathViewModel
                {
                    Route = $"/{routePrefix}/{model.CanonicalName}",
                    Title = model.Data.Title,
                };

                viewModel.Paths.Add(breadcrumbPathViewModel);
            }

            viewModel.Paths.Last().AddHyperlink = false;

            return viewModel;
        }

        #endregion
    }
}
