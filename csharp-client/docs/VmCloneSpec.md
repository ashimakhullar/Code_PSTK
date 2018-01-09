# IO.Swagger.Model.VmCloneSpec
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**VmId** | **string** | ID of the VM to be cloned | 
**VmName** | **string** | Name of the VM to be cloned | 
**CloneNamePrefix** | **string** | Prefix of clone&#39;s name | 
**NumberOfClones** | **int?** | Number of clones required | 
**Parallel** | **int?** | Number of parallel | [optional] 
**PowerOn** | **bool?** | Power on after cloning | [optional] [default to false]
**CustomSpecName** | **string** | Guest Customization spec for the cloned VM(s) | [optional] 
**VmGuestNamePrefix** | **string** | Guest name for the cloned VM(s) if different from vm name | [optional] 
**CloneNameStartNumber** | **int?** | Start number suffix for the VM clone name | [optional] 
**CloneNameIncrement** | **int?** | Increment of the suffix for the VM clones | [optional] 
**ResourcePoolId** | **string** | ID of the Resource pool to place the cloned VM(s) on | [optional] 
**ResourcePoolName** | **string** | Name of the Resource pool to place the cloned VM(s) on | [optional] 
**UserName** | **string** | User Name | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

