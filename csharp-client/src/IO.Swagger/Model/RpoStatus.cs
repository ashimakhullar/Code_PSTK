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
    /// RpoStatus
    /// </summary>
    [DataContract]
    public partial class RpoStatus :  IEquatable<RpoStatus>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RpoStatus" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected RpoStatus() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RpoStatus" /> class.
        /// </summary>
        /// <param name="RpoExceeded">Whether delayed (required) (default to false).</param>
        /// <param name="Expected">Expected.</param>
        /// <param name="Actual">Actual.</param>
        public RpoStatus(bool? RpoExceeded = false, long? Expected = default(long?), long? Actual = default(long?))
        {
            // to ensure "RpoExceeded" is required (not null)
            if (RpoExceeded == null)
            {
                throw new InvalidDataException("RpoExceeded is a required property for RpoStatus and cannot be null");
            }
            else
            {
                this.RpoExceeded = RpoExceeded;
            }
            this.Expected = Expected;
            this.Actual = Actual;
        }
        
        /// <summary>
        /// Whether delayed
        /// </summary>
        /// <value>Whether delayed</value>
        [DataMember(Name="rpoExceeded", EmitDefaultValue=false)]
        public bool? RpoExceeded { get; set; }

        /// <summary>
        /// Gets or Sets Expected
        /// </summary>
        [DataMember(Name="expected", EmitDefaultValue=false)]
        public long? Expected { get; set; }

        /// <summary>
        /// Gets or Sets Actual
        /// </summary>
        [DataMember(Name="actual", EmitDefaultValue=false)]
        public long? Actual { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RpoStatus {\n");
            sb.Append("  RpoExceeded: ").Append(RpoExceeded).Append("\n");
            sb.Append("  Expected: ").Append(Expected).Append("\n");
            sb.Append("  Actual: ").Append(Actual).Append("\n");
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
            return this.Equals(obj as RpoStatus);
        }

        /// <summary>
        /// Returns true if RpoStatus instances are equal
        /// </summary>
        /// <param name="other">Instance of RpoStatus to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RpoStatus other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.RpoExceeded == other.RpoExceeded ||
                    this.RpoExceeded != null &&
                    this.RpoExceeded.Equals(other.RpoExceeded)
                ) && 
                (
                    this.Expected == other.Expected ||
                    this.Expected != null &&
                    this.Expected.Equals(other.Expected)
                ) && 
                (
                    this.Actual == other.Actual ||
                    this.Actual != null &&
                    this.Actual.Equals(other.Actual)
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
                if (this.RpoExceeded != null)
                    hash = hash * 59 + this.RpoExceeded.GetHashCode();
                if (this.Expected != null)
                    hash = hash * 59 + this.Expected.GetHashCode();
                if (this.Actual != null)
                    hash = hash * 59 + this.Actual.GetHashCode();
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
