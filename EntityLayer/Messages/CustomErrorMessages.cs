namespace EntityLayer.Messages
{
    public class CustomErrorMessages
    {
        public const string UserNotExist = "User not exist. Please try again.";
        public const string RoleNotExist = "Role not exist. Please try again.";
        public const string PasswordNotMatch = "Password and Confirm Password must match";
        public const string UnauthorizedAccess = "Unauthorized Access";
        public const string ForbiddenAccess = "You do not have permission to this page.";
        public const string PageNotFound = "Page not exist. Please check your input.";
        public const string InternalError = "Please see your admin";
        public const string MethodNotAllowed = "Method not allowed. Check your HTTP Protocols";
        public const string UnsupportedEntry = "Unsupported media type. Server is not supporting the data you sent.";
        public const string ClientNotExist = "Client not exist. Please try again.";
        public const string ScopeNotExist = "Scope not exist. Please try again.";
    }
}
