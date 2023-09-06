// ovo je pristup koji necu koristiti, radicu preko Exceptiona

namespace Repositories.Utils;

[Obsolete("Use Exceptions instead of this class")]
internal class ActionStatus<TResult> where TResult : class
{
    public TResult? Result { get; set; }
    public string? ErrorMessage { get; set; }
    public StatusCodes StatusCode { get; set; }

    public ActionStatus(TResult result, StatusCodes code) {
        Result = result;
        StatusCode = code;
    }

    public ActionStatus(string error, StatusCodes code) {
        ErrorMessage = error;
        StatusCode = code;
        
    }
}

public enum StatusCodes { 
    Ok,
    NotFound,
    BadParameters
}
