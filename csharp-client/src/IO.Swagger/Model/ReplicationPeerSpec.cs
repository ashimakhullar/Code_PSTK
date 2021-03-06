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
    /// ReplicationPeerSpec
    /// </summary>
    [DataContract]
    public partial class ReplicationPeerSpec :  IEquatable<ReplicationPeerSpec>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicationPeerSpec" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ReplicationPeerSpec() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicationPeerSpec" /> class.
        /// </summary>
        /// <param name="MgmtIp">Peer Cluster management IP (required).</param>
        /// <param name="Cred">Cred (required).</param>
        /// <param name="Name">Name (required).</param>
        public ReplicationPeerSpec(string MgmtIp = default(string), Credential Cred = default(Credential), string Name = default(string))
        {
            // to ensure "MgmtIp" is required (not null)
            if (MgmtIp == null)
            {
                throw new InvalidDataException("MgmtIp is a required property for ReplicationPeerSpec and cannot be null");
            }
            else
            {
                this.MgmtIp = MgmtIp;
            }
            // to ensure "Cred" is required (not null)
            if (Cred == null)
            {
                throw new InvalidDataException("Cred is a required property for ReplicationPeerSpec and cannot be null");
            }
            else
            {
                this.Cred = Cred;
            }
            // to ensure "Name" is required (not null)
            if (Name == null)
            {
                throw new InvalidDataException("Name is a required property for ReplicationPeerSpec and cannot be null");
            }
            else
            {
                this.Name = Name;
            }
        }
        
        /// <summary>
        /// Peer Cluster management IP
        /// </summary>
        /// <value>Peer Cluster management IP</value>
        [DataMember(Name="mgmtIp", EmitDefaultValue=false)]
        public string MgmtIp { get; set; }

        /// <summary>
        /// Gets or Sets Cred
        /// </summary>
        [DataMember(Name="cred", EmitDefaultValue=false)]
        public Credential Cred { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ReplicationPeerSpec {\n");
            sb.Append("  MgmtIp: ").Append(MgmtIp).Append("\n");
            sb.Append("  Cred: ").Append(Cred).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
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
            return this.Equals(obj as ReplicationPeerSpec);
        }

        /// <summary>
        /// Returns true if ReplicationPeerSpec instances are equal
        /// </summary>
        /// <param name="other">Instance of ReplicationPeerSpec to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReplicationPeerSpec other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.MgmtIp == other.MgmtIp ||
                    this.MgmtIp != null &&
                    this.MgmtIp.Equals(other.MgmtIp)
                ) && 
                (
                    this.Cred == other.Cred ||
                    this.Cred != null &&
                    this.Cred.Equals(other.Cred)
                ) && 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
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
                if (this.MgmtIp != null)
                    hash = hash * 59 + this.MgmtIp.GetHashCode();
                if (this.Cred != null)
                    hash = hash * 59 + this.Cred.GetHashCode();
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
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
