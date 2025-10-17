# GUÍA DE USUARIO - MÓDULO DE VENTAS
*Sistema AdventureWorks Enterprise - Aplicación Web*

---

## ?? Introducción

¡Bienvenido al módulo de ventas de AdventureWorks Enterprise! Esta guía te ayudará a navegar y utilizar todas las funcionalidades disponibles para gestionar las ventas de tu empresa de manera fácil y eficiente.

### ¿Qué puedes hacer con este módulo?
- ?? Ver y gestionar todas las órdenes de venta
- ?? Consultar detalles completos de cada orden
- ?? Generar reportes de productos más vendidos
- ?? Monitorear el rendimiento de ventas
- ?? Analizar tendencias y métricas importantes

---

## ?? Cómo Acceder al Módulo de Ventas

### Paso 1: Navegar al módulo
1. Una vez que hayas iniciado sesión en el sistema
2. Busca en el menú lateral izquierdo la sección **"Ventas"** 
3. Verás las siguientes opciones disponibles:
   - **"Órdenes"** - Para ver todas las órdenes de venta
   - **"Top 10 Productos"** - Para ver el reporte de productos más vendidos

---

## ?? Pantalla de Órdenes de Venta

### ¿Qué verás en esta pantalla?
La pantalla principal de órdenes te muestra un resumen completo de todas las ventas de tu empresa.

### Elementos principales:

#### ??? **Panel de Control Superior**
- **Título de la página**: "Órdenes de Venta"
- **Botón "Actualizar"**: Presiona para obtener los datos más recientes
- **Botón "Top 10 Productos"**: Te lleva directamente al reporte de productos más vendidos

#### ?? **Tarjetas de Resumen** (4 tarjetas de colores)
Cuando cargan las órdenes, verás estas métricas importantes:

1. **Total Órdenes** (tarjeta azul)
   - Muestra cuántas órdenes hay en total en el sistema

2. **Completadas** (tarjeta verde)
   - Cuántas órdenes ya fueron enviadas exitosamente

3. **Pendientes** (tarjeta amarilla)
   - Cuántas órdenes aún están en proceso

4. **Valor Total** (tarjeta celeste)
   - El valor total en dinero de todas las órdenes

#### ?? **Tabla de Órdenes**
La tabla te muestra la información más importante de cada orden:

- **ID Orden**: Número único de identificación
- **Número**: Código de la orden para referencia
- **Cliente**: Identificación del cliente que hizo la compra
- **Fecha Orden**: Cuándo se creó la orden
- **Fecha Entrega**: Cuándo se envió (o "Pendiente" si no se ha enviado)
- **Estado**: El status actual de la orden con colores:
  - ?? **En Proceso**: La orden se está preparando
  - ?? **Aprobado**: La orden fue autorizada
  - ?? **Pedido Pendiente**: Esperando productos
  - ? **Rechazado**: La orden fue rechazada
  - ?? **Enviado**: La orden fue enviada al cliente
  - ? **Cancelado**: La orden fue cancelada
- **Subtotal**: Valor antes de impuestos
- **Total**: Valor final de la orden

#### ?? **Acciones Disponibles**
Para cada orden puedes:
- **Ver** ???: Presiona para ver todos los detalles de la orden
- **Procesar** ?: Aparece solo en órdenes pendientes para avanzar el proceso

### ?? Consejos para usar esta pantalla:
- Las órdenes se muestran ordenadas por fecha, las más recientes primero
- Las tarjetas de colores te dan un vistazo rápido del estado general
- Usa el botón "Actualizar" si sospechas que hay nuevas órdenes
- Los colores en el estado te ayudan a identificar rápidamente el progreso

---

## ?? Pantalla de Detalles de Orden

### ¿Cómo llegar aquí?
Presiona el botón "Ver" ??? en cualquier orden de la tabla principal.

### ¿Qué verás en esta pantalla?

#### ?? **Navegación Superior**
- **Breadcrumb**: Te muestra dónde estás: "Órdenes > Orden #123"
- **Botón "Regresar"**: Te devuelve a la lista de órdenes

#### ?? **Información Principal de la Orden** (panel grande izquierdo)
Esta sección verde te muestra todos los datos importantes:

**Información del Cliente y Fechas:**
- **Cliente**: Número de identificación del cliente
- **Fecha de Orden**: Cuándo se creó la orden
- **Fecha Requerida**: Cuándo el cliente necesita el pedido
- **Fecha de Envío**: Si ya se envió, cuándo fue

**Información del Proceso:**
- **Estado**: Estado actual con color y descripción
- **Vendedor**: Quién atendió al cliente (si aplica)
- **Número de Revisión**: Cuántas veces se ha modificado la orden
- **Orden Online**: Si fue hecha por internet o en persona

#### ?? **Detalles de Productos** (tabla en panel izquierdo)
Esta tabla azul te muestra cada producto que pidió el cliente:
- **ID Detalle**: Identificador de la línea
- **Producto**: Número del producto pedido
- **Cantidad**: Cuántas unidades se pidieron
- **Precio Unitario**: Precio por unidad
- **Descuento**: Si se aplicó algún descuento
- **Total Línea**: Precio total de esa línea (cantidad × precio)
- **Tracking**: Número de seguimiento si ya se envió

#### ?? **Resumen Financiero** (panel derecho celeste)
Aquí ves el desglose de dinero:
- **Subtotal**: Suma de todos los productos
- **Impuestos**: Impuestos aplicados
- **Flete**: Costo de envío
- **Total**: Cantidad final a pagar

También puede mostrar:
- **Comentarios**: Notas especiales de la orden
- **Última modificación**: Cuándo se actualizó por última vez

#### ?? **Panel de Acciones** (panel derecho amarillo)
Botones para hacer cosas con la orden:
- **?? Editar Orden**: Para modificar la orden (función futura)
- **??? Imprimir**: Imprime la información de la orden
- **? Procesar**: Avanza la orden al siguiente paso (solo si está pendiente)
- **? Cancelar**: Cancela la orden (solo si está en proceso)

### ?? Consejos para esta pantalla:
- Usa el botón "Imprimir" para tener una copia física de la orden
- El estado con colores te ayuda a entender rápidamente en qué fase está
- Los detalles de productos te permiten verificar exactamente qué se pidió
- El resumen financiero te da claridad total sobre el dinero involucrado

---

## ?? Pantalla de Reporte Top 10 Productos

### ¿Cómo llegar aquí?
Desde la pantalla de órdenes, presiona el botón "Top 10 Productos" en la parte superior.

### ¿Qué verás en esta pantalla?

#### ?? **Navegación Superior**
- **Breadcrumb**: "Ventas > Top 10 Productos"
- **Botón "Actualizar Reporte"**: Obtiene los datos más frescos
- **Botón "Regresar"**: Te devuelve a las órdenes

#### ?? **Métricas Generales** (4 tarjetas de colores)
Te dan un panorama general de las ventas:

1. **Unidades Vendidas** (azul): Total de productos vendidos
2. **Ventas Totales** (verde): Dinero total generado
3. **Órdenes Totales** (celeste): Cantidad de órdenes que incluyen estos productos
4. **Precio Promedio** (amarillo): Precio promedio de venta

#### ?? **Podio de Ganadores** (sección especial)
¡La parte más emocionante! Muestra los primeros 3 lugares como un podio:

- **?? 1° Lugar**: El producto más vendido (con corona dorada)
- **?? 2° Lugar**: El segundo producto más vendido
- **?? 3° Lugar**: El tercer producto más vendido

Para cada uno verás:
- Nombre del producto
- Número del producto
- Cuántas unidades se vendieron
- Cuánto dinero generó

#### ?? **Tabla Completa del Top 10** (sección verde)
Una tabla detallada con los 10 productos más vendidos:

**Columnas de la tabla:**
- **Posición**: Su lugar en el ranking (con íconos especiales para top 3)
- **Producto**: Nombre y código del producto
- **Categoría**: A qué familia de productos pertenece
- **Unidades Vendidas**: Total de unidades vendidas
- **Ventas Totales**: Dinero total generado por este producto
- **Precio Promedio**: Precio promedio al que se vendió
- **Precio Lista**: Precio oficial del producto
- **Órdenes**: En cuántas órdenes aparece este producto
- **Rendimiento**: Barra visual que muestra qué tan bien vende comparado con el #1

#### ?? **Acciones Disponibles**
- **?? Exportar**: Guarda el reporte en archivo (función futura)
- **??? Imprimir**: Imprime el reporte completo

### ?? Consejos para usar este reporte:
- El podio te da una vista rápida de tus mejores productos
- Las barras de rendimiento te ayudan a comparar visualmente
- Los colores en las posiciones hacen fácil identificar a los ganadores
- Usa este reporte para decidir qué productos promocionar más
- Imprime el reporte para presentaciones o reuniones

---

## ?? Estados de las Órdenes - Guía Visual

Para entender mejor el proceso de ventas, aquí tienes una explicación de cada estado:

### ?? **En Proceso** (Estado 1)
- La orden acaba de ser creada
- Está esperando revisión y aprobación
- Puedes cancelarla si es necesario

### ?? **Aprobado** (Estado 2)
- La orden fue autorizada
- Está lista para ser preparada
- Ya no se puede cancelar fácilmente

### ?? **Pedido Pendiente** (Estado 3)
- Algunos productos no están disponibles
- Esperando que llegue inventario
- La orden está temporalmente pausada

### ? **Rechazado** (Estado 4)
- La orden fue rechazada por algún motivo
- No se procesará
- Necesita revisión o corrección

### ?? **Enviado** (Estado 5)
- La orden fue enviada al cliente
- El proceso de venta está completo
- Tendrás fecha de envío y posiblemente tracking

### ? **Cancelado** (Estado 6)
- La orden fue cancelada
- No se procesará
- Puede haber sido cancelada por el cliente o por la empresa

---

## ? Funciones Rápidas y Atajos

### ?? **Actualización de Datos**
- Todos los botones "Actualizar" traen la información más reciente
- Úsalos si sospechas que hay cambios nuevos

### ??? **Impresión**
- Todas las pantallas tienen función de impresión
- El sistema oculta automáticamente botones y elementos no necesarios al imprimir

### ?? **Navegación**
- Usa las "migas de pan" (breadcrumbs) para saber dónde estás
- Los botones "Regresar" te llevan siempre a la pantalla anterior

### ?? **Colores y Visuales**
- **Verde**: Éxito, completado, positivo
- **Azul**: Información, procesos
- **Amarillo**: Advertencia, pendiente
- **Rojo**: Error, cancelado, atención
- **Celeste**: Información financiera

---

## ?? Solución de Problemas Comunes

### "No se encontraron órdenes"
- **Causa**: No hay órdenes en el sistema o hay un problema de conexión
- **Solución**: Presiona "Actualizar" y espera un momento

### "Error al cargar datos"
- **Causa**: Problema de conexión con el servidor
- **Solución**: Actualiza la página web completa (F5) y vuelve a intentar

### La página se ve extraña o sin colores
- **Causa**: Problema de carga de estilos
- **Solución**: Actualiza la página (F5) o limpia la caché del navegador

### Los botones no responden
- **Causa**: La página puede estar procesando algo
- **Solución**: Espera unos segundos y vuelve a intentar

---

## ?? ¿Necesitas Ayuda?

Si tienes problemas que no están cubiertos en esta guía:

1. **Actualiza la página**: Muchos problemas se resuelven simplemente actualizando
2. **Verifica tu conexión**: Asegúrate de tener internet estable
3. **Intenta en otro navegador**: A veces es un problema específico del navegador
4. **Contacta soporte técnico**: Si nada funciona, reporta el problema al equipo técnico

---

## ?? Resumen Rápido

### Para ver órdenes:
1. Ve a "Ventas" ? "Órdenes"
2. Mira las tarjetas de resumen para métricas generales
3. Usa la tabla para revisar órdenes específicas
4. Presiona "Ver" para detalles completos

### Para ver reportes:
1. Ve a "Ventas" ? "Top 10 Productos" 
2. O desde órdenes, presiona "Top 10 Productos"
3. Revisa el podio para los ganadores
4. Usa la tabla para análisis detallado

### Recuerda:
- ? Los colores tienen significado
- ? Siempre puedes imprimir
- ? "Actualizar" trae datos frescos
- ? Los breadcrumbs te orientan
- ? Los botones "Regresar" te llevan atrás

---

*¡Esperamos que esta guía te ayude a sacar el máximo provecho del módulo de ventas!*

*Sistema AdventureWorks Enterprise*  
*Versión: 1.0*  
*Fecha: Diciembre 2024*