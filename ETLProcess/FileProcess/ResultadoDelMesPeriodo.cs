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
    public class ResultadoDelMesPeriodo : IProcessData
    {
        public const string FileName = "Resultado del Mes.xls - PERIODO";

        public void Execute(ExcelApp.Workbook excelWorkbook, IDapper dapper, ILogger logger)
        {
            try
            {
                string sql = @"INSERT INTO Resultado_Del_Mes_Periodo (
                                                    Fecha_Inicio,                                                    
                                                    Fecha_Fin,
                                                    Id_Periodo,
                                                    Saldo_en_caja_fecha,
                                                    Saldo_en_caja,
                                                    Saldo_cuenta_dolares_fecha,
                                                    Saldo_cuenta_dolares,
                                                    Saldo_cuentas_de_subagentes_fecha, 
                                                    Saldo_cuentas_de_subagentes,
                                                    Valor_stock_raspaditas_fecha,
                                                    Valor_stock_raspaditas,
                                                    Valor_stock_rasp_mostrador_fecha,
                                                    Valor_stock_rasp_mostrador,
                                                    Aciertos_pagos_cruzados_inicio,
                                                    Aciertos_pagos_cruzados_fin,
                                                    Aciertos_pagos_cruzados,
                                                    Aciertos_no_cobrados_inicio,
                                                    Aciertos_no_cobrados_fin,
                                                    Aciertos_no_cobrados,
                                                    Saldo_comision_mostrador_fecha,
                                                    Saldo_comision_mostrador,
                                                    Aportes_pozos_cinco_de_oro_fecha,
                                                    Aportes_pozos_cinco_de_oro,
                                                    Recargas_inicio,
                                                    Recargas_fin,
                                                    Recargas,
                                                    Compras_raspaditas_inicio,
                                                    Compras_raspaditas_fin,
                                                    Compras_raspaditas,
                                                    Premios_raspaditas_inicio,
                                                    Premios_raspaditas_fin,
                                                    premios_raspaditas) 
                                               VALUES (
                                                    @Fecha_Inicio,                                                    
                                                    @Fecha_Fin,
                                                    @Id_Periodo,
                                                    @Saldo_en_caja_fecha,
                                                    @Saldo_en_caja,
                                                    @Saldo_cuenta_dolares_fecha,
                                                    @Saldo_cuenta_dolares,
                                                    @Saldo_cuentas_de_subagentes_fecha, 
                                                    @Saldo_cuentas_de_subagentes,
                                                    @Valor_stock_raspaditas_fecha,
                                                    @Valor_stock_raspaditas,
                                                    @Valor_stock_rasp_mostrador_fecha,
                                                    @Valor_stock_rasp_mostrador,
                                                    @Aciertos_pagos_cruzados_inicio,
                                                    @Aciertos_pagos_cruzados_fin,
                                                    @Aciertos_pagos_cruzados,
                                                    @Aciertos_no_cobrados_inicio,
                                                    @Aciertos_no_cobrados_fin,
                                                    @Aciertos_no_cobrados,
                                                    @Saldo_comision_mostrador_fecha,
                                                    @Saldo_comision_mostrador,
                                                    @Aportes_pozos_cinco_de_oro_fecha,
                                                    @Aportes_pozos_cinco_de_oro,
                                                    @Recargas_inicio,
                                                    @Recargas_fin,
                                                    @Recargas,
                                                    @Compras_raspaditas_inicio,
                                                    @Compras_raspaditas_fin,
                                                    @Compras_raspaditas,
                                                    @Premios_raspaditas_inicio,
                                                    @Premios_raspaditas_fin,
                                                    @premios_raspaditas)";

                using (var connection = dapper.GetDbconnection(true)) // true => se comunica a la base de datos transaccional
                {
                    connection.Open();

                    using (var tran = connection.BeginTransaction())
                    {
                        int sheet = 1;
                        ExcelApp._Worksheet excelSheet = excelWorkbook.Sheets[sheet];
                        ExcelApp.Range excelRange = excelSheet.UsedRange;

                        int rows = excelRange.Rows.Count;
                        ObjectResumenDeJuegoPeriodo obj = new ObjectResumenDeJuegoPeriodo();

                        try
                        {
                            string sqlGetPeriod = @"Select Id from Period where Active_Flag = 'S'";
                            var idPeriodo = connection.ExecuteScalar(sqlGetPeriod, param: null, transaction: tran);

                            if (idPeriodo == null)
                                throw new Exception("No existe periodo activo");
                            
                            obj.Id_Periodo = decimal.Parse(idPeriodo.ToString());

                            string sqlDelete = @"Delete from Resultado_Del_Mes_Periodo where Id_Periodo = @Id_Periodo";
                            connection.Execute(sqlDelete, obj, transaction: tran);
                        }
                        catch (Exception)
                        {
                            logger.LogError($"Error al borrar registros del mismo periodo.");
                            throw new Exception();
                        }

                        obj.Fecha_Inicio = DateTime.Now.AddDays((DateTime.Now.Day * -1) + 1);
                        obj.Fecha_Fin = DateTime.Now;

                        try
                        {
                            obj.Saldo_en_caja_fecha = DateTime.Parse(excelRange.Cells[5, 3].Value2.ToString());
                            obj.Saldo_en_caja = Decimal.Parse(excelRange.Cells[5, 7].Value2.ToString());
                            obj.Saldo_cuenta_dolares_fecha = DateTime.Parse(excelRange.Cells[7, 3].Value2.ToString());
                            obj.Saldo_cuenta_dolares = Decimal.Parse(excelRange.Cells[7, 7].Value2.ToString());
                            obj.Saldo_cuentas_de_subagentes_fecha = DateTime.Parse(excelRange.Cells[8, 3].Value2.ToString());
                            obj.Saldo_cuentas_de_subagentes = Decimal.Parse(excelRange.Cells[8, 7].Value2.ToString());
                            obj.Valor_stock_raspaditas_fecha = DateTime.Parse(excelRange.Cells[9, 3].Value2.ToString());
                            obj.Valor_stock_raspaditas = Decimal.Parse(excelRange.Cells[9, 7].Value2.ToString());
                            obj.Valor_stock_rasp_mostrador_fecha = DateTime.Parse(excelRange.Cells[10, 3].Value2.ToString());
                            obj.Valor_stock_rasp_mostrador = Decimal.Parse(excelRange.Cells[10, 7].Value2.ToString());
                            obj.Aciertos_pagos_cruzados_inicio = DateTime.Parse(excelRange.Cells[11, 3].Value2.ToString());
                            obj.Aciertos_pagos_cruzados_fin = DateTime.Parse(excelRange.Cells[11, 5].Value2.ToString());
                            obj.Aciertos_pagos_cruzados = Decimal.Parse(excelRange.Cells[12, 7].Value2.ToString());
                            obj.Aciertos_no_cobrados_inicio = DateTime.Parse(excelRange.Cells[15, 3].Value2.ToString());
                            obj.Aciertos_no_cobrados_fin = DateTime.Parse(excelRange.Cells[15, 5].Value2.ToString());
                            obj.Aciertos_no_cobrados = Decimal.Parse(excelRange.Cells[15, 7].Value2.ToString());
                            obj.Saldo_comision_mostrador_fecha = DateTime.Parse(excelRange.Cells[16, 3].Value2.ToString());
                            obj.Saldo_comision_mostrador = Decimal.Parse(excelRange.Cells[16, 7].Value2.ToString());
                            obj.Aportes_pozos_cinco_de_oro_fecha = DateTime.Parse(excelRange.Cells[17, 3].Value2.ToString());
                            obj.Aportes_pozos_cinco_de_oro = Decimal.Parse(excelRange.Cells[17, 7].Value2.ToString());
                            obj.Recargas_inicio = DateTime.Parse(excelRange.Cells[18, 3].Value2.ToString());
                            obj.Recargas_fin = DateTime.Parse(excelRange.Cells[18, 5].Value2.ToString());
                            obj.Recargas = Decimal.Parse(excelRange.Cells[18, 7].Value2.ToString());
                            obj.Compras_raspaditas_inicio = DateTime.Parse(excelRange.Cells[21, 3].Value2.ToString());
                            obj.Compras_raspaditas_fin = DateTime.Parse(excelRange.Cells[21, 5].Value2.ToString());
                            obj.Compras_raspaditas = Decimal.Parse(excelRange.Cells[21, 7].Value2.ToString());
                            obj.Premios_raspaditas_inicio = DateTime.Parse(excelRange.Cells[22, 3].Value2.ToString());
                            obj.Premios_raspaditas_fin = DateTime.Parse(excelRange.Cells[22, 5].Value2.ToString());
                            obj.premios_raspaditas = Decimal.Parse(excelRange.Cells[22, 7].Value2.ToString());
                        }
                        catch
                        {
                            logger.LogError($"Error al convertir datos.");
                            throw new Exception();
                        }

                        connection.Execute(sql, obj, transaction: tran);

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

        private class ObjectResumenDeJuegoPeriodo
        {
            public DateTime Fecha_Inicio { get; set; }
            public DateTime Fecha_Fin { get; set; }
            public Decimal Id_Periodo { get; set; }
            public DateTime Saldo_en_caja_fecha { get; set; }
            public Decimal Saldo_en_caja { get; set; }
            public DateTime Saldo_cuenta_dolares_fecha { get; set; }
            public Decimal Saldo_cuenta_dolares { get; set; }
            public DateTime Saldo_cuentas_de_subagentes_fecha { get; set; }
            public Decimal Saldo_cuentas_de_subagentes { get; set; }
            public DateTime Valor_stock_raspaditas_fecha { get; set; }
            public Decimal Valor_stock_raspaditas { get; set; }
            public DateTime Valor_stock_rasp_mostrador_fecha { get; set; }
            public Decimal Valor_stock_rasp_mostrador { get; set; }
            public DateTime Aciertos_pagos_cruzados_inicio { get; set; }
            public DateTime Aciertos_pagos_cruzados_fin { get; set; }
            public Decimal Aciertos_pagos_cruzados { get; set; }
            public DateTime Aciertos_no_cobrados_inicio { get; set; }
            public DateTime Aciertos_no_cobrados_fin { get; set; }
            public Decimal Aciertos_no_cobrados { get; set; }
            public DateTime Saldo_comision_mostrador_fecha { get; set; }
            public Decimal Saldo_comision_mostrador { get; set; }
            public DateTime Aportes_pozos_cinco_de_oro_fecha { get; set; }
            public Decimal Aportes_pozos_cinco_de_oro { get; set; }
            public DateTime Recargas_inicio { get; set; }
            public DateTime Recargas_fin { get; set; }
            public Decimal Recargas { get; set; }
            public DateTime Compras_raspaditas_inicio { get; set; }
            public DateTime Compras_raspaditas_fin { get; set; }
            public Decimal Compras_raspaditas { get; set; }
            public DateTime Premios_raspaditas_inicio { get; set; }
            public DateTime Premios_raspaditas_fin { get; set; }
            public Decimal premios_raspaditas { get; set; }
        }
    }
}
