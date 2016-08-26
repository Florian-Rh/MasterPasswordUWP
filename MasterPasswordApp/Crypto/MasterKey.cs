using System;
using System.Linq;
using System.Text;
using Skryptonite;
using Windows.Security.Cryptography.Core;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace MasterPasswordApp.Crypto
{
    class MasterKey
    {
        #region derivation parameters
        private const int MP_N = 32768; //CPU-Cost parameter
        private const int MP_r = 8; //Memory-Cost parameter
        private const int MP_p = 2; //Parallelization parameter
        private const int MP_dkLength = 64; //Length of derived key
        private const string MP_ScopeName = "com.lyndir.masterpassword"; //Scope to match
        #endregion
        private byte[] masterKeyBytes { get; set; }
        private string userName { get; set; }

        public static async Task<MasterKey> CreateMasterKeyAsync(string masterPassword, string userName)
        {
            if (instance == null || instance.userName != userName)
            {
                await Task.Run(() =>
                {
                    instance = new MasterKey(masterPassword, userName);
                });
            }
            return instance;
        }

        public static bool Exists { get { return instance != null; } }
        public static MasterKey GetMasterKey()
        {
            return instance;
        }

        private static MasterKey instance;
        private MasterKey(string masterPassword, string userName)
        {
            this.userName = userName;
            masterKeyBytes = DeriveKey(masterPassword, userName);
        }

        public byte[] GetBytes()
        {
            return masterKeyBytes;
        }

        private byte[] DeriveKey(string masterPassword, string userName)
        {
            byte[] masterPasswordBytes = System.Text.Encoding.UTF8.GetBytes(masterPassword);
            byte[] salt = GetSalt(userName);
            MyScrypt scryptor = new MyScrypt(MP_r, MP_N, MP_p);
            byte[] derivedKey = scryptor.DeriveKey(masterPasswordBytes.AsBuffer(), salt.AsBuffer(), MP_dkLength).ToArray();
            return derivedKey;            
        }

        private byte[] GetSalt(string userName)
        {
            byte[] myKeyScopeBytes = Encoding.UTF8.GetBytes(MP_ScopeName);
            byte[] userNameBytes = Encoding.UTF8.GetBytes(userName);
            byte[] userNameLengthBytes = BinaryConverter.BytesForInt(userNameBytes.Length);
            byte[] salt = myKeyScopeBytes.Concat(userNameLengthBytes).Concat(userNameBytes).ToArray();
            return salt;
        }

        internal byte[] GetPasswordSeed(string siteName, int count)
        {
            byte[] myKeyScopeBytes = Encoding.UTF8.GetBytes(MP_ScopeName);
            byte[] siteNameLengthBytes = BinaryConverter.BytesForInt(siteName.Length);
            byte[] siteNameBytes = Encoding.UTF8.GetBytes(siteName);
            byte[] countBytes = BinaryConverter.BytesForInt(count);
            byte[] concatenated = myKeyScopeBytes.Concat(siteNameLengthBytes).Concat(siteNameBytes).Concat(countBytes).ToArray();
            MacAlgorithmProvider provider = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
            CryptographicHash masterKeyHash = provider.CreateHash(masterKeyBytes.AsBuffer());
            masterKeyHash.Append(concatenated.AsBuffer());
            byte[] seed = masterKeyHash.GetValueAndReset().ToArray();
            return seed;
        }
        internal string GetPassword(byte[] passwordSeed, SiteType myType)
        {
            Template t = new Template(myType, passwordSeed);
            string password = String.Empty;
            for (int i = 0; i < t.templateLength; i++)
            {
                int randomFromSeed = passwordSeed[i + 1];
                char passwordChar = t.GetRandomChar(i, randomFromSeed);
                password += passwordChar;
            }
            return password;
        }

        internal static void Destroy()
        {
            instance.masterKeyBytes = null;
            instance.userName = null;
            instance = null;
        }
    }
}
