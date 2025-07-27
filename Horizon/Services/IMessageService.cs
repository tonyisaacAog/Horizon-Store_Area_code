using Microsoft.AspNetCore.Http;

namespace Horizon.Services
{
    public interface IMessageService
    {
        void Success(string message);
        void Error(string message);
        string GetSuccessMessage();
        string GetErrorMessage();
    }
}
