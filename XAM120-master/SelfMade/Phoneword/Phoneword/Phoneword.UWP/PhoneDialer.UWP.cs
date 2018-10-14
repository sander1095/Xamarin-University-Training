using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Phoneword.UWP;
using Windows.Foundation.Metadata;

[assembly: Dependency(typeof(PhoneDialer))]
namespace Phoneword.UWP
{
    public class PhoneDialer : IDialer
    {
        public Task<bool> DialAsync(string number)
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.Calls.CallsPhoneContract", 1,0))
            {
               Windows.ApplicationModel.Calls
                   .PhoneCallManager.ShowPhoneCallUI(number, "Phoneword");

               return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
