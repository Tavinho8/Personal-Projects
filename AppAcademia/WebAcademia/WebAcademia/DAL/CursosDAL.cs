using System;
using System.Collections.Generic;
using System.Linq;
using WebAcademia.Models;
using WebAcademia.Data;
using WebAcademia.Models.DTOs;

namespace WebAcademia.DAL
{
    /// <summary>
    /// DAL (Data Access Layer) para operaciones CRUD sobre la entidad Curso.
    /// </summary>
    public class CursosDAL
    {
        /// <summary>
        /// Obtiene una lista de cursos junto con datos del instructor.
        /// </summary>
        /// <returns>Lista de CursoDTO</returns>
        public List<CursoDTO> ObtenerCursos()
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    var query = from c in contexto.Cursos
                                join u in contexto.Usuarios on c.InstructorID equals u.UsuarioID
                                select new CursoDTO
                                {
                                    CursoID = c.CursoID,
                                    Nombre = c.Nombre,
                                    Descripcion = c.Descripcion,
                                    InstructorNombre = u.Nombre,
                                    CupoMaximo = c.CupoMaximo,
                                    Activo = c.Activo
                                };

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de cursos.", ex);
            }
        }

        /// <summary>
        /// Obtiene un curso por su ID.
        /// </summary>
        /// <param name="ID">ID del curso</param>
        /// <returns>Curso encontrado o null</returns>
        public Curso ObtenerPorID(int ID)
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    return contexto.Cursos.Find(ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el curso con ID {ID}.", ex);
            }
        }

        /// <summary>
        /// Inserta un nuevo curso en la base de datos.
        /// </summary>
        /// <param name="nuevo">Curso a insertar</param>
        public void InsertarCurso(Curso nuevo)
        {
            if (nuevo == null)
                throw new ArgumentNullException(nameof(nuevo), "El curso no puede ser nulo.");

            // Validación básica
            if (string.IsNullOrWhiteSpace(nuevo.Nombre))
                throw new ArgumentException("El nombre del curso es obligatorio.");

            if (nuevo.InstructorID <= 0)
                throw new ArgumentException("Debe indicar un InstructorID válido.");

            try
            {
                using (var contexto = new AppDBContext())
                {
                    contexto.Cursos.Add(nuevo);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el curso.", ex);
            }
        }

        /// <summary>
        /// Actualiza un curso existente.
        /// </summary>
        /// <param name="actualizado">Curso con datos actualizados</param>
        public void ActualizarCurso(Curso actualizado)
        {
            if (actualizado == null)
                throw new ArgumentNullException(nameof(actualizado), "El curso no puede ser nulo.");

            try
            {
                using (var contexto = new AppDBContext())
                {
                    var cursoExistente = contexto.Cursos.Find(actualizado.CursoID);

                    if (cursoExistente == null)
                        throw new Exception($"No se encontró el curso con ID {actualizado.CursoID}.");

                    cursoExistente.Nombre = actualizado.Nombre;
                    cursoExistente.Descripcion = actualizado.Descripcion;
                    cursoExistente.InstructorID = actualizado.InstructorID;
                    cursoExistente.CupoMaximo = actualizado.CupoMaximo;
                    cursoExistente.Activo = actualizado.Activo;

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el curso.", ex);
            }
        }

        /// <summary>
        /// Elimina un curso por su ID.
        /// </summary>
        /// <param name="id">ID del curso a eliminar</param>
        public void EliminarCurso(int id)
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    var curso = contexto.Cursos.Find(id);

                    if (curso == null)
                        throw new Exception($"No se encontró el curso con ID {id} para eliminar.");

                    contexto.Cursos.Remove(curso);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el curso.", ex);
            }
        }


        /// <summary>
        /// Obtiene la lista de cursos con los datos del instructor, aplicando filtros opcionales.
        /// </summary>
        /// <param name="nombre">Nombre parcial o completo del curso para filtrar (opcional)</param>
        /// <param name="activo">Estado del curso: true para activos, false para inactivos, null para todos</param>
        /// <param name="instructorId">ID del instructor para filtrar (opcional)</param>
        /// <returns>Lista de CursoDTO filtrada</returns>
        public List<CursoDTO> ObtenerCursosFiltrados(string nombre = null, bool? activo = null, int? instructorId = null)
        {
            try
            {
                using (var contexto = new AppDBContext())
                {
                    // Query inicial, uniendo Curso con Usuario (instructor)
                    var query = from c in contexto.Cursos
                                join u in contexto.Usuarios on c.InstructorID equals u.UsuarioID
                                select new { Curso = c, Usuario = u };

                    // Aplicar filtros en la entidad Curso
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        query = query.Where(x => x.Curso.Nombre.Contains(nombre));
                    }

                    if (activo.HasValue)
                    {
                        query = query.Where(x => x.Curso.Activo == activo.Value);
                    }

                    if (instructorId.HasValue && instructorId.Value > 0)
                    {
                        query = query.Where(x => x.Curso.InstructorID == instructorId.Value);
                    }

                    // Proyectar a DTO después de filtrar
                    return query.Select(x => new CursoDTO
                    {
                        CursoID = x.Curso.CursoID,
                        Nombre = x.Curso.Nombre,
                        Descripcion = x.Curso.Descripcion,
                        InstructorNombre = x.Usuario.Nombre,
                        CupoMaximo = x.Curso.CupoMaximo,
                        Activo = x.Curso.Activo
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de cursos filtrada.", ex);
            }
        }


        public List<CursoDTO> ObtenerUltimosCursos(int cantidad)
        {
            using (var db = new AppDBContext())
            {
                var cursos = db.Cursos
                    .OrderByDescending(c => c.CursoID)
                    .Take(cantidad)
                    .Select(c => new CursoDTO
                    {
                        CursoID = c.CursoID,
                        Nombre = c.Nombre,
                        Descripcion = c.Descripcion,
                        InstructorNombre = c.Instructor.Nombre,
                        CupoMaximo = c.CupoMaximo,
                        Activo = c.Activo
                    })
                    .ToList();

                return cursos;
            }
        }//ObtenerUltimosCursos

    }//class
}
