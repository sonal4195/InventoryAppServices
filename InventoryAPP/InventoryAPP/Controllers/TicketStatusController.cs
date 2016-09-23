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
using InventoryAPP.Models;

namespace InventoryAPP.Controllers
{
    public class TicketStatusController : ApiController
    {
        private InventoryControlManagementEntities1 db = new InventoryControlManagementEntities1();

        // GET: api/TicketStatus
        public IQueryable<TicketStatu> GetTicketStatus()
        {
            return db.TicketStatus;
        }

        // GET: api/TicketStatus/5
        [ResponseType(typeof(TicketStatu))]
        public IHttpActionResult GetTicketStatu(int id)
        {
            TicketStatu ticketStatu = db.TicketStatus.Find(id);
            if (ticketStatu == null)
            {
                return NotFound();
            }

            return Ok(ticketStatu);
        }

        // PUT: api/TicketStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicketStatu(int id, TicketStatu ticketStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticketStatu.StatusID)
            {
                return BadRequest();
            }

            db.Entry(ticketStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketStatuExists(id))
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

        // POST: api/TicketStatus
        [ResponseType(typeof(TicketStatu))]
        public IHttpActionResult PostTicketStatu(TicketStatu ticketStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TicketStatus.Add(ticketStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ticketStatu.StatusID }, ticketStatu);
        }

        // DELETE: api/TicketStatus/5
        [ResponseType(typeof(TicketStatu))]
        public IHttpActionResult DeleteTicketStatu(int id)
        {
            TicketStatu ticketStatu = db.TicketStatus.Find(id);
            if (ticketStatu == null)
            {
                return NotFound();
            }

            db.TicketStatus.Remove(ticketStatu);
            db.SaveChanges();

            return Ok(ticketStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketStatuExists(int id)
        {
            return db.TicketStatus.Count(e => e.StatusID == id) > 0;
        }
    }
}