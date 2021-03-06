﻿CREATE TABLE [obn].[PRODUCTOS] (
    [ID_PRODUCTO]     INT          NOT NULL,
    [NOMBRE]          VARCHAR (50) NULL,
    [DESCRIPCION]     VARCHAR (50) NULL,
    [PRECIO]          FLOAT (53)   NULL,
    [IVA_APLICADO]    INT          NULL,
    [TIPO_MATERIAL]   VARCHAR (50) NULL,
    [CODIGO_CONTABLE] VARCHAR (50) NULL,
    [ES_TARIFA_PLANA] BIT          NULL,
    PRIMARY KEY CLUSTERED ([ID_PRODUCTO] ASC)
);

