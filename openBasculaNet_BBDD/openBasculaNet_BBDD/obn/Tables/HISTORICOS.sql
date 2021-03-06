﻿CREATE TABLE [obn].[HISTORICOS] (
    [ID_HISTORICO]           INT          IDENTITY (1, 1) NOT NULL,
    [NUM_ALBARAN]            VARCHAR (50) NULL,
    [MAT_CABINA]             VARCHAR (50) NULL,
    [MAT_REMOLQUE]           VARCHAR (50) NULL,
    [FECHA_ENTRADA]          DATETIME     NULL,
    [FECHA_SALIDA]           DATETIME     NULL,
    [PESO_ENTRADA]           INT          NULL,
    [PESO_SALIDA]            INT          NULL,
    [NETO]                   INT          NULL,
    [ORIGEN]                 VARCHAR (50) NULL,
    [DESTINO]                VARCHAR (50) NULL,
    [FORMA_PAGO]             VARCHAR (50) NULL,
    [IVA_APLICABLE]          VARCHAR (50) NULL,
    [IMPORTE_FINAL]          VARCHAR (50) NULL,
    [IMPORTE_SIN_IVA]        VARCHAR (50) NULL,
    [IMPORTE_PRODUCTO]       VARCHAR (50) NULL,
    [NOMBRE_PRODUCTO]        VARCHAR (50) NULL,
    [TIPO_MATERIAL]          VARCHAR (50) NULL,
    [COD_CONTA_PROD]         INT          NULL,
    [RAZON_SOCIAL_CLIENTE]   VARCHAR (50) NULL,
    [CIF_CLIENTE]            VARCHAR (50) NULL,
    [DIRECCION_CLIENTE]      VARCHAR (50) NULL,
    [TLF_CLIENTE]            VARCHAR (50) NULL,
    [UBICACION_CLIENTE]      VARCHAR (50) NULL,
    [COD_CONTA_CLIENTE]      VARCHAR (50) NULL,
    [RAZON_SOCIAL_PROVEEDOR] VARCHAR (50) NULL,
    [CIF_PROVEEDOR]          VARCHAR (50) NULL,
    [DIRECCION_PROVEEDOR]    VARCHAR (50) NULL,
    [TLF_PROVEEDOR]          VARCHAR (50) NULL,
    [UBICACION_PROVEEDOR]    VARCHAR (50) NULL,
    [COD_CONTA_PROVEEDOR]    VARCHAR (50) NULL,
    [RAZON_SOCIAL_AGENCIA]   VARCHAR (50) NULL,
    [CIF_AGENCIA]            VARCHAR (50) NULL,
    [DIRECCION_AGENCIA]      VARCHAR (50) NULL,
    [TLF_AGENCIA]            VARCHAR (50) NULL,
    [UBICACION_AGENCIA]      VARCHAR (50) NULL,
    [COD_CONTA_AGENCIA]      VARCHAR (50) NULL,
    [RAZON_SOCIAL_POSEEDOR]  VARCHAR (50) NULL,
    [CIF_POSEEDOR]           VARCHAR (50) NULL,
    [DIRECCION_POSEEDOR]     VARCHAR (50) NULL,
    [TLF_POSEEDOR]           VARCHAR (50) NULL,
    [UBICACION_POSEEDOR]     VARCHAR (50) NULL,
    [COD_CONTA_POSEEDOR]     VARCHAR (50) NULL,
    [NOMBRE_CONDUCTOR]       VARCHAR (50) NULL,
    [NIF_CONDUCTOR]          VARCHAR (50) NULL,
    [DIRECCION_CONDUCTOR]    VARCHAR (50) NULL,
    [TLF_CONDUCTOR]          VARCHAR (50) NULL,
    [UBICACION_CONDUCTOR]    VARCHAR (50) NULL,
    CONSTRAINT [PK__HISTORIC__9679E8E0F458B459] PRIMARY KEY CLUSTERED ([ID_HISTORICO] ASC)
);

