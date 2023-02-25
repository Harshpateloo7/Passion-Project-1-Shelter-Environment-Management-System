using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_1.Models;

namespace Project_1.Controllers
{
    public class DepartmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DepartmentData/ListDepartments
        [HttpGet]
        public IEnumerable<DepartmentsDto> ListDepartments()
        {
            List<Departments> Departments = db.Departments.ToList();
            List<DepartmentsDto> DepartmentsDtos = new List<DepartmentsDto>();

            Departments.ForEach(d => DepartmentsDtos.Add(new DepartmentsDto()
            {
                DepartmentID = d.DepartmentID,
                DepartmentName = d.DepartmentName,
                DepartmentRole = d.DepartmentRole
            }));

            return DepartmentsDtos;
        }
        //GET: api/

        // GET: api/DepartmentData/FindDepartments/5
        [ResponseType(typeof(Departments))]
        [HttpGet]
        public IHttpActionResult FindDepartments(int id)
        {
            Departments Departments = db.Departments.Find(id);
            DepartmentsDto DepartmentDto = new DepartmentsDto()
            {
                DepartmentID = Departments.DepartmentID,
                DepartmentName = Departments.DepartmentName,
                DepartmentRole = Departments.DepartmentRole
            };
            if (Departments == null)
            {
                return NotFound();
            }

            return Ok(DepartmentDto);
        }

        // POST: api/DepartmentData/UpdateDepartments/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDepartments(int id, Departments departments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departments.DepartmentID)
            {
                return BadRequest();
            }

            db.Entry(departments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DepartmentData/AddDepartments
        [ResponseType(typeof(Departments))]
        [HttpPost]
        public IHttpActionResult AddDepartments(Departments departments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(departments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departments.DepartmentID }, departments);
        }

        // POST: api/DepartmentData/DeleteDepartments/5
        [ResponseType(typeof(Departments))]
        [HttpPost]
        public IHttpActionResult DeleteDepartments(int id)
        {
            Departments departments = db.Departments.Find(id);
            if (departments == null)
            {
                return NotFound();
            }

            db.Departments.Remove(departments);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentsExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentID == id) > 0;
        }
    }
}