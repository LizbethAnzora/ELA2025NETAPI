CREATE DATABASE ReclutamientoDB;

USE ReclutamientoDB;

CREATE TABLE usuarios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_github BIGINT UNIQUE,
    correo_electronico VARCHAR(50) UNIQUE,
    hash_contraseña VARCHAR(50),
    rol ENUM('solicitante', 'admin') NOT NULL,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE vacantes (
    id INT PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(100) NOT NULL,
    descripcion TEXT NOT NULL,
    requisitos TEXT,
    ubicacion VARCHAR(100),
    esta_activa BOOLEAN NOT NULL DEFAULT TRUE,
    creada_por INT,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (creada_por) REFERENCES usuarios(id)
);

CREATE TABLE solicitudes (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario INT,
    id_vacante INT,
    nombre_completo VARCHAR(100) NOT NULL,
    correo_electronico VARCHAR(50) NOT NULL,
    numero_telefono VARCHAR(10),
    foto VARCHAR(255),
    campos_personalizados JSON,
    estado ENUM('pendiente', 'revisada', 'aceptada', 'rechazada') NOT NULL DEFAULT 'pendiente',
    respuesta_enviada BOOLEAN NOT NULL DEFAULT FALSE,
    fecha_envio TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (id_usuario, id_vacante),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id),
    FOREIGN KEY (id_vacante) REFERENCES vacantes(id)
);

CREATE TABLE respuestas_solicitud (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_solicitud INT,
    enviada_por INT,
    contenido_mensaje TEXT NOT NULL,
    fecha_envio TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_solicitud) REFERENCES solicitudes(id),
    FOREIGN KEY (enviada_por) REFERENCES usuarios(id)
);