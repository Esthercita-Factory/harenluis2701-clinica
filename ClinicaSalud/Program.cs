
using ClinicaSalud.Models;
using ClinicaSalud.Services;

Console.WriteLine("==========================================================");
Console.WriteLine("    CLÍNICA VETERINARIA SALUD+ | Sistema de Gestión");
Console.WriteLine("==========================================================");

List<Paciente> pacientes = new();
ClinicaManager manager = new();

// Datos iniciales
Paciente pacientePrueba = new Paciente("Ana Lopez", 28, "Calle 1", "5551234");
pacientePrueba.AgregarMascota(new Perro("Rex", 4, "Labrador"));
pacientePrueba.AgregarMascota(new MascotaExotica("Saltarin", 1, "Conejo", "Enano")); // Prueba de otro animal
pacientes.Add(pacientePrueba);

bool continuar = true;

while (continuar)
{
    Console.WriteLine("\n==================================================");
    Console.WriteLine("                 MENÚ PRINCIPAL                   ");
    Console.WriteLine("==================================================");
    Console.WriteLine("1. Registrar nuevo paciente (Admite cualquier especie)");
    Console.WriteLine("2. Buscar paciente por nombre");
    Console.WriteLine("3. Pasar mascotas a sala de atención");
    Console.WriteLine("4. Generar reporte demográfico (Diccionarios y LINQ)");
    Console.WriteLine("5. Subir registro a la nube (Proceso Asíncrono)");
    Console.WriteLine("6. Ejecutar cierre del día (Procesos Paralelos)");
    Console.WriteLine("7. Cerrar sesión");
    Console.WriteLine("8. [Dev] Simular error crítico de sistema (Depuración)");
    Console.Write("\nSeleccione una acción (1-8): ");

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            manager.RegistrarPacienteInteractuando(pacientes);
            break;
        case "2":
            manager.BuscarPacientePorNombre(pacientes);
            break;
        case "3":
            manager.AtenderMascotas(pacientes);
            break;
        case "4":
            manager.MostrarEstadisticasLinq(pacientes);
            break;
        case "5":
            await manager.RegistrarPacienteAsync(pacientePrueba);
            break;
        case "6":
            await manager.SimularProcesosParalelosAsync(pacientePrueba);
            break;
        case "7":
            continuar = false;
            Console.WriteLine("\nCerrando sesión. Guardando datos localmente...");
            break;
        case "8":
            try 
            {
                Console.WriteLine("\n[Atención] Forzando un error de división por cero...");
                int error = 10 / int.Parse("0"); // ¡Pon el Breakpoint de tu IDE en esta misma línea!
            }
            catch (Exception ex) 
            {
                Logger.LogError("División por cero forzada para QA", ex);
            }
            break;
        default:
            Console.WriteLine("\n[Advertencia] Comando no reconocido. Intente nuevamente.");
            break;
    }
}