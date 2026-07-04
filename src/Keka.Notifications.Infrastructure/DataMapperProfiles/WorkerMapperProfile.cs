// -----------------------------------------------------------------------
// <copyright file="WorkerMapperProfile.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.DataMapperProfiles;

/// <summary>
/// The worker mapper profile class.
/// </summary>
/// <seealso cref="Profile"/>
public class WorkerMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkerMapperProfile"/> class.
    /// </summary>
    public WorkerMapperProfile()
    {
        this.CreateEmailMappings();

        this.CreateInAppNotificationMappings();
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
            .ForMember(dest => dest.RawResponse, opt => opt.MapFrom(src => src.RawResponse))
            .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => (int)src.DeliveryStatus));

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
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.InAppNotificationId.ToString()))
            .ForMember(dest => dest.MetaData, opt => opt.MapFrom(src => src.MetaData.ToJson()));
    }
}