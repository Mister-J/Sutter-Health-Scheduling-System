using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.DirectoryServices;


namespace SchedulingSystem
{
    public class ConnectToLdap
    {
        private static string username;
        private static string password;

        public ConnectToLdap(string usernameText, string passwordText)
        {
            username = usernameText;
            password = passwordText;
        }

        public static DirectoryEntry createDirectoryEntry()
        {
            DirectoryEntry ldapConnection = new DirectoryEntry("LDAP://example.com", username, password);
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
            return ldapConnection; 
        }

    }

}