using Microsoft.AspNetCore.Components.Server.Circuits;
using System.Threading;
using System.Threading.Tasks;

namespace MyRazorPagesApp.Services
{
    public class CustomCircuitHandler : CircuitHandler
    {
        public bool IsConnected { get; private set; }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            IsConnected = true;
            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            IsConnected = false;
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }
    }
}
