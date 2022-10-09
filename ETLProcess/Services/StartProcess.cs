using System.Data;
using System.Runtime.InteropServices;
using ETLProcess.Configuration;
using ETLProcess.FileProcess;
using ETLProcess.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace ETLProcess.Services
{
    public class StartProcess : IStartProcess
    {
        private IDapper _dapper;
        private ILogger _logger;
        private IOptions<Documents> _documents;

        public StartProcess(ILogger<StartProcess> logger, IDapper dapper, IOptions<Documents> documents)
        {
            _dapper = dapper;
            _logger = logger;
            _documents = documents;
        }

        public void Start()
        {
            ExcelApp.Application excelApp = new ExcelApp.Application();
            Dictionary<string, string> processedFiles = new Dictionary<string, string>();
            try
            {
                _logger.LogInformation("Inicio de ejecución");

                List<Documents.Doc> docsReadyToRead = GetDocuments(excelApp);

                if (docsReadyToRead.Any())
                {
                    foreach (var item in docsReadyToRead)
                    {
                        try
                        {
                            ExcelApp.Workbook excelBook = excelApp.Workbooks.Open($"{item.FolderRoute}{item.FileName}");

                            IProcessData processData = GetStrategyProcess(item.Strategy, item.FileName);
                            processData.Execute(excelBook, _dapper, _logger);

                            ReleaseObject.ReleaseObjectService(excelBook);
                            processedFiles.Add($"{item.FolderRoute}{item.FileName}", "PROCESADO");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            processedFiles.Add($"{item.FolderRoute}{item.FileName}", "ERROR");
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);

                _logger.LogInformation("Pausa por 5 seg para dar tiempo a que libere los archivos procesados");
                Thread.Sleep(5000); 
                UpdateFileNames(processedFiles);
            }
        }

        public List<Documents.Doc> GetDocuments(ExcelApp.Application excelApp)
        {
            _logger.LogInformation("Iniciando validacion de documentos");

            var documents = this._documents.Value;
            var validateDocs = new List<Documents.Doc>();

            foreach (var item in documents.ColDocuments)
            {
                try
                {
                    ExcelApp.Workbook validateDoc = null;
                    validateDoc = excelApp.Workbooks.Open($"{item.FolderRoute}{item.FileName}");

                    if (validateDoc != null)
                        validateDocs.Add(item);

                    ReleaseObject.ReleaseObjectService(validateDoc);
                }
                catch (Exception)
                {
                    _logger.LogError($"No se encuentra el archivo {item.FolderRoute}{item.FileName}");
                }
            }

            _logger.LogInformation($"Validacion finalizada. Documentos para procesar: {validateDocs.Count}.");

            return validateDocs;
        }

        public IProcessData GetStrategyProcess(string strategy, string fileName)
        {
            IProcessData processData;

            switch (strategy)
            {
                case "LiquidacionMensual":
                    processData = new LiquidacionMensual();
                    break;
                case "ResumenDeJuego":
                    processData = new ResumenDeJuego();
                    break;
                case "ResultadoDelMes":
                    processData = new ResultadoDelMes();
                    break;
                default:
                    throw new Exception($"Lectura no implementada para el archivo {fileName}");
            }

            return processData;
        }

        private void UpdateFileNames(Dictionary<string, string> processedFiles)
        {
            foreach (var item in processedFiles)
            {
                var split = item.Key.Split('.');
                string fileName = @"";
                for (int i = 0; i < split.Length; i++)
                {
                    if (i == split.Length - 1)
                        fileName += $"-{item.Value}.";

                    fileName += $"{split[i]}";
                }

                File.Move(item.Key, fileName);

                _logger.LogInformation($"Nombre de archivo modificado: {item.Key} => {fileName}");
            };
        }
    }
}
