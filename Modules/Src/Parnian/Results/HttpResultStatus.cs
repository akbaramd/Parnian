namespace Parnian.Results;

public enum HttpResultStatus
{
  Ok = 200,
  Created = 201,
  UnAuthorized = 400,
  NotFound = 404,
  Forbidden = 403,
  InternalException = 500,
}
