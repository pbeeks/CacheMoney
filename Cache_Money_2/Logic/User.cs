using System;

namespace Cache_Money_2
{
	public class User
	{
		private string userName;
		private string name;
		private string password;
		private string email;
		private int accountID;
		public Budget budget;
		private bool validCustomer;

		public User ()
		{
			this.userName = "";
			this.name = "";
			this.password = "";
			this.accountID = 0;
			this.email = "";
			this.budget = new Budget ();
		}

		public User(string userName, string name, string password, int accountID, string email){
			this.userName = userName;
			this.name = name;
			this.password = password;
			this.accountID = accountID;
			this.email = email;
		}

		public User(string userName, string password){
			this.userName = userName;
			this.password = password;
			budget = new Budget ();
		}



		//Getters and setters
		public void setUserName(string name){
			this.userName = name;
			return;
		} 

		public void setName(string name){
			this.name = name;
		}

		public void setPassword(string password){
			this.password = password;
		}

		public void setAccountID(int accountID){
			this.accountID = accountID;
		}

		public void setEmail(string email){
			this.email = email;
		}


		//Getters
		public string getUserName(){
			return this.userName;
		}

		public string getName(){
			return this.name;
		}

		public string getPassword(){
			return this.password;
		}

		public int getAccountID(){
			return this.accountID;
		}

		public string getEmail(){
			return this.email;
		}

		public double getTotalIncome(){
			return this.budget.incomeTotal ();
		}

		public double getTotalExpense(){
			return this.budget.expenseTotal ();
		}

		public bool getValidation(){
			return this.validCustomer;
		}
			
/* ==================================================================================
 * Methods to Add incomes and expenses to the list. If a recurring date is not passed
 * in then it is assumed that the expense/Income is not recurring.
 * ===================================================================================*/

		/*public bool addExpenditurePercent (double value, int freq, int category){
			if (value < 0 || value > 100) {
				return false;
			}
			double d = this.getTotalIncome ();
			double p = d / (value / 100);

			Database db = new Database ();
			db.AddExpenditure (this.accountID, category, p, DateTime.Now.ToString);


			return true;
		}*/


		//USE THIS ONE TO ADD TO DB	
	public bool addIncomeToUserAndDatabase(double value, int delayInDays, int category){
			Database db = new Database ();
			
			db.AddIncome (this.accountID, value, DateTime.Now.ToString(), delayInDays);
			return this.budget.addIncome (value, category);

		}
		
		//USE THIS ONE TO ADD TO DB
		public bool addExpenseToUserAndDatabase(int id, double money, int freq, int cat){
			Database db = new Database ();
		DateTime now = new DateTime ();
		now = DateTime.Now;
		now = now.AddDays (-8);
		db.AddExpenditure (id, cat, money,now.ToString() /*DateTime.Now.ToString()*/,freq);
			return this.budget.addExpense(money,cat);
		}


	public bool addExpenseJustToUser(int recordID, double value, int category, int recDays, string startDate){
		//Check and see if it recurring or not.
			if (recDays == 0) {
				//Simply add them to the users budget
				return this.budget.addExpense(value, category);
			} else {
				//Else pass all the info on. 
			return this.budget.addExpense(value,recDays,category,startDate);
			}


	}

	public bool addIncomeJustToUser(int incomeID, double value, int delayInDays, int category, string dateAdded){
			//Check and see if it is recurring.
			if (delayInDays == 0) {
				return this.budget.addIncome (value, category);
			}
			return this.budget.addIncome (value, delayInDays, category, dateAdded);
		}
			


		//public bool addExpense(int value, int delayInDays, int category){
		//	return this.budget.addExpense(value, delayInDays, category);
		//}
	

/* ========= Get Income/Expense at a specific index ================
 * Returns an income/Expense object at the passed in index		   *
 * ================================================================*/

		public Income getIncomeAt(int index){
			return this.budget.getIncomeAt (index);
		}

		public Expense getExpenseAt(int index){
			return this.budget.getExpenseAt (index);
		}
			
/* ================== lOGIN User ========================================================
 * Input: string Username																 *
 * Output: User Object.																	 *
 * 																						 *
 * This method takes a username and searches the user table for that username.			 *
 * If the username exists it will then take the password and try to match it			 *
 * If the passwords match, it will return true and create a user object based on		 *
 * The logged into user id. This user Object will contain all the users info.			 *
 * 																						 *
 * If its false it will create an empty user which can be used to register a user		 *
 * Using getters and setters. If that is not desired, Display class can just not use	 *
 * the user object. 																	 *
 * ======================================================================================*/

		public string getBudgetStartDay(){
			return this.budget.getStartDay ();
		}

	}
}

