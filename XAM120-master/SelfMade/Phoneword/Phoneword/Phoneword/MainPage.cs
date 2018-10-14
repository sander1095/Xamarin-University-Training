using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        private readonly Entry _phoneWordEntry;
        private readonly Button _translateButton;
        private readonly Button _callButton;

        private string _translatedNumber;

        public MainPage()
        {
            #region Views
            _phoneWordEntry = new Entry
            {
                Text = "1-855-XAMARIN"
            };

            _translateButton = new Button
            {
                Text = "Translate"
            };

            _callButton = new Button
            {
                Text = "Call",
                IsEnabled = false,
            };

            _translateButton.Clicked += OnTranslate;
            #endregion

            Padding = new Thickness(20);

            var layout = new StackLayout
            {
                Spacing = 15,
                Children =
                {
                    new Label
                    {
                        Text = "Enter a Phoneword:",
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                    },
                    _phoneWordEntry,
                    _translateButton,
                    _callButton
                }
            };

            Content = layout;        
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            _translatedNumber = PhonewordTranslator.ToNumber(_phoneWordEntry.Text.Trim());

            _callButton.IsEnabled = _translatedNumber != null;
            _callButton.Text = $"Call {_translatedNumber}".Trim();
        }
    }
}
