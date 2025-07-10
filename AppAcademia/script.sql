USE AcademiaDB;

-- ============================================
-- 1️⃣ Tabla de Roles
-- ============================================

CREATE TABLE Roles (
    RolID INT PRIMARY KEY IDENTITY(1,1),
    NombreRol NVARCHAR(50) NOT NULL UNIQUE
);

-- Insertar roles básicos
INSERT INTO Roles (NombreRol) VALUES ('Admin'), ('Instructor'), ('Estudiante');

-- ============================================
-- 2️⃣ Tabla de Usuarios
-- ============================================

CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Salt NVARCHAR(50) NOT NULL,
    RolID INT NOT NULL,
    Activo BIT DEFAULT 1,
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);

INSERT INTO Usuarios (Nombre, Email, PasswordHash, Salt, RolID)
VALUES 
('Admin Principal', 'admin@miapp.com', 'HASH_ADMIN', 'SALT_ADMIN', 1),

('Instructor 1', 'instructor1@miapp.com', 'HASH_INSTRUCTOR1', 'SALT_INSTRUCTOR1', 2),
('Instructor 2', 'instructor2@miapp.com', 'HASH_INSTRUCTOR2', 'SALT_INSTRUCTOR2', 2),

('Estudiante 1', 'estudiante1@miapp.com', 'HASH_ESTUDIANTE1', 'SALT_ESTUDIANTE1', 3),
('Estudiante 2', 'estudiante2@miapp.com', 'HASH_ESTUDIANTE2', 'SALT_ESTUDIANTE2', 3),
('Estudiante 3', 'estudiante3@miapp.com', 'HASH_ESTUDIANTE3', 'SALT_ESTUDIANTE3', 3);

SELECT * FROM Usuarios;

-- ============================================
-- 3️⃣ Tabla de Cursos
-- ============================================

CREATE TABLE Cursos (
    CursoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    InstructorID INT NOT NULL,
    CupoMaximo INT NOT NULL DEFAULT 50,
    Activo BIT DEFAULT 1,
    FOREIGN KEY (InstructorID) REFERENCES Usuarios(UsuarioID)
);



SELECT * FROM Cursos;

-- ============================================
-- 4️⃣ Tabla de Inscripciones
-- ============================================

CREATE TABLE Inscripciones (
    InscripcionID INT PRIMARY KEY IDENTITY(1,1),
    CursoID INT NOT NULL,
    EstudianteID INT NOT NULL,
    FechaInscripcion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CursoID) REFERENCES Cursos(CursoID),
    FOREIGN KEY (EstudianteID) REFERENCES Usuarios(UsuarioID)
);

-- ============================================
-- 5️⃣ Tabla de ContenidoCurso
-- ============================================

CREATE TABLE ContenidoCurso (
    ContenidoID INT PRIMARY KEY IDENTITY(1,1),
    CursoID INT NOT NULL,
    Tipo NVARCHAR(50), -- Archivo, Link
    URL NVARCHAR(MAX),
    Descripcion NVARCHAR(255),
    FOREIGN KEY (CursoID) REFERENCES Cursos(CursoID)
);

-- ============================================
-- 6️⃣ Tabla de Logs (opcional, para auditoría)
-- ============================================

CREATE TABLE Logs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT,
    Accion NVARCHAR(100),
    Fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);
