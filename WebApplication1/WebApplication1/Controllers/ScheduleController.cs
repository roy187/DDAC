using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            List<Schedule> schedules = new List<Schedule>();

            using (Model1Container container = new Model1Container())
            {
                schedules = container.Schedules.ToList();
            }

            return View(schedules);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(addScheduleViewModel asvm)
        {
            var schedule = new Schedule();

            using(Model1Container container = new Model1Container())
            {
                schedule.departure = asvm.departure;
                schedule.destination = asvm.destination;
                schedule.weight = asvm.weight;
                schedule.deliver_date = asvm.deliver_date;

                container.Schedules.Add(schedule);
                container.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var schedule = new Schedule();

            using(Model1Container container = new Model1Container())
            {
                schedule = container.Schedules.Where(a => a.Id == id).FirstOrDefault();
            }

            return View(schedule);
        }

        [HttpPost]
        public ActionResult Edit(Schedule obj)
        {
            var schedule = new Schedule();

            using(Model1Container container = new Model1Container())
            {
                schedule = container.Schedules.Single(a => a.Id == obj.Id);
                schedule.departure = obj.departure;
                schedule.destination = obj.destination;
                schedule.deliver_date = obj.deliver_date;
                schedule.weight = obj.weight;

                container.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var schedule = new Schedule();

            using(Model1Container container = new Model1Container())
            {
                schedule = container.Schedules.Where(a => a.Id == id).FirstOrDefault();
                container.Schedules.Remove(schedule);
                container.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}