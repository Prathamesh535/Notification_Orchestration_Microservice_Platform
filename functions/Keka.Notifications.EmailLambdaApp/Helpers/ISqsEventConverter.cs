// -----------------------------------------------------------------------
// <copyright file="ISqsEventConverter.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.EmailLambdaApp.Helpers;

/// <summary>
/// Defines a contract for converting SQS events SQS dtos.
/// </summary>
public interface ISqsEventConverter
{
    /// <summary>
    /// Converts sqs event records to raw event objects.
    /// </summary>
    /// <param name="sQSEvent">The SQS event.</param>
    /// <returns>A list of sqs event dto objects.</returns>
    public List<SqsEventDto> ConvertToSqsEventDtos(SQSEvent sQSEvent);
}