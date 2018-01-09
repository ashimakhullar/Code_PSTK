# IO.Swagger.Api.ReplicationApi

All URIs are relative to *https://api.springpathinc.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**OpDpReplicationHistoryGet**](ReplicationApi.md#opdpreplicationhistoryget) | **GET** /history | Gets history of replications for appliance
[**OpIpoolReplicationGet**](ReplicationApi.md#opipoolreplicationget) | **GET** /ippools/replication | Replication IP Pool details
[**OpIpoolReplicationPoolidDelete**](ReplicationApi.md#opipoolreplicationpooliddelete) | **DELETE** /ippools/replication/{poolId} | Delete an IP Pool
[**OpIpoolReplicationPoolidGet**](ReplicationApi.md#opipoolreplicationpoolidget) | **GET** /ippools/replication/{poolId} | Replication IP Pool details
[**OpIpoolReplicationPoolidPut**](ReplicationApi.md#opipoolreplicationpoolidput) | **PUT** /ippools/replication/{poolId} | Modify an existing IP Pool
[**OpIpoolReplicationPost**](ReplicationApi.md#opipoolreplicationpost) | **POST** /ippools/replication | Create a new replication pool
[**OpReplicationNetworkGet**](ReplicationApi.md#opreplicationnetworkget) | **GET** /network | Replication network details of the current cluster
[**OpReplicationNetworkPost**](ReplicationApi.md#opreplicationnetworkpost) | **POST** /network | create replication network setup for the current cluster
[**OpReplicationNetworkPut**](ReplicationApi.md#opreplicationnetworkput) | **PUT** /network | Modify replication network details of the current cluster
[**OpReplicationPeerGet**](ReplicationApi.md#opreplicationpeerget) | **GET** /peers | Replication peers of the current cluster
[**OpReplicationPeerPeerClusterIdDatastoresGet**](ReplicationApi.md#opreplicationpeerpeerclusteriddatastoresget) | **GET** /peers/{peerClusterId}/datastores | Get peer datastore information
[**OpReplicationPeerPeerClusterIdDatastoresMapPut**](ReplicationApi.md#opreplicationpeerpeerclusteriddatastoresmapput) | **PUT** /peers/{peerClusterId}/datastores/map | Update datastore mapping
[**OpReplicationPeerPeerClusterIdPut**](ReplicationApi.md#opreplicationpeerpeerclusteridput) | **PUT** /peers/{peerClusterId} | Delete a replication peer
[**OpReplicationPeerPost**](ReplicationApi.md#opreplicationpeerpost) | **POST** /peers | Add a new replication peer


<a name="opdpreplicationhistoryget"></a>
# **OpDpReplicationHistoryGet**
> List<DisplayReplicationDetails> OpDpReplicationHistoryGet (string acceptLanguage = null)

Gets history of replications for appliance



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpReplicationHistoryGetExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Gets history of replications for appliance
                List&lt;DisplayReplicationDetails&gt; result = apiInstance.OpDpReplicationHistoryGet(acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpDpReplicationHistoryGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<DisplayReplicationDetails>**](DisplayReplicationDetails.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opipoolreplicationget"></a>
# **OpIpoolReplicationGet**
> List<IpPoolInfo> OpIpoolReplicationGet (string acceptLanguage = null)

Replication IP Pool details



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpIpoolReplicationGetExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Replication IP Pool details
                List&lt;IpPoolInfo&gt; result = apiInstance.OpIpoolReplicationGet(acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpIpoolReplicationGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<IpPoolInfo>**](IpPoolInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opipoolreplicationpooliddelete"></a>
# **OpIpoolReplicationPoolidDelete**
> void OpIpoolReplicationPoolidDelete (string poolId, string acceptLanguage = null)

Delete an IP Pool

Delete the IP Pool

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpIpoolReplicationPoolidDeleteExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var poolId = poolId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Delete an IP Pool
                apiInstance.OpIpoolReplicationPoolidDelete(poolId, acceptLanguage);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpIpoolReplicationPoolidDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **poolId** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opipoolreplicationpoolidget"></a>
# **OpIpoolReplicationPoolidGet**
> IpPoolInfo OpIpoolReplicationPoolidGet (string poolId, string acceptLanguage = null)

Replication IP Pool details



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpIpoolReplicationPoolidGetExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var poolId = poolId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Replication IP Pool details
                IpPoolInfo result = apiInstance.OpIpoolReplicationPoolidGet(poolId, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpIpoolReplicationPoolidGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **poolId** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**IpPoolInfo**](IpPoolInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opipoolreplicationpoolidput"></a>
# **OpIpoolReplicationPoolidPut**
> IpPoolInfo OpIpoolReplicationPoolidPut (string poolId, List<IpRange> body, string acceptLanguage = null)

Modify an existing IP Pool

Modify the IP Pool

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpIpoolReplicationPoolidPutExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var poolId = poolId_example;  // string | 
            var body = new List<IpRange>(); // List<IpRange> | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Modify an existing IP Pool
                IpPoolInfo result = apiInstance.OpIpoolReplicationPoolidPut(poolId, body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpIpoolReplicationPoolidPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **poolId** | **string**|  | 
 **body** | [**List&lt;IpRange&gt;**](IpRange.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**IpPoolInfo**](IpPoolInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opipoolreplicationpost"></a>
# **OpIpoolReplicationPost**
> List<IpPoolInfo> OpIpoolReplicationPost (IpPoolSpec body, string acceptLanguage = null)

Create a new replication pool

Create Replication IP Pool, and assign the IPs to the nodes

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpIpoolReplicationPostExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var body = new IpPoolSpec(); // IpPoolSpec | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Create a new replication pool
                List&lt;IpPoolInfo&gt; result = apiInstance.OpIpoolReplicationPost(body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpIpoolReplicationPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**IpPoolSpec**](IpPoolSpec.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<IpPoolInfo>**](IpPoolInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationnetworkget"></a>
# **OpReplicationNetworkGet**
> ReplicationNetwork OpReplicationNetworkGet (string acceptLanguage = null)

Replication network details of the current cluster



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationNetworkGetExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Replication network details of the current cluster
                ReplicationNetwork result = apiInstance.OpReplicationNetworkGet(acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationNetworkGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ReplicationNetwork**](ReplicationNetwork.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationnetworkpost"></a>
# **OpReplicationNetworkPost**
> ReplicationNetwork OpReplicationNetworkPost (ReplicationNetwork body, string acceptLanguage = null)

create replication network setup for the current cluster



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationNetworkPostExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var body = new ReplicationNetwork(); // ReplicationNetwork | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // create replication network setup for the current cluster
                ReplicationNetwork result = apiInstance.OpReplicationNetworkPost(body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationNetworkPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ReplicationNetwork**](ReplicationNetwork.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ReplicationNetwork**](ReplicationNetwork.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationnetworkput"></a>
# **OpReplicationNetworkPut**
> ReplicationNetwork OpReplicationNetworkPut (ReplicationNetwork body, string acceptLanguage = null)

Modify replication network details of the current cluster



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationNetworkPutExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var body = new ReplicationNetwork(); // ReplicationNetwork | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Modify replication network details of the current cluster
                ReplicationNetwork result = apiInstance.OpReplicationNetworkPut(body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationNetworkPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ReplicationNetwork**](ReplicationNetwork.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ReplicationNetwork**](ReplicationNetwork.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationpeerget"></a>
# **OpReplicationPeerGet**
> List<MapPairErToReplicationPeerInfo> OpReplicationPeerGet (string acceptLanguage = null)

Replication peers of the current cluster



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationPeerGetExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Replication peers of the current cluster
                List&lt;MapPairErToReplicationPeerInfo&gt; result = apiInstance.OpReplicationPeerGet(acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationPeerGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<MapPairErToReplicationPeerInfo>**](MapPairErToReplicationPeerInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationpeerpeerclusteriddatastoresget"></a>
# **OpReplicationPeerPeerClusterIdDatastoresGet**
> List<ReplicationDatastore> OpReplicationPeerPeerClusterIdDatastoresGet (string peerClusterId, string acceptLanguage = null)

Get peer datastore information



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationPeerPeerClusterIdDatastoresGetExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var peerClusterId = peerClusterId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Get peer datastore information
                List&lt;ReplicationDatastore&gt; result = apiInstance.OpReplicationPeerPeerClusterIdDatastoresGet(peerClusterId, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationPeerPeerClusterIdDatastoresGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **peerClusterId** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<ReplicationDatastore>**](ReplicationDatastore.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationpeerpeerclusteriddatastoresmapput"></a>
# **OpReplicationPeerPeerClusterIdDatastoresMapPut**
> MapPairErToReplicationPeerInfo OpReplicationPeerPeerClusterIdDatastoresMapPut (string peerClusterId, List<MapReplPlatDatastoreToReplPlatDatastore> body, string acceptLanguage = null)

Update datastore mapping



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationPeerPeerClusterIdDatastoresMapPutExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var peerClusterId = peerClusterId_example;  // string | 
            var body = new List<MapReplPlatDatastoreToReplPlatDatastore>(); // List<MapReplPlatDatastoreToReplPlatDatastore> | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Update datastore mapping
                MapPairErToReplicationPeerInfo result = apiInstance.OpReplicationPeerPeerClusterIdDatastoresMapPut(peerClusterId, body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationPeerPeerClusterIdDatastoresMapPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **peerClusterId** | **string**|  | 
 **body** | [**List&lt;MapReplPlatDatastoreToReplPlatDatastore&gt;**](MapReplPlatDatastoreToReplPlatDatastore.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**MapPairErToReplicationPeerInfo**](MapPairErToReplicationPeerInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationpeerpeerclusteridput"></a>
# **OpReplicationPeerPeerClusterIdPut**
> void OpReplicationPeerPeerClusterIdPut (string peerClusterId, Credential body, string acceptLanguage = null)

Delete a replication peer



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationPeerPeerClusterIdPutExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var peerClusterId = peerClusterId_example;  // string | 
            var body = new Credential(); // Credential | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Delete a replication peer
                apiInstance.OpReplicationPeerPeerClusterIdPut(peerClusterId, body, acceptLanguage);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationPeerPeerClusterIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **peerClusterId** | **string**|  | 
 **body** | [**Credential**](Credential.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opreplicationpeerpost"></a>
# **OpReplicationPeerPost**
> MapPairErToReplicationPeerInfo OpReplicationPeerPost (ReplicationPeerSpec body, string acceptLanguage = null)

Add a new replication peer



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpReplicationPeerPostExample
    {
        public void main()
        {
            var apiInstance = new ReplicationApi();
            var body = new ReplicationPeerSpec(); // ReplicationPeerSpec | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Add a new replication peer
                MapPairErToReplicationPeerInfo result = apiInstance.OpReplicationPeerPost(body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReplicationApi.OpReplicationPeerPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ReplicationPeerSpec**](ReplicationPeerSpec.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**MapPairErToReplicationPeerInfo**](MapPairErToReplicationPeerInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

