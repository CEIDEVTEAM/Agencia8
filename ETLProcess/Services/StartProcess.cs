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
                            ExcelApp._Worksheet excelSheet = excelBook.Sheets[1];
                            ExcelApp.Range excelRange = excelSheet.UsedRange;

                            IProcessData processData = GetStrategyProcess(item.FileName);
                            processData.Execute(excelRange, _dapper, _logger);

                            ReleaseObject(excelBook);
                            ReleaseObject(excelSheet);
                            ReleaseObject(excelRange);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
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

                    ReleaseObject(validateDoc);
                }
                catch (Exception)
                {
                    _logger.LogError($"No se encuentra el archivo {item.FolderRoute}{item.FileName}");
                }
            }

            _logger.LogInformation($"Validacion finalizada. Documentos para procesar: {validateDocs.Count}.");

            return validateDocs;
        }

        public IProcessData GetStrategyProcess(string fileName)
        {
            IProcessData strategy;

            switch (fileName)
            {
                case "test2.xlsx":
                    strategy = new FileTest();
                    break;
                default:
                    throw new Exception($"Lectura no implementada para el archivo {fileName}");
            }

            return strategy;
        }

        public static void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
