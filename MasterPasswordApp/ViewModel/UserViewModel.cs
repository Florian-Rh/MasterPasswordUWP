using System.ComponentModel;

namespace MasterPasswordApp.ViewModel
{
    public class LoginViewModel : NotifiyingViewModel
    {
        public bool EditIsEnabled { get; set; }
        public string UserName { get; set; }
        public string MasterPassword { get; set; }
    }
}
