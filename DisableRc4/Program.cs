using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisableRc4
{
    class Program
    {
        static void Main(string[] args)
        {
            var subKeys = new []
        {
            "RC4 40/128",
            "RC4 56/128",
            "RC4 64/128",
            "RC4 128/128",
        };

            RegistryKey parentKey = Registry.LocalMachine.OpenSubKey(
                @"SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers", true);

            foreach (var keyName in subKeys)
            {
                if (parentKey != null)
                {
                    var newKey = parentKey.CreateSubKey(keyName);
                    if (newKey != null)
                    {
                        newKey.SetValue("Enabled", 0);
                        newKey.Close();
                    }
                }
            }
            if (parentKey != null) parentKey.Close();
        }
    }
}
