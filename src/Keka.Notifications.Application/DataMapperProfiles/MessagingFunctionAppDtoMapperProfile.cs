// -----------------------------------------------------------------------
// <copyright file="MessagingFunctionAppDtoMapperProfile.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DataMapperProfiles;

/// <summary>
/// The messaging function app dto mapper profile class.
/// </summary>
/// <seealso cref="Profile"/>
public class MessagingFunctionAppDtoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingFunctionAppDtoMapperProfile"/> class.
    /// </summary>
    public MessagingFunctionAppDtoMapperProfile()
    {
        this.CreateWhatsAppDtoMappings();
        this.CreateSmsDtoMappings();
        this.CreatePushNotificationDtoMappings();
        this.CreateInAppNotificationDtoMappings();
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

    private void CreatePushNotificationDtoMappings()
    {
        this.CreateMap<PushNotificationPersonalizationDto, PushNotificationPersonalization>().ReverseMap();
        this.CreateMap<PushNotificationRequestDto, PushNotificationRequest>().ReverseMap();
    }

    private void CreateInAppNotificationDtoMappings()
    {
        this.CreateMap<InAppNotification, SendWebPushNotificationRequest>()
            .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => new List<string>() { src.EmployeeId.ToString() }));
    }
}