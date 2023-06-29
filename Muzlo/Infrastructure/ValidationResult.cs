public class ValidationResult
{
    public static readonly bool Fail = false;
    public static readonly bool Ok = true;
    public string message {get; set;} = string.Empty;
    public bool state;
}