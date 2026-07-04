// -----------------------------------------------------------------------
// <copyright file="SqsEventConverter.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.EmailLambdaApp.Helpers;

/// <summary>
/// Provides functionality to convert SQS events to email delivery reports.
/// </summary>
public partial class SqsEventConverter : ISqsEventConverter
{
    /// <inheritdoc/>
    public List<SqsEventDto> ConvertToSqsEventDtos(SQSEvent sQSEvent)
    {
        List<SqsEventDto> sqsEventDtos = new List<SqsEventDto>();
        if (sQSEvent is null || sQSEvent.Records.Count == 0)
        {
            return sqsEventDtos;
        }

        foreach (var record in sQSEvent.Records)
        {
            sqsEventDtos.Add(new SqsEventDto()
            {
                MessageId = record.MessageId,
                Md5OfBody = record.Md5OfBody,
                Body = record.Body,
                EventSourceArn = record.EventSourceArn,
            });
        }

        return sqsEventDtos;
    }
}