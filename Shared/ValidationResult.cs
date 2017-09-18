using System.Collections.Generic;

namespace Shared
{
    public class ValidationResult
    {
        private readonly IList<string> _errors = new List<string>();

        public string Message { get; set; }
      
        public bool IsValid => _errors.Count == 0;

        public IEnumerable<string> Errors => _errors;

        public void AddError(string message)
        {
            _errors.Add(message);
        }
    }
}