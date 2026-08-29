using ClinicaSalud.Interfaces;

namespace ClinicaSalud.Models;

public abstract class Mascota : Animal, IRegistrable
{
    public string Raza { get; set; } = string.Empty;

    protected Mascota(string nombre, int edad, string especie, string raza)
    {
        Nombre = nombre;
        Edad = edad;
        Especie = especie;
        Raza = raza;
    }

    public void Registrar() => System.Console.WriteLine($"[Sistema] Mascota {Nombre} ({Especie}) ha sido registrada.");

    public void MostrarInformacion()
    {
        System.Console.WriteLine($"- Mascota: {Nombre} | Especie: {Especie} | Raza: {Raza} | Edad: {Edad} años");
    }
}

public class Perro : Mascota
{
    public Perro(string nombre, int edad, string raza) : base(nombre, edad, "Perro", raza) { }
    public override void EmitirSonido() => System.Console.WriteLine($"   🔊 {Nombre} hace: ¡Guau Guau!");
}

public class Gato : Mascota
{
    public Gato(string nombre, int edad, string raza) : base(nombre, edad, "Gato", raza) { }
    public override void EmitirSonido() => System.Console.WriteLine($"   🔊 {Nombre} hace: ¡Miau Miau!");
}

public class Ave : Mascota
{
    public Ave(string nombre, int edad, string raza) : base(nombre, edad, "Ave", raza) { }
    public override void EmitirSonido() => System.Console.WriteLine($"   🔊 {Nombre} hace: ¡Pío Pío / Canto!");
}

public class MascotaExotica : Mascota
{
    public MascotaExotica(string nombre, int edad, string especie, string raza) : base(nombre, edad, especie, raza) { }
    public override void EmitirSonido() => System.Console.WriteLine($"   🔊 {Nombre} ({Especie}) hace un sonido particular de su especie.");
}