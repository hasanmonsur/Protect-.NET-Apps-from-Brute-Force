using WebSeriLogApi.Contacts;

namespace WebSeriLogApi.Services
{
    public class MaskService : IMaskService
    {
        public string MaskEmail(string email)
        {
            var parts = email.Split('@');
            return $"{parts[0][0]}****@{parts[1]}"; // Mask the email
        }
    }
}
