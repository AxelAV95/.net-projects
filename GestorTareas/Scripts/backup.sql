-- Script de backup para base de datos de producción
-- Ejecutar regularmente para mantener copias de seguridad

-- 1. Backup completo
BACKUP DATABASE [GestorTareasProduction] 
TO DISK = 'C:\Backups\GestorTareas_Full_' + FORMAT(GETDATE(), 'yyyyMMdd_HHmmss') + '.bak'
WITH FORMAT, INIT, COMPRESSION, STATS = 10;

-- 2. Verificar integridad
DBCC CHECKDB('GestorTareasProduction');

-- 3. Estadísticas de uso
SELECT 
    COUNT(*) as TotalTareas,
    COUNT(DISTINCT UsuarioId) as TotalUsuarios,
    COUNT(CASE WHEN Completada = 1 THEN 1 END) as TareasCompletadas,
    COUNT(CASE WHEN FechaVencimiento < GETDATE() AND Completada = 0 THEN 1 END) as TareasVencidas
FROM Tareas;

-- 4. Cleanup de tareas muy antiguas (opcional)
-- DELETE FROM Tareas 
-- WHERE Completada = 1 
-- AND FechaModificacion < DATEADD(YEAR, -1, GETDATE());