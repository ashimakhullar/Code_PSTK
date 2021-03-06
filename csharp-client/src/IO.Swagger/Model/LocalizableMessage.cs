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
    /// LocalizableMessage
    /// </summary>
    [DataContract]
    public partial class LocalizableMessage :  IEquatable<LocalizableMessage>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableMessage" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected LocalizableMessage() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableMessage" /> class.
        /// </summary>
        /// <param name="Msgid">Message Id (required).</param>
        /// <param name="MsgidPlural">Message id plural .</param>
        /// <param name="Msgctxt">Message context.</param>
        /// <param name="Msgstr">Message content (required).</param>
        /// <param name="_Params">_Params (required).</param>
        public LocalizableMessage(string Msgid = default(string), string MsgidPlural = default(string), string Msgctxt = default(string), string Msgstr = default(string), List<LocalizableValue> _Params = default(List<LocalizableValue>))
        {
            // to ensure "Msgid" is required (not null)
            if (Msgid == null)
            {
                throw new InvalidDataException("Msgid is a required property for LocalizableMessage and cannot be null");
            }
            else
            {
                this.Msgid = Msgid;
            }
            // to ensure "Msgstr" is required (not null)
            if (Msgstr == null)
            {
                throw new InvalidDataException("Msgstr is a required property for LocalizableMessage and cannot be null");
            }
            else
            {
                this.Msgstr = Msgstr;
            }
            // to ensure "_Params" is required (not null)
            if (_Params == null)
            {
                throw new InvalidDataException("_Params is a required property for LocalizableMessage and cannot be null");
            }
            else
            {
                this._Params = _Params;
            }
            this.MsgidPlural = MsgidPlural;
            this.Msgctxt = Msgctxt;
        }
        
        /// <summary>
        /// Message Id
        /// </summary>
        /// <value>Message Id</value>
        [DataMember(Name="msgid", EmitDefaultValue=false)]
        public string Msgid { get; set; }

        /// <summary>
        /// Message id plural 
        /// </summary>
        /// <value>Message id plural </value>
        [DataMember(Name="msgidPlural", EmitDefaultValue=false)]
        public string MsgidPlural { get; set; }

        /// <summary>
        /// Message context
        /// </summary>
        /// <value>Message context</value>
        [DataMember(Name="msgctxt", EmitDefaultValue=false)]
        public string Msgctxt { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        /// <value>Message content</value>
        [DataMember(Name="msgstr", EmitDefaultValue=false)]
        public string Msgstr { get; set; }

        /// <summary>
        /// Gets or Sets _Params
        /// </summary>
        [DataMember(Name="params", EmitDefaultValue=false)]
        public List<LocalizableValue> _Params { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LocalizableMessage {\n");
            sb.Append("  Msgid: ").Append(Msgid).Append("\n");
            sb.Append("  MsgidPlural: ").Append(MsgidPlural).Append("\n");
            sb.Append("  Msgctxt: ").Append(Msgctxt).Append("\n");
            sb.Append("  Msgstr: ").Append(Msgstr).Append("\n");
            sb.Append("  _Params: ").Append(_Params).Append("\n");
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
            return this.Equals(obj as LocalizableMessage);
        }

        /// <summary>
        /// Returns true if LocalizableMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of LocalizableMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LocalizableMessage other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Msgid == other.Msgid ||
                    this.Msgid != null &&
                    this.Msgid.Equals(other.Msgid)
                ) && 
                (
                    this.MsgidPlural == other.MsgidPlural ||
                    this.MsgidPlural != null &&
                    this.MsgidPlural.Equals(other.MsgidPlural)
                ) && 
                (
                    this.Msgctxt == other.Msgctxt ||
                    this.Msgctxt != null &&
                    this.Msgctxt.Equals(other.Msgctxt)
                ) && 
                (
                    this.Msgstr == other.Msgstr ||
                    this.Msgstr != null &&
                    this.Msgstr.Equals(other.Msgstr)
                ) && 
                (
                    this._Params == other._Params ||
                    this._Params != null &&
                    this._Params.SequenceEqual(other._Params)
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
                if (this.Msgid != null)
                    hash = hash * 59 + this.Msgid.GetHashCode();
                if (this.MsgidPlural != null)
                    hash = hash * 59 + this.MsgidPlural.GetHashCode();
                if (this.Msgctxt != null)
                    hash = hash * 59 + this.Msgctxt.GetHashCode();
                if (this.Msgstr != null)
                    hash = hash * 59 + this.Msgstr.GetHashCode();
                if (this._Params != null)
                    hash = hash * 59 + this._Params.GetHashCode();
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
