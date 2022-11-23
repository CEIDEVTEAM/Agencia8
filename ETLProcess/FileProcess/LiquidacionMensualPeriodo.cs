using Dapper;
using ETLProcess.Services;
using ETLProcess.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace ETLProcess.FileProcess
{
    public class LiquidacionMensualPeriodo : IProcessData
    {
        public const string FileName = "Liquidacion Mensual Bancas (Todos).xls - PERIODO";

        public void Execute(ExcelApp.Workbook excelWorkbook, IDapper dapper, ILogger logger)
        {
            try
            {
                string sql = @"INSERT INTO Liquidacion_Mensual_Periodo (
                                                    Fecha_Inicio, 
                                                    Fecha_Fin, 
                                                    Id_Periodo, 
                                                    Agencia, 
                                                    Juego, 
                                                    Apuestas_Vespertinas, 
                                                    Apuestas_Nocturnas, 
                                                    Aciertos_Vespertinos,
                                                    Aciertos_Nocturnos,
                                                    Aportes) 
                                               VALUES (
                                                    @Fecha_Inicio, 
                                                    @Fecha_Fin, 
                                                    @Id_Periodo, 
                                                    @Agencia, 
                                                    @Juego, 
                                                    @Apuestas_Vespertinas, 
                                                    @Apuestas_Nocturnas, 
                                                    @Aciertos_Vespertinos,
                                                    @Aciertos_Nocturnos,
                                                    @Aportes)";

                using (var connection = dapper.GetDbconnection(true))
                {
                    connection.Open();

                    using (var tran = connection.BeginTransaction())
                    {
                        ObjectLiquidacionMensualPeriodo obj = new ObjectLiquidacionMensualPeriodo();

                        try
                        {
                            string sqlGetPeriod = @"Select Id from Period where Active_Flag = 'S'";
                            var idPeriodo = connection.ExecuteScalar(sqlGetPeriod, param: null, transaction: tran);


                            if (idPeriodo == null)
                                throw new Exception("No existe periodo activo");

                            obj.Id_Periodo = decimal.Parse(idPeriodo.ToString());

                            string sqlDelete = @"Delete from Liquidacion_Mensual_Periodo where Id_Periodo = @Id_Periodo";
                            connection.Execute(sqlDelete, obj, transaction: tran);
                        }
                        catch (Exception)
                        {
                            logger.LogError($"Error al borrar registros del mismo período.");
                            throw new Exception();
                        }

                        obj.Fecha_Inicio = DateTime.Now.AddDays((DateTime.Now.Day * -1) + 1);
                        obj.Fecha_Fin = DateTime.Now;

                        int rowStart = 3;

                        for (int sheet = 1; sheet <= 5; sheet++)
                        {
                            ExcelApp._Worksheet excelSheet = excelWorkbook.Sheets[sheet];
                            ExcelApp.Range excelRange = excelSheet.UsedRange;

                            int rows = excelRange.Rows.Count;

                            if (sheet == 1 || sheet == 2) //Quiniela y Tombola
                            {
                                string juego = sheet == 1 ? "Quiniela" : "Tombola";
                                obj.Juego = juego;

                                rowStart = sheet == 1 ? 5 : 3;

                                for (int r = rowStart; r <= rows - 1; r++)
                                {
                                    try
                                    {
                                        obj.Agencia = excelRange.Cells[r, 3].Value2.ToString();
                                        if (obj.Agencia.Length > 4 && obj.Agencia.Substring(0, 4) == "Tele")
                                            obj.Agencia = "Telefónico";
                                        obj.Apuestas_Vespertinas = Decimal.Parse(excelRange.Cells[r, 5].Value2.ToString());
                                        obj.Apuestas_Nocturnas = Decimal.Parse(excelRange.Cells[r, 6].Value2.ToString());
                                        obj.Aciertos_Vespertinos = Decimal.Parse(excelRange.Cells[r, 9].Value2.ToString());
                                        obj.Aciertos_Nocturnos = Decimal.Parse(excelRange.Cells[r, 10].Value2.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        logger.LogError($"Error al convertir datos en la fila: {r}, hoja:{sheet}");
                                        throw new Exception();
                                    }

                                    connection.Execute(sql, obj, transaction: tran);

                                    SetObjectEntityDefaultValues(obj);
                                }
                            }
                            else if (sheet == 3) //Cinco de Oro
                            {
                                obj.Juego = "Cinco de Oro";

                                for (int r = 3; r <= rows - 1; r++)
                                {
                                    try
                                    {
                                        obj.Agencia = excelRange.Cells[r, 3].Value2.ToString();
                                        if (obj.Agencia.Length > 4 && obj.Agencia.Substring(0, 4) == "Tele")
                                            obj.Agencia = "Telefónico";
                                        obj.Apuestas_Nocturnas = Decimal.Parse(excelRange.Cells[r, 5].Value2.ToString());
                                        obj.Aciertos_Nocturnos = Decimal.Parse(excelRange.Cells[r, 7].Value2.ToString());
                                        obj.Aportes = Decimal.Parse(excelRange.Cells[r, 8].Value2.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        logger.LogError($"Error a convertir datos en la fila: {r}, hoja:{sheet}");
                                        throw new Exception();
                                    }

                                    connection.Execute(sql, obj, transaction: tran);

                                    SetObjectEntityDefaultValues(obj);
                                }
                            }
                            else if (sheet == 4) //Supermatch
                            {
                                obj.Juego = "Supermatch";
                                for (int r = 3; r <= rows - 1; r++)
                                {
                                    try
                                    {
                                        obj.Agencia = excelRange.Cells[r, 3].Value2.ToString();
                                        if (obj.Agencia.Length > 4 && obj.Agencia.Substring(0, 4) == "Tele")
                                            obj.Agencia = "Telefónico";
                                        obj.Apuestas_Nocturnas = Decimal.Parse(excelRange.Cells[r, 4].Value2.ToString());
                                        obj.Aciertos_Nocturnos = Decimal.Parse(excelRange.Cells[r, 6].Value2.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        logger.LogError($"Error a convertir datos en la fila: {r}, hoja:{sheet}");
                                        throw new Exception();
                                    }

                                    connection.Execute(sql, obj, transaction: tran);

                                    SetObjectEntityDefaultValues(obj);
                                }
                            }
                            else if (sheet == 5) //Pines
                            {
                                obj.Juego = "Pines";

                                for (int r = 3; r <= rows - 1; r++)
                                {
                                    try
                                    {
                                        obj.Agencia = excelRange.Cells[r, 3].Value2.ToString();
                                        if (obj.Agencia.Length > 4 && obj.Agencia.Substring(0, 4) == "Tele")
                                            obj.Agencia = "Telefónico";
                                        obj.Apuestas_Nocturnas = Decimal.Parse(excelRange.Cells[r, 4].Value2.ToString());
                                        obj.Aciertos_Nocturnos = Decimal.Parse(excelRange.Cells[r, 7].Value2.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        logger.LogError($"Error a convertir datos en la fila: {r}, hoja:{sheet}");
                                        throw new Exception();
                                    }

                                    connection.Execute(sql, obj, transaction: tran);

                                    SetObjectEntityDefaultValues(obj);
                                }
                            }

                            ReleaseObject.ReleaseObjectService(excelSheet);
                            ReleaseObject.ReleaseObjectService(excelRange);
                        }

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

        private void SetObjectEntityDefaultValues(ObjectLiquidacionMensualPeriodo obj)
        {
            obj.Agencia = string.Empty;
            obj.Apuestas_Vespertinas = null;
            obj.Apuestas_Nocturnas = null;
            obj.Aciertos_Vespertinos = null;
            obj.Aciertos_Nocturnos = null;
            obj.Aportes = null;
        }

        private class ObjectLiquidacionMensualPeriodo
        {
            public DateTime Fecha_Inicio { get; set; }
            public DateTime Fecha_Fin { get; set; }
            public Decimal Id_Periodo { get; set; }
            public string Agencia { get; set; }
            public string Juego { get; set; }
            public Decimal? Apuestas_Vespertinas { get; set; }
            public Decimal? Apuestas_Nocturnas { get; set; }
            public Decimal? Aciertos_Vespertinos { get; set; }
            public Decimal? Aciertos_Nocturnos { get; set; }
            public Decimal? Aportes { get; set; }
        }
    }
}
