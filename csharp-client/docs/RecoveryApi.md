# IO.Swagger.Api.RecoveryApi

All URIs are relative to *https://api.springpathinc.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**OpDpGroupHaltPut**](RecoveryApi.md#opdpgrouphaltput) | **PUT** /groups/{groupId}/halt | Halt replication for the group
[**OpDpVmHaltPut**](RecoveryApi.md#opdpvmhaltput) | **PUT** /vms/{vmId}/halt | Halt replication for the given VM
[**OpDpVmRecoveryFailoverPut**](RecoveryApi.md#opdpvmrecoveryfailoverput) | **PUT** /vms/{vmId}/recovery/failover | Failover recovery for the given VM
[**OpDpVmRecoveryJobsGet**](RecoveryApi.md#opdpvmrecoveryjobsget) | **GET** /recovery/jobs | Recover job status for the given VM
[**OpDpVmRecoveryJobsJobIdGet**](RecoveryApi.md#opdpvmrecoveryjobsjobidget) | **GET** /recovery/jobs/{jobId} | Recover job status for the given VM
[**OpDpVmRecoveryTestPut**](RecoveryApi.md#opdpvmrecoverytestput) | **PUT** /vms/{vmId}/recovery/test | Test recovery for the given VM


<a name="opdpgrouphaltput"></a>
# **OpDpGroupHaltPut**
> void OpDpGroupHaltPut (string groupId, string acceptLanguage = null)

Halt replication for the group



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpGroupHaltPutExample
    {
        public void main()
        {
            var apiInstance = new RecoveryApi();
            var groupId = groupId_example;  // string | ID of the group
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Halt replication for the group
                apiInstance.OpDpGroupHaltPut(groupId, acceptLanguage);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecoveryApi.OpDpGroupHaltPut: " + e.Message );
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

<a name="opdpvmhaltput"></a>
# **OpDpVmHaltPut**
> void OpDpVmHaltPut (string vmId, string acceptLanguage = null)

Halt replication for the given VM



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmHaltPutExample
    {
        public void main()
        {
            var apiInstance = new RecoveryApi();
            var vmId = vmId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Halt replication for the given VM
                apiInstance.OpDpVmHaltPut(vmId, acceptLanguage);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecoveryApi.OpDpVmHaltPut: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmrecoveryfailoverput"></a>
# **OpDpVmRecoveryFailoverPut**
> string OpDpVmRecoveryFailoverPut (string vmId, RecoverVmOptions body = null, string acceptLanguage = null)

Failover recovery for the given VM



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmRecoveryFailoverPutExample
    {
        public void main()
        {
            var apiInstance = new RecoveryApi();
            var vmId = vmId_example;  // string | 
            var body = new RecoverVmOptions(); // RecoverVmOptions | Folder, Resource and Network options for recovery (optional) 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Failover recovery for the given VM
                string result = apiInstance.OpDpVmRecoveryFailoverPut(vmId, body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecoveryApi.OpDpVmRecoveryFailoverPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **vmId** | **string**|  | 
 **body** | [**RecoverVmOptions**](RecoverVmOptions.md)| Folder, Resource and Network options for recovery | [optional] 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmrecoveryjobsget"></a>
# **OpDpVmRecoveryJobsGet**
> List<Job> OpDpVmRecoveryJobsGet (string acceptLanguage = null)

Recover job status for the given VM



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmRecoveryJobsGetExample
    {
        public void main()
        {
            var apiInstance = new RecoveryApi();
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Recover job status for the given VM
                List&lt;Job&gt; result = apiInstance.OpDpVmRecoveryJobsGet(acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecoveryApi.OpDpVmRecoveryJobsGet: " + e.Message );
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

[**List<Job>**](Job.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmrecoveryjobsjobidget"></a>
# **OpDpVmRecoveryJobsJobIdGet**
> List<Job> OpDpVmRecoveryJobsJobIdGet (string jobId, string acceptLanguage = null)

Recover job status for the given VM



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmRecoveryJobsJobIdGetExample
    {
        public void main()
        {
            var apiInstance = new RecoveryApi();
            var jobId = jobId_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Recover job status for the given VM
                List&lt;Job&gt; result = apiInstance.OpDpVmRecoveryJobsJobIdGet(jobId, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecoveryApi.OpDpVmRecoveryJobsJobIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **jobId** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**List<Job>**](Job.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opdpvmrecoverytestput"></a>
# **OpDpVmRecoveryTestPut**
> string OpDpVmRecoveryTestPut (string vmId, RecoverVmOptions body = null, string acceptLanguage = null)

Test recovery for the given VM



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpDpVmRecoveryTestPutExample
    {
        public void main()
        {
            var apiInstance = new RecoveryApi();
            var vmId = vmId_example;  // string | 
            var body = new RecoverVmOptions(); // RecoverVmOptions | Folder, Resource and Network options for recovery (optional) 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Test recovery for the given VM
                string result = apiInstance.OpDpVmRecoveryTestPut(vmId, body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecoveryApi.OpDpVmRecoveryTestPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **vmId** | **string**|  | 
 **body** | [**RecoverVmOptions**](RecoverVmOptions.md)| Folder, Resource and Network options for recovery | [optional] 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

