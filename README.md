# Tarea Practica 2 — Calculadora con Genericos, Delegados y Excepciones


---

## ¿De que trata esto?

Basicamente hice una calculadora en consola con C# donde el usuario puede ir agregando numeros a una lista y luego elegir que operacion quiere hacer con todos esos numeros (sumar, restar, multiplicar o dividir). La idea principal de la tarea era practicar tres conceptos: genericos, delegados y manejo de excepciones.

Al principio me costo un poco entender como conectar todo, pero una vez que le agarre el hilo a los delegados fue mas facil.

---

## Como correr el programa

Necesitas tener instalado .NET SDK (version 6 o superior). Luego:

```bash
git clone https://github.com/TU_USUARIO/TareaPractica2.git
cd TareaPractica2
dotnet run
```

Y ya con eso te aparece el menu en consola.

---

## Menu del programa

```
1. Agregar numero a la lista
2. Ver lista actual
3. Sumar
4. Restar
5. Multiplicar
6. Dividir
7. Limpiar lista
8. Salir
```

---

## Como use los Genericos

Para esto cree una clase llamada `ListaNumeros<T>`. El `<T>` es el tipo generico, lo que significa que la misma clase puede trabajar con `int`, `double`, `float`, etc., sin tener que reescribir el codigo para cada tipo. En mi caso la use con `double` para tener mas precision en los calculos.

```csharp
class ListaNumeros<T> where T : struct
{
    private List<T> _numeros = new List<T>();
    // ...
}
```

El `where T : struct` lo puse para asegurarme de que solo se usen tipos de valor numericos y no cosas como strings o clases.

---

## Como use los Delegados

Cree un delegado llamado `OperacionMatematica<T>` que basicamente es una "plantilla" para cualquier metodo que reciba dos numeros y devuelva uno. Asi los metodos de Sumar, Restar, Multiplicar y Dividir se pueden pasar como argumento sin problema.

```csharp
delegate T OperacionMatematica<T>(T a, T b) where T : struct;
```

Luego en el switch, en lugar de escribir la logica de cada operacion ahi mismo, simplemente paso el metodo correspondiente:

```csharp
lista.EjecutarOperacion(Operaciones.Sumar, "Suma");
lista.EjecutarOperacion(Operaciones.Dividir, "Division");
```

---

## Como use los Enums

Para el menu use un `enum` llamado `OpcionMenu` para que el `switch` use nombres descriptivos en lugar de numeros sueltos como `"1"`, `"2"`, etc. Esto hace el codigo mas facil de leer.

```csharp
enum OpcionMenu
{
    AgregarNumero = 1,
    VerLista      = 2,
    Sumar         = 3,
    Restar        = 4,
    Multiplicar   = 5,
    Dividir       = 6,
    LimpiarLista  = 7,
    Salir         = 8
}
```

---

## Manejo de Excepciones

Esta parte me parecio importante porque el programa tiene que ser capaz de no romperse si el usuario mete algo mal. Cubri tres casos:

**1. El usuario escribe texto en vez de un numero**
```csharp
catch (FormatException)
{
    Console.WriteLine("El valor ingresado no es un numero valido.");
}
```

**2. Se intenta operar con menos de 2 numeros en la lista**
```csharp
if (_numeros.Count < 2)
    throw new InvalidOperationException("Se necesitan al menos 2 numeros...");
```

**3. Division por cero**
```csharp
if (b == 0)
    throw new DivideByZeroException("No se puede dividir por cero.");
```

Tambien use `?` y `??` para manejar el hecho de que `Console.ReadLine()` puede devolver `null` en C# moderno:

```csharp
string? input     = Console.ReadLine();   // puede ser null
string entradaRaw = input ?? string.Empty; // si es null, uso cadena vacia
```

Esto basicamente le dice al compilador "se que esto puede ser null y lo estoy manejando".

---

## Conclusion

Fue una tarea bastante completa. Me ayudo a entender mejor para que sirven los genericos en la practica (no solo en teoria), y los delegados me parecieron muy utiles para no repetir codigo. El manejo de excepciones tambien se siente mas natural ahora que lo aplique en un proyecto real.
