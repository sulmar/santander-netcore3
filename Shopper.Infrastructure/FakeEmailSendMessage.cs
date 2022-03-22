using Microsoft.Extensions.Options;
using Shopper.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{

    public class FakeEmailMessageServiceOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }

    public class FakeEmailMessageService : IMessageService
    {
        private readonly FakeEmailMessageServiceOptions options;
        public FakeEmailMessageService(IOptions<FakeEmailMessageServiceOptions> options)
        {
            this.options = options.Value;
        }

        public Task SendAsync(string message)
        {
            Trace.WriteLine($"Sending email {message} via {options.Address}:{options.Port}");

            return Task.CompletedTask;
        }
    }
}
