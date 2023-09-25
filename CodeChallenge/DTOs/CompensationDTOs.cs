using System;

namespace CodeChallenge.DTOs
{
    // Record structs are good for data transfer representations and are immutable
    public record struct CompensationSingleDTO // Represents the user-facing data we want to return representing a single instance of a Compensation Entity (sensitive fields removed).
    {
        public String CompensationId { get; set; } // Assuming we want to expose this to user/frontend
        public String EmployeeID { get; set; } // DTOs use ID instead of the entire domain model
        public decimal Salary { get; set; }
        public DateOnly EffectiveDate { get; set; }
    }
    public record struct CompensationAddDTO // Represents our Compensation Create DTO with all required fields to make our domain-entity for Compensation.
    {
        public String EmployeeID { get; set; } // DTOs use ID instead of the entire domain model
        public decimal Salary { get; set; }
        public DateOnly EffectiveDate { get; set; }
    }
}
