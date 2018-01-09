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
    /// ProtectionGroupSpec
    /// </summary>
    [DataContract]
    public partial class ProtectionGroupSpec :  IEquatable<ProtectionGroupSpec>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectionGroupSpec" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ProtectionGroupSpec() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectionGroupSpec" /> class.
        /// </summary>
        /// <param name="Name">Name of the group (required).</param>
        /// <param name="Description">Description of the group.</param>
        /// <param name="Members">Must be virtual machines for 1.0, may include groups later.</param>
        /// <param name="Schedule">A Map from Replication Cluster Id to the schedule (required).</param>
        public ProtectionGroupSpec(string Name = default(string), string Description = default(string), List<EntityRef> Members = default(List<EntityRef>), List<ReplicationClusterErToSchedule> Schedule = default(List<ReplicationClusterErToSchedule>))
        {
            // to ensure "Name" is required (not null)
            if (Name == null)
            {
                throw new InvalidDataException("Name is a required property for ProtectionGroupSpec and cannot be null");
            }
            else
            {
                this.Name = Name;
            }
            // to ensure "Schedule" is required (not null)
            if (Schedule == null)
            {
                throw new InvalidDataException("Schedule is a required property for ProtectionGroupSpec and cannot be null");
            }
            else
            {
                this.Schedule = Schedule;
            }
            this.Description = Description;
            this.Members = Members;
        }
        
        /// <summary>
        /// Name of the group
        /// </summary>
        /// <value>Name of the group</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the group
        /// </summary>
        /// <value>Description of the group</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Must be virtual machines for 1.0, may include groups later
        /// </summary>
        /// <value>Must be virtual machines for 1.0, may include groups later</value>
        [DataMember(Name="members", EmitDefaultValue=false)]
        public List<EntityRef> Members { get; set; }

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
            sb.Append("class ProtectionGroupSpec {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Members: ").Append(Members).Append("\n");
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
            return this.Equals(obj as ProtectionGroupSpec);
        }

        /// <summary>
        /// Returns true if ProtectionGroupSpec instances are equal
        /// </summary>
        /// <param name="other">Instance of ProtectionGroupSpec to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProtectionGroupSpec other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) && 
                (
                    this.Description == other.Description ||
                    this.Description != null &&
                    this.Description.Equals(other.Description)
                ) && 
                (
                    this.Members == other.Members ||
                    this.Members != null &&
                    this.Members.SequenceEqual(other.Members)
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
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
                if (this.Description != null)
                    hash = hash * 59 + this.Description.GetHashCode();
                if (this.Members != null)
                    hash = hash * 59 + this.Members.GetHashCode();
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
