using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterPasswordApp.Crypto
{
    public class Template
    {
        public int templateLength
        {
            get
            {
                return template.Length;
            }
        }

        private string template { get; set; }
        public Template(SiteType siteType, byte[] passwordSeed)
        {
            string[] templates = TemplateForType[siteType];
            int rollingIndex = passwordSeed[0] % templates.Length;
            template = templates[rollingIndex];
        }

        public char GetRandomChar(int index, int randomFromSeed)
        {
            string charactersToUse = CharacterClasses[template[index]];
            int rollingRandomIndex = randomFromSeed % charactersToUse.Length;
            char myChar = charactersToUse[rollingRandomIndex];
            return myChar;
        }

        private Dictionary<SiteType, string[]> TemplateForType = new Dictionary<SiteType, string[]>
        {
            { SiteType.MaximumSecurityPassword,
                new string[] 
                {
                    "anoxxxxxxxxxxxxxxxxx",
                    "axxxxxxxxxxxxxxxxxno"
                }
            },
            { SiteType.LongPassword,
                new string[]
                {
                    "CvcvnoCvcvCvcv",
                    "CvcvCvcvnoCvcv",
                    "CvcvCvcvCvcvno",
                    "CvccnoCvcvCvcv",
                    "CvccCvcvnoCvcv",
                    "CvccCvcvCvcvno",
                    "CvcvnoCvccCvcv",
                    "CvcvCvccnoCvcv",
                    "CvcvCvccCvcvno",
                    "CvcvnoCvcvCvcc",
                    "CvcvCvcvnoCvcc",
                    "CvcvCvcvCvccno",
                    "CvccnoCvccCvcv",
                    "CvccCvccnoCvcv",
                    "CvccCvccCvcvno",
                    "CvcvnoCvccCvcc",
                    "CvcvCvccnoCvcc",
                    "CvcvCvccCvccno",
                    "CvccnoCvcvCvcc",
                    "CvccCvcvnoCvcc",
                    "CvccCvcvCvccno"
                }
            },
            { SiteType.MediumPassword,
                new string[] 
                {
                    "CvcnoCvc",
                    "CvcCvcno"
                }
            },
            { SiteType.ShortPassword,
                new string[] 
                {
                    "Cvcn"
                }
            },
            { SiteType.BasicPassword,
                new string[] 
                {
                    "aaanaaan",
                    "aannaaan",
                    "aaannaaa"
                }
            },
            { SiteType.PIN,
                new string[] 
                {
                    "nnnn"
                }
            },
            { SiteType.Name,
                new string[]
                {
                    "cvccvcvcv"
                }
            },
            { SiteType.Phrase,
                new string[]
                {
                    "cvcc cvc cvccvcv cvc",
                    "cvc cvccvcvcv cvcv",
                    "cv cvccv cvc cvcvccv"
                }
            }
        };


        public Dictionary<char, string> CharacterClasses = new Dictionary<char, string>
        {
            { 'V', "AEIOU" },
            { 'C', "BCDFGHJKLMNPQRSTVWXYZ" },
            { 'v', "aeiou" },
            { 'c', "bcdfghjklmnpqrstvwxyz" },
            { 'A', "AEIOUBCDFGHJKLMNPQRSTVWXYZ" },
            { 'a', "AEIOUaeiouBCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz" },
            { 'n', "0123456789" },
            { 'o', "@&%?,=[]_:-+*$#!'^~;()/." },
            { 'x', "AEIOUaeiouBCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz0123456789!@#$%^&*()" },
            { ' ', " " }
		};
    }
}
