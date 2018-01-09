# IO.Swagger.Api.AboutApi

All URIs are relative to *https://api.springpathinc.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**OpAboutGet**](AboutApi.md#opaboutget) | **GET** /about | HX Installation information


<a name="opaboutget"></a>
# **OpAboutGet**
> AboutInfo OpAboutGet (string acceptLanguage = null)

HX Installation information



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OpAboutGetExample
    {
        public void main()
        {
            var apiInstance = new AboutApi();
            var acceptLanguage = acceptLanguage_example;  // string |  (optional) 

            try
            {
                // HX Installation information
                AboutInfo result = apiInstance.OpAboutGet(acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AboutApi.OpAboutGet: " + e.Message );
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

[**AboutInfo**](AboutInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

