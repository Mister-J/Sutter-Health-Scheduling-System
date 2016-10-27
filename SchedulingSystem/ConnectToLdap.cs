using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;

namespace SchedulingSystem
{
    public class ConnectToLdap
    {
        private LdapConnection connection;
        public void clientConnection(string username, string domain, string password, string url)
        {
            var credentials = new NetworkCredential(username, password, domain);
            var serverId = new LdapDirectoryIdentifier(url);


            connection = new LdapConnection(serverId, credentials);
        }




        public bool validateUserByBind(string username, string password)
        {
            bool result = true;
            var credentials = new NetworkCredential(username, password);
            var serverId = new LdapDirectoryIdentifier(connection.SessionOptions.HostName);


            var conn = new LdapConnection(serverId, credentials);
            try
            {
                conn.Bind();

            }
            catch (Exception)
            {
                result = false;
            }


            conn.Dispose();


            return result;
        }

    }

}