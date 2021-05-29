﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using db_proyecto.Models;

namespace db_proyecto.Controllers
    {
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.usuario.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(usuario usuario)
        {

            if (!ModelState.IsValid)

                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario.password = UsuarioController.HashSHA1(usuario.password);
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }





        }
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public ActionResult Edit(int id)
        {

            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuario usuarioEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario user = db.usuario.Find(usuarioEdit.id);
                    user.nombre = usuarioEdit.nombre;
                    user.apellido = usuarioEdit.apellido;
                    user.fecha_nacimiento = usuarioEdit.fecha_nacimiento;
                    user.email = usuarioEdit.email;
                    user.password = usuarioEdit.password;

                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)

            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                usuario user = db.usuario.Find(id);
                return View(user);
            }
        }

        public ActionResult Delete(int id)
        {
            using (inventario2021Entities db = new inventario2021Entities())
            {
                var Usuario = db.usuario.Find(id);
                db.usuario.Remove(Usuario);
                db.SaveChanges();
                return RedirectToAction("index");

            }
        }

    }
}