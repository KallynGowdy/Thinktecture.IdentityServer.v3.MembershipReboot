﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.MembershipReboot;

namespace SelfHost
{
    public class MembershipRebootUserServiceFactory
    {
        public static IUserService Factory(string connString)
        {
            var repo = new DefaultUserAccountRepository(connString);
            var userAccountService = new UserAccountService(config, repo);
            var userSvc = new MembershipRebootUserService<UserAccount>(userAccountService, repo);
            return userSvc;
        }

        static MembershipRebootConfiguration config;
        static MembershipRebootUserServiceFactory()
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<DefaultMembershipRebootDatabase, BrockAllen.MembershipReboot.Ef.Migrations.Configuration>());

            config = new MembershipRebootConfiguration();
            config.PasswordHashingIterationCount = 50000;
            config.AllowLoginAfterAccountCreation = true;
            config.RequireAccountVerification = false;
        }
    }
}
