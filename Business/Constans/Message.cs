using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constans
{
   public static class Message
    {
        //You can write messages here

        public static string hello = "hello word";
        public static string AccessTokenCreated = "token created";
        public static string UserAlreadyExists = "the user is already registered in the system";
        public static string UserRegistered = "Registration Successful";
        public static string AddedUser = "user added";
        public static string Deleted = "deletion successful";
        public static string UpdateUser = "user updated";
        public static string SameUserName = "same username";


        public static string UserUpdated = "user updated";
        public static string LoginRequired  = "Login Required";
        public static string AuthorizationDenied = "Authorization Denied";
		internal static string Updated;
		internal static string Added;
	}
}
