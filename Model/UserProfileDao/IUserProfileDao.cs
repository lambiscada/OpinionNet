﻿using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaIS.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByLoginName(String loginName);


        List<string> FindAllLogins();
    }
}
