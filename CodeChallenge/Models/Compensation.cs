using CodeChallenge.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        // I prefer to use EntityTypeConfigurations instead of DataAnnotations. But for a project this size and scope, it makes sense to use annotations.

        [Key]
        public String CompensationId { get; set; } // would normally use GUID but following established conventions for this project.

        [Required]
        public Employee Employee { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        [JsonConverter(typeof(DateOnlyJsonConverter))] // https://stackoverflow.com/questions/74246482/system-notsupportedexception-serialization-and-deserialization-of-system-dateo
        public DateOnly EffectiveDate { get; set; } // DateTime is most popular, but this specified just a date so it makes sense to use this.
    }
}
