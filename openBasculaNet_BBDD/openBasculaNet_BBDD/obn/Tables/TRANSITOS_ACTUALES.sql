CREATE TABLE [obn].[TRANSITO_ACTUALES] (
    [ID_TRANSITO]      INT          NOT NULL,
    [NUM_ALBARAN]      VARCHAR (50) NULL,
    [MAT_CABINA]       VARCHAR (50) NULL,
    [MAT_REMOLQUE]     VARCHAR (50) NULL,
    [FECHA_ENTRADA]    DATE         NULL,
    [FECHA_SALIDA]     DATE         NULL,
    [BRUTO]            INT          NULL,
    [TARA]             INT          NULL,
    [NETO]             INT          NULL,
    [IVA_APLICABLE]    VARCHAR (50) NULL,
    [IMPORTE_FINAL]    VARCHAR (50) NULL,
    [IMPORTE_PRODUCTO] VARCHAR (50) NULL,
    [ID_PRODUCTO]      INT          NULL,
    [ID_CLIENTE]       INT          NULL,
    [ID_PROVEEDOR]     INT          NULL,
    [ID_AGENCIA]       INT          NULL,
    [ID_POSEEDOR]      INT          NULL,
    [ID_CONDUCTOR]     INT          NULL,
    [ORIGEN]           VARCHAR (50) NULL,
    [DESTINO]          VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID_TRANSITO] ASC)
);

