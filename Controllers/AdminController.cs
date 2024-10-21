using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebVize.Data;
using WebVize.Models;

namespace WebVize.webui.Controllers
{
    public class AdminController: Controller
    {
        public List<Duyuru> GetAll()
        {
            using (var db = new DuyuruContext())
            {
                var duyurular = db.duyurularım.ToList();
                return duyurular;
            }
        }
        public IActionResult List()
        {
            return View(GetAll());
        }
        [HttpGet]
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult Create(Duyuru d){
            
            using (var db = new DuyuruContext())
                {
                    Duyuru duyuru = new Duyuru
                    {
                        Baslik = d.Baslik,
                        Ozet = d.Ozet,
                        Icerik = d.Icerik
                    };
                    db.duyurularım.Add(duyuru);
                    db.SaveChanges();
                    DuyurularRepository.AddDuyuru(d);
                }

            return RedirectToAction("duyurular");
        }
        [HttpGet]
        public IActionResult Duzen(){
            return View();
        }
        [HttpPost]
        public IActionResult Duzen(Duyuru d){
            using (var db = new DuyuruContext())
                {
                    foreach(var i in Sec(d.DuyuruId)){
                        i.Baslik = d.Baslik;
                        i.Ozet = d.Ozet;
                        i.Icerik = d.Icerik;
                    }
                    db.SaveChanges();
                    return RedirectToAction("duyurular");
                }
        }

        [HttpPost]
        public IActionResult Delete(int id){
            Deletes(id);
            return View();
        }
        public List<Duyuru> Sec(int id){
            using (var db = new DuyuruContext())
            {
                var duyuru = db.duyurularım.Where(i=>i.DuyuruId == id).ToList();
                
                return duyuru;
            }
        }
        public void Deletes(int id){

             using(DuyuruContext cont = new DuyuruContext()){
                Duyuru duy = (from i in cont.duyurularım where i.DuyuruId == id select i).FirstOrDefault<Duyuru>();
                    cont.duyurularım.Remove(duy);
                    cont.SaveChanges();
             }
         }
    }
}