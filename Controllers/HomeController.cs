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
            { "Ganador", 5 }, { "Gratis", 5 }, { "Oferta", 5 }, { "Promoci�n", 5 },
            { "Descuento", 5 }, { "Limitado", 5 }, { "Excepcional", 5 }, { "Exclusivo", 5 },
            { "Alerta", 5 }, { "Seguridad", 5 }, { "Cuenta", 5 }, { "Acceso", 5 },
            { "Bloqueo", 5 }, { "Suspensi�n", 5 }, { "Clave", 5 }, { "Contrase�a", 5 },
            { "Activar", 5 }, { "Actualizar", 5 }, { "Problema", 5 }, { "Soluci�n", 5 },
            { "Importante", 5 }, { "Notificaci�n", 5 }, { "Asistencia", 5 }, { "Reembolso", 5 },
            { "Transferencia", 5 }, { "Confirmar", 5 }, { "Credenciales", 5 }, { "Bancario", 5 },
            { "Tarjeta", 5 }, { "PIN", 5 }, { "C�digo", 5 }, { "Dinero", 5 }, { "Dep�sito", 5 },
            { "Premios", 5 }, { "Millones", 5 }, { "Propina", 5 }, { "Respuesta", 5 },
            { "Solicitud", 5 }, { "Detalles", 5 }, { "Pago", 5 }, { "Iniciar", 5 },
            { "Recuperar", 5 }, { "Restablecer", 5 }, { "Mensaje", 5 }, { "Inscripci�n", 5 },
            { "Participaci�n", 5 }, { "Verificaci�n", 5 }, { "Activaci�n", 5 }, { "Mensaje de voz", 5 },
            { "Confirmaci�n", 5 }, { "Informaci�n", 5 }, { "Requiere", 5 }, { "Autorizaci�n", 5 },
            { "Usuario", 5 }, { "Identidad", 5 }, { "Personal", 5 }, { "Temporal", 5 },
            { "Expira", 5 }, { "Reg�strate", 5 }, { "Evento", 5 }, { "Invitaci�n", 5 },
            { "Accesible", 5 }, { "Redenci�n", 5 }, { "Cup�n", 5 }, { "Sorteo", 5 },
            { "Validar", 5 }, { "Pr�stamo", 5 }, { "Factura", 5 }, { "Balance", 5 },
            { "Alerta de seguridad", 5 }, { "Control", 5 }, { "Protecci�n", 5 }, { "Hackeo", 5 },
            { "Fraude", 5 }, { "Phishing", 5 }, { "Suplantaci�n", 5 }, { "Enlace", 5 },
            { "Link", 5 }, { "Clic", 5 }, { "Adjuntar", 5 }, { "Archivo", 5 },
            { "Documento", 5 }, { "Revisar", 5 }, { "Abajo", 5 }, { "Descarga", 5 },
            { "Escanear", 5 }, { "Revisi�n", 5 }, { "Responder", 5 }, { "Autenticaci�n", 5 },
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
