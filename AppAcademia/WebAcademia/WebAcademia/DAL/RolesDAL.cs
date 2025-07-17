using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAcademia.Data;
using WebAcademia.Models;

namespace WebAcademia.DAL
{
    public class RolesDAL
    {

        /// <summary>
        /// Obtiene una lista de usuarios.
        /// </summary>
        /// <returns>Lista de Usuarios</returns>
        public List<Rol> getRoles()
        {
            try
            {
                using(var contexto = new AppDBContext())
                {
                    return contexto.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles", ex);
            }
        }//getRoles


    }//class
}