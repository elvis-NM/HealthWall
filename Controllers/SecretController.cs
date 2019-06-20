using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using secrets.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace secrets.Controllers
{
    [Route("secrets")]
    public class SecretController : Controller
    {
        private int? SessionUserId
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }
        private SecretDashboard DashboardModel
        {
            get
            {

                return new SecretDashboard()
                {
                    RecentSecrets = dbContext.Secrets
                    .Include(s => s.Likes)
                    .OrderByDescending(s => s.CreatedAt)
                    .ToList(),
                    CurrentUser = dbContext.Users.FirstOrDefault(u => u.UserId == SessionUserId)
                };
            }
        }
        private secretsContext dbContext;

        public SecretController(secretsContext context)
        {
            dbContext = context;
        }
        // localhost:5000/Secret/
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {

            return View(DashboardModel);

        }



        [HttpPost("create")]
        public IActionResult Create(Secret newSecret)
        {
            if (ModelState.IsValid)
            {
                dbContext.Secrets.Add(newSecret);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", DashboardModel);
        }
        [HttpGet("delete/{secretId}")]
        public IActionResult Delete(int secretId)
        {
            Secret toDelete = dbContext.Secrets.FirstOrDefault(s => s.SecretId == secretId && s.UserId == SessionUserId);
            if (toDelete != null)
            {
                dbContext.Secrets.Remove(toDelete);
                dbContext.SaveChanges();
            }

            return RedirectToAction("index");
        }
        [HttpGet("like/{secretId}")]
        public IActionResult Like(int secretId)
        {

            Like newLike = new Like();
            newLike.SecretId = secretId;
            newLike.UserId = (int)SessionUserId;
            dbContext.Likes.Add(newLike);
            dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet("unlike/{secretId}")]
        public IActionResult UnLike(int secretId)
        {

            Like toDelete = dbContext.Likes.FirstOrDefault(l => l.SecretId == secretId && l.UserId == SessionUserId);
            if (toDelete != null)
            {
                dbContext.Likes.Remove(toDelete);
                dbContext.SaveChanges();

            }

            return RedirectToAction("Index");

        }
        [HttpGet("edit/{secretId}")]
        public IActionResult Edit(int secretId)
        {
            // query for a post
            Secret secret = dbContext.Secrets.FirstOrDefault(s => s.SecretId == secretId);
            return View("EditSecret", secret);
        }

        [HttpPost("update/{secretId}")]
        public IActionResult Update(Secret secret, int secretId)
        {
            if (ModelState.IsValid)
            {
                // query for a post to update
                Secret toUpdate = dbContext.Secrets.FirstOrDefault(s => s.SecretId == secretId);
                toUpdate.Content = secret.Content;
                toUpdate.CreatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("EditSecret", secret);


        }


    }
}