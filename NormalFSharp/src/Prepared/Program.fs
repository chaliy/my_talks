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


type CoolDTO = {
    ID : int
    Name : string    
}
with
    member this.Test() = printfn "Test"
    member this.Test2() = printfn "Test2"

let c = { ID = 1; Name = "Test" }

let c' = { c with Name = "Test2" }

let x = (1, "sdf")

let a, b = x

printfn "%A" (a.GetType())


(* Async patterns *)

open System.IO
open System.Net    

let getHtml() =
    async {

        let getResult (rsp : WebResponse) =
            use stream = rsp.GetResponseStream()
            use reader = new StreamReader(stream)        
            reader.ReadToEnd()

        let req = WebRequest.Create("http://localhost:4444/file1.txt")
        let! rsp = req.AsyncGetResponse()
        let result1 = getResult(rsp)    

        let req2 = WebRequest.Create("http://localhost:4444/" + result1)
        let! rsp2 = req2.AsyncGetResponse()
        return getResult(rsp2)            

    } |> Async.RunSynchronously

let res = getHtml()

printfn "Result %s" res


(* Matching *)
let isOdd x = (x % 2 = 1)

let describeNumber x =
    match isOdd x with
    | true -> printfn "x is odd"
    | false -> printfn "x is even"

let testAnd x y =
    match x, y with
    | true, true -> true
    | true, false -> false
    | false, true -> false
    | false, false -> false

let anotherTest (x : string * string ) =
    match x with
    | ( z, "Test" ) when z.StartsWith("F") 
        -> printfn "Test with %s" z
    | _ -> printfn "Not test"


anotherTest ("Foo", "Boo" )
anotherTest ("Foo", "Test")