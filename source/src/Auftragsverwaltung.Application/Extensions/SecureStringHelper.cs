using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Auftragsverwaltung.Application.Extensions
{
    public static  class SecureStringHelper
    {
        public static SecureString GetSecureString()
        {
            char[] chars = { 'h', 'i', 'd', 'd', 'e', 'n', 's', 'e', 'c', 'r', 'e', 't' };
            SecureString secureString = new SecureString();
            foreach (char ch in chars)
                secureString.AppendChar(ch);

            return secureString;
        }
    }
}
