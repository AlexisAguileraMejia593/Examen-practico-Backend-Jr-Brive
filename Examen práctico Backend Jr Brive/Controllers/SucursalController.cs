using Microsoft.AspNetCore.Mvc;

namespace Examen_práctico_Backend_Jr_Brive.Controllers
{
    public class SucursalController : Controller
    {
        public IActionResult Index()
        {
            ML.Sucursal sucursal = new ML.Sucursal();
            ML.Result result = BL.Sucursal.GetAll();
            sucursal.Sucursales = result.Objects;
            return View(sucursal);
        }
        [HttpGet]
        public IActionResult Form(int? IdSucursal)
        {
            ML.Sucursal sucursal = new ML.Sucursal();
            if (IdSucursal != null)
            {
                ML.Result result = BL.Sucursal.GetById(IdSucursal.Value);
                if (result.Correct)
                {
                    sucursal = (ML.Sucursal)result.Object;
                }
                else
                {

                }
            }
            return View(sucursal);
        }
        [HttpPost]
        public IActionResult Form(ML.Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                if (sucursal.IdSucursal == 0)
                {
                    var result = BL.Sucursal.Add(sucursal);
                    if (result != null)
                    {
                        ViewBag.Mensaje = "Se ha ingresado correctamente la Sucursal";
                    }
                    else
                    {
                        ViewBag.Mensaje = "NO se ha ingresado correctamente la Sucursal";
                    }
                }
                else
                {
                    var result = BL.Sucursal.Update(sucursal);
                    if (result != null)
                    {
                        ViewBag.Mensaje = "Se ha actualizado correctamente la Sucursal";
                    }
                    else
                    {
                        ViewBag.Mensaje = "NO se ha actualizado correctamente la Sucursal";
                    }
                }
            }
            else
            {

            }

            return PartialView("Modal");
        }
        public ActionResult Delete(int? IdSucursal)
        {
            ML.Result result = BL.Sucursal.Delete(IdSucursal.Value);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente el registro";
            }
            else
            {
                ViewBag.Mensaje = "NO se ha eliminado correctamente el registro. Error: " + result;
            }
            return PartialView("Modal");
        }
        public IActionResult Logout(string Email, string password)
        {
            ML.Result result = BL.Usuario.GetByEmail(Email);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (password == usuario.Password)
                {
                    HttpContext.Session.SetInt32("UserID", usuario.IdUsuario);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Contraseña Incorrecta";
                    ViewBag.Correo = false;
                    return RedirectToAction("Login", "Usuario");
                }
            }
            else
            {
                ViewBag.Mensaje = "No existe la cuenta";
                ViewBag.Correo = false;
                return PartialView("Modal");
            }
        }
    }
}