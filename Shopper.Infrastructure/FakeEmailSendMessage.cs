using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FakeEmailMessageService> logger;
        public FakeEmailMessageService(ILogger<FakeEmailMessageService> logger, IOptions<FakeEmailMessageServiceOptions> options)
        {
            this.options = options.Value;
            this.logger = logger;
        }

        public Task SendAsync(string message)
        {
            logger.LogInformation("Sending email {0} via {1}:{2}", message, options.Address, options.Port);

            return Task.CompletedTask;
        }
    }
}
