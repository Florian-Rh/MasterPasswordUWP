namespace MasterPasswordApp.ViewModel
{
    public class SiteViewModel : NotifiyingViewModel
    {
        public string siteName { get; set; }

        private int _count;
        public int count
        {
            get {
                return _count;
            }
            set {
                if (value > 200)
                    _count = 200;
                else if (value < 1)
                    _count = 1;
                else
                    _count = value;
                OnPropertyChanged("count");
            }
        }


        private int _siteType;
        public int siteType
        {
            get {
                return _siteType;
            }
            set {
                _siteType = value;
            }
        }


        private string _password;
        public string password
        {
            get {
                return _password;
            }
            set {
                _password = value;
                OnPropertyChanged("password");
            }
        }

        private bool _showPassword;
        public bool showPassword
        {
            get { return _showPassword; }
            set {
                _showPassword = value;
                OnPropertyChanged("showPassword");
            }
        }

    }
}
