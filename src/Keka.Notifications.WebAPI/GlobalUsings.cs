// -----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

global using Asp.Versioning;
global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using HealthChecks.UI.Client;
global using Keka.Application;
global using Keka.Application.Types;
global using Keka.Logging;
global using Keka.MultiTenancy;
global using Keka.Notifications.Application.DTO.Emails;
global using Keka.Notifications.Application.DTO.EmailTemplates;
global using Keka.Notifications.Application.DTO.Employee;
global using Keka.Notifications.Application.DTO.InAppNotification;
global using Keka.Notifications.Application.DTO.Push;
global using Keka.Notifications.Application.DTO.Slack;
global using Keka.Notifications.Application.DTO.Sms;
global using Keka.Notifications.Application.DTO.Wa;
global using Keka.Notifications.Application.DTO.Webhook;
global using Keka.Notifications.Application.Services.Interfaces;
global using Keka.Notifications.Core.Models.EmailMessages;
global using Keka.Notifications.Core.Models.InAppNotifications;
global using Keka.Notifications.Infrastructure;
global using Keka.Notifications.WebAPI.Enums;
global using Keka.Notifications.WebAPI.Extensions;
global using Keka.Notifications.WebAPI.Filters;
global using Keka.Notifications.WebAPI.Models;
global using Keka.OAuth2;
global using Keka.Services.Common;
global using Keka.WebApi;
global using Keka.WebApi.Responses;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Prometheus;