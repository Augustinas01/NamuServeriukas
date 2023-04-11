using Mono.Nat;
using System.Collections;

namespace DeviceNetworkManager
{
    public class UPnPManager : IUPnPManager
    {
        private INatDevice? _device;
        private readonly List<Mapping> UpnpMappingList = new();
        private readonly int _startingPort = 42069;
        private readonly BitArray _availablePorts = new(100, true);


        public Task StartNetworkDeviceDiscovery()
        {
            NatUtility.StartDiscovery();
            NatUtility.DeviceFound += NatUtility_DeviceFound;
            return Task.CompletedTask;
        }



        public Task ShutDown()
        {
            UpnpMappingList.ForEach(m =>
            {
                _device.CreatePortMap(new Mapping(m.Protocol, m.PrivatePort, m.PublicPort, 5, m.Description));
            });
            return Task.CompletedTask;
        }

        public async Task<int> OpenPort(int protocol, int servicePort, string serviceName)
        {
            if (_device == null)
            {
                await StartNetworkDeviceDiscovery();
                while (NatUtility.IsSearching) { Thread.Sleep(10); }
                return await OpenPort(protocol, servicePort, serviceName);
            }
            else
            {
                var prot = protocol == 0 ? Protocol.Tcp : Protocol.Udp;
                var map = new Mapping(prot, servicePort, GetNextAvailablePort(), int.MaxValue, serviceName);
                var actualMap = await _device.CreatePortMapAsync(map);
                UpnpMappingList.Add(actualMap);
                return actualMap.PublicPort;

            }
        }

        public Task ClosePort(int internalPort)
        {
            UpnpMappingList
                .Where(m => m.PrivatePort == internalPort)
                .ToList()
                .ForEach(m => 
                { 
                    _device.CreatePortMap(new Mapping(m.Protocol, m.PrivatePort, m.PublicPort, 5, m.Description));
                    ReleasePort(m.PublicPort);
                }) ;
            return Task.CompletedTask;
        }
        private void NatUtility_DeviceFound(object? sender, DeviceEventArgs e)
        {
            NatUtility.StopDiscovery();
            _device = e.Device;
            NatUtility.DeviceFound -= NatUtility_DeviceFound;

        }

        private int GetNextAvailablePort()
        {
            int index = _availablePorts.Cast<bool>().ToList().IndexOf(true);
            if (index < 0) // no available port found
                return -1;

            _availablePorts[index] = false;
            return index + _startingPort;
        }

        private void ReleasePort(int port)
        {
            _availablePorts[port - _startingPort] = true;
        }

    }
}