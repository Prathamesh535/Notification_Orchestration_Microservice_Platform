// -----------------------------------------------------------------------
// <copyright file="ApiMapperProfile.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.DataMapperProfiles;

/// <summary>
/// The api mapper profile class.
/// </summary>
/// <seealso cref="Profile"/>
public class ApiMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiMapperProfile"/> class.
    /// </summary>
    public ApiMapperProfile()
    {
        this.CreateEmailMappings();

        this.CreateInAppNotificationMappings();

        this.CreateEmployeeMappings();

        this.CreateWhatsAppMappings();

        this.CreatePushNotificationMappings();

        this.CreateSmsMappings();

        this.CreateSlackMappings();

        this.CreateWebhookMappings();
    }

    private void CreateEmailMappings()
    {
        this.CreateMap<Email, DbEmail>()
            .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.ToJson()))
            .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Recipients.ToJson()))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.ToJson()))
            .ForMember(dest => dest.ReplyTo, opt => opt.MapFrom(src => src.ReplyTo.ToJson()))
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.ToJson()))
            .ForMember(dest => dest.EmailRequestId, opt => opt.MapFrom(src => src.EmailRequestId))
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (short)src.Status))
            .ReverseMap()
            .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.FromJson<Core.Models.EmailMessages.EmailAddress>()))
            .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Recipients.FromJson<EmailRecipients>()))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.FromJson<Core.Models.EmailMessages.EmailContent>()))
            .ForMember(dest => dest.ReplyTo, opt => opt.MapFrom(src => src.ReplyTo.FromJson<IEnumerable<Core.Models.EmailMessages.EmailAddress>>()))
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.FromJson<IEnumerable<EmailAttachment>>()))
            .ForMember(dest => dest.EmailRequestId, opt => opt.MapFrom(src => src.EmailRequestId))
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (NotificationStatus)src.Status))
            .ForMember(dest => dest.SentOn, opt => opt.MapFrom(src => src.UpdatedOn));

        this.CreateMap<EmailRequest, DbEmailRequest>()
            .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.ToJson()))
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.ToJson()))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.ToJson()))
            .ForMember(dest => dest.ReplyTo, opt => opt.MapFrom(src => src.ReplyTo.ToJson()))
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.ToJson()))
            .ForMember(dest => dest.TrackingSettings, opt => opt.MapFrom(src => src.TrackingSettings.ToJson()))
            .ReverseMap()
            .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.FromJson<Core.Models.EmailMessages.EmailAddress>()))
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.FromJson<List<EmailPersonalization>>()))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.FromJson<Core.Models.EmailMessages.EmailContent>()))
            .ForMember(dest => dest.ReplyTo, opt => opt.MapFrom(src => src.ReplyTo.FromJson<List<Core.Models.EmailMessages.EmailAddress>>()))
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.FromJson<List<EmailAttachment>>()))
            .ForMember(dest => dest.TrackingSettings, opt => opt.MapFrom(src => src.TrackingSettings.FromJson<EmailTrackingSettings>()));

        this.CreateMap<EmailTemplate, DbEmailTemplate>()
            .ReverseMap();

        this.CreateMap<EmailDeliveryHistory, DbEmailDeliveryHistory>()
            .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.ToEmail))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.DeliveryStatusId, opt => opt.MapFrom(src => src.DeliveryStatus != null ? (int?)src.DeliveryStatus : null))
            .ForMember(dest => dest.FailedReason, opt => opt.MapFrom(src => src.FailedReason != null ? (int?)src.FailedReason : null))
            .ReverseMap()
            .ForMember(dest => dest.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatusId != null ? (EmailDeliveryStatus?)src.DeliveryStatusId : null))
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp.Value.DateTime))
            .ForMember(dest => dest.FailedReason, opt => opt.MapFrom(src => src.FailedReason != null ? (EmailFailedReason?)src.FailedReason : null));

        this.CreateMap<EmailDeliveryRawData, DbEmailDeliveryRawData>()
            .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Md5Checksum))
            .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.DeliveryStatus != null ? (int?)src.DeliveryStatus : null))
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => new DateTimeOffset(src.TimeStamp)))
            .ReverseMap()
            .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.Timestamp.Value.UtcDateTime));

        this.CreateMap<EmailStatus, DbEmailStatus>()
            .ForMember(dest => dest.PreviousStateDetails, opt => opt.MapFrom(src => src.PreviousStateDetails.ToJson()))
            .ReverseMap()
            .ForMember(dest => dest.PreviousStateDetails, opt => opt.MapFrom(src => src.PreviousStateDetails.FromJson<EmailStatusPreviousStateDetails>()));
    }

    private void CreateInAppNotificationMappings()
    {
        this.CreateMap<InAppNotificationRequest, DbInAppNotificationRequest>()
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.ToJson()))
            .ForMember(dest => dest.InAppNotificationTemplateId, opt => opt.MapFrom(src => src.TemplateId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (short)src.Status))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.InAppNotificationRequestId))
            .ReverseMap()
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.FromJson<List<InAppNotificationPersonalization>>()))
            .ForMember(dest => dest.TemplateId, opt => opt.MapFrom(src => src.InAppNotificationTemplateId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (NotificationStatus)src.Status))
            .ForMember(dest => dest.InAppNotificationRequestId, opt => opt.MapFrom(src => src.RowKey));

        this.CreateMap<DbInAppNotificationTemplate, InAppNotificationTemplate>()
            .ForMember(dest => dest.Parameters, opt => opt.MapFrom(src => src.Parameters.FromJson<List<string>>()))
            .ReverseMap()
            .ForMember(dest => dest.Parameters, opt => opt.MapFrom(src => src.Parameters.ToJson()));

        this.CreateMap<DbInAppNotification, InAppNotification>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.PartitionKey))
            .ForMember(dest => dest.InAppNotificationId, opt => opt.MapFrom(src => src.RowKey))
            .ForMember(dest => dest.MetaData, opt => opt.MapFrom(src => src.MetaData.FromJson<Dictionary<string, string>>()))
            .ReverseMap()
            .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.InAppNotificationId))
            .ForMember(dest => dest.MetaData, opt => opt.MapFrom(src => src.MetaData.ToJson()));
    }

    private void CreateEmployeeMappings()
    {
        this.CreateMap<DbJobConfig, JobConfig>().ReverseMap();

        this.CreateMap<EmployeeNotificationPreference, DbEmployeeNotificationPreference>().ReverseMap();
        this.CreateMap<EmployeeNotificationPreference, NotificationPreferenceEventInfo>()
            .ForMember(dest => dest.NotificationChannels, opt => opt.MapFrom(src => src.NotificationChannels.ToJson()))
            .ReverseMap()
            .ForMember(dest => dest.NotificationChannels, opt => opt.MapFrom(src => JsonExtensions.FromJson<List<NotificationChannel>>(src.NotificationChannels)));
    }

    private void CreateWhatsAppMappings()
    {
        this.CreateMap<WaMessageRequest, DbWaMessageRequest>()
            .ForMember(dest => dest.WaTemplateId, opt => opt.MapFrom(src => src.TemplateId))
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.ToJson()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.WaMessageRequestId))
            .ReverseMap()
            .ForMember(dest => dest.TemplateId, opt => opt.MapFrom(src => src.WaTemplateId))
            .ForMember(dest => dest.WaMessageRequestId, opt => opt.MapFrom(src => Guid.Parse(src.RowKey)))
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.FromJson<List<WaPersonalization>>()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (NotificationStatus)src.Status));

        this.CreateMap<WaTemplate, DbWaTemplate>()
            .ForMember(dest => dest.ParameterMapping, opt => opt.MapFrom(src => src.ParameterMapping.ToJson()))
            .ReverseMap()
            .ForMember(dest => dest.ParameterMapping, opt => opt.MapFrom(src => src.ParameterMapping.FromJson<Dictionary<string, string>>()));
    }

    private void CreatePushNotificationMappings()
    {
        this.CreateMap<PushNotificationRequest, DbPushNotificationRequest>()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (short)src.Status))
           .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.ToJson()))
           .ReverseMap()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (NotificationStatus)src.Status))
           .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.FromJson<List<PushNotificationPersonalization>>()));

        this.CreateMap<PushNotificationTemplate, DbPushNotificationTemplate>()
           .ForMember(dest => dest.TemplateParameters, opt => opt.MapFrom(src => src.TemplateParameters.ToJson()))
           .ForMember(dest => dest.DataParameters, opt => opt.MapFrom(src => src.DataParameters.ToJson()))
           .ReverseMap()
           .ForMember(dest => dest.TemplateParameters, opt => opt.MapFrom(src => src.TemplateParameters.FromJson<List<string>>()))
           .ForMember(dest => dest.DataParameters, opt => opt.MapFrom(src => src.DataParameters.FromJson<List<string>>()));
    }

    private void CreateSmsMappings()
    {
        this.CreateMap<SmsRequest, DbSmsRequest>()
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.ToJson()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.SmsRequestId.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (NotificationStatus)src.Status))
            .ForMember(dest => dest.SmsRequestId, opt => opt.MapFrom(src => Guid.Parse(src.RowKey)))
            .ForMember(dest => dest.Personalization, opt => opt.MapFrom(src => src.Personalization.FromJson<List<SmsPersonalization>>()));

        this.CreateMap<SmsTemplate, DbSmsTemplate>()
            .ForMember(dest => dest.ParameterMapping, opt => opt.MapFrom(src => src.ParameterMapping.ToJson()))
            .ReverseMap()
            .ForMember(dest => dest.ParameterMapping, opt => opt.MapFrom(src => src.ParameterMapping.FromJson<Dictionary<string, string>>()));
    }

    private void CreateWebhookMappings()
    {
        this.CreateMap<WebhookRequest, DbWebhookRequest>()
            .ForMember(dest => dest.RequestHeaders, opt => opt.MapFrom(src => src.RequestHeaders != null && src.RequestHeaders.Count > 0 ? src.RequestHeaders.ToJson() : string.Empty))
            .ForMember(dest => dest.NotificationStatus, opt => opt.MapFrom(src => (int)src.NotificationStatus))
            .ForMember(dest => dest.HttpMethod, opt => opt.MapFrom(src => (int)src.HttpMethod))
            .ForMember(dest => dest.Payload, opt => opt.MapFrom(src => src.Payload != null && !src.Payload.Equals(string.Empty) ? src.Payload.ToJson() : string.Empty))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.WebhookRequestId.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.RequestHeaders, opt => opt.MapFrom(src => src.RequestHeaders.FromJson<Dictionary<string, string>>()))
            .ForMember(dest => dest.NotificationStatus, opt => opt.MapFrom(src => (NotificationStatus)src.NotificationStatus))
            .ForMember(dest => dest.HttpMethod, opt => opt.MapFrom(src => (HttpMethodType)src.HttpMethod))
            .ForMember(dest => dest.Payload, opt => opt.MapFrom(src => src.Payload.FromJson<object>()))
            .ForMember(dest => dest.WebhookRequestId, opt => opt.MapFrom(src => Guid.Parse(src.RowKey)));
    }

    private void CreateSlackMappings()
    {
        this.CreateMap<SlackNotificationRequest, DbSlackNotificationRequest>()
            .ForMember(dest => dest.SlackUrl, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Payload, opt => opt.MapFrom(src =>
                src.Payload != null && !src.Payload.Equals(string.Empty) ? src.Payload.ToJson() : string.Empty))
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.SlackNotificationRequestId.ToString()))
            .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.TenantId.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.SlackUrl))
            .ForMember(dest => dest.Payload, opt => opt.MapFrom(src => src.Payload.FlattenJsonToDictionary()))
            .ForMember(dest => dest.SlackNotificationRequestId, opt => opt.MapFrom(src => Guid.Parse(src.RowKey)));

        this.CreateMap<SlackNotificationTemplate, DbSlackNotificationTemplate>()
            .ForMember(dest => dest.SlackNotificationTemplateId, opt => opt.MapFrom(src => src.SlackTemplateId))
            .ReverseMap()
            .ForMember(dest => dest.SlackTemplateId, opt => opt.MapFrom(src => src.SlackNotificationTemplateId));
    }
}