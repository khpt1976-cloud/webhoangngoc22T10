using HoangNgoc.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HoangNgoc.Core.Services;

public class PaymentGatewayFactory : IPaymentGatewayFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PaymentGatewayFactory> _logger;

    public PaymentGatewayFactory(IServiceProvider serviceProvider, ILogger<PaymentGatewayFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public IEnumerable<IPaymentGateway> GetAllGateways()
    {
        try
        {
            return _serviceProvider.GetServices<IPaymentGateway>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all payment gateways");
            return Enumerable.Empty<IPaymentGateway>();
        }
    }

    public IEnumerable<IPaymentGateway> GetEnabledGateways()
    {
        try
        {
            return GetAllGateways().Where(g => g.IsEnabled);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting enabled payment gateways");
            return Enumerable.Empty<IPaymentGateway>();
        }
    }

    public IPaymentGateway GetGateway(string name)
    {
        try
        {
            return GetAllGateways().FirstOrDefault(g => 
                string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting payment gateway {GatewayName}", name);
            return null;
        }
    }

    public bool IsGatewayEnabled(string name)
    {
        try
        {
            var gateway = GetGateway(name);
            return gateway?.IsEnabled == true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if gateway {GatewayName} is enabled", name);
            return false;
        }
    }
}