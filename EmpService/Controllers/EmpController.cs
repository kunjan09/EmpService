using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmpService.Controllers
{
    public class EmpController : ApiController
    {
        public HttpResponseMessage Get(string gender="All")
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {

                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(x=>x.Gender=="male") .ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(x => x.Gender == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Only male or female allowed");

                }


            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Emp with id: " + id + " not found.");
            }
        }

        public HttpResponseMessage Post([FromBody]Employee e)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Database.Connection.Open();
                    entities.Employees.Add(e);

                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, e);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + e.ID.ToString());

                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var val = entities.Employees.FirstOrDefault(x => x.ID == id);
                    if (val == null)
                    { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Emp id " + id + " could not found!"); }
                    else
                    {
                        entities.Employees.Remove(val);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Employee e)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(x => x.ID == id);

                if (entity != null)
                {
                    entity.FirstName = e.FirstName;
                    entity.LastName = e.LastName;
                    entity.Gender = e.Gender;
                    entity.Salary = e.Salary;
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Id " + id + " not found to update");
                }
            }
        }
    }
}
