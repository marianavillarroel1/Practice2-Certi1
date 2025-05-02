using Microsoft.AspNetCore.Mvc;

namespace Practice2_Certi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        // Endpoint POST /patients (crear paciente)
        [HttpPost]
        public IActionResult CreatePatient([FromBody] PatientDto patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name) ||
                string.IsNullOrWhiteSpace(patient.LastName) ||
                string.IsNullOrWhiteSpace(patient.CI))
            {
                return BadRequest("Datos incompletos");
            }

            // Asignar grupo sanguíneo aleatorio
            string[] bloodGroups = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            Random rnd = new Random();
            string randomBlood = bloodGroups[rnd.Next(bloodGroups.Length)];

            // Crear línea a guardar
            string line = $"{patient.Name},{patient.LastName},{patient.CI},{randomBlood}";

            // Ruta del archivo
            string filePath = "patients.txt";

            // Guardar en el archivo
            try
            {
                System.IO.File.AppendAllLines(filePath, new[] { line });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el paciente: {ex.Message}");
            }

            return Ok($"Paciente creado con grupo sanguíneo: {randomBlood}");
        }


        // Endpoint PUT /patients/{ci} (actualizar nombre/apellido)
        [HttpPut("{ci}")]
        public IActionResult UpdatePatient(string ci, [FromBody] PatientDto updatedPatient)
        {
            return Ok("Paciente actualizado");
        }

        // Endpoint DELETE /patients/{ci}
        [HttpDelete("{ci}")]
        public IActionResult DeletePatient(string ci)
        {
            return Ok("Paciente eliminado");
        }

        // Endpoint GET /patients
        [HttpGet]
        public IActionResult GetAllPatients()
        {
            return Ok();
        }

        // Endpoint GET /patients/{ci}
        [HttpGet("{ci}")]
        public IActionResult GetPatientByCi(string ci)
        {
            return Ok();
        }
    }

    // DTO temporal (lo moveremos más adelante a una clase externa)
    public class PatientDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CI { get; set; }
    }
}
