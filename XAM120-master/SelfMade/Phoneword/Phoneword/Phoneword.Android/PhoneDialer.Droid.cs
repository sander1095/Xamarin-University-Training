﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Phoneword.Droid;
using Android.Net;

[assembly: Dependency(typeof(PhoneDialer))]
namespace Phoneword.Droid
{
    public class PhoneDialer : IDialer
    {
        /// <summary>
        /// Dial the phone
        /// </summary>
        public async Task<bool> DialAsync(string number)
        {
            var permissionToCheck = Plugin.Permissions.Abstractions.Permission.Phone;
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permissionToCheck);
            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionToCheck);
                //Best practice to always check that the key exists
                if (results.ContainsKey(permissionToCheck))
                    status = results[permissionToCheck];
            }

            if (status == PermissionStatus.Granted)
            {
                var context = Android.App.Application.Context;
                if (context != null)
                {
                    var intent = new Intent(Intent.ActionCall);
                    intent.SetData(Uri.Parse("tel:" + number));

                    if (IsIntentAvailable(context, intent))
                    {
                        context.StartActivity(intent);
                        return await Task.FromResult(true);
                    }
                }
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Checks if an intent can be handled.
        /// </summary>
        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
                return true;

            TelephonyManager mgr = TelephonyManager.FromContext(context);
            return mgr.PhoneType != PhoneType.None;
        }
    }
}