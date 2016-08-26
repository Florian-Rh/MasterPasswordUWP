using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MasterPasswordApp
{
    public static class Extension
    {
            public static bool CanSetScreenCaptureEnabled(this Page page)
            {
                throw new NotImplementedException();
                //return Environment.OSVersion.Version >= new Version(8, 0, 10322);
            }

            public static void SetScreenCaptureEnabled(this Page page, bool enabled)
            {
                var propertyInfo = typeof(Page).GetProperty("IsScreenCaptureEnabled");

                if (propertyInfo == null)
                {
                    throw new NotSupportedException("Not supported in this Windows Phone version!");
                }

                propertyInfo.SetValue(page, enabled);
            }

            public static bool GetScreenCaptureEnabled(this Page page)
            {
                var propertyInfo = typeof(Page).GetProperty("IsScreenCaptureEnabled");

                if (propertyInfo == null)
                {
                    throw new NotSupportedException("Not supported in this Windows Phone version!");
                }

                return (bool)propertyInfo.GetValue(page);
            }


        }
    }
