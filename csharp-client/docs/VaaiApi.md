# IO.Swagger.Api.VaaiApi

All URIs are relative to *https://api.springpathinc.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**OpVaaiVmCloneJobJobidGetStatus**](VaaiApi.md#opvaaivmclonejobjobidgetstatus) | **GET** /vaai/vm/clone/job/{jobid}/status | Return job status
[**OpVaaiVmCloneJobPost**](VaaiApi.md#opvaaivmclonejobpost) | **POST** /vaai/vm/clone/job | Creates a specified number of clones for the given vm.
[**OpVaaiVmCloneJobsGet**](VaaiApi.md#opvaaivmclonejobsget) | **GET** /vaai/vm/clone/jobs | Get list of clones jobs.


<a name="opvaaivmclonejobjobidgetstatus"></a>
# **OpVaaiVmCloneJobJobidGetStatus**
> Job OpVaaiVmCloneJobJobidGetStatus (string jobid, string acceptLanguage = null)

Return job status



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpVaaiVmCloneJobJobidGetStatusExample
    {
        public void main()
        {
            var apiInstance = new VaaiApi();
            var jobid = jobid_example;  // string | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Return job status
                Job result = apiInstance.OpVaaiVmCloneJobJobidGetStatus(jobid, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling VaaiApi.OpVaaiVmCloneJobJobidGetStatus: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **jobid** | **string**|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**Job**](Job.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opvaaivmclonejobpost"></a>
# **OpVaaiVmCloneJobPost**
> Job OpVaaiVmCloneJobPost (VmCloneSpec body, string acceptLanguage = null)

Creates a specified number of clones for the given vm.



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpVaaiVmCloneJobPostExample
    {
        public void main()
        {
            var apiInstance = new VaaiApi();
            var body = new VmCloneSpec(); // VmCloneSpec | 
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // Creates a specified number of clones for the given vm.
                Job result = apiInstance.OpVaaiVmCloneJobPost(body, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling VaaiApi.OpVaaiVmCloneJobPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**VmCloneSpec**](VmCloneSpec.md)|  | 
 **acceptLanguage** | **string**|  | [optional] 

### Return type

[**Job**](Job.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="opvaaivmclonejobsget"></a>
# **OpVaaiVmCloneJobsGet**
> List<Job> OpVaaiVmCloneJobsGet (int? maxJobs = null, long? maxTimeDiffMinutes = null)

Get list of clones jobs.



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpVaaiVmCloneJobsGetExample
    {
        public void main()
        {
            var apiInstance = new VaaiApi();
            var maxJobs = 56;  // int? | maximum number of jobs will be returned (optional) 
            var maxTimeDiffMinutes = 789;  // long? | jobs created in last \"maxTimeDiffMinutes\" minutes (optional) 

            try
            {
                // Get list of clones jobs.
                List&lt;Job&gt; result = apiInstance.OpVaaiVmCloneJobsGet(maxJobs, maxTimeDiffMinutes);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling VaaiApi.OpVaaiVmCloneJobsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **maxJobs** | **int?**| maximum number of jobs will be returned | [optional] 
 **maxTimeDiffMinutes** | **long?**| jobs created in last \&quot;maxTimeDiffMinutes\&quot; minutes | [optional] 

### Return type

[**List<Job>**](Job.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

