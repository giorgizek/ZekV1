namespace Zek.Model
{
    public enum ErrorCode
    {
        None = 0,

        InternalError = 500,

        RequestIdIsRequired = 400,
        ApplicationKeyIsInvalid = 300,
        RequestValueIsNull,
        RequestValueParametersIsEmpty,


        WebServiceError,
    }
}
