-- Stored procedure: PA_OBTENER_VENTA_COMPLETA
-- Devuelve la cabecera y detalle de una venta
CREATE PROCEDURE PA_OBTENER_VENTA_COMPLETA
    @numVenta VARCHAR(10)
AS
BEGIN
    -- Primer result set: cabecera y cliente
    SELECT v.numVenta, v.fecha, v.total,
           c.idCli, c.nomCli, c.apeCli, c.dni, c.dirCli
    FROM Ventas v
        JOIN Clientes c ON v.idCli = c.idCli
    WHERE v.numVenta = @numVenta;

    -- Segundo result set: detalle
    SELECT d.idProd, d.nomProd, d.cantidad AS stock, d.precio
    FROM DetalleVentas d
    WHERE d.numVenta = @numVenta;
END;
GO
