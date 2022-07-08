﻿// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    internal static partial class Advapi32
    {
        #region ---------- Public static methods ----------

        public static WindowsIdentity Logon(
                string domain,
                string userName,
                string password,
                LogonType logonType,
                LogonProvider logonProvider)
        {
            bool result = Advapi32UnsafeNativeMethods.LogonUser(
                    userName, domain, password, (int)logonType, (int)logonProvider, out var userToken);

            if (!result)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            return new WindowsIdentity(userToken);
        }

        #endregion
    }
}
