namespace ClinicaSalud.Models;

public abstract class Animal
{
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Especie { get; set; } = string.Empty;

    public abstract void EmitirSonido();
}