using Data.Model;
using System.Collections.Generic;

namespace PublicWorks.API.Tests.Models
{
    public class CategoryTestData
    {
        public string Scenario { get; set; } = string.Empty;  // Example: "GetAll", "GetByIdValid", "GetByIdInvalid"
        public int Id { get; set; }                           // For GetById tests
        public string ExpectedCategoriesJson { get; set; } = string.Empty; // JSON array of IssueCategory
        public string ExpectedCategoryJson { get; set; } = string.Empty;   // JSON single IssueCategory
    }
}
