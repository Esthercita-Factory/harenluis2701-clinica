
using ClinicaSalud.Models;
using ClinicaSalud.Exceptions;


namespace ClinicaSalud.Services;

public class ClinicaManager
{
    // --- LÓGICA SÍNCRONA ---

    public void RegistrarPacienteInteractuando(List<Paciente> lista)
    {
        try
        {
            Console.WriteLine("\n--- REGISTRO DE PACIENTE ---");
            Paciente nuevoPaciente = new();

            nuevoPaciente.Nombre = SolicitarDatoObligatorio("Nombre del dueño: ");
            Console.Write("Edad del dueño: ");
            nuevoPaciente.Edad = int.Parse(Console.ReadLine() ?? string.Empty);
            nuevoPaciente.Direccion = SolicitarDatoObligatorio("Dirección: ");
            Console.Write("Teléfono: ");
            nuevoPaciente.Telefono = Console.ReadLine() ?? "";

            Console.WriteLine("\n--- DATOS DE LA MASCOTA ---");
            string nombreMascota = SolicitarDatoObligatorio("Nombre de la mascota: ");
            Console.Write("Edad de la mascota: ");
            int edadMascota = int.Parse(Console.ReadLine() ?? string.Empty);
            string especie = SolicitarDatoObligatorio("Especie (Ej. Perro, Gato, Loro, Conejo): ");
            string raza = SolicitarDatoObligatorio("Raza (o 'Desconocida'): ");

            // CUALQUIER ANIMAL: Creación dinámica usando polimorfismo
            Mascota nuevaMascota = especie.ToLower() switch
            {
                "perro" => new Perro(nombreMascota, edadMascota, raza),
                "gato" => new Gato(nombreMascota, edadMascota, raza),
                "ave" or "loro" => new Ave(nombreMascota, edadMascota, raza),
                _ => new MascotaExotica(nombreMascota, edadMascota, especie, raza)
            };

            nuevoPaciente.AgregarMascota(nuevaMascota);
            lista.Add(nuevoPaciente);

            nuevoPaciente.Registrar();
            nuevaMascota.Registrar();
            nuevaMascota.EmitirSonido();
        }
        catch (FormatException ex)
        {
            Logger.LogError("Error de formato al ingresar un número", ex);
        }
        finally 
        {
            Console.WriteLine("[Sistema] Módulo de registro finalizado. Liberando memoria...");
        }
    }

    public void BuscarPacientePorNombre(List<Paciente> lista)
    {
        string nombre = SolicitarDatoObligatorio("\nIngrese el nombre del paciente a buscar: ");
        
        var paciente = lista.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        if (paciente != null)
        {
            paciente.MostrarInformacion();
        }
        else
        {
            Console.WriteLine($"\n[Aviso] No se encontró a ningún paciente llamado '{nombre}'.");
        }
    }

    public void AtenderMascotas(List<Paciente> lista)
    {
        Console.WriteLine("\n--- ATENCIÓN VETERINARIA ---");
        try
        {
            var todasLasMascotas = lista.SelectMany(p => p.Mascotas).ToList();
            if (!todasLasMascotas.Any())
                throw new MascotaNoEncontradaException("No existen mascotas registradas en el sistema para atender.");

            ServicioVeterinario consulta = new ConsultaGeneral();
            ServicioVeterinario vacuna = new Vacunacion();

            foreach (var mascota in todasLasMascotas)
            {
                if (mascota is Perro) vacuna.Atender(mascota);
                else consulta.Atender(mascota);
            }
        }
        catch (MascotaNoEncontradaException ex)
        {
            Logger.LogError("Validación de atención fallida", ex);
        }
    }

    public void MostrarEstadisticasLinq(List<Paciente> lista)
    {
        if (!lista.Any())
        {
            Console.WriteLine("\n[Advertencia] No hay datos para analizar.");
            return;
        }

        Console.WriteLine("\n--- REPORTE DEMOGRÁFICO AVANZADO ---");
        
        Dictionary<string, Paciente> dirPacientes = lista.ToDictionary(p => p.Nombre, p => p);
        Console.WriteLine($"Total de dueños indexados: {dirPacientes.Count}");

        var todasLasMascotas = lista.SelectMany(p => p.Mascotas).ToList();

        var perrosQuery = from m in todasLasMascotas
                          where m.Especie.Equals("Perro", StringComparison.OrdinalIgnoreCase)
                          select m;
        Console.WriteLine($"Cantidad de Perros registrados: {perrosQuery.Count()}");

        var masJoven = lista.OrderBy(p => p.Edad).First();
        Console.WriteLine($"Dueño más joven registrado: {masJoven.Nombre} ({masJoven.Edad} años)");

        Console.WriteLine("\nConteo general de mascotas por especie:");
        foreach (var grupo in todasLasMascotas.GroupBy(m => m.Especie))
        {
            Console.WriteLine($"- {grupo.Key}: {grupo.Count()} mascota(s)");
        }

        // US 2: Uso del método 'All'
        bool todosTienenMascota = lista.All(p => p.Mascotas.Count > 0);
        Console.WriteLine($"\n¿Todos los clientes tienen al menos una mascota?: {(todosTienenMascota ? "Sí" : "No")}");

        // US 2: Consultas encadenadas (Select + OrderBy)
        Console.WriteLine("\nLista de dueños (Alfabético y en MAYÚSCULAS):");
        var nombresMayusculas = lista.Select(p => p.Nombre.ToUpper()).OrderBy(n => n);
        foreach (var nom in nombresMayusculas)
        {
            Console.WriteLine($"- {nom}");
        }
    }

    // --- LÓGICA ASÍNCRONA ---

    public async Task RegistrarPacienteAsync(Paciente pacienteNuevo)
    {
        Console.WriteLine($"\n[Inicio] Subiendo registros a la nube de {pacienteNuevo.Nombre}...");
        await Task.Delay(3000); 
        Console.WriteLine($"[Fin] Sincronización exitosa.");
    }

    public async Task SimularProcesosParalelosAsync(Paciente pacienteActual)
    {
        Console.WriteLine($"\n--- INICIANDO CIERRE DEL DÍA (PARALELO) ---");

        Task tareaHistorial = Task.Run(async () => { await Task.Delay(4000); Console.WriteLine("[1] Copia de seguridad de historiales creada."); });
        Task tareaCita = Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("[2] Citas del día siguiente confirmadas."); });
        Task tareaNotificacion = Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("[3] SMS de recordatorio enviados."); });

        await Task.WhenAll(tareaHistorial, tareaCita, tareaNotificacion);
        Console.WriteLine("\n[Sistema] Cierre del día concluido correctamente.");
    }

    public async Task ProcesarMultiplesMascotasAsync(List<Mascota> mascotasRegistradas)
    {
        if (!mascotasRegistradas.Any()) return;

        Console.WriteLine("\n--- ACTUALIZACIÓN MASIVA DE HISTORIALES ---");
        List<Task> tareasDeRegistro = new List<Task>();

        foreach (var mascota in mascotasRegistradas)
        {
            tareasDeRegistro.Add(RegistrarMascotaFicticiaAsync(mascota));
        }

        await Task.WhenAll(tareasDeRegistro);
        Console.WriteLine("\n[Éxito] Todas las mascotas han sido procesadas concurrentemente.");
    }

    private async Task RegistrarMascotaFicticiaAsync(Mascota mascotaProcesar)
    {
        int tiempoEspera = new Random().Next(1000, 3000);
        await Task.Delay(tiempoEspera);
        Console.WriteLine($"- Expediente de {mascotaProcesar.Nombre} actualizado tras {tiempoEspera}ms.");
    }

    // --- UTILIDADES ---

    private string SolicitarDatoObligatorio(string mensaje)
    {
        string? entrada = "";
        while (string.IsNullOrWhiteSpace(entrada))
        {
            Console.Write(mensaje);
            entrada = Console.ReadLine();
        }
        return entrada;
    }
}