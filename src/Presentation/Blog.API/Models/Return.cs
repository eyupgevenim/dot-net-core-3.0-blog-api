using System.Collections.Generic;

namespace Blog.API.Models
{
    public class Return : Return<object>
    {
    }

    public class Return<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public Return()
        {
            Data = default(T);
        }
    }

    public class ReturnError : Return
    {
        public List<ReturnErrorModel> Errors { get; set; }
        public string InternalMessage { get; set; }

        public ReturnError()
        {
            Errors = new List<ReturnErrorModel>();
        }
    }

    public class ReturnErrorModel
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
    }
}
