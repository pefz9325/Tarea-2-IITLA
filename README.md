# Calculadora Generica con Delegados - Tarea Practica 2

**Instituto Tecnologico de las Americas (ITLA)**

---

## Descripcion general

Aplicacion de consola desarrollada en **C#** que permite al usuario construir una lista de numeros y realizar operaciones matematicas sobre todos sus elementos. Demuestra el uso de **Genericos**, **Delegados** y **Control de Excepciones** junto a ENUMS (incluyendo los operadores `?` y `??` para valores nullable).

---

## Como ejecutar el programa

### Requisitos
- .NET SDK 6.0 o superior

### Pasos
```bash
# 1. Clonar el repositorio
git clone https://github.com/TU_USUARIO/TareaPractica2.git
cd TareaPractica2

# 2. Ejecutar
dotnet run
```

---

## Estructura del codigo

### Delegado `OperacionMatematica<T>`
```csharp
delegate T OperacionMatematica<T>(T a, T b) where T : struct;
```
Representa cualquier operacion que reciba dos valores del mismo tipo y devuelva uno del mismo tipo. Permite pasar **Sumar, Restar, Multiplicar o Dividir** como argumento sin cambiar la firma del metodo que los ejecuta.

### Clase generica `ListaNumeros<T>`
- `where T : struct` garantiza que el tipo sea un valor numerico (nunca nulo).
- `AgregarNumero(T numero)` - añade un elemento a la lista interna `List<T>`.
- `MostrarLista()` - imprime los elementos actuales.
- `LimpiarLista()` - vacia la lista.
- `EjecutarOperacion(OperacionMatematica<T> operacion, string nombreOp)` - aplica el delegado de forma secuencial sobre todos los elementos.

### Clase estatica `Operaciones`
Contiene los cuatro metodos matematicos con la firma que coincide con el delegado:
- `Sumar(double a, double b)`
- `Restar(double a, double b)`
- `Multiplicar(double a, double b)`
- `Dividir(double a, double b)` lanza `DivideByZeroException` si `b == 0`

---

## Excepciones manejadas

| Excepcion | Cuando se lanza |
|---|---|
| `FormatException` | El usuario ingresa texto en lugar de un numero |
| `InvalidOperationException` | Se intenta operar con menos de 2 elementos en la lista |
| `DivideByZeroException` | Un elemento de la lista es `0` durante la division |

### Operadores nullable (`?` y `??`)
```csharp
string? input = Console.ReadLine();        // ? → puede ser null
string opcion = input ?? string.Empty;     // ?? → si es null, usa cadena vacia
```
Esto le indica al compilador que somos conscientes de que `ReadLine()` puede devolver `null` y lo tratamos explicitamente.

---

## Uso del delegado en el menu (switch)

```csharp
case "3":   // Suma
    lista.EjecutarOperacion(Operaciones.Sumar, "Suma");
    break;
case "6":   // Division - captura excepcion adicional
    lista.EjecutarOperacion(Operaciones.Dividir, "Division");
    break;
```
El método `Sumar`, `Restar`, etc. se pasa directamente como argumento al parametro de tipo delegado.
