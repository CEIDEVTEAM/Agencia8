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
    public class SaldosSubAgentes : IProcessData
    {
        public const string FileName = "Saldo Cuentas Subagentes.xls";

        public void Execute(ExcelApp.Workbook excelWorkbook, IDapper dapper, ILogger logger)
        {
            try
            {
                string sql = @"INSERT INTO Saldo_Cuentas_Subagentes (
                                                   Fecha,
                                                   Sub_Agente,
                                                   Ultima_Cobranza,
                                                   Saldo_Cuenta,
                                                   Deuda_Juego,
                                                   Deuda_Raspadita,
                                                   Pagos,
                                                   Ultima_Deuda_Juego_Cobrada,
                                                   Ultima_Deuda_Rasp_Cobrada,
                                                   Deuda_Viva_Juego,
                                                   Deuda_Viva,
                                                   Deuda_Viva_Raspadita,
                                                   Saldo_Final)
                                               VALUES (
                                                   @Fecha,
                                                   @Sub_Agente,
                                                   @Ultima_Cobranza,
                                                   @Saldo_Cuenta,
                                                   @Deuda_Juego,
                                                   @Deuda_Raspadita,
                                                   @Pagos,
                                                   @Ultima_Deuda_Juego_Cobrada,
                                                   @Ultima_Deuda_Rasp_Cobrada,
                                                   @Deuda_Viva_Juego,
                                                   @Deuda_Viva,
                                                   @Deuda_Viva_Raspadita,
                                                   @Saldo_Final)";

                using (var connection = dapper.GetDbconnection())
                {
                    connection.Open();

                    using (var tran = connection.BeginTransaction())
                    {
                        int sheet = 1;
                        ExcelApp._Worksheet excelSheet = excelWorkbook.Sheets[sheet];
                        ExcelApp.Range excelRange = excelSheet.UsedRange;

                        int rows = excelRange.Rows.Count;
                        ObjectSaldoSubAgente obj = new ObjectSaldoSubAgente();

                        try
                        {
                            string date = excelRange.Cells[2, 2].Value2.ToString();
                            date = date.Substring(3, 10);
                            obj.Fecha = DateTime.Parse(date);
                        }
                        catch
                        {
                            logger.LogError($"Error al convertir fecha, hoja:{sheet}");
                            throw new Exception();
                        }

                        for (int r = 4; r <= rows -1; r++)
                        {
                            try
                            {
                                obj.Sub_Agente = excelRange.Cells[r, 2].Value2.ToString();
                                obj.Ultima_Cobranza = DateTime.FromOADate(double.Parse(excelRange.Cells[r, 3].Value2.ToString()));
                                obj.Saldo_Cuenta = Decimal.Parse(excelRange.Cells[r, 4].Value2.ToString());
                                obj.Deuda_Juego = Decimal.Parse(excelRange.Cells[r, 5].Value2.ToString());
                                obj.Deuda_Raspadita = Decimal.Parse(excelRange.Cells[r, 6].Value2.ToString());
                                obj.Pagos = Decimal.Parse(excelRange.Cells[r, 7].Value2.ToString());
                                obj.Ultima_Deuda_Juego_Cobrada = DateTime.FromOADate(double.Parse(excelRange.Cells[r, 8].Value2.ToString()));
                                obj.Ultima_Deuda_Rasp_Cobrada = DateTime.FromOADate(double.Parse(excelRange.Cells[r, 9].Value2.ToString()));
                                obj.Deuda_Viva_Juego = Decimal.Parse(excelRange.Cells[r, 10].Value2.ToString());
                                obj.Deuda_Viva = Decimal.Parse(excelRange.Cells[r, 11].Value2.ToString());
                                obj.Deuda_Viva_Raspadita = Decimal.Parse(excelRange.Cells[r, 12].Value2.ToString());
                                obj.Saldo_Final = Decimal.Parse(excelRange.Cells[r, 13].Value2.ToString());
                            }
                            catch
                            {
                                logger.LogError($"Error al convertir datos en la fila: {r}, hoja: {sheet}");
                                throw new Exception();
                            }

                            connection.Execute(sql, obj, transaction: tran);

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

        private void SetObjectEntityDefaultValues(ObjectSaldoSubAgente obj)
        {
            obj.Sub_Agente = "";
            obj.Ultima_Cobranza = null;
            obj.Saldo_Cuenta = null;
            obj.Deuda_Juego = null;
            obj.Deuda_Raspadita = null;
            obj.Pagos = null;
            obj.Ultima_Deuda_Juego_Cobrada = null;
            obj.Ultima_Deuda_Rasp_Cobrada = null;
            obj.Deuda_Viva_Juego = null;
            obj.Deuda_Viva = null;
            obj.Deuda_Viva_Raspadita = null;
            obj.Saldo_Final = null;
        }

        private class ObjectSaldoSubAgente
        {
            public DateTime Fecha { get; set; }
            public string Sub_Agente { get; set; }
            public DateTime? Ultima_Cobranza { get; set; }
            public Decimal? Saldo_Cuenta { get; set; }
            public Decimal? Deuda_Juego { get; set; }
            public Decimal? Deuda_Raspadita { get; set; }
            public Decimal? Pagos { get; set; }
            public DateTime? Ultima_Deuda_Juego_Cobrada { get; set; }
            public DateTime? Ultima_Deuda_Rasp_Cobrada { get; set; }
            public Decimal? Deuda_Viva_Juego { get; set; }
            public Decimal? Deuda_Viva { get; set; }
            public Decimal? Deuda_Viva_Raspadita { get; set; }
            public Decimal? Saldo_Final { get; set; }

        }
    }
}
