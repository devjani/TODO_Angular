namespace TODO.Model.Constants
{
    public static class Constants
    {
        #region Logging

        public static class LogMessages
        {

            public const string GETERRORMESSAGE = "Error while getting data from";
            public const string ADDERRORMESSAGE = "Error while adding data from";
            public const string UPDATEERRORMESSAGE = "Error while updating data from";
            public const string DELETEERRORMESSAGE = "Error while deleting data from";
        }

        public static class ErrorMessages
        {

            #region User
            public const string REQUIREDFIELD = "This is required field.";
            public const string REQUIREDFIRSTNAME = "RequiredFirstName";

            public const string REQUIREDLASTNAME = "RequiredLastName";

            public const string REQUIREDEMAILID = "RequiredEmailId";

            public const string INVALIDEMAILID = "InvalidEmailId";

            public const string REQUIREDPASSWORD = "RequiredPassword";

            public const string PASSWORDVALIDATEREGEX = "PasswordValidateRegex";

            public const string REQUIREDCONFIRMPASSWROD = "RequiredConfirmPassword";

            public const string MATCHPASSWORDWITHCONFIRMPASSWORD = "MatchPasswordWithConfirmPassword";

            public const string REQUIREDROLE = "RequiredRole";

            public const string ERRORINCHANGINGPASSWORD = "ErrorInChangingPassword";

            public const string EMAILORKYBNOTCONFIRMED = "EmailOrKybNotConfirmed";

            public const string INVALIDCREDENTIAL = "InvalidCredential";

            public const string ERRORINREGISTERUSER = "ErrorInRegisterUser";

            public const string USERDOESNOTEXIST = "User does not exist.";

            public const string ERRORINIDENTIFYLINK = "ErrorInIdentifyLink";

            public const string ERRORINSENDINGMAIL = "ErrorInSendingEmail";

            public const string EMAILNOTEXISTORNOTCONFIRMED = "EmailNotExistOrNotConfirmed";

            public const string SUCCESSUPDATEPROFILE = "SuccessUpdateProfile";

            public const string ERRORUPDATEPROFILE = "ErrorUpdateProfile";

            public const string ERRORINCOMPLETINGKYBPROCESS = "ErrorInCompletingKybProcess";

            public const string ERRORINRESETPASSWORD = "ErrorInResetPassword";
            public const string EMAILNOTCONFIRMED = "Email not confirmed.";
            #endregion
        }

        #endregion

        public static class Cookie
        {
            #region Cookies
            public const string REMEMBERMECOOKIE = "RememberMeCookie";
            #endregion
        }

        public static class ClaimType
        {
            public const string Permission = "Permission";
        }
        public static class Formats
        {
            public const string DateFormat = "yyyy-MM-dd";

        }

        public static class RegexConstants
        {
            public const string UrlFormat = @"^(http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
            public const string EmailFormat = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
        }

        public static class DateTimeConstants
        {
            public const string ddMMyyyy = "dd/MM/yyyy";
            public const string MMddyyyy = "MM/dd/yyyy";
        }

        public static class FileExtensions
        {
            public const string JPGExtension = ".jpg";
            public const string JPEGExtension = ".jpeg";
            public const string PNGExtension = ".png";
        }


        #region Email
        public static class Email
        {
            #region templates
            public const string CONFIRMATIONEMAILTEMPLATE = "templates\\emailTemplate.html";
            public const string REACTIVATIONACCOUNTEMAILTEMPLATE = "templates\\reactivationAccountEmailTemplate.html";
            public const string RESETPASSWORDTEMPLATE = "templates\\forgotPasswordTemplate.html";
            public const string USERSECURITYUPDATEDTEMPLATE = "templates\\userSecurityUpdated.html";
            public const string NOUSERIMAGE = "img\\no-user-image.png";

            #endregion

            #region Subject

            public const string CONFIRMATIONEMAILSUBJECT = "Confirm your email";

            public const string REACTIVATIONACCOUNTEMAILSUBJECT = "Say Goodbye";

            

            public const string RESETPASSWORDSUBJECT = "Reset Password";

            public const string SECURITYSETTINGSSUBJECT = "Security Settings";

            #endregion

            #region Links

            public const string CONFIRMATIONEMAILLINK = "confirmemail";

            public const string REACTIVATIONCONFIRMATIONMAILLINK = "reactivationaccountmail";

            public const string FORGOTPASSWORDLINK = "resetpassword";

            #endregion
        }
        #endregion

    }
}
