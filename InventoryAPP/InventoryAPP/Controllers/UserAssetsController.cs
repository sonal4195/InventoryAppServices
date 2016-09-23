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
    public class UserAssetsController : ApiController
    {
        private InventoryControlManagementEntities1 db = new InventoryControlManagementEntities1();

        // GET: api/UserAssets
        public IQueryable<UserAsset> GetUserAssets()
        {
            return db.UserAssets;
        }
        public IQueryable<UserAsset> GetUserAssetsForEmployee(UserProfile userProfile)
        {
            return db.UserAssets.Where(x => x.EmailID == userProfile.EmailID);
        }

        // GET: api/UserAssets/5
        [ResponseType(typeof(UserAsset))]
        public IHttpActionResult GetUserAsset(int id)
        {
            UserAsset userAsset = db.UserAssets.Find(id);
            if (userAsset == null)
            {
                return NotFound();
            }

            return Ok(userAsset);
        }

        // PUT: api/UserAssets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserAsset(int id, UserAsset userAsset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAsset.UserAssetID)
            {
                return BadRequest();
            }

            db.Entry(userAsset).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAssetExists(id))
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

        // POST: api/UserAssets
        [ResponseType(typeof(UserAsset))]
        public IHttpActionResult PostUserAsset(UserAsset userAsset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserAssets.Add(userAsset);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userAsset.UserAssetID }, userAsset);
        }

        // DELETE: api/UserAssets/5
        [ResponseType(typeof(UserAsset))]
        public IHttpActionResult DeleteUserAsset(int id)
        {
            UserAsset userAsset = db.UserAssets.Find(id);
            if (userAsset == null)
            {
                return NotFound();
            }

            db.UserAssets.Remove(userAsset);
            db.SaveChanges();

            return Ok(userAsset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAssetExists(int id)
        {
            return db.UserAssets.Count(e => e.UserAssetID == id) > 0;
        }
    }
}