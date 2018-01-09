using IO.Swagger.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SP_powershell
{
    public sealed class VirtualMachineEx
    {

        /// <summary>
        /// Gets the VMS.
        /// </summary>
        /// <param name="SPServer">The SpringPath server.</param>
        /// <param name="protect">If true, adds the protect parameter (REST API query string).</param>
        /// <returns>Collection{VirtualMachine}.</returns>
        ///  Creates your output container
      /*  public VirtualMachine[] GetVMs()
        {
            //Verify if the server is valid
            //Debug.Assert(SPServer != null);
            //dummy data in the format as recieved from the api Response
            // Configure OAuth2 access token for authorization: petstore_auth
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new IO.Swagger.Api.ProtectApi();
            //var petId = 789;  // long? | Pet id to delete
           // var apiKey = "apiKey_example";  // string |  (optional) 

            try
            {
                // gets the vms
                List<IO.Swagger.Model.ProtectionGroupInfo> v1;
                //apiInstance.DeletePet(petId, apiKey);
                v1=apiInstance.OpDpGroupGet();
                var res1 = JsonConvert.DeserializeObject<VirtualMachine[]>(v1);
                return res1;
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PetApi.DeletePet: " + e.Message);
            }

            //////////string[] sentences = new string[3];
            //////////sentences[0] = "[{\"name\":\"VM1\",\"uuid\":\"dmmmm_vm1\",\"type\":\"INCOMING\",\"statusCode\" : \"NONE\"},";
            //////////sentences[1] = "{\"name\":\"VM2\",\"uuid\":\"dmmmm_vm2\",\"type\":\"INCOMING\",\"statusCode\" : \"VM_ACCESSIBLE\"},";
            //////////sentences[2] = "{\"name\":\"VM3\",\"uuid\":\"dmmmm_vm3\",\"type\":\"OUTGOING\",\"statusCode\" : \"VM_INACCESSIBLE\"}]";

            //////////string newArr = sentences[0] + sentences[1] + sentences[2];


            //////////var res = JsonConvert.DeserializeObject<VirtualMachine[]>(newArr);
            ////////////returns the json response to the GetSPVM class
            //////////return res;
            ////////////var ts = serverMap[SPServer];
            ////////////  return ts.VM.GetAllVms().GetAwaiter().GetResult();



        }*/

    }
}
