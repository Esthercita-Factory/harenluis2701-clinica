using ClinicaSalud.Models;

namespace ClinicaSalud.Interfaces;

public interface IRegistrable 
{ 
    void Registrar(); 
}

public interface IAtendible 
{ 
    void Atender(Mascota mascota); 
}

public interface INotificable 
{ 
    void EnviarNotificacion(string mensaje); 
}