using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

public class Votes
{
    public int CategoryId { get; set; }

    public Votes(int categoryId)
    {
        CategoryId = categoryId;
    }

    public Votes()
    {

    }

    //add vote to the selected category
    public int AddNewVoteToDB(int categoryId)
    {
        DBservices dbs = new DBservices();
        return dbs.addNewVoteToDB(categoryId);
    }

    
    public int UpdateParamsValue()
    {
        DBservices dbs = new DBservices();
        return dbs.UpdateParamsValue();
    }

    




}
