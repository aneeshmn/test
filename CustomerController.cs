using FSE_19_ADODotNet_2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FSE_19_ADODotNet_2.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        public ActionResult Index()

        {

            var result = DataAccess();

            DataTable customerDataTable = result.Tables[0];

            var list = CustomerTableDetails(customerDataTable);

            return View(list);

        }
        [HttpPost]

        public ActionResult Index(Customer cus)

        {

            var result = DataAccess();

            DataTable customerDataTable = result.Tables[0];

            var list = CustomerTableDetails(customerDataTable).Where(x => x.DOB == cus.searchdate);

            return View(list);

        }

        // GET: Customer/Edit/5

        public ActionResult Edit(int id)

        {

            var result = DataAccess();

            DataTable customerDataTable = result.Tables[0];

            var list = CustomerTableDetails(customerDataTable);

            var res = list.Where(x => x.CustId == id).FirstOrDefault();

            return View(res);

        }

        // POST: Customer/Edit/5

        [HttpPost]

        public ActionResult Edit(int id, Customer cus)

        {

            try

            {

                // TODO: Add update logic here

                SqlConnection conString = new SqlConnection("Data Source=DOTNET;Initial Catalog=FSD;Integrated Security=True");

                conString.Open();

                SqlCommand cmd = new SqlCommand();            

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "UpdateCustomerInfo";

                cmd.Parameters.Add("@Custid", SqlDbType.VarChar).Value = cus.CustId;

                cmd.Parameters.Add("@Custname", SqlDbType.VarChar).Value = cus.CustName;

                cmd.Parameters.Add("@CustAddress", SqlDbType.VarChar).Value = cus.CustAddress;

                cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = cus.DOB;

                cmd.Parameters.Add("@Salary", SqlDbType.Int).Value = cus.Salary;

                cmd.Connection = conString;

                try

                {

                cmd.ExecuteNonQuery();

                }

                catch (Exception ex)

                {

                throw ex;

                }

                finally

                {

                conString.Close();

                conString.Dispose();

                }

                return RedirectToAction("Index");

}

catch

{

return View();

}

}

public ActionResult Create()

{

    return View();

}

[HttpPost]

public ActionResult Create(Customer cus)

{

    try

    {

        // TODO: Add insert logic here

        SqlConnection conString = new SqlConnection("Data Source=DOTNET;Initial Catalog=FSD;Integrated Security=True");

        conString.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "SaveCustomerDetails";

        //cmd.Parameters.Add("@Custid", SqlDbType.VarChar).Value = cus.CustId;

        cmd.Parameters.Add("@Custname", SqlDbType.VarChar).Value = cus.CustName;

        cmd.Parameters.Add("@CustAddress", SqlDbType.VarChar).Value = cus.CustAddress;

        cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = cus.DOB;

        cmd.Parameters.Add("@Salary", SqlDbType.Int).Value = cus.Salary;

        cmd.Connection = conString;

        try

        {
            cmd.ExecuteNonQuery();

        }

        catch (Exception ex)

        {

            throw ex;

        }

        finally

        {

            conString.Close();

            conString.Dispose();

        }

        return RedirectToAction("Index");

    }

    catch

    {

        return View();

    }

}

// GET: Customer/Delete/5

public ActionResult Delete(int id)

{

    // TODO: Add update logic here

    SqlConnection conString = new SqlConnection("Data Source=DOTNET;Initial Catalog=FSD;Integrated Security=True");

    conString.Open();

    SqlCommand cmd = new SqlCommand();

    cmd.CommandType = CommandType.StoredProcedure;

    cmd.CommandText = "DeleteCustomerInfo";

    cmd.Parameters.Add("@Custid", SqlDbType.VarChar).Value = id;

    cmd.Connection = conString;

    try

    {

        cmd.ExecuteNonQuery();

    }

    catch (Exception ex)

    {

        throw ex;

    }

    finally

    {

        conString.Close();

        conString.Dispose();

    }

    return RedirectToAction("Index");

}

public DataSet DataAccess()

{

    SqlConnection conString = new SqlConnection("Data Source=DOTNET;Initial Catalog=FSD;Integrated Security=True");

    conString.Open();

    SqlCommand cmdQuery = new SqlCommand("GetAllCustomerInfo", conString);

    cmdQuery.CommandType = CommandType.StoredProcedure;

    SqlDataAdapter da = new SqlDataAdapter();

    da.SelectCommand = cmdQuery;

    DataSet dsData = new DataSet();
    da.Fill(dsData);

    conString.Close();

    return dsData;

}

public List<Customer> CustomerTableDetails(DataTable customerDataTable)

{

    var query = customerDataTable.AsEnumerable().

    Select(cat => new

    {

        Custid = cat.Field<int>("Custid"),

        Custname = cat.Field<string>("Custname"),

        CustAddress = cat.Field<string>("CustAddress"),

        DOB = cat.Field<DateTime>("DOB"),

        Salary = cat.Field<int>("Salary")

    });

    List<Customer> list = new List<Customer>();

    foreach (var item in query)

    {

        Customer obj = new Customer();

        obj.CustId = item.Custid;

        obj.CustName = item.Custname;

        obj.CustAddress = item.CustAddress;

        obj.DOB = item.DOB;

        obj.Salary = item.Salary;

        list.Add(obj);

    }

    return list;

}
}
}