using System;
using System.Linq;
using Raven.Client.Document;
using Raven.Client.Indexes;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Connection..");
        var db = new DocumentStore
        {
            Url = "http://localhost:9653/"
        };
        db.Initialize();







        Console.WriteLine("First doc...");
        string docId;
        using (var session = db.OpenSession())
        {
            var rec = new SaleRecord();
            session.Store(rec);

            session.SaveChanges();

            docId = rec.Id;
        }

        Console.WriteLine(docId);






        Console.WriteLine("Update doc..");
        using (var session = db.OpenSession())
        {
            var rec = session.Load<SaleRecord>(docId);
            //var rec = session.Query<SaleRecord>().FirstOrDefault(x => x.Id == docId);
            rec.Total = 12.0m;
            rec.Date = DateTime.Now;

            session.SaveChanges();
        }





        Console.WriteLine("Delete doc...");
        using (var session = db.OpenSession())
        {
            var rec = session.Load<SaleRecord>(docId);
            session.Delete(rec);

            session.SaveChanges();
        }







        Console.WriteLine("Index, Map/Reduce demo...");
        IndexCreation.CreateIndexes(typeof(Program).Assembly, db);

        using (var session = db.OpenSession())
        {
            session.Store(new SaleRecord
            {
                Total = 10.0m,
                Date = new DateTime(2012, 11, 12)
            });

            session.Store(new SaleRecord
            {
                Total = 13.0m,
                Date = new DateTime(2012, 11, 13)
            });

            session.Store(new SaleRecord
            {
                Total = 12.0m,
                Date = new DateTime(2012, 11, 13)
            });

            session.SaveChanges();
        }		
		
		db.Dispose();		
			
	}		
}	

public class SaleRecord
{
    public string Id { get; set; }
	public DateTime Date {get; set;}
	public decimal Total {get; set;}
}


public class SaleRecords_Daily 
	: AbstractIndexCreationTask<SaleRecord>
{
	// http://localhost:8080/indexes/salerecords/daily 
	public SaleRecords_Daily()    
	{
		Map = docs => from doc in docs				
			select new
			{                      
				Date = doc.Date,
				Total = doc.Total
			};       
		Reduce = results => from result in results 
			group result by result.Date.Date into g         
			select new 
			{                             
				
				Date = g.Key,            
				Total = g.Sum(x => x.Total)   
			};   
	}
}