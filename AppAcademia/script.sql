USE AcademiaDB;

-- Primero eliminar las tablas hijas que tienen FK
DROP TABLE IF EXISTS Inscripciones;
DROP TABLE IF EXISTS ContenidoCurso;

-- Luego eliminar las tablas padres
DROP TABLE IF EXISTS Cursos;
DROP TABLE IF EXISTS Usuarios;

-- Crear tabla Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    Password NVARCHAR(100),
    Rol NVARCHAR(50) -- Admin, Instructor, Estudiante
);

-- Crear tabla Cursos
CREATE TABLE Cursos (
    CursoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Descripcion NVARCHAR(MAX),
    InstructorID INT,
    CupoMaximo INT NOT NULL DEFAULT 50,
    FOREIGN KEY (InstructorID) REFERENCES Usuarios(UsuarioID)
);

-- Crear tabla Inscripciones
CREATE TABLE Inscripciones (
    InscripcionID INT PRIMARY KEY IDENTITY(1,1),
    CursoID INT,
    EstudianteID INT,
    FechaInscripcion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CursoID) REFERENCES Cursos(CursoID),
    FOREIGN KEY (EstudianteID) REFERENCES Usuarios(UsuarioID)
);

-- Crear tabla ContenidoCurso
CREATE TABLE ContenidoCurso (
    ContenidoID INT PRIMARY KEY IDENTITY(1,1),
    CursoID INT,
    Tipo NVARCHAR(50), -- Archivo, Link
    URL NVARCHAR(MAX),
    Descripcion NVARCHAR(255),
    FOREIGN KEY (CursoID) REFERENCES Cursos(CursoID)
);

-- Insertar datos de prueba
INSERT INTO Usuarios (Nombre, Email, Password, Rol)
VALUES 
('Admin Principal', 'admin@academia.com', 'admin123', 'Admin'),
('Instructor A', 'instructorA@academia.com', 'pass123', 'Instructor'),
('Instructor B', 'instructorB@academia.com', 'pass456', 'Instructor'),
('Estudiante X', 'estudianteX@academia.com', '123456', 'Estudiante'),
('Estudiante Y', 'estudianteY@academia.com', 'abcdef', 'Estudiante');

-- Insertar Cursos (ahora sí existe Usuarios)
INSERT INTO Cursos (Nombre, Descripcion, InstructorID, CupoMaximo)
VALUES 
('Introducción a C#', 'Curso básico de programación en C# y fundamentos de .NET.', 2, 30),
('Desarrollo Web con ASP.NET', 'Aprende a crear aplicaciones web usando ASP.NET Web Forms y MVC.', 3, 25),
('SQL Server para Desarrolladores', 'Consulta, modelado y administración básica de bases de datos en SQL Server.', 2, 40),
('Fundamentos de JavaScript', 'Desde cero hasta lógica intermedia con JS moderno.', 3, 50),
('Diseño de Interfaces con Bootstrap', 'Aprende a crear interfaces modernas y responsivas.', 2, 35);


-- Inscripciones de Estudiante X (ID 4) y Estudiante Y (ID 5)
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (1, 4);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (2, 4);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (3, 4);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (4, 4);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (5, 4);

INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (1, 5);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (2, 5);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (3, 5);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (4, 5);
INSERT INTO Inscripciones (CursoID, EstudianteID) VALUES (5, 5);
