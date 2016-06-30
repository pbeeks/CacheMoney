using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;


namespace Cache_Money_2
{
	[Activity (Label = "@string/overviewActivity")]            
	public class OverviewActivity : MainActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			SetContentView (Resource.Layout.Overview);
			int id = Intent.GetIntExtra ("id", -1);
			UserManager man = new UserManager ();
			User me = man.loginUser (id);
			User parseMe = man.createUserbyID(me.getAccountID());


			List<string> expenditures = new List<string>();
			EditText balance = FindViewById<EditText> (Resource.Id.balanceText);
			balance.Text = (parseMe.getTotalIncome() - parseMe.getTotalExpense()).ToString();

			EditText income = FindViewById<EditText> (Resource.Id.incomeText);
			income.Text = parseMe.getTotalIncome().ToString();


			Button addExpense = FindViewById<Button> (Resource.Id.expenseButton);
			Button addIncome = FindViewById<Button> (Resource.Id.incomeButton);

			addExpense.Click += (object sender, System.EventArgs e) => {
				Intent intent = new Intent (this, typeof(AddExpenseActivity));
				intent.PutExtra ("id", id);
				StartActivity (intent);
			};


			addIncome.Click += (object sender, System.EventArgs e) => {
				Intent intent = new Intent (this, typeof(AddIncomeActivity));
				intent.PutExtra ("id", id);
				StartActivity (intent);
			};

			Button expendituresButton = FindViewById<Button> (Resource.Id.expendituresButton);
			expendituresButton.Click += (sender, e) =>
			{
				Database db = new Database();
				string recIDs = db.GetRecordIDByUserID(id);
				string[] records = recIDs.Split(',');
				for(int i = 0; i < records.Length -1 ;i++){
					expenditures.Add(db.GetRecordInfoByRecordID(int.Parse(records[i])));
				}
				var intent = new Intent(this, typeof(ExpendituresActivity));
				intent.PutStringArrayListExtra("expenditures", expenditures);
				StartActivity(intent);
			};
		}
	}
}