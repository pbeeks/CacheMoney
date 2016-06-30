using System;

namespace Cache_Money_2{
	
	public class Income{

		private string startTime = DateTime.Now.ToString("MM/dd/yyyy");
		private double value;
		private bool recurring;
		private int time;
		private int categoryID;
		private string category;

		//TODO Make a enum for types of categories. 

		public Income (double value, bool rec, int time, int cat){
			this.value = value;
			this.recurring = rec;
			this.time = time;
			this.categoryID = cat;
		}

		public double getValue(){
			return this.value;
		}

		public void setValue(double v){
			this.value = v;
		}

		public string getCategory(){
			// Make a call to the database here and return the string. 
			// Using the Category id.
			return null;
		}

		public bool Recur(){
			return false;
		}

		public string getStartDate(){
			return this.startTime;
		}

		public int getReccuredAmount(string t){
			double days = (DateTime.Parse(t) - DateTime.Now).TotalDays;
			int retme = (int) Math.Round (days);
			return retme;
		}
	}

}

