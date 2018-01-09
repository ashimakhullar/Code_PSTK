# IO.Swagger.Model.ReplicationNetwork
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**NetworkName** | **string** | Name of the network/portgroup. | [optional] 
**Subnet** | **string** | Subnet in p.q.r.s notation. | [optional] 
**IpRanges** | [**List&lt;IpRange&gt;**](IpRange.md) | Ranges of the IP Addresses. | [optional] 
**Gateway** | **string** | Gateway. | [optional] 
**Vlanid** | **int?** | VLAN id | [optional] 
**Bandwidth** | **long?** | Cluster wide send side bandwidth. | [optional] 
**UnusedIps** | **int?** | Unused Ips from IpPools | [optional] 
**UsedIps** | **int?** | Used Ips from IpPools | [optional] 
**VlanReplication** | [**VlanReplication**](VlanReplication.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

