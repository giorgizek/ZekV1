namespace Zek.Model.Dictionary
{
    public enum ErrorCode
    {
        None = 0,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,

        InternalError = 500,

        RequestIsNull,
//        RequestValueIsNull,
        RequestValueParametersIsEmpty,
    }
}
