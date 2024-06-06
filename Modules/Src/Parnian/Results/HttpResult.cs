namespace Parnian.Results;

public class HttpResult<TResult> : Result<TResult,HttpResultStatus>
{
  public static implicit operator HttpResult<TResult>(TResult data)
  {
    return new HttpResult<TResult>
    {
      IsSuccess = true,
      Status = HttpResultStatus.Ok,
      Data = data
    };
  }
  public static implicit operator HttpResult<TResult>(bool isSuccess)
  {
    return new HttpResult<TResult>
    {
      IsSuccess = isSuccess, 
      Status = isSuccess ? HttpResultStatus.Ok : HttpResultStatus.InternalException
    };
  }
  public static implicit operator HttpResult<TResult>(HttpResultStatus status)
  {
    return new HttpResult<TResult>
    {
      IsSuccess = status is HttpResultStatus.Ok or HttpResultStatus.Created, 
      Status = status
    };
  }
}
