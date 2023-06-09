using MutliUserBooking.Application.DTOs.Email;
using System.Threading.Tasks;

namespace MutliUserBooking.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}