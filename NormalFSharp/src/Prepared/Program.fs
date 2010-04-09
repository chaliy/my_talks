exception MyError of string
raise (MyError("Error message"))

//type MyError() =
//    inherit System.Exception()

printfn "Hello word!"

System.Console.WriteLine("Hello word!")

open Microsoft.SqlServer.Management.Smo

module TableCreator =

    /// 	Method creates table...    
    let createTable() =
        let srv = Server()
        let db = srv.Databases.["AdventureWorks"]
        let tb = Table(db, "Test_Table")

        let col1 = Column(tb, "Name", DataType.NChar(50))
        col1.Collation <- "Latin1_General_CI_AS"
        col1.Nullable <- true
        tb.Columns.Add(col1)

        let col2 = new Column(tb, "ID", DataType.Int)
        col2.Identity <- true
        col2.IdentitySeed <- 1L
        col2.IdentityIncrement <- 1L
        tb.Columns.Add(col2)
        
        tb.Create()