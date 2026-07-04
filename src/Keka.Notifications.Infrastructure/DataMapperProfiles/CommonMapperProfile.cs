// -----------------------------------------------------------------------
// <copyright file="CommonMapperProfile.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.DataMapperProfiles;

/// <summary>
/// The common mapper profile class.
/// </summary>
/// <seealso cref="Profile"/>
public class CommonMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommonMapperProfile"/> class.
    /// </summary>
    public CommonMapperProfile()
    {
        this.CreateMap<DbCountry, Country>().ReverseMap();
    }
}