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
