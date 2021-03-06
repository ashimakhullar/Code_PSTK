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
    /// DisplayReplicationDetails
    /// </summary>
    [DataContract]
    public partial class DisplayReplicationDetails :  IEquatable<DisplayReplicationDetails>, IValidatableObject
    {
        /// <summary>
        /// Status of replication between current and remote cluster
        /// </summary>
        /// <value>Status of replication between current and remote cluster</value>
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
            /// Enum PAUSED for "PAUSED"
            /// </summary>
            [EnumMember(Value = "PAUSED")]
            PAUSED,
            
            /// <summary>
            /// Enum STARTING for "STARTING"
            /// </summary>
            [EnumMember(Value = "STARTING")]
            STARTING,
            
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
            /// Enum NONE for "NONE"
            /// </summary>
            [EnumMember(Value = "NONE")]
            NONE
        }

        /// <summary>
        /// Status of replication between current and remote cluster
        /// </summary>
        /// <value>Status of replication between current and remote cluster</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayReplicationDetails" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected DisplayReplicationDetails() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayReplicationDetails" /> class.
        /// </summary>
        /// <param name="VmName">Name of the Virtual Machine (required).</param>
        /// <param name="RemoteClusterName">Name of the remote Cluster (required).</param>
        /// <param name="Status">Status of replication between current and remote cluster (required).</param>
        /// <param name="StartTime">Start time of replication between current and remote cluster in milliseconds (required).</param>
        /// <param name="EndTime">End time of replication between current and remote cluster in milliseconds.</param>
        /// <param name="ProtectionGroup">Protection Group to which the Virtual Machine belongs.</param>
        /// <param name="Direction">Direction of replication between current and remote cluster (required).</param>
        /// <param name="DataTransferred">Bytes transferred between current and remote cluster as part of this replication (required).</param>
        /// <param name="PrecentTransferred">Percentage of replication complete between current and remote cluster as part of this replication (required).</param>
        public DisplayReplicationDetails(string VmName = default(string), string RemoteClusterName = default(string), StatusEnum? Status = default(StatusEnum?), long? StartTime = default(long?), long? EndTime = default(long?), string ProtectionGroup = default(string), string Direction = default(string), long? DataTransferred = default(long?), int? PrecentTransferred = default(int?))
        {
            // to ensure "VmName" is required (not null)
            if (VmName == null)
            {
                throw new InvalidDataException("VmName is a required property for DisplayReplicationDetails and cannot be null");
            }
            else
            {
                this.VmName = VmName;
            }
            // to ensure "RemoteClusterName" is required (not null)
            if (RemoteClusterName == null)
            {
                throw new InvalidDataException("RemoteClusterName is a required property for DisplayReplicationDetails and cannot be null");
            }
            else
            {
                this.RemoteClusterName = RemoteClusterName;
            }
            // to ensure "Status" is required (not null)
            if (Status == null)
            {
                throw new InvalidDataException("Status is a required property for DisplayReplicationDetails and cannot be null");
            }
            else
            {
                this.Status = Status;
            }
            // to ensure "StartTime" is required (not null)
            if (StartTime == null)
            {
                throw new InvalidDataException("StartTime is a required property for DisplayReplicationDetails and cannot be null");
            }
            else
            {
                this.StartTime = StartTime;
            }
            // to ensure "Direction" is required (not null)
            if (Direction == null)
            {
                throw new InvalidDataException("Direction is a required property for DisplayReplicationDetails and cannot be null");
            }
            else
            {
                this.Direction = Direction;
            }
            // to ensure "DataTransferred" is required (not null)
            if (DataTransferred == null)
            {
                throw new InvalidDataException("DataTransferred is a required property for DisplayReplicationDetails and cannot be null");
            }
            else
            {
                this.DataTransferred = DataTransferred;
            }
            // to ensure "PrecentTransferred" is required (not null)
            if (PrecentTransferred == null)
            {
                throw new InvalidDataException("PrecentTransferred is a required property for DisplayReplicationDetails and cannot be null");
            }
            else
            {
                this.PrecentTransferred = PrecentTransferred;
            }
            this.EndTime = EndTime;
            this.ProtectionGroup = ProtectionGroup;
        }
        
        /// <summary>
        /// Name of the Virtual Machine
        /// </summary>
        /// <value>Name of the Virtual Machine</value>
        [DataMember(Name="vmName", EmitDefaultValue=false)]
        public string VmName { get; set; }

        /// <summary>
        /// Name of the remote Cluster
        /// </summary>
        /// <value>Name of the remote Cluster</value>
        [DataMember(Name="remoteClusterName", EmitDefaultValue=false)]
        public string RemoteClusterName { get; set; }


        /// <summary>
        /// Start time of replication between current and remote cluster in milliseconds
        /// </summary>
        /// <value>Start time of replication between current and remote cluster in milliseconds</value>
        [DataMember(Name="startTime", EmitDefaultValue=false)]
        public long? StartTime { get; set; }

        /// <summary>
        /// End time of replication between current and remote cluster in milliseconds
        /// </summary>
        /// <value>End time of replication between current and remote cluster in milliseconds</value>
        [DataMember(Name="endTime", EmitDefaultValue=false)]
        public long? EndTime { get; set; }

        /// <summary>
        /// Protection Group to which the Virtual Machine belongs
        /// </summary>
        /// <value>Protection Group to which the Virtual Machine belongs</value>
        [DataMember(Name="protectionGroup", EmitDefaultValue=false)]
        public string ProtectionGroup { get; set; }

        /// <summary>
        /// Direction of replication between current and remote cluster
        /// </summary>
        /// <value>Direction of replication between current and remote cluster</value>
        [DataMember(Name="direction", EmitDefaultValue=false)]
        public string Direction { get; set; }

        /// <summary>
        /// Bytes transferred between current and remote cluster as part of this replication
        /// </summary>
        /// <value>Bytes transferred between current and remote cluster as part of this replication</value>
        [DataMember(Name="dataTransferred", EmitDefaultValue=false)]
        public long? DataTransferred { get; set; }

        /// <summary>
        /// Percentage of replication complete between current and remote cluster as part of this replication
        /// </summary>
        /// <value>Percentage of replication complete between current and remote cluster as part of this replication</value>
        [DataMember(Name="precentTransferred", EmitDefaultValue=false)]
        public int? PrecentTransferred { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DisplayReplicationDetails {\n");
            sb.Append("  VmName: ").Append(VmName).Append("\n");
            sb.Append("  RemoteClusterName: ").Append(RemoteClusterName).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  StartTime: ").Append(StartTime).Append("\n");
            sb.Append("  EndTime: ").Append(EndTime).Append("\n");
            sb.Append("  ProtectionGroup: ").Append(ProtectionGroup).Append("\n");
            sb.Append("  Direction: ").Append(Direction).Append("\n");
            sb.Append("  DataTransferred: ").Append(DataTransferred).Append("\n");
            sb.Append("  PrecentTransferred: ").Append(PrecentTransferred).Append("\n");
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
            return this.Equals(obj as DisplayReplicationDetails);
        }

        /// <summary>
        /// Returns true if DisplayReplicationDetails instances are equal
        /// </summary>
        /// <param name="other">Instance of DisplayReplicationDetails to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DisplayReplicationDetails other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.VmName == other.VmName ||
                    this.VmName != null &&
                    this.VmName.Equals(other.VmName)
                ) && 
                (
                    this.RemoteClusterName == other.RemoteClusterName ||
                    this.RemoteClusterName != null &&
                    this.RemoteClusterName.Equals(other.RemoteClusterName)
                ) && 
                (
                    this.Status == other.Status ||
                    this.Status != null &&
                    this.Status.Equals(other.Status)
                ) && 
                (
                    this.StartTime == other.StartTime ||
                    this.StartTime != null &&
                    this.StartTime.Equals(other.StartTime)
                ) && 
                (
                    this.EndTime == other.EndTime ||
                    this.EndTime != null &&
                    this.EndTime.Equals(other.EndTime)
                ) && 
                (
                    this.ProtectionGroup == other.ProtectionGroup ||
                    this.ProtectionGroup != null &&
                    this.ProtectionGroup.Equals(other.ProtectionGroup)
                ) && 
                (
                    this.Direction == other.Direction ||
                    this.Direction != null &&
                    this.Direction.Equals(other.Direction)
                ) && 
                (
                    this.DataTransferred == other.DataTransferred ||
                    this.DataTransferred != null &&
                    this.DataTransferred.Equals(other.DataTransferred)
                ) && 
                (
                    this.PrecentTransferred == other.PrecentTransferred ||
                    this.PrecentTransferred != null &&
                    this.PrecentTransferred.Equals(other.PrecentTransferred)
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
                if (this.VmName != null)
                    hash = hash * 59 + this.VmName.GetHashCode();
                if (this.RemoteClusterName != null)
                    hash = hash * 59 + this.RemoteClusterName.GetHashCode();
                if (this.Status != null)
                    hash = hash * 59 + this.Status.GetHashCode();
                if (this.StartTime != null)
                    hash = hash * 59 + this.StartTime.GetHashCode();
                if (this.EndTime != null)
                    hash = hash * 59 + this.EndTime.GetHashCode();
                if (this.ProtectionGroup != null)
                    hash = hash * 59 + this.ProtectionGroup.GetHashCode();
                if (this.Direction != null)
                    hash = hash * 59 + this.Direction.GetHashCode();
                if (this.DataTransferred != null)
                    hash = hash * 59 + this.DataTransferred.GetHashCode();
                if (this.PrecentTransferred != null)
                    hash = hash * 59 + this.PrecentTransferred.GetHashCode();
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
