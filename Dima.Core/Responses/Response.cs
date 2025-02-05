using System.Text.Json.Serialization;

namespace Dima.Core.Responses;
public class Response<TData> 
{
    [JsonConstructor]
    public Response()
    {
        _StatusCode = Configuration.DefaultStatusCode;    
    }

    public Response(TData? data, int statusCode = Configuration.DefaultStatusCode, string? message = null)
    {
        Data = data;
        Message = message;
        _StatusCode = statusCode;
    }

    private readonly int _StatusCode;
    public TData? Data { get; set; }
    public string? Message { get; set; } = string.Empty;
    [JsonIgnore]
    public bool IsSuccess => _StatusCode is >= 200 and <= 299;

}
