using Dapper;
using ETLProcess.Services;
using ETLProcess.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace ETLProcess.FileProcess
{
    public class LiquidacionMensual : IProcessData
    {
        public const string FileName = "Liquidacion Mensual Bancas (Todos).xls";

        public void Execute(ExcelApp.Workbook excelWorkbook, IDapper dapper, ILogger logger)
        {
            try
            {
                string sql = @"INSERT INTO Liquidacion_Mensual (
                                                    Fecha, 
                                                    Agencia, 
                                                    Juego, 
                                                    Apuestas_Vespertinas, 
                                                    Apuestas_Nocturnas, 
                                                    Aciertos_Vespertinos,
                                                    Aciertos_Nocturnos,
                                                    Aportes) 
                                               VALUES (
                                                    @Fecha, 
                                                    @Agencia, 
                                                    @Juego, 
                                                    @Apuestas_Vespertinas, 
                                                    @Apuestas_Nocturnas, 
                                                    @Aciertos_Vespertinos,
                                                    @Aciertos_Nocturnos,
                                                    @Aportes)";

                using (var connection = dapper.GetDbconnection())
                {
                    connection.Open();

                    using (var tran = connection.BeginTransaction())
                    {
                        ObjectLiquidacionMensual obj = new ObjectLiquidacionMensual();

                        ExcelApp._Worksheet excelSheet = excelWorkbook.Sheets[1];
                        ExcelApp.Range excelRange = excelSheet.UsedRange;

                        string strDate = excelRange.Cells[2, 2].Value2.ToString();

                        strDate = strDate.Substring(6, 11);

                        if (!DateTime.TryParse(strDate, out DateTime date))
                        {
                            logger.LogError($"Error al acceder a la fecha del archivo");
                            throw new Exception();
                        }

                        obj.Fecha = date;

                        try
                        {
                            string sqlGetPeriod = @"Select Juego from Liquidacion_Mensual where Fecha = @Fecha";
                            var idPeriodo = connection.ExecuteScalar(sqlGetPeriod, obj, transaction: tran);

                            if (idPeriodo != null)
                            {
                                logger.LogInformation($"Borrando registros previos con misma fecha ({date})");

                                string sqlDelete = @"Delete from Liquidacion_Mensual where Fecha = @Fecha";
                                connection.Execute(sqlDelete, obj, transaction: tran);
                            }
                        }
                        catch (Exception)
                        {
                            logger.LogError($"Error al borrar registros del mismo día.");
                            throw new Exception();
                        }

                        int rowStart = 3;

                        for (int sheet = 1; sheet <= 5; sheet++)
                        {
                            excelSheet = excelWorkbook.Sheets[sheet];
                            excelRange = excelSheet.UsedRange;

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

        private void SetObjectEntityDefaultValues(ObjectLiquidacionMensual obj)
        {
            obj.Agencia = string.Empty;
            obj.Apuestas_Vespertinas = null;
            obj.Apuestas_Nocturnas = null;
            obj.Aciertos_Vespertinos = null;
            obj.Aciertos_Nocturnos = null;
            obj.Aportes = null;
        }

        private class ObjectLiquidacionMensual
        {
            public DateTime Fecha { get; set; }
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
