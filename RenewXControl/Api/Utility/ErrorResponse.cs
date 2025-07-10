namespace RenewXControl.Api.Utility
{
    public class ErrorResponse
    {
        public string Name { get; set; }    // E.g. "Password"
        public string Message{ get; set; }    // E.g. "Password is too short"
    }
}
