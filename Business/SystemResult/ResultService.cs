using Business.SystemResult.Enums;

namespace Business.SystemResult
{
    public class ResultService<T>
    {
        public T ResultData { get; set; }

        public bool isSuccess
        {
            get { return Error == null; }
        }

        public ResultError Error { get; set; }

        public ResultSuccess Success { get; set; }

        public void AddError(string message)
        {
            Error = new(message);
        }

        public void AddSuccess(string message)
        {
            Success = new(message);
        }
    }

    public class ResultError
    {
        public ResultError(string message)
        {
            this.Message = message;
        }
        public string Message { get; set; }

        public string Icon { get { return "❌"; } }

        public override string ToString()
        {
            return Message + " " + Icon;
        }
    }

    public class ResultSuccess
    {
        public ResultSuccess(string message)
        {
            this.Message = message;
        }
        public string Message { get; set; }

        public string Icon { get { return "✔"; } }

        public override string ToString()
        {
            return Message + " " + Icon;
        }
    }
}
