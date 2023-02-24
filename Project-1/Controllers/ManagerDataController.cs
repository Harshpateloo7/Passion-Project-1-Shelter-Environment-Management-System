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
    public class ManagerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagerData/ListManagers
        [HttpGet]
        public IEnumerable<ManagerDto> ListManagers()
        {
            List<Manager> Managers = db.Managers.ToList();
            List<ManagerDto> ManagerDtos = new List<ManagerDto>();

            Managers.ForEach(m => ManagerDtos.Add(new ManagerDto()
            {
                ManagerID = m.ManagerID,
                ManagerName = m.ManagerName,
                ManagerPosition = m.ManagerPosition,
                ManagerBranch = m.ManagerBranch
            }));

            return ManagerDtos;
        }

        // GET: api/ManagerData/FindManager/5
        [ResponseType(typeof(Manager))]
        [HttpGet]
        public IHttpActionResult FindManager(int id)
        {
            Manager Manager = db.Managers.Find(id);
            ManagerDto ManagerDto = new ManagerDto()
            {
                ManagerID = Manager.ManagerID,
                ManagerName = Manager.ManagerName,
                ManagerPosition = Manager.ManagerPosition,
                ManagerBranch = Manager.ManagerBranch

            };
            if (Manager == null)
            {
                return NotFound();
            }

            return Ok(ManagerDto);
        }

        // POST: api/ManagerData/UpdateManager/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateManager(int id, Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manager.ManagerID)
            {
                return BadRequest();
            }

            db.Entry(manager).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(id))
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

        // POST: api/ManagerData/AddManager
        [ResponseType(typeof(Manager))]
        [HttpPost]
        public IHttpActionResult AddManager(Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Managers.Add(manager);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = manager.ManagerID }, manager);
        }

        // POST: api/ManagerData/DeleteManager/5
        [ResponseType(typeof(Manager))]
        [HttpPost]
        public IHttpActionResult DeleteManager(int id)
        {
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return NotFound();
            }

            db.Managers.Remove(manager);
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

        private bool ManagerExists(int id)
        {
            return db.Managers.Count(e => e.ManagerID == id) > 0;
        }
    }
}