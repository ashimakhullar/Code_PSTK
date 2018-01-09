# IO.Swagger.Model.IpPoolInfo
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | Name - not necessarily unique. | [optional] 
**Uuid** | **string** | Pool Identitifer. | [optional] 
**Description** | **string** | Optional description. | [optional] 
**IpRanges** | [**List&lt;IpRange&gt;**](IpRange.md) | IP Ranges for the pool. | [optional] 
**UsedIpRanges** | [**List&lt;IpRange&gt;**](IpRange.md) | Currently used IP Ranges in the pool. | [optional] 
**Subnet** | **string** | Subnet as p.q.r.s/&lt;number of bits&gt;, can&#39;t be changed once set | [optional] 
**Gateway** | **string** | Gateway for the pool. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

