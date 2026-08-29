using ClinicaSalud.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Services;

public abstract class ServicioVeterinario : IAtendible
{
    public abstract void Atender(Mascota mascota);
}

public class ConsultaGeneral : ServicioVeterinario
{
    public override void Atender(Mascota mascota)
    {
        Console.WriteLine($"[Consulta] Realizando chequeo general a {mascota.Nombre}...");
    }
}

public class Vacunacion : ServicioVeterinario
{
    public override void Atender(Mascota mascota)
    {
        Console.WriteLine($"[Vacunación] Aplicando dosis anual a {mascota.Nombre}...");
    }
}