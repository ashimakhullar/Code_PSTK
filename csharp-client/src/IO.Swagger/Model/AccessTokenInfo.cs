/* 
 * Swagger Server
 *
 **Ashima Bahl
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
    /// AccessTokenInfo
    /// </summary>
    [DataContract]
    public partial class AccessTokenInfo :  IEquatable<AccessTokenInfo>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets ProtectionStatus
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ProtectionStatusEnum
        {
            
            /// <summary>
            /// Enum ACTIVE for "ACTIVE"
            /// </summary>
            [EnumMember(Value = "ACTIVE")]
            ACTIVE,
            
            /// <summary>
            /// Enum HALTED for "HALTED"
            /// </summary>
            [EnumMember(Value = "HALTED")]
            HALTED,
            
            /// <summary>
            /// Enum RECOVERED for "RECOVERED"
            /// </summary>
            [EnumMember(Value = "RECOVERED")]
            RECOVERED,
            
            /// <summary>
            /// Enum INPROGRESS for "IN_PROGRESS"
            /// </summary>
            [EnumMember(Value = "IN_PROGRESS")]
            INPROGRESS,
            
            /// <summary>
            /// Enum FAILED for "FAILED"
            /// </summary>
            [EnumMember(Value = "FAILED")]
            FAILED
        }

        /// <summary>
        /// Gets or Sets ProtectionStatus
        /// </summary>
        [DataMember(Name="protectionStatus", EmitDefaultValue=false)]
        public ProtectionStatusEnum? ProtectionStatus { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenInfo" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected AccessTokenInfo() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenInfo" /> class.
        /// </summary>
        /// <param name="TokenType">TokenType (required).</param>
        /// <param name="RefreshToken">RefreshToken (required).</param>
        /// <param name="AccessToken">AccessToken (required).</param>

        public AccessTokenInfo(String TknType = default(String), String RfrshToken = default(String), String AccssToken = default(String))
        {
            // to ensure "Er" is required (not null)
            if (TknType == null)
            {
                throw new InvalidDataException("TknType is a required property for ProtectedVMInfo and cannot be null");
            }
            else
            {
                this.TknType = TknType;
            }
            // to ensure "RfrshToken" is required (not null)
            if (RfrshToken == null)
            {
                throw new InvalidDataException("RfrshToken is a required property for ProtectedVMInfo and cannot be null");
            }
            else
            {
                this.RfrshToken = RfrshToken;
            }
           
            // to ensure "ProtectionStatus" is required (not null)
            if (AccssToken == null)
            {
                throw new InvalidDataException("AccssToken is a required property for ProtectedVMInfo and cannot be null");
            }
            else
            {
                this.AccssToken = AccssToken;
            }
           
        }
        
        /// <summary>
        /// Gets or Sets Er
        /// </summary>
        [DataMember(Name="er", EmitDefaultValue=false)]
        public TokenType Er { get; set; }

        /// <summary>
        /// Gets or Sets ClusterEr
        /// </summary>
        [DataMember(Name="clusterEr", EmitDefaultValue=false)]
        public TokenType ClusterEr { get; set; }
        public object TknType { get; private set; }
        public string RfrshToken { get; private set; }
        public string AccssToken { get; private set; }





        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProtectedVMInfo {\n");
            sb.Append("  Er: ").Append(Er).Append("\n");
            sb.Append("  ClusterEr: ").Append(ClusterEr).Append("\n");
            //sb.Append("  Schedule: ").Append(Schedule).Append("\n");
            sb.Append("  ProtectionStatus: ").Append(ProtectionStatus).Append("\n");
            //sb.Append("  ProtectionInfo: ").Append(ProtectionInfo).Append("\n");
            //sb.Append("  Ex: ").Append(Ex).Append("\n");
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
            return this.Equals(obj as AccessTokenInfo);
        }

        /// <summary>
        /// Returns true if ProtectedVMInfo instances are equal
        /// </summary>
        /// <param name="other">Instance of ProtectedVMInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AccessTokenInfo other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.TknType == other.TknType ||
                    this.TknType != null &&
                    this.TknType.Equals(other.TknType)
                ) && 
                (
                    this.ClusterEr == other.ClusterEr ||
                    this.ClusterEr != null &&
                    this.ClusterEr.Equals(other.ClusterEr)
                ) && 
                //(
                //    this.Schedule == other.Schedule ||
                //    this.Schedule != null &&
                //    this.Schedule.SequenceEqual(other.Schedule)
                //) && 
                (
                    this.ProtectionStatus == other.ProtectionStatus ||
                    this.ProtectionStatus != null &&
                    this.ProtectionStatus.Equals(other.ProtectionStatus)
                //) 
                //&& 
                //(
                //    this.ProtectionInfo == other.ProtectionInfo ||
                //    this.ProtectionInfo != null &&
                //    this.ProtectionInfo.Equals(other.ProtectionInfo)
                //) && 
                //(
                //    this.Ex == other.Ex ||
                //    this.Ex != null &&
                //    this.Ex.Equals(other.Ex)
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
                if (this.Er != null)
                    hash = hash * 59 + this.Er.GetHashCode();
                if (this.ClusterEr != null)
                    hash = hash * 59 + this.ClusterEr.GetHashCode();
                //if (this.Schedule != null)
                //    hash = hash * 59 + this.Schedule.GetHashCode();
                if (this.ProtectionStatus != null)
                    hash = hash * 59 + this.ProtectionStatus.GetHashCode();
                //if (this.ProtectionInfo != null)
                //    hash = hash * 59 + this.ProtectionInfo.GetHashCode();
                //if (this.Ex != null)
                //    hash = hash * 59 + this.Ex.GetHashCode();
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

    /// <summary>
    /// TokenType
    /// </summary>
    [DataContract]
    public partial class TokenType : IEquatable<TokenType>, IValidatableObject
    {
        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(Object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as TokenType);
        }

        bool IEquatable<TokenType>.Equals(TokenType other)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }

}


