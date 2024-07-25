using Microsoft.AspNetCore.Mvc;
using PhishingScanner.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhishingScanner.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Dictionary<string, int> Palabras = new Dictionary<string, int>
        {
            { "Urgente", 5 }, { "Inmediato", 5 }, { "Instrucciones", 5 }, { "Verificar", 5 },
            { "Ganador", 5 }, { "Gratis", 5 }, { "Oferta", 5 }, { "Promoción", 5 },
            { "Descuento", 5 }, { "Limitado", 5 }, { "Excepcional", 5 }, { "Exclusivo", 5 },
            { "Alerta", 5 }, { "Seguridad", 5 }, { "Cuenta", 5 }, { "Acceso", 5 },
            { "Bloqueo", 5 }, { "Suspensión", 5 }, { "Clave", 5 }, { "Contraseña", 5 },
            { "Activar", 5 }, { "Actualizar", 5 }, { "Problema", 5 }, { "Solución", 5 },
            { "Importante", 5 }, { "Notificación", 5 }, { "Asistencia", 5 }, { "Reembolso", 5 },
            { "Transferencia", 5 }, { "Confirmar", 5 }, { "Credenciales", 5 }, { "Bancario", 5 },
            { "Tarjeta", 5 }, { "PIN", 5 }, { "Código", 5 }, { "Dinero", 5 }, { "Depósito", 5 },
            { "Premios", 5 }, { "Millones", 5 }, { "Propina", 5 }, { "Respuesta", 5 },
            { "Solicitud", 5 }, { "Detalles", 5 }, { "Pago", 5 }, { "Iniciar", 5 },
            { "Recuperar", 5 }, { "Restablecer", 5 }, { "Mensaje", 5 }, { "Inscripción", 5 },
            { "Participación", 5 }, { "Verificación", 5 }, { "Activación", 5 }, { "Mensaje de voz", 5 },
            { "Confirmación", 5 }, { "Información", 5 }, { "Requiere", 5 }, { "Autorización", 5 },
            { "Usuario", 5 }, { "Identidad", 5 }, { "Personal", 5 }, { "Temporal", 5 },
            { "Expira", 5 }, { "Regístrate", 5 }, { "Evento", 5 }, { "Invitación", 5 },
            { "Accesible", 5 }, { "Redención", 5 }, { "Cupón", 5 }, { "Sorteo", 5 },
            { "Validar", 5 }, { "Préstamo", 5 }, { "Factura", 5 }, { "Balance", 5 },
            { "Alerta de seguridad", 5 }, { "Control", 5 }, { "Protección", 5 }, { "Hackeo", 5 },
            { "Fraude", 5 }, { "Phishing", 5 }, { "Suplantación", 5 }, { "Enlace", 5 },
            { "Link", 5 }, { "Clic", 5 }, { "Adjuntar", 5 }, { "Archivo", 5 },
            { "Documento", 5 }, { "Revisar", 5 }, { "Abajo", 5 }, { "Descarga", 5 },
            { "Escanear", 5 }, { "Revisión", 5 }, { "Responder", 5 }, { "Autenticación", 5 },
            { "Token", 5 }, { "Secuestro", 5 }, { "Solicita", 5 }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Scan()
        {
            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    var content = await streamReader.ReadToEndAsync();
                    var results = AnalyzeText(content);
                    ViewBag.OriginalText = content;
                    return View("Results", results);
                }
            }
            return View("Index");
        }

        private Dictionary<string, WordData> AnalyzeText(string text)
        {
            var results = new Dictionary<string, WordData>();

            foreach (var word in Palabras)
            {
                var regex = new Regex($@"\b{word.Key}\b", RegexOptions.IgnoreCase);
                var matches = regex.Matches(text);
                if (matches.Count > 0)
                {
                    results[word.Key] = new WordData
                    {
                        Word = word.Key,
                        OriginalWeight = word.Value,
                        Occurrences = matches.Count,
                        NewWeight = word.Value + matches.Count
                    };
                }
            }

            return results;
        }
    }
}
