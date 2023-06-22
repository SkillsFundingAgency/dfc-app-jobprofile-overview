using AutoMapper;
using DFC.App.JobProfileOverview.Data.Enums;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.Data.ServiceBusModels.PatchModels;
using DFC.Logger.AppInsights.Contracts;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMapper mapper;
        private readonly IHttpClientService httpClientService;
        private readonly IMappingService mappingService;
        private readonly ILogService logService;

        public MessageProcessor(IMapper mapper, IHttpClientService httpClientService, IMappingService mappingService, ILogService logService)
        {
            this.mapper = mapper;
            this.httpClientService = httpClientService;
            this.mappingService = mappingService;
            this.logService = logService;
        }

        public async Task<HttpStatusCode> ProcessAsync(string message, long sequenceNumber, MessageContentType messageContentType, MessageAction messageAction)
        {
            logService.LogInformation($"MessageProcessor message {message} sequenceNumber {sequenceNumber}");
            switch (messageContentType)
            {
                case MessageContentType.WorkingPattern:
                    {
                        var serviceBusMessage = JsonConvert.DeserializeObject<PatchWorkingPatternServiceBusModel>(message);
                        var patchWorkingPatternModel = mapper.Map<PatchWorkingPatternModel>(serviceBusMessage);
                        patchWorkingPatternModel.MessageAction = messageAction;
                        patchWorkingPatternModel.SequenceNumber = sequenceNumber;

                        return await httpClientService.PatchAsync(patchWorkingPatternModel, "workingPattern").ConfigureAwait(false);
                    }

                case MessageContentType.HiddenAlternativeTitle:
                    {
                        var serviceBusMessage = JsonConvert.DeserializeObject<PatchHiddenAlternativeTitleServiceBusModel>(message);
                        var patchHiddenAlternativeTitleModel = mapper.Map<PatchHiddenAlternativeTitleModel>(serviceBusMessage);
                        patchHiddenAlternativeTitleModel.MessageAction = messageAction;
                        patchHiddenAlternativeTitleModel.SequenceNumber = sequenceNumber;

                        return await httpClientService.PatchAsync(patchHiddenAlternativeTitleModel, "hiddenAlternativeTitle").ConfigureAwait(false);
                    }

                case MessageContentType.JobProfileSpecialism:
                    {
                        var serviceBusMessage = JsonConvert.DeserializeObject<PatchJobProfileSpecialismServiceBusModel>(message);
                        var patchJobProfileSpecialismModel = mapper.Map<PatchJobProfileSpecialismModel>(serviceBusMessage);
                        patchJobProfileSpecialismModel.MessageAction = messageAction;
                        patchJobProfileSpecialismModel.SequenceNumber = sequenceNumber;

                        return await httpClientService.PatchAsync(patchJobProfileSpecialismModel, "jobProfileSpecialism").ConfigureAwait(false);
                    }

                case MessageContentType.WorkingHoursDetails:
                    {
                        var serviceBusMessage = JsonConvert.DeserializeObject<PatchWorkingHoursDetailServiceBusModel>(message);
                        var patchWorkingHoursDetailModel = mapper.Map<PatchWorkingHoursDetailModel>(serviceBusMessage);
                        patchWorkingHoursDetailModel.MessageAction = messageAction;
                        patchWorkingHoursDetailModel.SequenceNumber = sequenceNumber;

                        return await httpClientService.PatchAsync(patchWorkingHoursDetailModel, "workingHoursDetail").ConfigureAwait(false);
                    }

                case MessageContentType.WorkingPatternDetails:
                    {
                        var serviceBusMessage = JsonConvert.DeserializeObject<PatchWorkingPatternDetailServiceBusModel>(message);
                        var patchWorkingPatternDetailModel = mapper.Map<PatchWorkingPatternDetailModel>(serviceBusMessage);
                        patchWorkingPatternDetailModel.MessageAction = messageAction;
                        patchWorkingPatternDetailModel.SequenceNumber = sequenceNumber;

                        return await httpClientService.PatchAsync(patchWorkingPatternDetailModel, "workingPatternDetail").ConfigureAwait(false);
                    }

                case MessageContentType.JobProfileSoc:
                    {
                        var serviceBusMessage = JsonConvert.DeserializeObject<PatchSocDataServiceBusModel>(message);
                        var patchSocDataModel = mapper.Map<PatchSocDataModel>(serviceBusMessage);
                        patchSocDataModel.MessageAction = messageAction;
                        patchSocDataModel.SequenceNumber = sequenceNumber;

                        return await httpClientService.PatchAsync(patchSocDataModel, "socCodeData").ConfigureAwait(false);
                    }

                case MessageContentType.JobProfile:
                    return await ProcessJobProfileMessageAsync(message, messageAction, sequenceNumber).ConfigureAwait(false);

                default:
                    logService.LogInformation($"MessageProcessor Unexpected sitefinity content type {nameof(messageContentType)}");
                    throw new ArgumentOutOfRangeException(nameof(messageContentType), $"Unexpected sitefinity content type '{messageContentType}'");
            }
        }

        private async Task<HttpStatusCode> ProcessJobProfileMessageAsync(string message, MessageAction messageAction, long sequenceNumber)
        {
            var jobProfile = mappingService.MapToSegmentModel(message, sequenceNumber);

            logService.LogInformation($"ProcessJobProfileMessageAsync message {message} sequenceNumber {sequenceNumber} ");

            switch (messageAction)
            {
                case MessageAction.Draft:
                case MessageAction.Published:
                    var result = await httpClientService.PutAsync(jobProfile).ConfigureAwait(false);
                    if (result == HttpStatusCode.NotFound)
                    {
                        return await httpClientService.PostAsync(jobProfile).ConfigureAwait(false);
                    }

                    return result;

                case MessageAction.Deleted:
                    return await httpClientService.DeleteAsync(jobProfile.DocumentId).ConfigureAwait(false);

                default:
                    throw new ArgumentOutOfRangeException(nameof(messageAction), $"Invalid message action '{messageAction}' received, should be one of '{string.Join(",", Enum.GetNames(typeof(MessageAction)))}'");
            }
        }
    }
}