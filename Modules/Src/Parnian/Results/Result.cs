namespace Parnian.Results;

public class Result
{
  public bool IsSuccess { get; set; }
}

public class Result<TResult, TStatus> : Result
{
  public TResult Data { get; set; } = default!;
  public TStatus Status { get; set; } = default!;

  public static implicit operator Result<TResult, TStatus>(TResult result)
  {
    return new Result<TResult, TStatus> { IsSuccess = true, Data = result };
  }
}

public class Result<TResult> : Result<TResult, ResultStatus>
{
  public static implicit operator Result<TResult>(bool isSuccess)
  {
    return new Result<TResult>
    {
      IsSuccess = isSuccess, Status = isSuccess ? ResultStatus.Success : ResultStatus.Failed
    };
  }
  public static implicit operator Result<TResult>(ResultStatus status)
  {
    return new Result<TResult>
    {
      IsSuccess = status == ResultStatus.Success, Status = status
    };
  }
}
