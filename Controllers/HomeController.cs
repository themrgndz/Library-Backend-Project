using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebVize.ViewModels;
using WebVize.Models;
using WebVize.Data;

namespace WebVize.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            return View(Sec(id));
        }

        public List<Duyuru> GetAll()
        {
            using (var db = new DuyuruContext())
            {
                var duyurular = db.duyurularım.ToList();
                return duyurular;
            }
        }



        public List<Duyuru> Sec(int id){
            using (var db = new DuyuruContext())
            {
                var duyuru = db.duyurularım.Where(i=>i.DuyuruId == id).ToList();
                
                return duyuru;
            }
        }

        //Örnek Boş Sayfa
        public IActionResult Universitemiz(){
            return View();
        }
        public IActionResult Akademik(){
            return View();
        }public IActionResult Arastirma(){
            return View();
        }public IActionResult Ihtisaslasma(){
            return View();
        }public IActionResult DuYayinlar(){
            return View();
        }public IActionResult DuDuyurular(){
            return View();
        }public IActionResult Personel(){
            return View();
        }public IActionResult Ogrenci(){
            return View();
        }public IActionResult Hastane(){
            return View();
        }public IActionResult Teknopark(){
            return View();
        }public IActionResult HızlıErisim(){
            return View();
        }public IActionResult SanatGalerisi(){
            return View();
        }public IActionResult Mezunlar(){
            return View();
        }public IActionResult RadyoDuet(){
            return View();
        }



    }
}





