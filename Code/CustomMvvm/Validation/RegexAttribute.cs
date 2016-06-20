using System.Text.RegularExpressions;

namespace CustomMvvm.Validation
{
    public class RegexAttribute : ValidationAttribute
    {
        private string _regex;

        public RegexAttribute( string regex )
        {
            _regex = regex;
        }

        public override string GetValidationError( object value )
        {
            var match = Regex.IsMatch( (string) value, _regex );

            return match ? null : "Sequence contains not allowed characters.";
        }
    }
}