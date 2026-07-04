// -----------------------------------------------------------------------
// <copyright file="WorkerDtoMapperProfile.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DataMapperProfiles;

/// <summary>
/// The worker dto mapper profile class.
/// </summary>
/// <seealso cref="Profile"/>
public class WorkerDtoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkerDtoMapperProfile"/> class.
    /// </summary>
    public WorkerDtoMapperProfile()
    {
        this.CreateEmailDtoMappings();

        this.CreateInAppNotificationDtoMappings();
    }

    private void CreateEmailDtoMappings()
    {
        this.CreateMap<EmailRequestDto, EmailRequest>();

        this.CreateMap<EmailPersonalizationDto, EmailPersonalization>();

        this.CreateMap<EmailRecipientsDto, EmailRecipients>();

        this.CreateMap<EmailAddressDto, Core.Models.EmailMessages.EmailAddress>();

        this.CreateMap<EmailAttachmentDto, EmailAttachment>();

        this.CreateMap<EmailContentDto, EmailContent>();

        this.CreateMap<EmailTrackingSettingsDto, EmailTrackingSettings>();

        this.CreateMap<EmailTemplateCreateDto, EmailTemplate>();

        this.CreateMap<EmailTemplateUpdateDto, EmailTemplate>();

        this.CreateMap<EmailDeliveryHistory, EmailDeliveryHistoryDto>()
            .ForMember(dest => dest.DeliveryStatusId, opt => opt.MapFrom(src => src.DeliveryStatus != null ? src.DeliveryStatus : null))
            .ForMember(dest => dest.FailedReasonId, opt => opt.MapFrom(src => src.FailedReason != null ? src.FailedReason : null));

        this.CreateMap<EmailStatus, EmailStatusDto>();

        this.CreateMap<ClickTrackingDto, ClickTracking>();

        this.CreateMap<OpenTrackingDto, OpenTracking>();
    }

    private void CreateInAppNotificationDtoMappings()
    {
        this.CreateMap<InAppNotification, InAppNotificationDto>();

        this.CreateMap<InAppNotificationRequestDto, InAppNotificationRequest>();

        this.CreateMap<InAppNotificationPersonalizationDto, InAppNotificationPersonalization>();
    }
}