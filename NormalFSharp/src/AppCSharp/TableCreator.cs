using Microsoft.SqlServer.Management.Smo;

namespace AppCSharp
{
	public static class TableCreator
	{
		/// <summary>
		/// 	Method creates table...
		/// </summary>
		public static void CreateTable()
		{			
			var srv = new Server();
			var db = srv.Databases["AdventureWorks"];
			var tb = new Table(db, "Test_Table");

			var col1 = new Column(tb, "Name", DataType.NChar(50));
			col1.Collation = "Latin1_General_CI_AS";
			col1.Nullable = true;
			tb.Columns.Add(col1);

			var col2 = new Column(tb, "ID", DataType.Int);
			col2.Identity = true;
			col2.IdentitySeed = 1;
			col2.IdentityIncrement = 1;
			tb.Columns.Add(col2);
			
			tb.Create();
		}
	}
}
