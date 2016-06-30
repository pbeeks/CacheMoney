using System;

namespace Cache_Money_2
{
	public class UserManager
	{
		public UserManager ()
		{
			createDBIfFIrstTime ();
		}


/* ========== Add User to Database =============================/
 * Takes a user and creates the database if it does not exist.
 * Then, it will add the user to the database.
 * ============================================================*/
		public int addUserToDatabase(User u)
		{
			createDBIfFIrstTime ();
			Database db = new Database ();
			int retString = db.AddUser (u.getUserName (), u.getPassword ());
			return retString;
		}



		//Private method to create the database if its the first time.
		private void createDBIfFIrstTime ()
		{
			Database db = new Database ();
			db.CreateDB();
		}


/* ==========  Verify User ======================================/
 * Takes a user and Checks whether the database contains that 
 * Username yet or not. 
 * 
 * If username does exist in database : Return True;
 * If username does not exist in database : Return false;
 * =============================================================*/
		public bool verifyUser (User u)
		{
			
			Database dv = new Database ();
			bool ret = dv.VerifyUser(u.getUserName ());
			return ret;
		}


/* ======================== Login User =====================================================/
 * Input : User																				/
 * Output : Logged in user																	/
 * 																							/
 * Alright, so this is mostly for the making the phase II easier sorry.						/
 * What you are going to have to do it create a user with just the username					/
 * and password. then pass that user in and set it to the result. Example below.			/
 * 																							/
 * User j = new User(usernameBox.text,passwordBox.text);									/
 * UserManager oManager = new UserManager();												/
 * 																							/
 * j = oManager.loginUser(j);																/
 *																							/
 * // End of example																		/
																							/
 * This will set the user up with all the information from the user in the database			/
 * instead of just having username and password.											/
 * ========================================================================================*/
		public User loginUser (User u){
			Database db = new Database ();
			int userId = db.LoginUser (u.getUserName (), u.getPassword ());
			User retMe;
			if (userId < 0) {
				//FUCK SHIT THIS ISNT SUPPOSED TO HAPPEN
					//FUCKING FUCK FUCK wAHT DO WE DO
				//OH OK , Ill just make an empty user here. 

				retMe = new User ("Null", "Null");
				retMe.setAccountID (-1);
				return retMe;
			}
			string parseMe = db.GetUserByID (userId);
			string[] usersStuff;
			usersStuff = parseMe.Split (',');

			usersStuff [2].Replace (':', ' ');
			retMe = new User ();
			retMe.setUserName (usersStuff [1]);
			retMe.setPassword (usersStuff [2]);
			retMe.setAccountID (int.Parse (usersStuff [0]));
			return retMe;

		}

		public User loginUser (int id){
			User retMe;
			if (id < 0) {
				retMe = new User ("NULL", "NULL");
					//FUCK SHIT THIS ISNT SUPPOSED TO HAPPEN
					//FUCKING FUCK FUCK wAHT DO WE DO
					//OH OK , Ill just make an empty user here. 

					retMe = new User ("Null", "Null");
					retMe.setAccountID (-1);
					return retMe;
			}
			Database db = new Database ();
			string parseMe = db.GetUserByID (id);
			string[] usersStuff;
			usersStuff = parseMe.Split (',');
			retMe = new User ();
			usersStuff [2].Replace (':', ' ');
			retMe.setUserName (usersStuff [1]);
			retMe.setPassword (usersStuff [2]);
			retMe.setAccountID (int.Parse (usersStuff [0]));
			return retMe;

		}


		public User stringLoginUser(string name, string pass){
			Database db = new Database ();
			int userId = db.LoginUser (name, pass);
			User retMe;
			if (userId < 0) {
				//FUCK SHIT THIS ISNT SUPPOSED TO HAPPEN
				//FUCKING FUCK FUCK wAHT DO WE DO
				//OH OK , Ill just make an empty user here. 

				retMe = new User ("Null", "Null");
				retMe.setAccountID (-1);
				return retMe;
			}

			string parseMe = db.GetUserByID (userId);
			string[] usersStuff;
			usersStuff = parseMe.Split (',');
			retMe = new User ();
			usersStuff [2].Replace (':', ' ');
			retMe.setUserName (usersStuff [1]);
			retMe.setPassword (usersStuff [2]);
			retMe.setAccountID (int.Parse (usersStuff [0]));
			return retMe;

		}

		public User createUserbyID(int id){
			Database db = new Database ();
			// Create a new DB

			string parseMe = db.GetUserByID (id);
			// Get the persons Info. 

			string[] usersStuff;
			usersStuff = parseMe.Split (',');
			User retMe = new User ();
			usersStuff [2].Replace (':', ' ');
			retMe.setUserName (usersStuff [1]);
			retMe.setPassword (usersStuff [2]);
			retMe.setAccountID (int.Parse (usersStuff [0]));

			// cReate a new user. 


			// Get all of there records 
			parseMe = db.GetRecordIDByUserID (retMe.getAccountID ());

			// Split up thsoe records.
			usersStuff = parseMe.Split (',');
			 
			//Case when the user doesnt have anything at all
			if (parseMe.Equals ("")) {
				return retMe;
			}

			//Start parsing all this shit.
			int count = usersStuff.Length;
			for (int i = 0; i < count - 1; i++) {
				string info = db.GetRecordInfoByRecordID (int.Parse(usersStuff [i]));
				if (info.Equals ("")) {
					return retMe;
					//Case when the user has no records
				}
				string[] infoTable = info.Split (',');

				//infoTable[0] == RecordID
				//infoTable[1] == UserID
				//infoTable[2] == Category ID
				//infoTable[3] == Amount
				//infoTable[4] == Start Date
				//infoTable[5] == Reccuring or not

				//Checking to see if it is an income or an expense by category id. 
				infoTable[5] = infoTable [5].Replace (":","");
				if (int.Parse(infoTable [2]) == 1) {
					
					retMe.addIncomeJustToUser (int.Parse(infoTable[0]), double.Parse(infoTable [3]), int.Parse(infoTable [5]), int.Parse(infoTable [2]), infoTable[4]);
				} else {
							retMe.addExpenseJustToUser (int.Parse(infoTable [0]), double.Parse(infoTable [3]), int.Parse(infoTable [2]) , int.Parse(infoTable[5]),infoTable[4]);
				}
			}

			parseMe = retMe.getTotalExpense ().ToString () + " " + retMe.getTotalIncome ();
			return retMe;

		}
			
	}
}

