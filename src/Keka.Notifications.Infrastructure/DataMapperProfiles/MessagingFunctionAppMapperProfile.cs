// -----------------------------------------------------------------------
// <copyright file="MessagingFunctionAppMapperProfile.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.DataMapperProfiles;

/// <summary>
/// The messaging function app mapper profile class.
/// </summary>
/// <seealso cref="Profile"/>
public class MessagingFunctionAppMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingFunctionAppMapperProfile"/> class.
    /// </summary>
    public MessagingFunctionAppMapperProfile()
    {
        this.CreateMap<Country, DbCountry>().ReverseMap();

        this.CreatePushNotificationMappings();

        this.CreateSmsMappings();

        this.CreateWhatsAppMappings();

        this.CreateInAppNotificationMappings();
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

    private void CreateInAppNotificationMappings()
    {
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