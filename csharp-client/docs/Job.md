# IO.Swagger.Model.Job
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Jobid** | **string** | job id | 
**State** | **string** | State of the job | 
**TimeSubmittedMillis** | **long?** | Job submitted time | 
**TimeStartedMillis** | **long?** | Time elapsed | [optional] 
**TimeElapsedMillis** | **long?** | Time elapsed | [optional] 
**OutParameters** | [**List&lt;StringToListStringMap&gt;**](StringToListStringMap.md) |  | 
**JobSteps** | [**List&lt;EntityRefToStepMap&gt;**](EntityRefToStepMap.md) |  | 
**SuspendOnExit** | **bool?** |  suspend on exit | [default to false]
**MethodName** | **string** | Method name | 
**Description** | **string** | Job description | [optional] 
**Tag** | **string** | Job tag | [optional] 
**Owner** | **string** | Job owner | [optional] 
**Message** | **string** | Job message | [optional] 
**IsCancelRequested** | **bool?** | Cancel requested | [default to false]
**IsCanceled** | **bool?** | Job Canceled | [default to false]
**PercentComplete** | **int?** | Percent completed | [optional] 
**LifetimeAfterExitMillis** | **long?** | Lifetime after exit in milliseconds | 
**SummaryStepState** | **string** | Step state | 
**LcDescription** | [**LocalizableMessage**](LocalizableMessage.md) |  | [optional] 
**LcMessage** | [**LocalizableMessage**](LocalizableMessage.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

