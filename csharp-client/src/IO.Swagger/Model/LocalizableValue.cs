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
    /// LocalizableValue
    /// </summary>
    [DataContract]
    public partial class LocalizableValue :  IEquatable<LocalizableValue>, IValidatableObject
    {
        /// <summary>
        /// Localizable type
        /// </summary>
        /// <value>Localizable type</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LtypeEnum
        {
            
            /// <summary>
            /// Enum UNKNOWN for "UNKNOWN"
            /// </summary>
            [EnumMember(Value = "UNKNOWN")]
            UNKNOWN,
            
            /// <summary>
            /// Enum NUMBER for "NUMBER"
            /// </summary>
            [EnumMember(Value = "NUMBER")]
            NUMBER,
            
            /// <summary>
            /// Enum STRING for "STRING"
            /// </summary>
            [EnumMember(Value = "STRING")]
            STRING,
            
            /// <summary>
            /// Enum NAME for "NAME"
            /// </summary>
            [EnumMember(Value = "NAME")]
            NAME,
            
            /// <summary>
            /// Enum EMAIL for "EMAIL"
            /// </summary>
            [EnumMember(Value = "EMAIL")]
            EMAIL,
            
            /// <summary>
            /// Enum HOSTNAME for "HOSTNAME"
            /// </summary>
            [EnumMember(Value = "HOSTNAME")]
            HOSTNAME,
            
            /// <summary>
            /// Enum DATETIME for "DATETIME"
            /// </summary>
            [EnumMember(Value = "DATETIME")]
            DATETIME,
            
            /// <summary>
            /// Enum TIMEDELTA for "TIMEDELTA"
            /// </summary>
            [EnumMember(Value = "TIMEDELTA")]
            TIMEDELTA,
            
            /// <summary>
            /// Enum BYTESIZE for "BYTESIZE"
            /// </summary>
            [EnumMember(Value = "BYTESIZE")]
            BYTESIZE
        }

        /// <summary>
        /// Localizable type
        /// </summary>
        /// <value>Localizable type</value>
        [DataMember(Name="ltype", EmitDefaultValue=false)]
        public LtypeEnum? Ltype { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableValue" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected LocalizableValue() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableValue" /> class.
        /// </summary>
        /// <param name="Position">Position (required).</param>
        /// <param name="Ltype">Localizable type (required).</param>
        /// <param name="Key">Key (required).</param>
        /// <param name="Value">Value (required).</param>
        public LocalizableValue(int? Position = default(int?), LtypeEnum? Ltype = default(LtypeEnum?), string Key = default(string), string Value = default(string))
        {
            // to ensure "Position" is required (not null)
            if (Position == null)
            {
                throw new InvalidDataException("Position is a required property for LocalizableValue and cannot be null");
            }
            else
            {
                this.Position = Position;
            }
            // to ensure "Ltype" is required (not null)
            if (Ltype == null)
            {
                throw new InvalidDataException("Ltype is a required property for LocalizableValue and cannot be null");
            }
            else
            {
                this.Ltype = Ltype;
            }
            // to ensure "Key" is required (not null)
            if (Key == null)
            {
                throw new InvalidDataException("Key is a required property for LocalizableValue and cannot be null");
            }
            else
            {
                this.Key = Key;
            }
            // to ensure "Value" is required (not null)
            if (Value == null)
            {
                throw new InvalidDataException("Value is a required property for LocalizableValue and cannot be null");
            }
            else
            {
                this.Value = Value;
            }
        }
        
        /// <summary>
        /// Position
        /// </summary>
        /// <value>Position</value>
        [DataMember(Name="position", EmitDefaultValue=false)]
        public int? Position { get; set; }


        /// <summary>
        /// Key
        /// </summary>
        /// <value>Key</value>
        [DataMember(Name="key", EmitDefaultValue=false)]
        public string Key { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        /// <value>Value</value>
        [DataMember(Name="value", EmitDefaultValue=false)]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LocalizableValue {\n");
            sb.Append("  Position: ").Append(Position).Append("\n");
            sb.Append("  Ltype: ").Append(Ltype).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
            return this.Equals(obj as LocalizableValue);
        }

        /// <summary>
        /// Returns true if LocalizableValue instances are equal
        /// </summary>
        /// <param name="other">Instance of LocalizableValue to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LocalizableValue other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Position == other.Position ||
                    this.Position != null &&
                    this.Position.Equals(other.Position)
                ) && 
                (
                    this.Ltype == other.Ltype ||
                    this.Ltype != null &&
                    this.Ltype.Equals(other.Ltype)
                ) && 
                (
                    this.Key == other.Key ||
                    this.Key != null &&
                    this.Key.Equals(other.Key)
                ) && 
                (
                    this.Value == other.Value ||
                    this.Value != null &&
                    this.Value.Equals(other.Value)
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
                if (this.Position != null)
                    hash = hash * 59 + this.Position.GetHashCode();
                if (this.Ltype != null)
                    hash = hash * 59 + this.Ltype.GetHashCode();
                if (this.Key != null)
                    hash = hash * 59 + this.Key.GetHashCode();
                if (this.Value != null)
                    hash = hash * 59 + this.Value.GetHashCode();
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
