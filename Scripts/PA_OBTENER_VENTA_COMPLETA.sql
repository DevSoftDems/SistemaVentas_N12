-- Stored procedure: PA_OBTENER_VENTA_COMPLETA
-- Devuelve la cabecera y detalle de una venta
CREATE PROCEDURE PA_OBTENER_VENTA_COMPLETA
    @numVenta VARCHAR(10)
AS
BEGIN
    -- Primer result set: cabecera y cliente
    SELECT v.numVenta, v.fecVenta AS fecha, v.totVenta AS total,
           c.idCli, c.nomCli, c.apeCli, c.dni, c.dirCli
    FROM Ventas v
        JOIN Clientes c ON v.idCli = c.idCli
    WHERE v.numVenta = @numVenta;

    -- Segundo result set: detalle
    SELECT d.idProd, p.nomProd, d.cantidad AS stock, d.precio
    FROM DetalleVentas d
        JOIN Productos p ON d.idProd = p.idProd
    WHERE d.numVenta = @numVenta;
END;
GO
