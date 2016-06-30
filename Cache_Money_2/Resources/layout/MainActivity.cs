using Android.App;
using Android.Widget;
using Android.OS;
using Cache_Money_2;
using Android.Content;


namespace Cache_Money_2
{
	[Activity (Label = "Cache Money", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Button loginButton = FindViewById<Button> (Resource.Id.loginButton);
			EditText usernameTxt = FindViewById<EditText> (Resource.Id.usernameBox);
			EditText passwordTxt = FindViewById<EditText> (Resource.Id.passwordBox);
			CheckBox checkbox = FindViewById<CheckBox> (Resource.Id.newCheckBox);


			loginButton.Click += (object sender, System.EventArgs e) => {
				
				if (usernameTxt.Text != "" && passwordTxt.Text != ""){
				User u = new User (usernameTxt.Text, passwordTxt.Text);
				UserManager uManager = new UserManager ();
				bool credentials = uManager.verifyUser (u);

				// Login a normal user that's already in the db
				if (checkbox.Checked == false) {
					if (credentials == true) {
						
						// login the user if the username and password match
						u = uManager.loginUser(u);
							Toast.MakeText(this,u.getAccountID().ToString(),ToastLength.Long).Show();
						int id = u.getAccountID();
							if(id < 0){

							}else{
								Intent intent = new Intent (this, typeof(OverviewActivity));
								intent.PutExtra("id",id);
								StartActivity (intent);
							}
	
						
					} else if (credentials == false) {
						
						// Show fail message if username and password don't match 
						var loginFailed = new AlertDialog.Builder (this);
						loginFailed.SetMessage ("Login failed");
						loginFailed.SetNegativeButton ("Ok", delegate {
						});
						loginFailed.Show ();
					}
				} 
				// Add a new user to db
				else if (checkbox.Checked == true) {
					// check if the username is already taken
					bool newUser = uManager.verifyUser(u);
						if (newUser == true) {
						var usernameTaken = new AlertDialog.Builder (this);
						usernameTaken.SetMessage ("Username Taken");
						usernameTaken.SetNegativeButton ("Try again", delegate {
						});
						usernameTaken.Show ();

						} else if (newUser == false) {
							
						// add a new user to the database and move to the overview
						int id = uManager.addUserToDatabase(u);
						Intent intent = new Intent (this, typeof(OverviewActivity));
						intent.PutExtra("id",id);
						StartActivity(intent);
					}
				}				
				}
				// if a username or a password isn't entered 
				else{
					var blank = new AlertDialog.Builder (this);
					blank.SetMessage ("Please enter a username and password");
					blank.SetNegativeButton ("Try again", delegate {});
					blank.Show ();
				}

			};

		}
	}
}