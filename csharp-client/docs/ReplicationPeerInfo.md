# IO.Swagger.Model.ReplicationPeerInfo
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Er** | [**EntityRef**](EntityRef.md) |  | 
**Mcip** | **string** | Management Cluster IP | 
**Dcip** | **string** | Data Cluster IP | [optional] 
**ReplCip** | **string** | Replication Cluster IP | 
**Ports** | [**List&lt;MapPortTypeToPortNumber&gt;**](MapPortTypeToPortNumber.md) |  | [optional] 
**Datastores** | [**List&lt;ReplicationPlatDatastorePair&gt;**](ReplicationPlatDatastorePair.md) | List of Paired Datastores | [optional] 
**Status** | **string** | Peer Cluster Status | [optional] 
**StatusDetails** | **string** | Peer Cluster Status Details | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

