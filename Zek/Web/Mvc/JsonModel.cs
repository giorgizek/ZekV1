namespace Zek.Web.Mvc
{

    public class JsonModel
    {
        public JsonModel()
        {
        }

        public JsonModel(int errorCode = 0, string errorMessage = null)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public bool IsValid => ErrorCode == 0;

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }

    //[Serializable]
    public class JsonModel<T> : JsonModel
    {
        public JsonModel()
        {
        }

        //public JsonModel(int errorCode = 0, string errorMessage = null)
        //    : this(default(T), errorCode, errorMessage)
        //{
        //}

        public JsonModel(T value, int errorCode = 0, string errorMessage = null)
        {
            Value = value;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public T Value { get; set; }

    }
}
