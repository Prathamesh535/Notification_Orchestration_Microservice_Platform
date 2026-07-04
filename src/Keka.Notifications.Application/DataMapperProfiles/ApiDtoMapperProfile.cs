// -----------------------------------------------------------------------
// <copyright file="ApiDtoMapperProfile.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DataMapperProfiles;

/// <summary>
/// The api dto mapper profile class.
/// </summary>
/// <seealso cref="Profile"/>
public class ApiDtoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiDtoMapperProfile"/> class.
    /// </summary>
    public ApiDtoMapperProfile()
    {
        this.CreateEmailDtoMappings();

        this.CreateInAppNotificationDtoMappings();

        this.CreateWhatsAppDtoMappings();

        this.CreateSmsDtoMappings();

        this.CreateEmployeeDtoMappings();

        this.CreatePushNotificationDtoMappings();

        this.CreateWebhookDtoMappings();

        this.CreateSlackNotificationDtoMappings();
    }

    private void CreateEmailDtoMappings()
    {
        this.CreateMap<EmailTemplate, EmailTemplateDto>();

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

        this.CreateMap<EmailDeliveryRawData, EmailDeliveryRawDataBasicDetailsDto>();

        this.CreateMap<EmailStatus, EmailStatusDto>();

        this.CreateMap<ClickTrackingDto, ClickTracking>();

        this.CreateMap<OpenTrackingDto, OpenTracking>();

        this.CreateMap<EmailTemplate, EmailTemplateDto>();
    }

    private void CreateInAppNotificationDtoMappings()
    {
        this.CreateMap<InAppNotification, InAppNotificationDto>();

        this.CreateMap<InAppNotificationRequestDto, InAppNotificationRequest>();

        this.CreateMap<InAppNotificationPersonalizationDto, InAppNotificationPersonalization>();
    }

    private void CreateWhatsAppDtoMappings()
    {
        this.CreateMap<WaPersonalizationDto, WaPersonalization>().ReverseMap();

        this.CreateMap<WaMessageRequestDto, WaMessageRequest>().ReverseMap();

        this.CreateMap<WaAttachmentDto, Core.Models.Wa.WaAttachment>().ReverseMap();
    }

    private void CreateSmsDtoMappings()
    {
        this.CreateMap<SmsPersonalizationDto, SmsPersonalization>().ReverseMap();

        this.CreateMap<SmsRequestDto, SmsRequest>().ReverseMap();
    }

    private void CreateEmployeeDtoMappings()
    {
        this.CreateMap<SaveEmployeeNotificationPreferenceDto, EmployeeNotificationPreference>().ReverseMap();
        this.CreateMap<EmployeeNotificationPreferenceDto, EmployeeNotificationPreference>().ReverseMap();
    }

    private void CreatePushNotificationDtoMappings()
    {
        this.CreateMap<PushNotificationPersonalizationDto, PushNotificationPersonalization>().ReverseMap();

        this.CreateMap<PushNotificationRequestDto, PushNotificationRequest>().ReverseMap();
    }

    private void CreateWebhookDtoMappings()
    {
        this.CreateMap<WebhookRequestDto, WebhookRequest>()
              .ForMember(dest => dest.HttpMethod, opt => opt.MapFrom(src => Enum.Parse<HttpMethodType>(src.HttpMethod.ToUpper())));
    }

    private void CreateSlackNotificationDtoMappings()
    {
        this.CreateMap<SlackNotificationRequestDto, SlackNotificationRequest>().ReverseMap();
    }
}