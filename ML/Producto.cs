namespace ML
{
    public class Producto
    {
        public int IdProducto {  get; set; }
        public string? Nombre { get; set; }
        public string? SDK { get; set; }
        public ML.Sucursal? Sucursal { get; set; }
        public List<object>? Productos { get; set; }
    }
}