using System;
using Android.Database.Sqlite;
using SQLite;
using System.IO;
using System.Data;
using System.Data.Sql;

namespace Cache_Money_2
{

	public class Database
	{

		//-------------------------------------------------------------------------------------------------------------
		// Used on initial setup. Creates a database for your phone and creates the tables, and populates the required tables for the app.
		//-------------------------------------------------------------------------------------------------------------


		public string CreateDB ()

		{

			try {
				string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");
				var db = new SQLiteConnection (dbPath);

				db.CreateTable<UserTable> ();
				db.CreateTable<RecordTable> ();
				db.CreateTable<CategoryTable> ();

				Database insert = new Database ();
				insert.InsertCategoryRecord ();

				string result = "Base database created...";
				return result;

			} catch (Exception ex) {
				return "Error: " + ex.Message;
			}

		}


		//-------------------------------------------------------------------------------------------------------------
		//Populates the CategoryRecord Table. Used in conjunction with CreateDBBase.
		//-------------------------------------------------------------------------------------------------------------

		public string InsertCategoryRecord ()
		{
			try {
				string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");
				var db = new SQLiteConnection (dbPath);

				var a = new CategoryTable { Name = "Income" };
				db.Insert (a);
				var b = new CategoryTable { Name = "Housing" };
				db.Insert (b);
				var c = new CategoryTable { Name = "Food" };
				db.Insert (c);
				var d = new CategoryTable { Name = "Medical / Dental" };
				db.Insert (d);
				var e = new CategoryTable { Name = "Auto" };
				db.Insert (e);
				var f = new CategoryTable { Name = "Entertainment" };
				db.Insert (f);
				var g = new CategoryTable { Name = "Personal Care / Clothing" };
				db.Insert (g);
				var h = new CategoryTable { Name = "Miscellaneous" };
				db.Insert (h);
				var i = new CategoryTable { Name = "Charitable Giving" };
				db.Insert (i);
				var j = new CategoryTable { Name = "Debts" };
				db.Insert (j);
				var k = new CategoryTable { Name = "Insurance" };
				db.Insert (k);
				var l = new CategoryTable { Name = "Charitable Giving" };
				db.Insert (l);
				var m = new CategoryTable { Name = "Investments / Savings" };

				db.Insert (m);

				string result = "CategoryTable populated...";
				return result;

			} catch (Exception ex) {
				return "Error: " + ex.Message;
			}
		}


		//-------------------------------------------------------------------------------------------------------------
		//Use for testing. Will return the CategoryTable records
		//-------------------------------------------------------------------------------------------------------------


		public string TestRecord ()

		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");
			var db = new SQLiteConnection (dbPath);

			string output = "";
			output += "Retreiving testrecords...";
			var Table = db.Table<CategoryTable> ();
			foreach (var item in Table) {
				output += "" + item.CategoryID + "," + item.Name + ":";
			}

			return output;
		}


		//-------------------------------------------------------------------------------------------------------------
		// Delete SP's used for testing. With a functional app, the user will never use these as the tables are deleted upon uninstallment of the applcication.
		//-------------------------------------------------------------------------------------------------------------

		public string DeleteCategoryTable ()

		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");
			var db = new SQLiteConnection (dbPath);

			string SQL = "DROP TABLE CategoryTable;";
			db.Execute (SQL);

			string result = "CategoryTable deleted...";
			return result;

		}

		public string DeleteUserTable ()
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");
			var db = new SQLiteConnection (dbPath);

			string SQL = "DROP TABLE UserTable;";
			db.Execute (SQL);

			string result = "UserTable deleted...";
			return result;

		}

		public string DeleteRecordTable ()
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");
			var db = new SQLiteConnection (dbPath);

			string SQL = "DROP TABLE RecordTable;";
			db.Execute (SQL);

			string result = "RecordTable deleted...";
			return result;

		}


		//-------------------------------------------------------------------------------------------------------------
		//Input a UserName and Password and it will be stored in the UserTable. Output is the UserID associated with the inputted user.
		//-------------------------------------------------------------------------------------------------------------

		public int AddUser (string USERNAME, string PASSWORD)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var User = new UserTable { UserName = USERNAME, Password = PASSWORD, Email = "" };
			db.Insert (User);

			int output = 0;
			var Table = db.Table<UserTable> ();
			foreach (var item in Table) {
				output = item.UserID;
			}

			return output;
		}

		//-------------------------------------------------------------------------------------------------------------
		//Used on the login page to register. A Username has to be unique in order for someone to use it as their login UserName, otherwise it will return false.
		//-------------------------------------------------------------------------------------------------------------

		public bool VerifyUser (string USERNAME)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "dbMain.db3");
			var db = new SQLiteConnection (dbPath);

			var VerifiedUser = db.Query<UserTable> ("SELECT UserName FROM UserTable WHERE UserName = ?", USERNAME);

			int count = 0;


			foreach (var s in VerifiedUser) 
			{
				count++;
			}

			if (count > 0) {
				return false;
			} else {
				return true;
			}

		}


		//-------------------------------------------------------------------------------------------------------------
		//Used on the login page to access the rest of the application. A UserName and password must match inorder for 
		//the User to be logged in. A successful match will return the UserID for said User.
		//-------------------------------------------------------------------------------------------------------------

		public int LoginUser(string USERNAME, string PASSWORD)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var loginUser = db.Query<UserTable> ("SELECT * FROM UserTable WHERE UserName = ? and Password = ?", USERNAME, PASSWORD);

			int output = 0;


			foreach (var s in loginUser) {
				output = s.UserID;
			}

			if (output == 0) {
				return -1;
			} else {
				return output;
			}
		}


		//-------------------------------------------------------------------------------------------------------------
		//SP used to return all Users using the application from the UserTable. It will return: UserID, UserName, and Password
		//-------------------------------------------------------------------------------------------------------------

		public string GetUser(){
			string dbPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var getUsers = db.Query<UserTable> ("SELECT * From UserTable;");

			string output = "";
			foreach (var s in getUsers) {
				output += s.UserID + "," + s.UserName + "," + s.Password + ":";
			}
			return output;
		}


		//-------------------------------------------------------------------------------------------------------------
		//SP used to return a specific user's information based on a passed in UserID. It will return: UserID, UserName, and Password
		//-------------------------------------------------------------------------------------------------------------

		public string GetUserByID(int id){
			string dbPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var getUsers = db.Query<UserTable> ("SELECT * FROM UserTable WHERE _Id = ?", id);

			string output = "";
			foreach (var s in getUsers) {
				output += s.UserID + "," + s.UserName + "," + s.Password + ":";
			}
			return output;
		}


		//-------------------------------------------------------------------------------------------------------------
		//SP used to add an income. Default CategoryID is 1 for income. Input the UserID, Amount, and StartDate
		//-------------------------------------------------------------------------------------------------------------

		public int AddIncome (int UserID, double AMOUNT, string DATE, int OccurType){
			string dbPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var a = new RecordTable { UserID = UserID, CategoryID = 1, Amount = AMOUNT, StartDate = DATE, OccuranceType = OccurType };
			db.Insert (a);

			int output = 0;
			var Table = db.Table<RecordTable> ();
			foreach (var item in Table) {
				output = item.RecordID;
			}

			return output;
		}


		//-------------------------------------------------------------------------------------------------------------
		//SP used to add an expenditure. Input a UserID, CategoryID, Amount, and StartDate
		//-------------------------------------------------------------------------------------------------------------

		public int AddExpenditure (int UserID, int Categoryid, double AMOUNT, string DATE, int OccurType){
			string dbPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var a = new RecordTable { UserID = UserID, CategoryID = Categoryid, Amount = -Math.Abs (AMOUNT), StartDate = DATE, OccuranceType = OccurType };
			db.Insert (a);

			int output = 0;
			var Table = db.Table<RecordTable> ();
			foreach (var item in Table) {
				output = item.RecordID;
			}

			return output;

		}


		//-------------------------------------------------------------------------------------------------------------
		//SP to return all the recordID's associated with a passed in UserID.
		//-------------------------------------------------------------------------------------------------------------

		public string GetRecordIDByUserID (int id){
			string dbPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var getRecord = db.Query<RecordTable> ("SELECT * FROM RecordTable WHERE UserID = ?", id);

			string output = "";
			foreach (var s in getRecord) {
				output += s.RecordID + ",";
			}
			return output;

		}


		//-------------------------------------------------------------------------------------------------------------
		//SP to Get a records information based on a passed in RecordID. Returns the records: RecordID, UserID, CategoryID, Amount, and StartDate
		//-------------------------------------------------------------------------------------------------------------

		public string GetRecordInfoByRecordID (int id){
			string dbPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbMain.db3");

			var db = new SQLiteConnection (dbPath);

			var getRecord = db.Query<RecordTable> ("SELECT * FROM RecordTable WHERE _Id = ?", id);

			string output = "";
			foreach (var s in getRecord) {
				output +=  s.RecordID + "," + s.UserID + "," + s.CategoryID + "," + s.Amount + "," + s.StartDate + "," + s.OccuranceType + ":";
			}
			return output;

		}

	}

}
