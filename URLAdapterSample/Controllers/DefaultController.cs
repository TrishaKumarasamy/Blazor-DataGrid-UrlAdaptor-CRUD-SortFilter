using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.Blazor;
using URLAdaptorSample.Data;

namespace URLAdaptorSample.Controllers
{

    public class DefaultController : ControllerBase
    {
        public static List<Order> Orders = new List<Order>();
        private void BindDataSource()
        {
            int code = 10000;
            for (int i = 0; i < 5; i++)
            {
                Orders.Add(new Order() { OrderID = code + 1, CustomerID = "ALFKI", EmployeeID = i + 0, Freight = 2.3 * i, OrderDate = new DateTime(1991, 05, 15) });
                Orders.Add(new Order() { OrderID = code + 2, CustomerID = "ANATR", EmployeeID = i + 2, Freight = 3.3 * i, OrderDate = new DateTime(1990, 04, 04) });
                Orders.Add(new Order() { OrderID = code + 3, CustomerID = "ANTON", EmployeeID = i + 1, Freight = 4.3 * i, OrderDate = new DateTime(1957, 11, 30) });
                Orders.Add(new Order() { OrderID = code + 4, CustomerID = "BLONP", EmployeeID = i + 3, Freight = 5.3 * i, OrderDate = new DateTime(1930, 10, 22) });
                Orders.Add(new Order() { OrderID = code + 5, CustomerID = "BOLID", EmployeeID = i + 4, Freight = 6.3 * i, OrderDate = new DateTime(1953, 02, 18) });
                code += 5;
            }
        }
        [HttpPost]
        [Route("api/[controller]")]
        public object Post([FromBody] DataManagerRequest dm)
        {
            if (Orders.Count == 0)
            {
                BindDataSource();
            }
            IEnumerable DataSource = Orders.ToList();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = DataOperations.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = DataOperations.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = DataOperations.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Order>().Count();
            if (dm.Skip != 0)
            {
                DataSource = DataOperations.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = DataOperations.PerformTake(DataSource, dm.Take);
            }
            if (dm.RequiresCounts)
                return new { result = DataSource, count = count };
            else
                return new { result = DataSource };

        }
        [HttpPost]
        [Route("api/Default/Update")]
        public Order Update([FromBody] CRUDModel<Order> value)
        {
            var data = Orders.Where(or => or.OrderID == (value.Value).OrderID).FirstOrDefault();
            if (data != null)
            {
                data.OrderID = (value.Value).OrderID;
                data.CustomerID = (value.Value).CustomerID;
                data.EmployeeID = (value.Value).EmployeeID;
                data.OrderDate = (value.Value).OrderDate;
                data.Freight = (value.Value).Freight;
            }
            return value.Value;
        }
        [HttpPost]
        [Route("api/Default/Insert")]
        public Order Insert([FromBody] CRUDModel<Order> value)
        {
            Orders.Insert(0, value.Value);
            return value.Value;

        }
        [HttpPost]
        [Route("api/Default/Delete")]
        public Order Delete([FromBody] CRUDModel<Order> value)
        {
            Orders.Remove(Orders.Where(or => or.OrderID == value.Key).FirstOrDefault());
            return value.Value;
        }
        public class CRUDModel<T> where T : class
        {

            [JsonProperty("action")]
            public string Action { get; set; }
            [JsonProperty("table")]
            public string Table { get; set; }
            [JsonProperty("keyColumn")]
            public string KeyColumn { get; set; }
            [JsonProperty("key")]
            public int Key { get; set; }
            [JsonProperty("value")]
            public T Value { get; set; }
            [JsonProperty("added")]
            public List<T> Added { get; set; }
            [JsonProperty("changed")]
            public List<T> Changed { get; set; }
            [JsonProperty("deleted")]
            public List<T> Deleted { get; set; }
            [JsonProperty("params")]
            public IDictionary<string, object> Params { get; set; }
        }
    }
}