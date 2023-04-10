using Contracts;
using Microsoft.Extensions.Hosting;
using Services.Abstractions.Generic;

namespace Services.Abstractions.Facades
{
    public interface IProcessManager : IProcessHandler, IHostedService
    { }
}
