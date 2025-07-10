using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAcademia.Data;
using WebAcademia.Models;

namespace WebAcademia.DAL
{
    public class UsuarioDAL
    {

        public List<Usuario> ListadoIntrctores()
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    return contexto.Usuarios
                        .Where(u => u.Rol.NombreRol == "Instructor") // Aquí el campo que uses
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener instructores", ex);
            }
        }//ListadoIntrctores

    }//class
}