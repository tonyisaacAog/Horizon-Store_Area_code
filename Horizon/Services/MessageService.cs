namespace Horizon.Services
{
    public class MessageService : IMessageService
    {
        private const string SuccessKey = "SuccessMessage";
        private const string ErrorKey = "ErrorMessage";
        private readonly IHttpContextAccessor _httpContext;
        public MessageService(IHttpContextAccessor httpContext) { _httpContext = httpContext; }
        public void Success(string message) => _httpContext.HttpContext.Session.SetString(SuccessKey, message);
        public void Error(string message) => _httpContext.HttpContext.Session.SetString(ErrorKey, message);

        public string GetSuccessMessage()
        {
            var msg = _httpContext.HttpContext.Session.GetString(SuccessKey);
            _httpContext.HttpContext.Session.Remove(SuccessKey);
            return msg;
        }

        public string GetErrorMessage()
        {
            var msg = _httpContext.HttpContext.Session.GetString(ErrorKey);
            _httpContext.HttpContext.Session.Remove(ErrorKey);
            return msg;
        }
    }

}
