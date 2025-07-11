using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAcademia.Data;
using WebAcademia.Models;

namespace WebAcademia.DAL
{
    //<sumary>
    //DAL (Data Access Layer) para operaciones CRUD sobre la entidad UsuariO
    //</sumary>
    public class UsuarioDAL
    {
        /// <summary>
        /// Obtiene una lista de Instructores.
        /// </summary>
        /// <returns>Lista de instructores</returns>
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

        /// <summary>
        /// Obtiene una lista de todos los usuarios.
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public List<Usuario> GetUsuarios()
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    return contexto.Usuarios
                        .Include("Rol")
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el listado de usuarios", ex);
            }
        }//GetUsuarios

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="ID">ID del usuario</param>
        /// <returns>Usuario encontrado o null</returns>
        public Usuario GetUsuario(int ID)
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    return contexto.Usuarios
                        .Include("Rol")
                        .FirstOrDefault(u => u.UsuarioID == ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por su ID", ex);
            }
        }//GetUsuario

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="nuevo">Usuario a insertar</param>
        public void InsertUsuario(Usuario nuevo)
        {
            if (nuevo == null)
                throw new ArgumentNullException(nameof(nuevo), "El usuario no puede ser nulo o vacío");

            try
            {
                using (var contexto = new AppDBContext())
                {
                    if (contexto.Usuarios.Any(u => u.Email == nuevo.Email))
                        throw new Exception("Ya existe un usuario con ese correo electrónico.");

                    contexto.Usuarios.Add(nuevo);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el usuario", ex);
            }
        }//InsertUsuario

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="actualizado">Usuario con datos actualizados</param>
        public void UpdateUsuario(Usuario actualizado)
        {
            if (actualizado == null)
                throw new ArgumentNullException(nameof(actualizado), "El usuario no puede ser nulo o vacío");

            try
            {
                using (var contexto = new AppDBContext())
                {
                    var usuarioExistente = contexto.Usuarios.Find(actualizado.UsuarioID);

                    if (usuarioExistente == null)
                        throw new Exception($"No se encontró el usuario con ID {actualizado.UsuarioID}.");

                    usuarioExistente.Nombre = actualizado.Nombre;
                    usuarioExistente.Email = actualizado.Email;
                    usuarioExistente.PasswordHash = actualizado.PasswordHash;
                    usuarioExistente.Salt = actualizado.Salt;
                    usuarioExistente.RolID = actualizado.RolID;
                    usuarioExistente.Activo = actualizado.Activo;

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario", ex);
            }
        }//UpdateUsuario

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="ID">ID del usuario a eliminar</param>
        public void DeleteUsuario(int ID)
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    var usuario = contexto.Usuarios.Find(ID);

                    if (usuario == null)
                        throw new Exception($"No se encontró el usuario con ID {ID} para eliminar.");

                    contexto.Usuarios.Remove(usuario);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario por su ID", ex);
            }
        }//DeleteUsuario

        /// <summary>
        /// Obtiene la lista de Usuarios, aplicando filtros opcionales.
        /// </summary>
        /// <param name="nombre">Nombre parcial o completo del Usuario para filtrar (opcional)</param>
        /// <param name="activo">Estado del Usuario: true para activos, false para inactivos, null para todos</param>
        /// <param name="gmail">Gmail parcial o completo del Usuario para filtrar (opcional)</param>
        /// <returns>Lista de Usuarios filtrada</returns>
        public List<Usuario> GetUsuariosFiltrados(string nombre = null, bool? activo = null, string email = null)
        {
            try
            {
                using(var contexto = new AppDBContext())
                {
                    var query = contexto.Usuarios.Include("Rol").AsQueryable();

                    //validacion para los datos si estan vacio
                    if (!string.IsNullOrEmpty(nombre))
                        query = query.Where(u => u.Nombre.Contains(nombre));

                    if (activo.HasValue)
                        query = query.Where(u => u.Activo == activo.Value);

                    if (!string.IsNullOrEmpty(email))
                        query = query.Where(u => u.Email.Contains(email));

                    return query.ToList();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de usuarios filtrados", ex);
            }
        }//GetUsuariosFiltrados

    }//class
}