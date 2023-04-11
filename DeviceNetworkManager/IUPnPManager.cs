namespace DeviceNetworkManager
{
    public interface IUPnPManager
    {
        Task ClosePort(int internalPort);
        Task<int> OpenPort(int protocol, int servicePort, string serviceName);
        Task ShutDown();
        Task StartNetworkDeviceDiscovery();
    }
}