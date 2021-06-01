﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarkomPos.Model.Model;
using MarkomPos.Model.ViewModel;
using MarkomPos.Repository;
using MarkomPos.Repository.Repository;

namespace MarkomPos.Web.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class OffersController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: Offers
        public ActionResult Index()
        {
            using (var offerRepository = new OfferRepository())
            {
                var offers = offerRepository.GetAll();
                return View(offers);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var offerRepository = new OfferRepository())
            {
                var offer = offerRepository.GetById(id.GetValueOrDefault(0));
                if (offer == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", offer);
            }
        }

        public ActionResult Create()
        {
            using (var offerRepository = new OfferRepository())
            {
                var offer = offerRepository.GetById(0);
                if (offer == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOffer", offer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferVm offer)
        {
            if (ModelState.IsValid)
            {
                using (var offerRepository = new OfferRepository())
                {
                    var result = offerRepository.AddUpdateOffer(offer);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var offerRepository = new OfferRepository())
            {
                var offer = offerRepository.GetById(id.GetValueOrDefault(0));
                if (offer == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOffer", offer);
            }
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
