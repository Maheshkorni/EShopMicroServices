using System;

public class BadRequestException
{
    public BadRequestException(string message) : base(message)
    {

    }
    public BadRequestException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}
