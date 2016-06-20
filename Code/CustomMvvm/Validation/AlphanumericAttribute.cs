namespace CustomMvvm.Validation
{
    public class AlphanumericAttribute : RegexAttribute
    {
        public AlphanumericAttribute() 
            : base( "^[a-zA-Z0-9]*$" )
        {
        }
    }
}