using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Models;
using WebApplicationTest.Repository;

namespace WebApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitofwork;

        public HomeController()
        {
            unitofwork = new UnitOfWork();
        }
        
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index(int page = 1)
        {
            int Pagesize = 3;
            var players = await unitofwork.Players.GetAllPage(page,Pagesize);
            PageInfo pageInfo = new PageInfo() { PageNumber = page, PageSize = Pagesize, TotalItems = unitofwork.Players.GetCountPlayer()  };
            IndexViewModel imv = new IndexViewModel() { PageInfo = pageInfo, Players = players };
            return View(imv);
        }

        [HttpGet]
        public async Task<ActionResult> ListTeams()
        {
            var Teams = await unitofwork.Teams.GetAll();
            return View(Teams);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            SelectList teams = new SelectList(await unitofwork.Teams.GetAll(), "Id", "Name");
            ViewBag.Teams = teams;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Player player)
        {
            SelectList teams = new SelectList(await unitofwork.Teams.GetAll(), "Id", "Name");
            ViewBag.Teams = teams;

            if (ModelState.IsValid)
            {
                unitofwork.Players.Create(player);
                unitofwork.Save();

                return RedirectToAction("Index");
            }

            return View(player);
        }

        

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            Player player = await unitofwork.Players.Get(id);

            if (player != null)
            {
                SelectList teams = new SelectList(await unitofwork.Teams.GetAll(), "Id", "Name");
                ViewBag.Teams = teams;

                return View(player);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Player player)
        {
            SelectList teams = new SelectList(await unitofwork.Teams.GetAll(), "Id", "Name");
            ViewBag.Teams = teams;

            if (ModelState.IsValid == true)
            {
                unitofwork.Players.Update(player);

                unitofwork.Save();

                return RedirectToAction("Index");
            }

            return View(player);
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            Player player = await unitofwork.Players.Get(id);

            if(player == null)
            {
                return HttpNotFound();
            }

            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
           
            unitofwork.Players.Delete(id);
            unitofwork.Save();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult CreateTeams()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTeams(Team team)
        {
            if(ModelState.IsValid == true)
            {
                unitofwork.Teams.Create(team);
                unitofwork.Save();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        [HttpGet]
        public async Task<ActionResult> EditTeam(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Team team = await unitofwork.Teams.Get(id);

            if (team != null)
            {
                return View(team);
            }
            return RedirectToAction("ListTeams");
        }

        [HttpPost]
        public ActionResult EditTeam(Team team)
        {
            if(team == null)
            {
                return HttpNotFound();
            }

            if(ModelState.IsValid == true)
            {
                unitofwork.Teams.Update(team);

                unitofwork.Save();

                return RedirectToAction("ListTeams");
            }

            return View(team);
        }


        [HttpGet]
        public async Task<ActionResult> DeleteTeam(int? id)
        {
            Team player = await unitofwork.Teams.Get(id);

            if (player == null)
            {
                return HttpNotFound();
            }

            return View(player);
        }

        [HttpPost, ActionName("DeleteTeam")]
        public ActionResult DeleteConfirmedTeam(int? id)
        {

            unitofwork.Teams.Delete(id);
            unitofwork.Save();
            return RedirectToAction("ListTeams");

        }
    }
}