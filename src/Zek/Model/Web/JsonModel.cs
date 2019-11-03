namespace Zek.Model.Web
{
    public class JsonModel
    {
        public JsonModel()
        {
        }


        public JsonModel(string message) :this(0, message)
        {
        }

        public JsonModel(int errorCode = 0, string message = null)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        //public bool IsValid => ErrorCode == 0;

        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }

    public class JsonModel<T> : JsonModel
    {
        public JsonModel()
        {
        }

        public JsonModel(T value, string message) : this(value, 0, message)
        {
        }
        public JsonModel(T value, int errorCode = 0, string message = null)
        {
            Value = value;
            ErrorCode = errorCode;
            Message = message;
        }

        public T Value { get; set; }

    }
}
