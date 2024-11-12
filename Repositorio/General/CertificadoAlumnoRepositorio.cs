using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Bouncycastleconnector;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using WebITSC.Shared.General.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repositorio.General
{
    public class CertificadoAlumnoRepositorio : Repositorio<CertificadoAlumno>, ICertificadoAlumnoRepositorio
    {
        private readonly Context context;
        public CertificadoAlumnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<ActionResult<GetDatosCertificadosDTO>> SelectDatosCertificado(string? nombre, string? apellido, string? documento)
        {
            var query = context.Alumnos.AsQueryable();


            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(a => a.Usuario.Persona.Nombre.Contains(nombre));

            if (!string.IsNullOrWhiteSpace(apellido))
                query = query.Where(a => a.Usuario.Persona.Apellido.Contains(apellido));

            if (!string.IsNullOrWhiteSpace(documento))
                query = query.Where(a => a.Usuario.Persona.Documento.Contains(documento));



            var datos = await query.Select(a => new GetDatosCertificadosDTO
            {
                ApellidoyNombre = a.Usuario.Persona.Apellido + " " + a.Usuario.Persona.Nombre,
                TipoDocumentoCertificado = a.Usuario.Persona.TipoDocumento.Nombre,
                NroDocumento = a.Usuario.Persona.Documento,
                FechadeNacimiento = DateOnly.FromDateTime(a.FechaNacimiento),
                LugarNacimiento = a.Departamento + " " + a.Provincia + " " + a.Pais,
                NroTelefono = a.Usuario.Persona.Telefono,
                TituloHabilitante = a.TituloBase,
                Legajo = context.InscripcionesCarrera
                            .Where(ic => ic.Alumno.Id == a.Id)
                            .Select(ic => ic.Legajo)
                            .FirstOrDefault(),
                LibroMatriz = context.InscripcionesCarrera
                            .Where(ic => ic.Alumno.Id == a.Id)
                            .Select(ic => ic.LibroMatriz)
                            .FirstOrDefault(),
                Folio = context.Notas
                            .Where(n => n.CursadoMateria.Alumno.Id == a.Id && n.Evaluacion.TipoEvaluacion == "Final")
                            .Select(n => n.Evaluacion.Folio)
                            .FirstOrDefault(),
                FilasTabla = context.CursadosMateria
                            .Where(cm => cm.AlumnoId == a.Id)
                            .Select(cm => new FilasMateriaDTO
                            {
                                Asignatura = cm.Turno.MateriaEnPlanEstudio.Materia.Nombre,
                                ValorNota = context.Notas
                                    .Where(n => n.Evaluacion.TipoEvaluacion == "Final" && n.CursadoMateria.AlumnoId == a.Id)
                                    .Select(n => n.ValorNota)
                                    .First(),
                                NotaLetra = NumeroANombre(context.Notas
                                            .Where(n => n.Evaluacion.TipoEvaluacion == "Final" && n.CursadoMateria.AlumnoId == a.Id)
                                            .Select(n => n.ValorNota)
                                            .First()),
                                Libro = context.Notas
                                    .Where(n => n.CursadoMateria.AlumnoId == a.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                    .Select(n => n.Evaluacion.Libro)
                                    .FirstOrDefault(),
                                Folio = context.Notas
                                    .Where(n => n.CursadoMateria.AlumnoId == a.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                    .Select(n => n.Evaluacion.Folio)
                                    .FirstOrDefault(),
                                Dia = context.Notas
                                    .Where(n => n.CursadoMateria.AlumnoId == a.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                    .Select(n => n.Evaluacion.Fecha.Day)
                                    .FirstOrDefault(),
                                Mes = context.Notas
                                    .Where(n => n.CursadoMateria.AlumnoId == a.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                    .Select(n => n.Evaluacion.Fecha.Month)
                                    .FirstOrDefault(),
                                Anno = context.Notas
                                    .Where(n => n.CursadoMateria.AlumnoId == a.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                    .Select(n => n.Evaluacion.Fecha.Year)
                                    .FirstOrDefault(),
                                CondicionActual = context.Notas
                                    .Where(n => n.CursadoMateria.AlumnoId == a.Id)
                                    .Select(n => n.CursadoMateria.CondicionActual)
                                    .FirstOrDefault(),
                                Sede = context.CursadosMateria
                                    .Where(cm => cm.Alumno.Id == a.Id)
                                    .Select(cm => cm.Turno.Sede)
                                    .FirstOrDefault()
                            }).ToList(),
                Fecha = DateOnly.FromDateTime(DateTime.Now)
            }).FirstOrDefaultAsync();



            return datos;

        }

		public byte[] GenerarCertificadoPDF(GetDatosCertificadosDTO datos)
		{
			using (var stream = new MemoryStream())
			{
				var writer = new PdfWriter(stream);
				var pdf = new PdfDocument(writer);
				var document = new iText.Layout.Document(pdf);

				// Título
				document.Add(new Paragraph("Certificado de Estudios")
								.SetFontSize(20)
								.SetBold()
								.SetTextAlignment(TextAlignment.CENTER));

				// Datos del alumno
				document.Add(new Paragraph($"Nombre: {datos.ApellidoyNombre}")
								.SetFontSize(12)
								.SetTextAlignment(TextAlignment.LEFT));
				document.Add(new Paragraph($"Tipo de Documento: {datos.TipoDocumentoCertificado}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Nro Documento: {datos.NroDocumento}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Fecha de Nacimiento: {datos.FechadeNacimiento}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Lugar de Nacimiento: {datos.LugarNacimiento}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Teléfono: {datos.NroTelefono}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Título Habilitante: {datos.TituloHabilitante}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Legajo: {datos.Legajo}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Libro Matriz: {datos.LibroMatriz}")
								.SetFontSize(12));
				document.Add(new Paragraph($"Fecha de Emisión: {datos.Fecha}")
								.SetFontSize(12));

				// Agregar espacio antes de la tabla
				document.Add(new Paragraph(" "));

				// Tabla de materias
				iText.Layout.Element.Table table = new iText.Layout.Element.Table(8).UseAllAvailableWidth(); // 8 columnas para la tabla de materias
				table.AddHeaderCell(new Cell().Add(new Paragraph("Asignatura").SetBold()));
				table.AddHeaderCell(new Cell().Add(new Paragraph("Valor Nota").SetBold()));
				table.AddHeaderCell(new Cell().Add(new Paragraph("Nota Letra").SetBold()));
				table.AddHeaderCell(new Cell().Add(new Paragraph("Libro").SetBold()));
				table.AddHeaderCell(new Cell().Add(new Paragraph("Folio").SetBold()));
				table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha").SetBold()));
				table.AddHeaderCell(new Cell().Add(new Paragraph("Condición Actual").SetBold()));
				table.AddHeaderCell(new Cell().Add(new Paragraph("Sede").SetBold()));

				// Filas de la tabla
				foreach (var fila in datos.FilasTabla)
				{
					table.AddCell(new Cell().Add(new Paragraph(fila.Asignatura)));
					table.AddCell(new Cell().Add(new Paragraph(fila.ValorNota.ToString())));
					table.AddCell(new Cell().Add(new Paragraph(fila.NotaLetra)));
					table.AddCell(new Cell().Add(new Paragraph(fila.Libro)));
					table.AddCell(new Cell().Add(new Paragraph(fila.Folio)));
					table.AddCell(new Cell().Add(new Paragraph($"{fila.Dia}/{fila.Mes}/{fila.Anno}")));
					table.AddCell(new Cell().Add(new Paragraph(fila.CondicionActual)));
					table.AddCell(new Cell().Add(new Paragraph(fila.Sede)));
				}

				document.Add(table);

				document.Close();
				return stream.ToArray();
			}
		}

		private static string NumeroANombre(int numero)
        {
            switch (numero)
            {
                case 0: return "Cero";
                case 1: return "Uno";
                case 2: return "Dos";
                case 3: return "Tres";
                case 4: return "Cuatro";
                case 5: return "Cinco";
                case 6: return "Seis";
                case 7: return "Siete";
                case 8: return "Ocho";
                case 9: return "Nueve";
                case 10: return "Diez";

                default: return "-";
            }
        }

    }
}
