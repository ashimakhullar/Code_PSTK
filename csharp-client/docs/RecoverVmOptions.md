# IO.Swagger.Model.RecoverVmOptions
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ResourcePoolEr** | [**EntityRef**](EntityRef.md) |  | [optional] 
**FolderEr** | [**EntityRef**](EntityRef.md) |  | [optional] 
**TestNetwork** | **string** | Test (Bubble) network to use for test recovery. Either networkMap or testNetwork can be specified, but not both. This cannot be used for failover recovery. | [optional] 
**PowerOn** | **bool?** | Power ON the VM after recovery or not | [optional] [default to false]
**NetworkMap** | [**List&lt;NetworkMapping&gt;**](NetworkMapping.md) | Array of network mappings. Either networkMap or testNetwork can be specified, but not both. | [optional] 
**NewName** | **string** | New name for the test recovery VM. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

