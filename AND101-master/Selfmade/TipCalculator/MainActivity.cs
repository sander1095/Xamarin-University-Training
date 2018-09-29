using Android.App;
using Android.Widget;
using Android.OS;

namespace TipCalculator
{
    [Activity(Label = "TipCalculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private EditText _inputBill;
        private Button _calculateButton;
        private TextView _outputTip;
        private TextView _outputTotal;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            GetViews();

            _calculateButton.Click += OnCalculateClick;
        }

        private void OnCalculateClick(object sender, System.EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(_inputBill.Text) || !double.TryParse(_inputBill.Text, out double bill))
            {
                Toast
                    .MakeText(this, "The bill is not valid input", ToastLength.Short)
                    .Show();
                return;
            }

            var tip = bill * 0.15;
            var total = bill + tip;

            _outputTip.Text = tip.ToString();
            _outputTotal.Text = total.ToString();
            
        }


        private void GetViews()
        {
            _inputBill = FindViewById<EditText>(Resource.Id.inputBill);
            _calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            _outputTip = FindViewById<TextView>(Resource.Id.outputTip);
            _outputTotal = FindViewById<TextView>(Resource.Id.outputTotal);
        }
    }
}


