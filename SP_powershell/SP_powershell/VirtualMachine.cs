using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SP_powershell
{
  
    public class VirtualMachine
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("statusCode")]
        public StatusCode vmStatusCode { get; set; }

        //public string runTimeInfo { get; set; }
        
        // [JsonProperty("type")]
        //public string Type { get; set; }

        //public bool IsProtected { get; set; }

        [JsonProperty("type")]
        public VirtualMachineType VMachineType { get; set; }
           

       
                

        public VirtualMachine()
        {
            //explicitly initializing variables
            
        }
    }
    public enum VirtualMachineType
    {
        INCOMING,

        OUTGOING
    }

    public enum StatusCode
    {
        VM_ACCESSIBLE,

        VM_INACCESSIBLE,

        VM_NOT_SUPPORTED,

        NONE

    }
}