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
    /// ReplicationPlatDatastorePair
    /// </summary>
    [DataContract]
    public partial class ReplicationPlatDatastorePair :  IEquatable<ReplicationPlatDatastorePair>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicationPlatDatastorePair" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ReplicationPlatDatastorePair() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicationPlatDatastorePair" /> class.
        /// </summary>
        /// <param name="Ads">Ads (required).</param>
        /// <param name="Bds">Bds (required).</param>
        public ReplicationPlatDatastorePair(ReplicationPlatDatastore Ads = default(ReplicationPlatDatastore), ReplicationPlatDatastore Bds = default(ReplicationPlatDatastore))
        {
            // to ensure "Ads" is required (not null)
            if (Ads == null)
            {
                throw new InvalidDataException("Ads is a required property for ReplicationPlatDatastorePair and cannot be null");
            }
            else
            {
                this.Ads = Ads;
            }
            // to ensure "Bds" is required (not null)
            if (Bds == null)
            {
                throw new InvalidDataException("Bds is a required property for ReplicationPlatDatastorePair and cannot be null");
            }
            else
            {
                this.Bds = Bds;
            }
        }
        
        /// <summary>
        /// Gets or Sets Ads
        /// </summary>
        [DataMember(Name="ads", EmitDefaultValue=false)]
        public ReplicationPlatDatastore Ads { get; set; }

        /// <summary>
        /// Gets or Sets Bds
        /// </summary>
        [DataMember(Name="bds", EmitDefaultValue=false)]
        public ReplicationPlatDatastore Bds { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ReplicationPlatDatastorePair {\n");
            sb.Append("  Ads: ").Append(Ads).Append("\n");
            sb.Append("  Bds: ").Append(Bds).Append("\n");
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
            return this.Equals(obj as ReplicationPlatDatastorePair);
        }

        /// <summary>
        /// Returns true if ReplicationPlatDatastorePair instances are equal
        /// </summary>
        /// <param name="other">Instance of ReplicationPlatDatastorePair to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReplicationPlatDatastorePair other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Ads == other.Ads ||
                    this.Ads != null &&
                    this.Ads.Equals(other.Ads)
                ) && 
                (
                    this.Bds == other.Bds ||
                    this.Bds != null &&
                    this.Bds.Equals(other.Bds)
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
                if (this.Ads != null)
                    hash = hash * 59 + this.Ads.GetHashCode();
                if (this.Bds != null)
                    hash = hash * 59 + this.Bds.GetHashCode();
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
