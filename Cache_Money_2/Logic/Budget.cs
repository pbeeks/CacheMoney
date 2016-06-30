using System;
using System.Collections.Generic;

namespace Cache_Money_2{
	
	public class Budget{
		
		//Variables that will indicate budget. 
		private string intialStartTime = DateTime.Now.ToString("MM/dd/yyyy");
		private List<Income> incomes;
		private List<Expense> expenses;
		private double currentIncome;
		private double currentExpense;


		//Constructor for the Budget object. Creates two lists that will be added to as the user creates them.
		public Budget (){
			incomes = new List<Income> ();
			expenses = new List<Expense> ();
		}

		//Constructor for when the user as existed and already has a running balance. 
		public Budget(double i, double e){
			this.currentIncome = i;
			this.currentExpense = e;
			incomes = new List<Income> ();
			expenses = new List<Expense> ();
		}



 /* ==================================================================================
 * Methods to Add incomes and expenses to the list. If a recurring date is not passed
 * in then it is assumed that the expense/Income is not recurring.
 * ===================================================================================*/
		public bool addIncome(double value, int category){
			try{
			Income i = new Income (value, false, 0,category);
			this.incomes.Add (i);
			return true;
			}catch(Exception e){
				return false;
			}
		}

		public bool addIncome(double value, int delayInDays, int category, string StartDate){
			try{
			Income i = new Income (value, true, delayInDays,category);
			this.incomes.Add (i);
			return true;
			}catch(Exception e){
				return false;
			}
		}

	// Adding expenses start below this. Same as adding income, Just expenses. 
	
		public bool addExpense(double value, int category){
			try{
				Expense i = new Expense (value, false, 0,category);
				this.expenses.Add (i);
				return true;
			}catch(Exception e){
				return false;
			}
		}

		public bool addExpense(double value, int delayInDays, int category, string StartDate){
			try{
				Expense i = new Expense (value, true, delayInDays,category);
				double num = i.getReccuredAmount(StartDate);
				for(int k = 0; k > num + 1; k--){
					i.setValue(i.getValue()+value);
				}

				this.expenses.Add (i);
				return true;
			}catch(Exception e){
				return false;
			}
		}


/* ===== Get Totals ==============================================================
 * Allows the user class to call these to receive the total incomes and expenses.*
 * It Loops through the income and expense objects and totals their values       *
 * ==============================================================================*/ 

		public double incomeTotal(){
			double retVal = 0;
			for (int i = 0; i < this.incomes.Count; i++) {
				retVal += incomes [i].getValue ();
			}
			return retVal;
		}

		//Expenses get total below. 

		public double expenseTotal(){
			double retVal = 0;
			for (int i = 0; i < this.expenses.Count; i++) {
				retVal -= expenses[i].getValue ();
			}
			return retVal;
		}

/* ========= Get Income/Expense at a specific index ================
 * Returns an income/Expense object at the passed in index		   *
 * ================================================================*/

		public Income getIncomeAt (int index){
			return this.incomes [index];
		}

		public Expense getExpenseAt(int index){
			return this.expenses [index];
		}

		public string getStartDay(){
			string s = this.intialStartTime;
			return s;
		}


	}
}

