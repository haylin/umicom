namespace Umicom.Passport.Quickstart.UI
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }

        public bool ShowView => ViewModel != null;
        public ConsentViewModel ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;
        public string ValidationError { get; set; }
    }
}