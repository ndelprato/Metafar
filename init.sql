USE master;
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Metafar')
BEGIN
    CREATE DATABASE Metafar;
END;
GO

USE Metafar;
GO

-- Crear tabla de operaciones
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operaciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Operacion] [nvarchar](max) NOT NULL,
	[MontoInicial] [nvarchar](max) NOT NULL,
	[MontoFinal] [nvarchar](max) NOT NULL,
	[Tarjeta] [nvarchar](max) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Operaciones] ADD  CONSTRAINT [PK_Operaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

-- Crear Saldo

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Saldo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tarjeta] [nvarchar](max) NOT NULL,
	[Monto] [nvarchar](max) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Saldo] ADD  CONSTRAINT [PK_Saldo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--Crear tabla Usuario

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tarjeta] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Intentos] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



-- Insertar datos de Usuario

INSERT INTO Usuario (Tarjeta,Password,Intentos) VALUES ('123','21232f297a57a5a743894a0e4a801fc3',3);
INSERT INTO Usuario (Tarjeta,Password,Intentos) VALUES ('1234','21232f297a57a5a743894a0e4a801fc3',3);
INSERT INTO Usuario (Tarjeta,Password,Intentos) VALUES ('12345','21232f297a57a5a743894a0e4a801fc3',3);
INSERT INTO Usuario (Tarjeta,Password,Intentos) VALUES ('123456','21232f297a57a5a743894a0e4a801fc3',3);
GO

-- Insertar datos de Operaciones

INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','15000','14500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','14500','14000','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','14000','13500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','13500','12000','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','12000','11500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','11500','11000','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','11000','10500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','10500','10000','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','10000','9500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','9500','8000','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','8000','7500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','7500','6000','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','6000','5500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','5500','4000','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','4000','3500','123456',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','4000','3500','123',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','4000','3500','1234',SYSDATETIME());
INSERT INTO Operaciones (Operacion,MontoInicial,MontoFinal,Tarjeta,Fecha) VALUES ('Extracción','4000','3500','12345',SYSDATETIME());
GO


-- Insertar datos de saldos

INSERT INTO Saldo (Tarjeta,Monto,Fecha) VALUES ('123','3500',SYSDATETIME());
INSERT INTO Saldo (Tarjeta,Monto,Fecha) VALUES ('1234','3500',SYSDATETIME());
INSERT INTO Saldo (Tarjeta,Monto,Fecha) VALUES ('12345','3500',SYSDATETIME());
INSERT INTO Saldo (Tarjeta,Monto,Fecha) VALUES ('123456','3500',SYSDATETIME());
GO