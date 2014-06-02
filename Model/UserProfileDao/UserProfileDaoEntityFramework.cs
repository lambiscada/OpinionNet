﻿using System;
using System.Data.Objects;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Linq;
using System.Data.Metadata.Edm;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaIS.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {

        public UserProfileDaoEntityFramework() { }

        #region IUserProfileDao Members
        public List<string> FindAllLogins()
        {
           ObjectSet<UserProfile> userProfiles = Context.CreateObjectSet<UserProfile>();

            var result =
                  (from u in userProfiles
                   select u.loginName).ToList();
           
            return result;

        }
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            #region Option 1: Using Linq.

            ObjectSet<UserProfile> userProfiles = 
                Context.CreateObjectSet<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            #endregion

            #region Option 2: Using Entity SQL and Object Services.

            //String query =
            //    "SELECT VALUE u FROM MiniPortalEntitiesContainer.UserProfiles AS u " +
            //    "WHERE u.loginName=@loginName";

            //ObjectParameter param = new ObjectParameter("loginName", loginName);

            //ObjectResult<UserProfile> result =
            //    this.Context.CreateQuery<UserProfile>(query, param).Execute(MergeOption.AppendOnly);

            //try
            //{
            //    userProfile = result.First<UserProfile>();
            //}
            //catch (Exception)
            //{
            //    userProfile = null;
            //}

            #endregion

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;

        #endregion

        }
    }

}

