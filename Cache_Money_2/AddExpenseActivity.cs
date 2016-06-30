using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;


namespace Cache_Money_2
{
	[Activity (Label = "@string/addExpenseActivity")]            
	public class AddExpenseActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			SetContentView (Resource.Layout.AddExpense);

			//get the user object
			int id = Intent.GetIntExtra ("id", -1);
			UserManager man = new UserManager ();
			User me = man.loginUser (id);
			User UserWeAreEditing = man.createUserbyID (me.getAccountID ());

			// Spinner selector
			Spinner catSpinner = FindViewById<Spinner> (Resource.Id.categorySpinner);
			var catAdapter = ArrayAdapter.CreateFromResource (this, Resource.Array.categories_array, Android.Resource.Layout.SimpleSpinnerItem);
			catAdapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			catSpinner.Adapter = catAdapter;
			int cat = catSpinner.SelectedItemPosition + 1;

			// Monetary amount
			EditText amount = FindViewById<EditText> (Resource.Id.expenseAmount);
			double money;
			//Check box 
			CheckBox checkbox = FindViewById<CheckBox> (Resource.Id.freqCheckBox);
			bool cBox = checkbox.Checked;

			// Frequency spinner
			Spinner freqSpinner = FindViewById<Spinner> (Resource.Id.frequencySpinner);
			var freqAdapter = ArrayAdapter.CreateFromResource (this, Resource.Array.frequency_array, Android.Resource.Layout.SimpleSpinnerItem);
			freqAdapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			freqSpinner.Adapter = freqAdapter;
			int freq = freqSpinner.SelectedItemPosition + 1;

			// Botton activities here
			Button save = FindViewById<Button> (Resource.Id.expenseSave);
			Button cancel = FindViewById<Button> (Resource.Id.expenseCancel);
			amount.Text = "0";

			// actions when the save button is clicked
			save.Click += (object sender, System.EventArgs e) => {
				cat = catSpinner.SelectedItemPosition +1;

				//If recurring is checked
				if(checkbox.Checked){
					//Check the freq spinner and set freq accordingly
					if(freqSpinner.SelectedItemPosition == 0){
						freq = 1;
					}else if(freqSpinner.SelectedItemPosition == 1){
						freq = 7;
					}else{
						freq = 30;
					}

					//If the recurring box is not set
				}else{
					freq = 0;
				}

				//Getting the money value
				money = double.Parse(amount.Text);

				//Adding the expense to the database
				bool addE = UserWeAreEditing.addExpenseToUserAndDatabase (id, money, freq, cat);
				if (addE == true) {
					Toast.MakeText (this, "Successfully added income", ToastLength.Long).Show ();
				}

				//Going back to home. 
				Intent intent = new Intent (this, typeof(OverviewActivity));
				intent.PutExtra ("id", id);
				StartActivity (intent);


			};
			// actions for when the cancel button is clicked
			cancel.Click += (object sender, System.EventArgs e) => {
				Intent intent = new Intent (this, typeof(OverviewActivity));
				intent.PutExtra ("id", id);
				StartActivity (intent);
			};

		}
	}
}