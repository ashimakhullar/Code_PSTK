/* 
 * Swagger Server
 *
 *Ashima Bahl
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
    /// AccessTokenSpec
    /// </summary>
    [DataContract]
    public partial class AccessTokenSpec :  IEquatable<AccessTokenSpec>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedVMSpec" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected AccessTokenSpec() { }
        /// <summary>AccessTokenSpec
        /// Initializes a new instance of the <see cref="AccessTokenSpec" /> class.
        /// </summary>
        /// <param name="VmEr">VmEr (required).</param>
        /// <param name="Schedule">A Map from Replication Cluster Id to the schedule.</param>
        public AccessTokenSpec(EntityRef VmEr = default(EntityRef), List<ReplicationClusterErToSchedule> Schedule = default(List<ReplicationClusterErToSchedule>))
        {
            // to ensure "VmEr" is required (not null)
                 if (VmEr == null)
            {
                throw new InvalidDataException("VmEr is a required property for ProtectedVMSpec and cannot be null");
            }
            else
            {
                this.VmEr = VmEr;
            }
            this.Schedule = Schedule;
        }
        
        /// <summary>
        /// Gets or Sets VmEr
        /// </summary>
        [DataMember(Name="vmEr", EmitDefaultValue=false)]
        public EntityRef VmEr { get; set; }

        /// <summary>
        /// A Map from Replication Cluster Id to the schedule
        /// </summary>
        /// <value>A Map from Replication Cluster Id to the schedule</value>
        [DataMember(Name="schedule", EmitDefaultValue=false)]
        public List<ReplicationClusterErToSchedule> Schedule { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProtectedVMSpec {\n");
            sb.Append("  VmEr: ").Append(VmEr).Append("\n");
            sb.Append("  Schedule: ").Append(Schedule).Append("\n");
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
            return this.Equals(obj as ProtectedVMSpec);
        }

        /// <summary>
        /// Returns true if ProtectedVMSpec instances are equal
        /// </summary>
        /// <param name="other">Instance of ProtectedVMSpec to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AccessTokenSpec other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.VmEr == other.VmEr ||
                    this.VmEr != null &&
                    this.VmEr.Equals(other.VmEr)
                ) && 
                (
                    this.Schedule == other.Schedule ||
                    this.Schedule != null &&
                    this.Schedule.SequenceEqual(other.Schedule)
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
                if (this.VmEr != null)
                    hash = hash * 59 + this.VmEr.GetHashCode();
                if (this.Schedule != null)
                    hash = hash * 59 + this.Schedule.GetHashCode();
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
