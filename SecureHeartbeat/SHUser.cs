using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Microsoft.Phone.Maps.Controls;
using Parse;

namespace SecureHeartbeat
{
    public class SHUser
    {
        private ParseUser parseUser;
        private String _userName;
        private String _password;
        private String _forename;
        private String _surname;
        private String _department;
        private DateTime _dob;
        private bool _withinBoundary;
        private LocationRectangle _secureZone;


        public SHUser(ParseUser user)
        {
            /*parseUser = user;

            _userName = user.Username;
            _password = user.Password;
            _forename = 
            _surname;
            _department;
            _dob;
            _withinBoundary;
            _secureZone;*/
        }

        public SHUser()
        {
        }
    }
}
