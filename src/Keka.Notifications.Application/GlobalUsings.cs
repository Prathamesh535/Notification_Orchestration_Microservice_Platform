// -----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using System.Web;
global using Autofac;
global using AutoMapper;
global using AutoMapper.Contrib.Autofac.DependencyInjection;
global using HandlebarsDotNet;
global using Keka.EventBus.Abstractions;
global using Keka.HTTP;
global using Keka.MultiTenancy;
global using Keka.Notifications.Abstractions.Email;
global using Keka.Notifications.Abstractions.Push;
global using Keka.Notifications.Abstractions.Slack;
global using Keka.Notifications.Abstractions.SMS;
global using Keka.Notifications.Abstractions.WA;
global using Keka.Notifications.Abstractions.WebPush;
global using Keka.Notifications.Application;
global using Keka.Notifications.Application.Builders;
global using Keka.Notifications.Application.DataMapperProfiles;
global using Keka.Notifications.Application.DTO.Emails;
global using Keka.Notifications.Application.DTO.EmailTemplates;
global using Keka.Notifications.Application.DTO.Employee;
global using Keka.Notifications.Application.DTO.InAppNotification;
global using Keka.Notifications.Application.DTO.Push;
global using Keka.Notifications.Application.DTO.Slack;
global using Keka.Notifications.Application.DTO.Sms;
global using Keka.Notifications.Application.DTO.SQSEvents;
global using Keka.Notifications.Application.DTO.Wa;
global using Keka.Notifications.Application.DTO.Webhook;
global using Keka.Notifications.Application.Enums;
global using Keka.Notifications.Application.Extensions;
global using Keka.Notifications.Application.Helpers;
global using Keka.Notifications.Application.Helpers.Interfaces;
global using Keka.Notifications.Application.Services;
global using Keka.Notifications.Application.Services.Interfaces;
global using Keka.Notifications.Core.Enums;
global using Keka.Notifications.Core.Events;
global using Keka.Notifications.Core.Exceptions;
global using Keka.Notifications.Core.Models;
global using Keka.Notifications.Core.Models.EmailMessages;
global using Keka.Notifications.Core.Models.Employees;
global using Keka.Notifications.Core.Models.InAppNotifications;
global using Keka.Notifications.Core.Models.Jobs;
global using Keka.Notifications.Core.Models.PushNotifications;
global using Keka.Notifications.Core.Models.Slack;
global using Keka.Notifications.Core.Models.Sms;
global using Keka.Notifications.Core.Models.Wa;
global using Keka.Notifications.Core.Models.Webhook;
global using Keka.Notifications.Core.Repositories;
global using Keka.Persistence.Redis;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;