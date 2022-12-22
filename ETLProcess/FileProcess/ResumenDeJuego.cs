using Dapper;
using ETLProcess.Services;
using ETLProcess.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace ETLProcess.FileProcess
{
    public class ResumenDeJuego : IProcessData
    {
        public const string FileName = "Resumen de Juego.xls";

        public void Execute(ExcelApp.Workbook excelWorkbook, IDapper dapper, ILogger logger)
        {
            try
            {
                string sql = @"INSERT INTO Resumen_De_Juego (
                                                    Fecha, 
                                                    Sub_Agente, 
                                                    Quiniela, 
                                                    Tombola, 
                                                    Oro, 
                                                    Loteria,
                                                    Deportivo,
                                                    Pin,
                                                    Bruto,
                                                    Comision,
                                                    Servicio,
                                                    Total) 
                                               VALUES (
                                                    @Fecha, 
                                                    @Sub_Agente, 
                                                    @Quiniela, 
                                                    @Tombola, 
                                                    @Oro, 
                                                    @Loteria,
                                                    @Deportivo,
                                                    @Pin,
                                                    @Bruto,
                                                    @Comision,
                                                    @Servicio,
                                                    @Total)";

                using (var connection = dapper.GetDbconnection())
                {
                    connection.Open();

                    using (var tran = connection.BeginTransaction())
                    {
                        int sheet = 1;
                        ExcelApp._Worksheet excelSheet = excelWorkbook.Sheets[sheet];
                        ExcelApp.Range excelRange = excelSheet.UsedRange;

                        int rows = excelRange.Rows.Count;
                        ObjectResumenDeJuego obj = new ObjectResumenDeJuego();

                        try
                        {
                            string date = excelRange.Cells[2, 3].Value2.ToString();
                            //date = date.Substring(0, date.Length - 1);
                            obj.Fecha = DateTime.Parse(date);
                        }
                        catch
                        {
                            logger.LogError($"Error al convertir fecha, hoja:{sheet}");
                            throw new Exception();
                        }

                        try
                        {
                            string sqlGetPeriod = @"Select Sub_Agente from Resumen_De_Juego where Fecha = @Fecha";
                            var idPeriodo = connection.ExecuteScalar(sqlGetPeriod, obj, transaction: tran);

                            if (idPeriodo != null)
                            {
                                logger.LogInformation($"Borrando registros previos con misma fecha ({obj.Fecha})");

                                string sqlDelete = @"Delete from Resumen_De_Juego where Fecha = @Fecha";
                                connection.Execute(sqlDelete, obj, transaction: tran);
                            }
                        }
                        catch (Exception)
                        {
                            logger.LogError($"Error al borrar registros del mismo día.");
                            throw new Exception();
                        }

                        for (int r = 4; r <= rows - 1; r++)
                        {
                            try
                            {
                                obj.Sub_Agente = excelRange.Cells[r, 2].Value2.ToString();
                                obj.Quiniela = Decimal.Parse(excelRange.Cells[r, 3].Value2.ToString());
                                obj.Tombola = Decimal.Parse(excelRange.Cells[r, 4].Value2.ToString());
                                obj.Oro = Decimal.Parse(excelRange.Cells[r, 5].Value2.ToString());
                                obj.Loteria = Decimal.Parse(excelRange.Cells[r, 6].Value2.ToString());
                                obj.Deportivo = Decimal.Parse(excelRange.Cells[r, 7].Value2.ToString());
                                obj.Pin = Decimal.Parse(excelRange.Cells[r, 8].Value2.ToString());
                                obj.Bruto = Decimal.Parse(excelRange.Cells[r, 9].Value2.ToString());
                                obj.Comision = Decimal.Parse(excelRange.Cells[r, 10].Value2.ToString());
                                obj.Servicio = Decimal.Parse(excelRange.Cells[r, 11].Value2.ToString());
                                obj.Total = Decimal.Parse(excelRange.Cells[r, 12].Value2.ToString());


                                connection.Execute(sql, obj, transaction: tran);
                            }
                            catch
                            {
                                logger.LogError($"Error al convertir datos en la fila: {r}, hoja: {sheet}");
                                //throw new Exception();
                            }

                            SetObjectEntityDefaultValues(obj);
                        }

                        ReleaseObject.ReleaseObjectService(excelSheet);
                        ReleaseObject.ReleaseObjectService(excelRange);

                        tran.Commit();
                        logger.LogInformation($"Archivo {FileName} mapeado con éxito.");
                    }
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception($"Error al mapear {FileName} en la base de datos");
            }
        }

        private void SetObjectEntityDefaultValues(ObjectResumenDeJuego obj)
        {
            obj.Sub_Agente = string.Empty;
            obj.Quiniela = null;
            obj.Tombola = null;
            obj.Oro = null;
            obj.Loteria = null;
            obj.Deportivo = null;
            obj.Pin = null;
            obj.Bruto = null;
            obj.Comision = null;
            obj.Servicio = null;
            obj.Total = null;
        }

        private class ObjectResumenDeJuego
        {
            public DateTime Fecha { get; set; }
            public string Sub_Agente { get; set; }
            public Decimal? Quiniela { get; set; }
            public Decimal? Tombola { get; set; }
            public Decimal? Oro { get; set; }
            public Decimal? Loteria { get; set; }
            public Decimal? Deportivo { get; set; }
            public Decimal? Pin { get; set; }
            public Decimal? Bruto { get; set; }
            public Decimal? Comision { get; set; }
            public Decimal? Servicio { get; set; }
            public Decimal? Total { get; set; }
        }
    }
}
