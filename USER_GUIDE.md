# GU�A DE USUARIO - M�DULO DE VENTAS
*Sistema AdventureWorks Enterprise - Aplicaci�n Web*

---

## ?? Introducci�n

�Bienvenido al m�dulo de ventas de AdventureWorks Enterprise! Esta gu�a te ayudar� a navegar y utilizar todas las funcionalidades disponibles para gestionar las ventas de tu empresa de manera f�cil y eficiente.

### �Qu� puedes hacer con este m�dulo?
- ?? Ver y gestionar todas las �rdenes de venta
- ?? Consultar detalles completos de cada orden
- ?? Generar reportes de productos m�s vendidos
- ?? Monitorear el rendimiento de ventas
- ?? Analizar tendencias y m�tricas importantes

---

## ?? C�mo Acceder al M�dulo de Ventas

### Paso 1: Navegar al m�dulo
1. Una vez que hayas iniciado sesi�n en el sistema
2. Busca en el men� lateral izquierdo la secci�n **"Ventas"** 
3. Ver�s las siguientes opciones disponibles:
   - **"�rdenes"** - Para ver todas las �rdenes de venta
   - **"Top 10 Productos"** - Para ver el reporte de productos m�s vendidos

---

## ?? Pantalla de �rdenes de Venta

### �Qu� ver�s en esta pantalla?
La pantalla principal de �rdenes te muestra un resumen completo de todas las ventas de tu empresa.

### Elementos principales:

#### ??? **Panel de Control Superior**
- **T�tulo de la p�gina**: "�rdenes de Venta"
- **Bot�n "Actualizar"**: Presiona para obtener los datos m�s recientes
- **Bot�n "Top 10 Productos"**: Te lleva directamente al reporte de productos m�s vendidos

#### ?? **Tarjetas de Resumen** (4 tarjetas de colores)
Cuando cargan las �rdenes, ver�s estas m�tricas importantes:

1. **Total �rdenes** (tarjeta azul)
   - Muestra cu�ntas �rdenes hay en total en el sistema

2. **Completadas** (tarjeta verde)
   - Cu�ntas �rdenes ya fueron enviadas exitosamente

3. **Pendientes** (tarjeta amarilla)
   - Cu�ntas �rdenes a�n est�n en proceso

4. **Valor Total** (tarjeta celeste)
   - El valor total en dinero de todas las �rdenes

#### ?? **Tabla de �rdenes**
La tabla te muestra la informaci�n m�s importante de cada orden:

- **ID Orden**: N�mero �nico de identificaci�n
- **N�mero**: C�digo de la orden para referencia
- **Cliente**: Identificaci�n del cliente que hizo la compra
- **Fecha Orden**: Cu�ndo se cre� la orden
- **Fecha Entrega**: Cu�ndo se envi� (o "Pendiente" si no se ha enviado)
- **Estado**: El status actual de la orden con colores:
  - ?? **En Proceso**: La orden se est� preparando
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
- **Procesar** ?: Aparece solo en �rdenes pendientes para avanzar el proceso

### ?? Consejos para usar esta pantalla:
- Las �rdenes se muestran ordenadas por fecha, las m�s recientes primero
- Las tarjetas de colores te dan un vistazo r�pido del estado general
- Usa el bot�n "Actualizar" si sospechas que hay nuevas �rdenes
- Los colores en el estado te ayudan a identificar r�pidamente el progreso

---

## ?? Pantalla de Detalles de Orden

### �C�mo llegar aqu�?
Presiona el bot�n "Ver" ??? en cualquier orden de la tabla principal.

### �Qu� ver�s en esta pantalla?

#### ?? **Navegaci�n Superior**
- **Breadcrumb**: Te muestra d�nde est�s: "�rdenes > Orden #123"
- **Bot�n "Regresar"**: Te devuelve a la lista de �rdenes

#### ?? **Informaci�n Principal de la Orden** (panel grande izquierdo)
Esta secci�n verde te muestra todos los datos importantes:

**Informaci�n del Cliente y Fechas:**
- **Cliente**: N�mero de identificaci�n del cliente
- **Fecha de Orden**: Cu�ndo se cre� la orden
- **Fecha Requerida**: Cu�ndo el cliente necesita el pedido
- **Fecha de Env�o**: Si ya se envi�, cu�ndo fue

**Informaci�n del Proceso:**
- **Estado**: Estado actual con color y descripci�n
- **Vendedor**: Qui�n atendi� al cliente (si aplica)
- **N�mero de Revisi�n**: Cu�ntas veces se ha modificado la orden
- **Orden Online**: Si fue hecha por internet o en persona

#### ?? **Detalles de Productos** (tabla en panel izquierdo)
Esta tabla azul te muestra cada producto que pidi� el cliente:
- **ID Detalle**: Identificador de la l�nea
- **Producto**: N�mero del producto pedido
- **Cantidad**: Cu�ntas unidades se pidieron
- **Precio Unitario**: Precio por unidad
- **Descuento**: Si se aplic� alg�n descuento
- **Total L�nea**: Precio total de esa l�nea (cantidad � precio)
- **Tracking**: N�mero de seguimiento si ya se envi�

#### ?? **Resumen Financiero** (panel derecho celeste)
Aqu� ves el desglose de dinero:
- **Subtotal**: Suma de todos los productos
- **Impuestos**: Impuestos aplicados
- **Flete**: Costo de env�o
- **Total**: Cantidad final a pagar

Tambi�n puede mostrar:
- **Comentarios**: Notas especiales de la orden
- **�ltima modificaci�n**: Cu�ndo se actualiz� por �ltima vez

#### ?? **Panel de Acciones** (panel derecho amarillo)
Botones para hacer cosas con la orden:
- **?? Editar Orden**: Para modificar la orden (funci�n futura)
- **??? Imprimir**: Imprime la informaci�n de la orden
- **? Procesar**: Avanza la orden al siguiente paso (solo si est� pendiente)
- **? Cancelar**: Cancela la orden (solo si est� en proceso)

### ?? Consejos para esta pantalla:
- Usa el bot�n "Imprimir" para tener una copia f�sica de la orden
- El estado con colores te ayuda a entender r�pidamente en qu� fase est�
- Los detalles de productos te permiten verificar exactamente qu� se pidi�
- El resumen financiero te da claridad total sobre el dinero involucrado

---

## ?? Pantalla de Reporte Top 10 Productos

### �C�mo llegar aqu�?
Desde la pantalla de �rdenes, presiona el bot�n "Top 10 Productos" en la parte superior.

### �Qu� ver�s en esta pantalla?

#### ?? **Navegaci�n Superior**
- **Breadcrumb**: "Ventas > Top 10 Productos"
- **Bot�n "Actualizar Reporte"**: Obtiene los datos m�s frescos
- **Bot�n "Regresar"**: Te devuelve a las �rdenes

#### ?? **M�tricas Generales** (4 tarjetas de colores)
Te dan un panorama general de las ventas:

1. **Unidades Vendidas** (azul): Total de productos vendidos
2. **Ventas Totales** (verde): Dinero total generado
3. **�rdenes Totales** (celeste): Cantidad de �rdenes que incluyen estos productos
4. **Precio Promedio** (amarillo): Precio promedio de venta

#### ?? **Podio de Ganadores** (secci�n especial)
�La parte m�s emocionante! Muestra los primeros 3 lugares como un podio:

- **?? 1� Lugar**: El producto m�s vendido (con corona dorada)
- **?? 2� Lugar**: El segundo producto m�s vendido
- **?? 3� Lugar**: El tercer producto m�s vendido

Para cada uno ver�s:
- Nombre del producto
- N�mero del producto
- Cu�ntas unidades se vendieron
- Cu�nto dinero gener�

#### ?? **Tabla Completa del Top 10** (secci�n verde)
Una tabla detallada con los 10 productos m�s vendidos:

**Columnas de la tabla:**
- **Posici�n**: Su lugar en el ranking (con �conos especiales para top 3)
- **Producto**: Nombre y c�digo del producto
- **Categor�a**: A qu� familia de productos pertenece
- **Unidades Vendidas**: Total de unidades vendidas
- **Ventas Totales**: Dinero total generado por este producto
- **Precio Promedio**: Precio promedio al que se vendi�
- **Precio Lista**: Precio oficial del producto
- **�rdenes**: En cu�ntas �rdenes aparece este producto
- **Rendimiento**: Barra visual que muestra qu� tan bien vende comparado con el #1

#### ?? **Acciones Disponibles**
- **?? Exportar**: Guarda el reporte en archivo (funci�n futura)
- **??? Imprimir**: Imprime el reporte completo

### ?? Consejos para usar este reporte:
- El podio te da una vista r�pida de tus mejores productos
- Las barras de rendimiento te ayudan a comparar visualmente
- Los colores en las posiciones hacen f�cil identificar a los ganadores
- Usa este reporte para decidir qu� productos promocionar m�s
- Imprime el reporte para presentaciones o reuniones

---

## ?? Estados de las �rdenes - Gu�a Visual

Para entender mejor el proceso de ventas, aqu� tienes una explicaci�n de cada estado:

### ?? **En Proceso** (Estado 1)
- La orden acaba de ser creada
- Est� esperando revisi�n y aprobaci�n
- Puedes cancelarla si es necesario

### ?? **Aprobado** (Estado 2)
- La orden fue autorizada
- Est� lista para ser preparada
- Ya no se puede cancelar f�cilmente

### ?? **Pedido Pendiente** (Estado 3)
- Algunos productos no est�n disponibles
- Esperando que llegue inventario
- La orden est� temporalmente pausada

### ? **Rechazado** (Estado 4)
- La orden fue rechazada por alg�n motivo
- No se procesar�
- Necesita revisi�n o correcci�n

### ?? **Enviado** (Estado 5)
- La orden fue enviada al cliente
- El proceso de venta est� completo
- Tendr�s fecha de env�o y posiblemente tracking

### ? **Cancelado** (Estado 6)
- La orden fue cancelada
- No se procesar�
- Puede haber sido cancelada por el cliente o por la empresa

---

## ? Funciones R�pidas y Atajos

### ?? **Actualizaci�n de Datos**
- Todos los botones "Actualizar" traen la informaci�n m�s reciente
- �salos si sospechas que hay cambios nuevos

### ??? **Impresi�n**
- Todas las pantallas tienen funci�n de impresi�n
- El sistema oculta autom�ticamente botones y elementos no necesarios al imprimir

### ?? **Navegaci�n**
- Usa las "migas de pan" (breadcrumbs) para saber d�nde est�s
- Los botones "Regresar" te llevan siempre a la pantalla anterior

### ?? **Colores y Visuales**
- **Verde**: �xito, completado, positivo
- **Azul**: Informaci�n, procesos
- **Amarillo**: Advertencia, pendiente
- **Rojo**: Error, cancelado, atenci�n
- **Celeste**: Informaci�n financiera

---

## ?? Soluci�n de Problemas Comunes

### "No se encontraron �rdenes"
- **Causa**: No hay �rdenes en el sistema o hay un problema de conexi�n
- **Soluci�n**: Presiona "Actualizar" y espera un momento

### "Error al cargar datos"
- **Causa**: Problema de conexi�n con el servidor
- **Soluci�n**: Actualiza la p�gina web completa (F5) y vuelve a intentar

### La p�gina se ve extra�a o sin colores
- **Causa**: Problema de carga de estilos
- **Soluci�n**: Actualiza la p�gina (F5) o limpia la cach� del navegador

### Los botones no responden
- **Causa**: La p�gina puede estar procesando algo
- **Soluci�n**: Espera unos segundos y vuelve a intentar

---

## ?? �Necesitas Ayuda?

Si tienes problemas que no est�n cubiertos en esta gu�a:

1. **Actualiza la p�gina**: Muchos problemas se resuelven simplemente actualizando
2. **Verifica tu conexi�n**: Aseg�rate de tener internet estable
3. **Intenta en otro navegador**: A veces es un problema espec�fico del navegador
4. **Contacta soporte t�cnico**: Si nada funciona, reporta el problema al equipo t�cnico

---

## ?? Resumen R�pido

### Para ver �rdenes:
1. Ve a "Ventas" ? "�rdenes"
2. Mira las tarjetas de resumen para m�tricas generales
3. Usa la tabla para revisar �rdenes espec�ficas
4. Presiona "Ver" para detalles completos

### Para ver reportes:
1. Ve a "Ventas" ? "Top 10 Productos" 
2. O desde �rdenes, presiona "Top 10 Productos"
3. Revisa el podio para los ganadores
4. Usa la tabla para an�lisis detallado

### Recuerda:
- ? Los colores tienen significado
- ? Siempre puedes imprimir
- ? "Actualizar" trae datos frescos
- ? Los breadcrumbs te orientan
- ? Los botones "Regresar" te llevan atr�s

---

*�Esperamos que esta gu�a te ayude a sacar el m�ximo provecho del m�dulo de ventas!*

*Sistema AdventureWorks Enterprise*  
*Versi�n: 1.0*  
*Fecha: Diciembre 2024*