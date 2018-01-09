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
    /// SnapshotStatus
    /// </summary>
    [DataContract]
    public partial class SnapshotStatus :  IEquatable<SnapshotStatus>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum SUCCESS for "SUCCESS"
            /// </summary>
            [EnumMember(Value = "SUCCESS")]
            SUCCESS,
            
            /// <summary>
            /// Enum FAILED for "FAILED"
            /// </summary>
            [EnumMember(Value = "FAILED")]
            FAILED,
            
            /// <summary>
            /// Enum INPROGRESS for "IN_PROGRESS"
            /// </summary>
            [EnumMember(Value = "IN_PROGRESS")]
            INPROGRESS,
            
            /// <summary>
            /// Enum DELETED for "DELETED"
            /// </summary>
            [EnumMember(Value = "DELETED")]
            DELETED,
            
            /// <summary>
            /// Enum DELETING for "DELETING"
            /// </summary>
            [EnumMember(Value = "DELETING")]
            DELETING,
            
            /// <summary>
            /// Enum NONE for "NONE"
            /// </summary>
            [EnumMember(Value = "NONE")]
            NONE
        }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotStatus" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected SnapshotStatus() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotStatus" /> class.
        /// </summary>
        /// <param name="Status">Status (required).</param>
        /// <param name="Timestamp">Timestamp at which the Snapshot is taken.</param>
        /// <param name="Description">Description of this Snapshot Point.</param>
        /// <param name="UsedSpace">Space Used by this Snapshot Point.</param>
        /// <param name="PctComplete">PctComplete.</param>
        /// <param name="Ex">Ex.</param>
        public SnapshotStatus(StatusEnum? Status = default(StatusEnum?), long? Timestamp = default(long?), string Description = default(string), long? UsedSpace = default(long?), int? PctComplete = default(int?), ErrorStack Ex = default(ErrorStack))
        {
            // to ensure "Status" is required (not null)
            if (Status == null)
            {
                throw new InvalidDataException("Status is a required property for SnapshotStatus and cannot be null");
            }
            else
            {
                this.Status = Status;
            }
            this.Timestamp = Timestamp;
            this.Description = Description;
            this.UsedSpace = UsedSpace;
            this.PctComplete = PctComplete;
            this.Ex = Ex;
        }
        

        /// <summary>
        /// Timestamp at which the Snapshot is taken
        /// </summary>
        /// <value>Timestamp at which the Snapshot is taken</value>
        [DataMember(Name="timestamp", EmitDefaultValue=false)]
        public long? Timestamp { get; set; }

        /// <summary>
        /// Description of this Snapshot Point
        /// </summary>
        /// <value>Description of this Snapshot Point</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Space Used by this Snapshot Point
        /// </summary>
        /// <value>Space Used by this Snapshot Point</value>
        [DataMember(Name="usedSpace", EmitDefaultValue=false)]
        public long? UsedSpace { get; set; }

        /// <summary>
        /// Gets or Sets PctComplete
        /// </summary>
        [DataMember(Name="pctComplete", EmitDefaultValue=false)]
        public int? PctComplete { get; set; }

        /// <summary>
        /// Gets or Sets Ex
        /// </summary>
        [DataMember(Name="ex", EmitDefaultValue=false)]
        public ErrorStack Ex { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SnapshotStatus {\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  UsedSpace: ").Append(UsedSpace).Append("\n");
            sb.Append("  PctComplete: ").Append(PctComplete).Append("\n");
            sb.Append("  Ex: ").Append(Ex).Append("\n");
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
            return this.Equals(obj as SnapshotStatus);
        }

        /// <summary>
        /// Returns true if SnapshotStatus instances are equal
        /// </summary>
        /// <param name="other">Instance of SnapshotStatus to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SnapshotStatus other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Status == other.Status ||
                    this.Status != null &&
                    this.Status.Equals(other.Status)
                ) && 
                (
                    this.Timestamp == other.Timestamp ||
                    this.Timestamp != null &&
                    this.Timestamp.Equals(other.Timestamp)
                ) && 
                (
                    this.Description == other.Description ||
                    this.Description != null &&
                    this.Description.Equals(other.Description)
                ) && 
                (
                    this.UsedSpace == other.UsedSpace ||
                    this.UsedSpace != null &&
                    this.UsedSpace.Equals(other.UsedSpace)
                ) && 
                (
                    this.PctComplete == other.PctComplete ||
                    this.PctComplete != null &&
                    this.PctComplete.Equals(other.PctComplete)
                ) && 
                (
                    this.Ex == other.Ex ||
                    this.Ex != null &&
                    this.Ex.Equals(other.Ex)
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
                if (this.Status != null)
                    hash = hash * 59 + this.Status.GetHashCode();
                if (this.Timestamp != null)
                    hash = hash * 59 + this.Timestamp.GetHashCode();
                if (this.Description != null)
                    hash = hash * 59 + this.Description.GetHashCode();
                if (this.UsedSpace != null)
                    hash = hash * 59 + this.UsedSpace.GetHashCode();
                if (this.PctComplete != null)
                    hash = hash * 59 + this.PctComplete.GetHashCode();
                if (this.Ex != null)
                    hash = hash * 59 + this.Ex.GetHashCode();
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
