using System.Collections.Generic;
using ClinicaSalud.Interfaces;

namespace ClinicaSalud.Models;

public class Paciente : IRegistrable, INotificable
{
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Direccion { get; set; } = string.Empty;

    private string _telefono = string.Empty;
    public string Telefono 
    { 
        get => _telefono; 
        set 
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length >= 7)
                _telefono = value;
            else
                _telefono = "No válido";
        } 
    }

    public List<Mascota> Mascotas { get; private set; } = new();

    public Paciente() { } 

    public Paciente(string nombre, int edad, string direccion, string telefono)
    {
        Nombre = nombre;
        Edad = edad;
        Direccion = direccion;
        Telefono = telefono;
    }

    public void AgregarMascota(Mascota mascota)
    {
        Mascotas.Add(mascota);
    }

    public void Registrar()
    {
        System.Console.WriteLine($"[Sistema] Paciente {Nombre} registrado correctamente.");
    }

    public void EnviarNotificacion(string mensaje)
    {
        System.Console.WriteLine($"\n[SMS a {Telefono}] Hola {Nombre}: {mensaje}");
    }

    public void MostrarInformacion()
    {
        System.Console.WriteLine($"\nPaciente: {Nombre} | Edad: {Edad} | Tel: {Telefono} | Dir: {Direccion}");
        System.Console.WriteLine($"Total Mascotas: {Mascotas.Count}");
        
        foreach (var mascota in Mascotas)
        {
            mascota.MostrarInformacion();
            mascota.EmitirSonido(); 
        }
    }
}