using Microsoft.AspNetCore.Mvc;

namespace Examen_práctico_Backend_Jr_Brive.Controllers
{
    public class Bitacora : Controller
    {
        public IActionResult Index()
        {
            ML.Bitacora bitacora = new ML.Bitacora();
            ML.Result result = BL.Bitacora.GetAll();
            bitacora.Bitacoras = result.Objects;
            return View(bitacora);
        }
        [HttpGet]
        public IActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Sucursal = new ML.Sucursal();
            ML.Result result1 = BL.Sucursal.GetAll();
            producto.Sucursal.Sucursales = result1.Objects;
            if (IdProducto != null)
            {
                ML.Result result = BL.Bitacora.GetById(IdProducto.Value);
                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    producto.Sucursal.Sucursales = result1.Objects;
                }
                else
                {

                }
            }
            return View(producto);
        }
        [HttpPost]
        public IActionResult Form(ML.Producto producto, int UserID)
        {
            if (ModelState.IsValid)
            {
                if (producto.IdProducto == 0)
                {
                    var result = BL.Bitacora.Add(producto, UserID);
                    if (result != null)
                    {
                        ViewBag.Mensaje = "Se ha ingresado correctamente el Producto";
                    }
                    else
                    {
                        ViewBag.Mensaje = "NO se ha ingresado correctamente el Producto";
                    }
                }
                else
                {
                    var result = BL.Bitacora.Update(producto);
                    if (result != null)
                    {
                        ViewBag.Mensaje = "Se ha actualizado correctamente el Producto";
                    }
                    else
                    {
                        ViewBag.Mensaje = "NO se ha actualizado correctamente el Producto";
                    }
                }
            }
            else
            {
                ML.Result result = BL.Sucursal.GetAll();
                producto.Sucursal.Sucursales = result.Objects;
            }

            return PartialView("Modal");
        }
        public ActionResult Delete(int? IdProducto)
        {
            ML.Result result = BL.Bitacora.Delete(IdProducto.Value);
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
    }
}