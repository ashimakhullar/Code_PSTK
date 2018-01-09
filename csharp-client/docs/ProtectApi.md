# IO.Swagger.Api.ProtectApi

All URIs are relative to *https://api.springpathinc.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**OpDpGroupGet**](ProtectApi.md#opdpgroupget) | **GET** /groups | Gets list of protection groups
[**OpDpGroupGroupidDelete**](ProtectApi.md#opdpgroupgroupiddelete) | **DELETE** /groups/{groupId} | Deletes the protection group
[**OpDpGroupGroupidGet**](ProtectApi.md#opdpgroupgroupidget) | **GET** /groups/{groupId} | Gets the protection group detail
[**OpDpGroupGroupidPut**](ProtectApi.md#opdpgroupgroupidput) | **PUT** /groups/{groupId} | Modifies the protection group
[**OpDpGroupPost**](ProtectApi.md#opdpgrouppost) | **POST** /groups | Add a new protection group
[**OpDpGroupScheduleReplicationPutTargetClusterId**](ProtectApi.md#opdpgroupschedulereplicationputtargetclusterid) | **PUT** /groups/{groupId}/schedule/replication/{targetClusterId} | Modifies replication schedule for a group
[**OpDpGroupScheduleReplicationTargetClusterIdDelete**](ProtectApi.md#opdpgroupschedulereplicationtargetclusteriddelete) | **DELETE** /groups/{groupId}/schedule/replication/{targetClusterId} | Removes replication schedules for the group for the cluster id
[**OpDpVmGet**](ProtectApi.md#opdpvmget) | **GET** /vms | Gets list of protected VMs
[**OpDpVmPost**](ProtectApi.md#opdpvmpost) | **POST** /vms | Protect a VM
[**OpDpVmScheduleReplicationPutTargetClusterId**](ProtectApi.md#opdpvmschedulereplicationputtargetclusterid) | **PUT** /vms/{vmId}/schedule/replication/{targetClusterId} | Modifies replication schedule for a vm
[**OpDpVmScheduleReplicationTargetClusterIdDelete**](ProtectApi.md#opdpvmschedulereplicationtargetclusteriddelete) | **DELETE** /vms/{vmId}/schedule/replication/{targetClusterId} | Removes replication schedules for the vm for the cluster id
[**OpDpVmVmidGet**](ProtectApi.md#opdpvmvmidget) | **GET** /vms/{vmId} | Gets the details of a protected VM


<a name="opdpgroupget"></a>
# **OpDpGroupGet**
> List<ProtectionGroupInfo> OpDpGroupGet (string name = null, string type = null, string acceptLanguage = null)

Gets list of protection groups



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupGetExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var name = name_example;  // string | name of the Group to look for, perhaps we can support wild card here (optional) 
            var type = type_example;  // string | Limits the search to incoming or outgoing groups (optional) 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Gets list of protection groups
                List&lt;ProtectionGroupInfo&gt; result = apiInstance.OpDpGroupGet(name, type, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpGroupGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **string**| name of the Group to look for, perhaps we can support wild card here | [optional] 
 **type** | **string**| Limits the search to incoming or outgoing groups | [optional] 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<ProtectionGroupInfo>**](ProtectionGroupInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpgroupgroupiddelete"></a>
# **OpDpGroupGroupidDelete**
> void OpDpGroupGroupidDelete (string groupId, string acceptLanguage = null)

Deletes the protection group



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupGroupidDeleteExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var groupId = groupId_example;  // string | ID of the group
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Deletes the protection group
                apiInstance.OpDpGroupGroupidDelete(groupId, acceptLanguage);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpGroupGroupidDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **groupId** | **string**| ID of the group | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpgroupgroupidget"></a>
# **OpDpGroupGroupidGet**
> ProtectionGroupInfo OpDpGroupGroupidGet (string groupId, string fields = null, string acceptLanguage = null)

Gets the protection group detail



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupGroupidGetExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var groupId = groupId_example;  // string | ID of the group
            var fields = fields_example;  // string | List of fields to return - currently all will be returned (optional) 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Gets the protection group detail
                ProtectionGroupInfo result = apiInstance.OpDpGroupGroupidGet(groupId, fields, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpGroupGroupidGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **groupId** | **string**| ID of the group | 
 **fields** | **string**| List of fields to return - currently all will be returned | [optional] 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ProtectionGroupInfo**](ProtectionGroupInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpgroupgroupidput"></a>
# **OpDpGroupGroupidPut**
> ProtectionGroupInfo OpDpGroupGroupidPut (string groupId, GroupEditParams body = null, string acceptLanguage = null)

Modifies the protection group



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupGroupidPutExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var groupId = groupId_example;  // string | ID of the group
            var body = new GroupEditParams(); // GroupEditParams | VMs to add / remove or the new name (optional) 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Modifies the protection group
                ProtectionGroupInfo result = apiInstance.OpDpGroupGroupidPut(groupId, body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpGroupGroupidPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **groupId** | **string**| ID of the group | 
 **body** | [**GroupEditParams**](GroupEditParams.md)| VMs to add / remove or the new name | [optional] 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ProtectionGroupInfo**](ProtectionGroupInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpgrouppost"></a>
# **OpDpGroupPost**
> ProtectionGroupInfo OpDpGroupPost (ProtectionGroupSpec body, string acceptLanguage = null)

Add a new protection group



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupPostExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var body = new ProtectionGroupSpec(); // ProtectionGroupSpec | Create group spec
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Add a new protection group
                ProtectionGroupInfo result = apiInstance.OpDpGroupPost(body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpGroupPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ProtectionGroupSpec**](ProtectionGroupSpec.md)| Create group spec | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ProtectionGroupInfo**](ProtectionGroupInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpgroupschedulereplicationputtargetclusterid"></a>
# **OpDpGroupScheduleReplicationPutTargetClusterId**
> ReplicationSchedule OpDpGroupScheduleReplicationPutTargetClusterId (string groupId, string targetClusterId, ReplicationSchedule body = null, string acceptLanguage = null)

Modifies replication schedule for a group

Since the group owner is unique, no confusion about which side this can be done

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupScheduleReplicationPutTargetClusterIdExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var groupId = groupId_example;  // string | 
            var targetClusterId = targetClusterId_example;  // string | 
            var body = new ReplicationSchedule(); // ReplicationSchedule | Does this work? Else define a new struct (optional) 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Modifies replication schedule for a group
                ReplicationSchedule result = apiInstance.OpDpGroupScheduleReplicationPutTargetClusterId(groupId, targetClusterId, body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpGroupScheduleReplicationPutTargetClusterId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **groupId** | **string**|  | 
 **targetClusterId** | **string**|  | 
 **body** | [**ReplicationSchedule**](ReplicationSchedule.md)| Does this work? Else define a new struct | [optional] 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ReplicationSchedule**](ReplicationSchedule.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpgroupschedulereplicationtargetclusteriddelete"></a>
# **OpDpGroupScheduleReplicationTargetClusterIdDelete**
> ReplicationSchedule OpDpGroupScheduleReplicationTargetClusterIdDelete (string groupId, string targetClusterId, string acceptLanguage = null)

Removes replication schedules for the group for the cluster id



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupScheduleReplicationTargetClusterIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var groupId = groupId_example;  // string | 
            var targetClusterId = targetClusterId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Removes replication schedules for the group for the cluster id
                ReplicationSchedule result = apiInstance.OpDpGroupScheduleReplicationTargetClusterIdDelete(groupId, targetClusterId, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpGroupScheduleReplicationTargetClusterIdDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **groupId** | **string**|  | 
 **targetClusterId** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ReplicationSchedule**](ReplicationSchedule.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmget"></a>
# **OpDpVmGet**
> List<ProtectedVMInfo> OpDpVmGet (string name = null, string type = null, string acceptLanguage = null)

Gets list of protected VMs



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmGetExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var name = name_example;  // string | name of the VM to look for, perhaps we can support wild card here (optional) 
            var type = type_example;  // string | Limits the search to incoming or outgoing virtual machines (optional) 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Gets list of protected VMs
                List&lt;ProtectedVMInfo&gt; result = apiInstance.OpDpVmGet(name, type, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpVmGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **string**| name of the VM to look for, perhaps we can support wild card here | [optional] 
 **type** | **string**| Limits the search to incoming or outgoing virtual machines | [optional] 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<ProtectedVMInfo>**](ProtectedVMInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmpost"></a>
# **OpDpVmPost**
> ProtectedVMInfo OpDpVmPost (ProtectedVMSpec body, string acceptLanguage = null)

Protect a VM



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmPostExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var body = new ProtectedVMSpec(); // ProtectedVMSpec | Protect VM Spec
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Protect a VM
                ProtectedVMInfo result = apiInstance.OpDpVmPost(body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpVmPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ProtectedVMSpec**](ProtectedVMSpec.md)| Protect VM Spec | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ProtectedVMInfo**](ProtectedVMInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmschedulereplicationputtargetclusterid"></a>
# **OpDpVmScheduleReplicationPutTargetClusterId**
> ReplicationSchedule OpDpVmScheduleReplicationPutTargetClusterId (string vmId, string targetClusterId, ReplicationSchedule body, string acceptLanguage = null)

Modifies replication schedule for a vm



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmScheduleReplicationPutTargetClusterIdExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var vmId = vmId_example;  // string | 
            var targetClusterId = targetClusterId_example;  // string | 
            var body = new ReplicationSchedule(); // ReplicationSchedule | Does this work? Else define a new struct
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Modifies replication schedule for a vm
                ReplicationSchedule result = apiInstance.OpDpVmScheduleReplicationPutTargetClusterId(vmId, targetClusterId, body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpVmScheduleReplicationPutTargetClusterId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **vmId** | **string**|  | 
 **targetClusterId** | **string**|  | 
 **body** | [**ReplicationSchedule**](ReplicationSchedule.md)| Does this work? Else define a new struct | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ReplicationSchedule**](ReplicationSchedule.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmschedulereplicationtargetclusteriddelete"></a>
# **OpDpVmScheduleReplicationTargetClusterIdDelete**
> ReplicationSchedule OpDpVmScheduleReplicationTargetClusterIdDelete (string vmId, string targetClusterId, string acceptLanguage = null)

Removes replication schedules for the vm for the cluster id

Unprotects a VM

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmScheduleReplicationTargetClusterIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var vmId = vmId_example;  // string | 
            var targetClusterId = targetClusterId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Removes replication schedules for the vm for the cluster id
                ReplicationSchedule result = apiInstance.OpDpVmScheduleReplicationTargetClusterIdDelete(vmId, targetClusterId, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpVmScheduleReplicationTargetClusterIdDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **vmId** | **string**|  | 
 **targetClusterId** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ReplicationSchedule**](ReplicationSchedule.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmvmidget"></a>
# **OpDpVmVmidGet**
> ProtectedVMInfo OpDpVmVmidGet (string vmId, string acceptLanguage = null)

Gets the details of a protected VM



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmVmidGetExample
    {
        public void main()
        {
            var apiInstance = new ProtectApi();
            var vmId = vmId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Gets the details of a protected VM
                ProtectedVMInfo result = apiInstance.OpDpVmVmidGet(vmId, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProtectApi.OpDpVmVmidGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **vmId** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**ProtectedVMInfo**](ProtectedVMInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

