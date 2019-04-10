﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Catan.API.SignalR
{
    public class ClientProxyGroup : IClientProxy
    {
        private readonly IClientProxy[] _proxies;

        public ClientProxyGroup(params IClientProxy[] proxies)
        {
            _proxies = proxies;
        }

        public Task SendCoreAsync(string method, object[] args, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.WhenAll(_proxies.Select(proxy => proxy.SendCoreAsync(method, args, cancellationToken)));
        }
    }
}
