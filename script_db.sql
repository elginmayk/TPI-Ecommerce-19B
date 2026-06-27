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
---- =====================
---- ROPA (IdCategoria = 1)
---- =====================
--('Remera Blanca', 'Remera de algodón básica', 8000, 50, 1, 'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab', 1),
--('Campera Invierno', 'Campera abrigada impermeable', 75000, 12, 1, 'https://images.unsplash.com/photo-1520975916090-3105956dac38', 1),
--('Jeans Slim', 'Pantalón jeans moderno', 45000, 18, 1, 'https://images.unsplash.com/photo-1514996937319-344454492b37', 1);

-- CATEGORIAS
INSERT INTO CATEGORIAS (Nombre) VALUES
('Electrónica'),
('Ropa'),
('Calzado'),
('Accesorios')

-- =====================
-- INSERT PRODUCTOS CORREGIDO
-- =====================
-- PRODUCTOS
INSERT INTO PRODUCTOS (Nombre, Descripcion, Precio, Stock, IdCategoria, UrlImagen) VALUES
('Mouse Inalámbrico', 'Mouse inalámbrico ergonómico', 15000, 50, 1, 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=400'),
('Teclado Mecánico', 'Teclado mecánico RGB', 45000, 30, 1, 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=400'),
('Auriculares Bluetooth', 'Auriculares inalámbricos', 32000, 25, 1, 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400'),
('Remera Básica', 'Remera 100% algodón', 8000, 100, 2, 'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=400'),
('Buzo con Capucha', 'Buzo canguro de polar', 22000, 60, 2, 'https://images.unsplash.com/photo-1556821840-3a63f15732ce?w=400'),
('Zapatillas Urbanas', 'Zapatillas deportivas urbanas', 55000, 40, 3, 'https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400'),
('Botas de Cuero', 'Botas de cuero genuino', 95000, 20, 3, 'https://images.unsplash.com/photo-1608256246200-53e635b5b65f?w=400'),
('Mochila Urbana', 'Mochila resistente al agua', 35000, 45, 4, 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=400'),
('Reloj Deportivo', 'Reloj resistente al agua', 28000, 35, 4, 'https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=400')

-- FORMA DE PAGO
INSERT INTO FORMAS_PAGO (Nombre, Estado) VALUES
('Efectivo', 1),
('Tarjeta de débito', 1),
('Tarjeta de crédito', 1),
('Transferencia bancaria', 1),
('Mercado Pago', 1)

-- FORMA DE ENTREGA 
INSERT INTO FORMAS_ENTREGA (Nombre, Estado) VALUES
('Envío a domicilio', 1),
('Retiro en punto', 1)

-- =====================
-- USUARIO ADMINISTRADOR
-- =====================
INSERT INTO USUARIOS (Nombre, Apellido, Email, Pass, Telefono, Rol) VALUES

('Administrador', '-', 'Admin@TheDibaStore.com', 'Administrador', '1234', 1),
('Usuario', '-', 'Usuario@TheDibaStore.com', 'Usuario', '1234', 2);
