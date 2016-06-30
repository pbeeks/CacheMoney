using System;
using Android.Database.Sqlite;
using SQLite;
using System.IO;
using System.Data;
using System.Data.Sql;

namespace Cache_Money_2
{


	//-------------------------------------------------------------------------------------------------------------
	//Table Creations. RecordTable will never be touched by the user as it has predefined rows in it.
	//UserTable and RecordTable will be different by User.
	//-------------------------------------------------------------------------------------------------------------

	[Table("UserTable")]


	public class UserTable
	{

		[PrimaryKey, AutoIncrement, Column ("_Id")]
		public int UserID { get; set; }

		[MaxLength(20), NotNullAttribute]

		public string UserName { get; set; }

		[MaxLength (20), NotNullAttribute]
		public string Password { get; set; }

		[MaxLength (50)]
		public string Email { get; set; }


	}

	[Table ("RecordTable")]

	public class RecordTable
	{
		[PrimaryKey, AutoIncrement, Column ("_Id")]
		public int RecordID { get; set; }

		[NotNullAttribute]
		public int UserID { get; set; }

		[NotNullAttribute]
		public int CategoryID { get; set; }

		[NotNullAttribute]
		public double Amount { get; set; }

		public string StartDate { get; set; }

		public int OccuranceType { get; set; }

	}

	[Table ("CategoryTable")]

	public class CategoryTable
	{
		[PrimaryKey, AutoIncrement, Column ("_Id")]
		public int CategoryID { get; set; }

		[NotNullAttribute]
		public string Name { get; set; }

	}


}
