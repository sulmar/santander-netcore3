using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain.Services
{
    public interface IMessageService
    {
        Task SendAsync(string message);
    }
}
