# IO.Swagger.Model.VirtualMachineRuntimeInfo
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | Name of the Virtual Machine | 
**Moid** | **string** | Virtual machine MOID | 
**VmxPath** | **string** | vmxPath in VC datastore format | [optional] 
**Folder** | **string** | Folder which VM belongs to | [optional] 
**ResourcePool** | **string** | Resource pool which VM belongs to | [optional] 
**HostName** | **string** | Hostname of Virtual Machine | [optional] 
**NumCpu** | **int?** | Number of CPUs of VM | [optional] 
**GuestId** | **string** | Guest operating system identifier (short name), if known. | [optional] 
**GuestFullName** | **string** |  Guest operating system full name, if known.  | [optional] 
**GuestFamily** | **string** |  Guest operating system family, if known | [optional] 
**GuestState** | **string** | state are: running, shuttingdown, resetting, standby, notrunning, unknown | [optional] 
**ProvisionedSize** | **long?** | Provisioned Size of Virtual Machine | [optional] 
**UsedSize** | **long?** | Used Size of Virtual Machine | [optional] 
**CpuUsage** | **int?** | CPU Usage of Virtual Machine | [optional] 
**MemoryMB** | **int?** | CPU Memory in MB of VM | [optional] 
**MemoryUsage** | **int?** | memory usage of Virtual Machine | [optional] 
**Version** | **string** | Vm version | [optional] 
**PowerState** | **string** | States: poweredOff/poweredOn/suspended | [optional] 
**ConnectionState** | **string** | States:connected/disconnected/inaccessible/invalid/orphaned | [optional] 
**DvsDeviceNetworkMaps** | [**List&lt;DvsDeviceNetworkMap&gt;**](DvsDeviceNetworkMap.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

