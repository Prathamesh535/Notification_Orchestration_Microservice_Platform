// -----------------------------------------------------------------------
// <copyright file="Response.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models;


internal class Response<T>
{
    public Response(T data)
    {
        this.IsSuccess = true;
        this.Data = data;
    }

    public T Data { get; set; }

    public bool IsSuccess { get; set; }
}