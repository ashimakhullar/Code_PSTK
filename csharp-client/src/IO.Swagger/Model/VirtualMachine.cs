/* 
 * Swagger Server
 *
 * No descripton provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// Virtual Machine
    /// </summary>
    [DataContract]
    public partial class VirtualMachine :  IEquatable<VirtualMachine>, IValidatableObject
    {
        /// <summary>
        /// Status of the VM
        /// </summary>
        /// <value>Status of the VM</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusCodeEnum
        {
            
            /// <summary>
            /// Enum VMACCESSIBLE for "VM_ACCESSIBLE"
            /// </summary>
            [EnumMember(Value = "VM_ACCESSIBLE")]
            VMACCESSIBLE,
            
            /// <summary>
            /// Enum VMINACCESSIBLE for "VM_INACCESSIBLE"
            /// </summary>
            [EnumMember(Value = "VM_INACCESSIBLE")]
            VMINACCESSIBLE,
            
            /// <summary>
            /// Enum VMNOTSUPPORTED for "VM_NOT_SUPPORTED"
            /// </summary>
            [EnumMember(Value = "VM_NOT_SUPPORTED")]
            VMNOTSUPPORTED,
            
            /// <summary>
            /// Enum NONE for "NONE"
            /// </summary>
            [EnumMember(Value = "NONE")]
            NONE
        }

        /// <summary>
        /// Status of the VM
        /// </summary>
        /// <value>Status of the VM</value>
        [DataMember(Name="statusCode", EmitDefaultValue=false)]
        public StatusCodeEnum? StatusCode { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMachine" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected VirtualMachine() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMachine" /> class.
        /// </summary>
        /// <param name="Uuid">Virtual machine BIOS UUID (required).</param>
        /// <param name="StatusCode">Status of the VM (required).</param>
        /// <param name="RunTimeInfo">Virtual machine info details.</param>
        public VirtualMachine(string Uuid = default(string), StatusCodeEnum? StatusCode = default(StatusCodeEnum?), VirtualMachineRuntimeInfo RunTimeInfo = default(VirtualMachineRuntimeInfo))
        {
            // to ensure "Uuid" is required (not null)
            if (Uuid == null)
            {
                throw new InvalidDataException("Uuid is a required property for VirtualMachine and cannot be null");
            }
            else
            {
                this.Uuid = Uuid;
            }
            // to ensure "StatusCode" is required (not null)
            if (StatusCode == null)
            {
                throw new InvalidDataException("StatusCode is a required property for VirtualMachine and cannot be null");
            }
            else
            {
                this.StatusCode = StatusCode;
            }
            this.RunTimeInfo = RunTimeInfo;
        }
        
        /// <summary>
        /// Virtual machine BIOS UUID
        /// </summary>
        /// <value>Virtual machine BIOS UUID</value>
        [DataMember(Name="uuid", EmitDefaultValue=false)]
        public string Uuid { get; set; }


        /// <summary>
        /// Virtual machine info details
        /// </summary>
        /// <value>Virtual machine info details</value>
        [DataMember(Name="runTimeInfo", EmitDefaultValue=false)]
        public VirtualMachineRuntimeInfo RunTimeInfo { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VirtualMachine {\n");
            sb.Append("  Uuid: ").Append(Uuid).Append("\n");
            sb.Append("  StatusCode: ").Append(StatusCode).Append("\n");
            sb.Append("  RunTimeInfo: ").Append(RunTimeInfo).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as VirtualMachine);
        }

        /// <summary>
        /// Returns true if VirtualMachine instances are equal
        /// </summary>
        /// <param name="other">Instance of VirtualMachine to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VirtualMachine other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Uuid == other.Uuid ||
                    this.Uuid != null &&
                    this.Uuid.Equals(other.Uuid)
                ) && 
                (
                    this.StatusCode == other.StatusCode ||
                    this.StatusCode != null &&
                    this.StatusCode.Equals(other.StatusCode)
                ) && 
                (
                    this.RunTimeInfo == other.RunTimeInfo ||
                    this.RunTimeInfo != null &&
                    this.RunTimeInfo.Equals(other.RunTimeInfo)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.Uuid != null)
                    hash = hash * 59 + this.Uuid.GetHashCode();
                if (this.StatusCode != null)
                    hash = hash * 59 + this.StatusCode.GetHashCode();
                if (this.RunTimeInfo != null)
                    hash = hash * 59 + this.RunTimeInfo.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
