using Shopper.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public class FakeEmailSendMessage : IMessageService
    {
        public Task SendAsync(string message)
        {
            Trace.WriteLine($"Sending email {message}");

            return Task.CompletedTask;
        }
    }
}
