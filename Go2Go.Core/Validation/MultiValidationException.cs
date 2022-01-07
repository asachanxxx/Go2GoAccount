using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Core.Validation
{
    [Serializable]
    public class MultiValidationException : Exception
    {
        public List<MultiValidationResult> MultiValidationResults { get; set; }

        public MultiValidationException()
        {
        }

        public MultiValidationException(string message, List<MultiValidationResult> results)
            : base(message)
        {
            MultiValidationResults = results;
        }
    }
}
