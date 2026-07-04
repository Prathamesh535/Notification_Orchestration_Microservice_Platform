// -----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

global using System.Diagnostics.CodeAnalysis;
global using System.Reflection;
global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using Hangfire;
global using HealthChecks.UI.Client;
global using Keka.Application;
global using Keka.Application.Types;
global using Keka.EventBus.Abstractions;
global using Keka.Hangfire;
global using Keka.Logging;
global using Keka.MultiTenancy;
global using Keka.Notifications.Application.Services.Interfaces;
global using Keka.Notifications.Core.Events;
global using Keka.Notifications.Infrastructure;
global using Keka.Notifications.Worker.EventHandlers;
global using Keka.Notifications.Worker.Infrastructure.Hangfire;
global using Keka.WebApi;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Prometheus;