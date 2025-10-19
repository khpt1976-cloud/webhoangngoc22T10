namespace HoangNgoc.Core.Abstractions;

/// <summary>
/// Factory for creating payment gateway instances
/// </summary>
public interface IPaymentGatewayFactory
{
    /// <summary>
    /// Get all available payment gateways
    /// </summary>
    IEnumerable<IPaymentGateway> GetAllGateways();
    
    /// <summary>
    /// Get enabled payment gateways
    /// </summary>
    IEnumerable<IPaymentGateway> GetEnabledGateways();
    
    /// <summary>
    /// Get payment gateway by name
    /// </summary>
    IPaymentGateway GetGateway(string name);
    
    /// <summary>
    /// Check if gateway exists and is enabled
    /// </summary>
    bool IsGatewayEnabled(string name);
}