using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Go2Go.Core.Validation
{
    public class MultiValidationResult
    {
        public bool Success { get; set; }
        public string UniqueId { get; set; }
        public List<ValidationResult> ValidationResults { get; set; }
    }
}
