using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
namespace Cache_Money_2
{
	[Activity(Label = "@string/ExpendituresView")]            
	public class ExpendituresActivity : ListActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			// Create your application here
			var expenditures = Intent.Extras.GetStringArrayList("expenditures") ?? new string[0];
			this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, expenditures);
		}
	}
}