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
    /// Step
    /// </summary>
    [DataContract]
    public partial class Step :  IEquatable<Step>, IValidatableObject
    {
        /// <summary>
        /// state of step
        /// </summary>
        /// <value>state of step</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StateEnum
        {
            
            /// <summary>
            /// Enum NOTSTARTED for "NOTSTARTED"
            /// </summary>
            [EnumMember(Value = "NOTSTARTED")]
            NOTSTARTED,
            
            /// <summary>
            /// Enum INPROGRESS for "INPROGRESS"
            /// </summary>
            [EnumMember(Value = "INPROGRESS")]
            INPROGRESS,
            
            /// <summary>
            /// Enum SUCCEEDED for "SUCCEEDED"
            /// </summary>
            [EnumMember(Value = "SUCCEEDED")]
            SUCCEEDED,
            
            /// <summary>
            /// Enum FAILED for "FAILED"
            /// </summary>
            [EnumMember(Value = "FAILED")]
            FAILED
        }

        /// <summary>
        /// state of step
        /// </summary>
        /// <value>state of step</value>
        [DataMember(Name="state", EmitDefaultValue=false)]
        public StateEnum? State { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Step" /> class.
        /// </summary>
        /// <param name="Name">Step name.</param>
        /// <param name="State">state of step.</param>
        /// <param name="Description">Step description.</param>
        /// <param name="Entity">Entity.</param>
        /// <param name="Fault">Fault.</param>
        /// <param name="Completion">Completion.</param>
        /// <param name="Parent">Parent.</param>
        /// <param name="IsValidation">Validation (default to false).</param>
        /// <param name="LcDescription">LcDescription.</param>
        public Step(string Name = default(string), StateEnum? State = default(StateEnum?), string Description = default(string), EntityRef Entity = default(EntityRef), ErrorStack Fault = default(ErrorStack), int? Completion = default(int?), EntityRef Parent = default(EntityRef), bool? IsValidation = false, LocalizableMessage LcDescription = default(LocalizableMessage))
        {
            this.Name = Name;
            this.State = State;
            this.Description = Description;
            this.Entity = Entity;
            this.Fault = Fault;
            this.Completion = Completion;
            this.Parent = Parent;
            // use default value if no "IsValidation" provided
            if (IsValidation == null)
            {
                this.IsValidation = false;
            }
            else
            {
                this.IsValidation = IsValidation;
            }
            this.LcDescription = LcDescription;
        }
        
        /// <summary>
        /// Step name
        /// </summary>
        /// <value>Step name</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }


        /// <summary>
        /// Step description
        /// </summary>
        /// <value>Step description</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Entity
        /// </summary>
        [DataMember(Name="entity", EmitDefaultValue=false)]
        public EntityRef Entity { get; set; }

        /// <summary>
        /// Gets or Sets Fault
        /// </summary>
        [DataMember(Name="fault", EmitDefaultValue=false)]
        public ErrorStack Fault { get; set; }

        /// <summary>
        /// Gets or Sets Completion
        /// </summary>
        [DataMember(Name="completion", EmitDefaultValue=false)]
        public int? Completion { get; set; }

        /// <summary>
        /// Gets or Sets Parent
        /// </summary>
        [DataMember(Name="parent", EmitDefaultValue=false)]
        public EntityRef Parent { get; set; }

        /// <summary>
        /// Validation
        /// </summary>
        /// <value>Validation</value>
        [DataMember(Name="isValidation", EmitDefaultValue=false)]
        public bool? IsValidation { get; set; }

        /// <summary>
        /// Gets or Sets LcDescription
        /// </summary>
        [DataMember(Name="lcDescription", EmitDefaultValue=false)]
        public LocalizableMessage LcDescription { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Step {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Entity: ").Append(Entity).Append("\n");
            sb.Append("  Fault: ").Append(Fault).Append("\n");
            sb.Append("  Completion: ").Append(Completion).Append("\n");
            sb.Append("  Parent: ").Append(Parent).Append("\n");
            sb.Append("  IsValidation: ").Append(IsValidation).Append("\n");
            sb.Append("  LcDescription: ").Append(LcDescription).Append("\n");
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
            return this.Equals(obj as Step);
        }

        /// <summary>
        /// Returns true if Step instances are equal
        /// </summary>
        /// <param name="other">Instance of Step to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Step other)
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
                    this.State == other.State ||
                    this.State != null &&
                    this.State.Equals(other.State)
                ) && 
                (
                    this.Description == other.Description ||
                    this.Description != null &&
                    this.Description.Equals(other.Description)
                ) && 
                (
                    this.Entity == other.Entity ||
                    this.Entity != null &&
                    this.Entity.Equals(other.Entity)
                ) && 
                (
                    this.Fault == other.Fault ||
                    this.Fault != null &&
                    this.Fault.Equals(other.Fault)
                ) && 
                (
                    this.Completion == other.Completion ||
                    this.Completion != null &&
                    this.Completion.Equals(other.Completion)
                ) && 
                (
                    this.Parent == other.Parent ||
                    this.Parent != null &&
                    this.Parent.Equals(other.Parent)
                ) && 
                (
                    this.IsValidation == other.IsValidation ||
                    this.IsValidation != null &&
                    this.IsValidation.Equals(other.IsValidation)
                ) && 
                (
                    this.LcDescription == other.LcDescription ||
                    this.LcDescription != null &&
                    this.LcDescription.Equals(other.LcDescription)
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
                if (this.State != null)
                    hash = hash * 59 + this.State.GetHashCode();
                if (this.Description != null)
                    hash = hash * 59 + this.Description.GetHashCode();
                if (this.Entity != null)
                    hash = hash * 59 + this.Entity.GetHashCode();
                if (this.Fault != null)
                    hash = hash * 59 + this.Fault.GetHashCode();
                if (this.Completion != null)
                    hash = hash * 59 + this.Completion.GetHashCode();
                if (this.Parent != null)
                    hash = hash * 59 + this.Parent.GetHashCode();
                if (this.IsValidation != null)
                    hash = hash * 59 + this.IsValidation.GetHashCode();
                if (this.LcDescription != null)
                    hash = hash * 59 + this.LcDescription.GetHashCode();
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
