USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TPI_ECOMMERCE')
BEGIN
    CREATE DATABASE TPI_ECOMMERCE;
END
GO

USE TPI_ECOMMERCE;
GO

-- =========================
-- USUARIOS
-- =========================
IF OBJECT_ID('USUARIOS', 'U') IS NULL
BEGIN
    CREATE TABLE USUARIOS(
        IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Apellido VARCHAR(50) NOT NULL,
        Email VARCHAR(70) NOT NULL,
        Pass VARCHAR(50) NOT NULL,
        Telefono VARCHAR(20) NOT NULL,
        Rol SMALLINT NOT NULL
    );
END
GO

-- =========================
-- DIRECCIONES
-- =========================
IF OBJECT_ID('DIRECCIONES', 'U') IS NULL
BEGIN
    CREATE TABLE DIRECCIONES(
        IdDireccion INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        Calle VARCHAR(70) NOT NULL,
        Numero VARCHAR(10) NOT NULL,
        Localidad VARCHAR(50) NOT NULL,
        CodigoPostal VARCHAR(10) NOT NULL,
        Observaciones VARCHAR(100) NULL,

        CONSTRAINT FK_DIRECCIONES_USUARIOS
        FOREIGN KEY (IdUsuario)
        REFERENCES USUARIOS(IdUsuario)
    );
END
GO

-- =========================
-- CATEGORIAS
-- =========================
IF OBJECT_ID('CATEGORIAS', 'U') IS NULL
BEGIN
    CREATE TABLE CATEGORIAS(
        IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL
    );
END
GO

-- =========================
-- PRODUCTOS
-- =========================
IF OBJECT_ID('PRODUCTOS', 'U') IS NULL
BEGIN
    CREATE TABLE PRODUCTOS(
        IdProducto INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Descripcion VARCHAR(200),
        Precio DECIMAL(10,2) NOT NULL DEFAULT 0,
        Stock INT NOT NULL DEFAULT 0,
        Estado BIT NOT NULL DEFAULT 1,
        UrlImagen VARCHAR(255),
        IdCategoria INT NOT NULL,

        CONSTRAINT FK_PRODUCTOS_CATEGORIAS
        FOREIGN KEY (IdCategoria)
        REFERENCES CATEGORIAS(IdCategoria)
    );
END
GO

-- =========================
-- FORMAS DE PAGO
-- =========================
IF OBJECT_ID('FORMAS_PAGO', 'U') IS NULL
BEGIN
    CREATE TABLE FORMAS_PAGO(
        IdFormaPago INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Estado VARCHAR(50) NOT NULL DEFAULT 'Falta pago'
    );
END
GO

-- =========================
-- FORMAS DE ENTREGA
-- =========================
IF OBJECT_ID('FORMAS_ENTREGA', 'U') IS NULL
BEGIN
    CREATE TABLE FORMAS_ENTREGA(
        IdFormaEntrega INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Estado VARCHAR(50) NOT NULL DEFAULT 'Preparando Pedido'
    );
END
GO

-- =========================
-- PEDIDOS
-- =========================
IF OBJECT_ID('PEDIDOS', 'U') IS NULL
BEGIN
    CREATE TABLE PEDIDOS(
        IdPedido INT IDENTITY(1,1) PRIMARY KEY,
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        Total DECIMAL(10,2) NOT NULL,
        Estado VARCHAR(30) NOT NULL,

        IdUsuario INT NOT NULL,
        IdFormaPago INT NOT NULL,
        IdFormaEntrega INT NOT NULL,
        IdDireccion INT NULL,

        CONSTRAINT FK_PEDIDOS_USUARIOS
        FOREIGN KEY (IdUsuario)
        REFERENCES USUARIOS(IdUsuario),

        CONSTRAINT FK_PEDIDOS_FORMAS_PAGO
        FOREIGN KEY (IdFormaPago)
        REFERENCES FORMAS_PAGO(IdFormaPago),

        CONSTRAINT FK_PEDIDOS_FORMAS_ENTREGA
        FOREIGN KEY (IdFormaEntrega)
        REFERENCES FORMAS_ENTREGA(IdFormaEntrega),

        CONSTRAINT FK_PEDIDOS_DIRECCIONES
        FOREIGN KEY (IdDireccion)
        REFERENCES DIRECCIONES(IdDireccion)
    );
END
GO

-- =========================
-- DETALLE PEDIDOS
-- =========================
IF OBJECT_ID('DETALLE_PEDIDOS', 'U') IS NULL
BEGIN
    CREATE TABLE DETALLE_PEDIDOS(
        IdDetallePedido INT IDENTITY(1,1) PRIMARY KEY,
        IdPedido INT NOT NULL,
        IdProducto INT NOT NULL,
        Cantidad INT NOT NULL,

        CONSTRAINT FK_DETALLE_PEDIDOS_PEDIDOS
        FOREIGN KEY (IdPedido)
        REFERENCES PEDIDOS(IdPedido),

        CONSTRAINT FK_DETALLE_PEDIDOS_PRODUCTOS
        FOREIGN KEY (IdProducto)
        REFERENCES PRODUCTOS(IdProducto)
    );
END
GO

-- =========================
-- OBSERVACIONES PEDIDOS
-- =========================
IF OBJECT_ID('OBSERVACION_PEDIDOS', 'U') IS NULL
BEGIN
    CREATE TABLE OBSERVACION_PEDIDOS(
        IdObservacion INT IDENTITY(1,1) PRIMARY KEY,
        IdPedido INT NOT NULL,
        Descripcion VARCHAR(200),
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_OBSERVACIONES_PEDIDOS
        FOREIGN KEY (IdPedido)
        REFERENCES PEDIDOS(IdPedido)
    );
END
GO
INSERT INTO CATEGORIAS (Nombre)
VALUES 
('Ropa'),
('Calzado'),
('Accesorios');

GO

---- =====================
---- ROPA (IdCategoria = 1)
---- =====================
--('Remera Blanca', 'Remera de algodón básica', 8000, 50, 1, 'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab', 1),
--('Campera Invierno', 'Campera abrigada impermeable', 75000, 12, 1, 'https://images.unsplash.com/photo-1520975916090-3105956dac38', 1),
--('Jeans Slim', 'Pantalón jeans moderno', 45000, 18, 1, 'https://images.unsplash.com/photo-1514996937319-344454492b37', 1);

-- =====================
-- INSERT PRODUCTOS CORREGIDO
-- =====================
INSERT INTO PRODUCTOS (Nombre, Descripcion, Precio, Stock, Estado, UrlImagen, IdCategoria) VALUES

-- =====================
-- CALZADO (IdCategoria = 2)
-- =====================
('Zapatillas Nike Air', 'Zapatillas deportivas Nike originales', 120000, 15, 1, 'https://images.unsplash.com/photo-1542291026-7eec264c27ff', 2),
('Adidas Running', 'Zapatillas para correr muy cómodas', 95000, 20, 1, 'https://images.unsplash.com/photo-1519744792095-2f2205e87b6f', 2),
('Botas Cuero', 'Botas de cuero elegantes', 110000, 10, 1, 'https://images.unsplash.com/photo-1600185365483-26d7a4cc7519', 2),

-- =====================
-- ACCESORIOS (IdCategoria = 3)
-- =====================
('Mochila Urbana', 'Mochila resistente para uso diario', 35000, 25, 1, 'https://images.unsplash.com/photo-1503342217505-b0a15ec3261c', 3),
('Reloj Deportivo', 'Reloj digital resistente al agua', 22000, 30, 1, 'https://images.unsplash.com/photo-1518546305927-5a555bb7020d', 3),
('Gorra Negra', 'Gorra clásica ajustable', 9000, 40, 1, 'https://images.unsplash.com/photo-1521369909029-2afed882baee', 3);

-- =====================
-- USUARIO ADMINISTRADOR
-- =====================
INSERT INTO USUARIOS (Nombre, Apellido, Email, Pass, Telefono, Rol) VALUES

('Administrador', '-', 'Admin@TheDibaStore.com', 'Administrador', '1234', 1),
('Usuario', '-', 'Usuario@TheDibaStore.com', 'Usuario', '1234', 2);