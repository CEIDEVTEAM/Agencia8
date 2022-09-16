using Dapper;
using ETLProcess.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace ETLProcess.FileProcess
{
    public class FileTest : IProcessData
    {
        public const string FileName = "test2.xlsx";

        public void Execute(ExcelApp.Range excelRange, IDapper dapper, ILogger logger)
        {
            try
            {
                int rows = excelRange.Rows.Count;
                string sql = @"INSERT INTO TEST_TABLE (Name, LastName, Age) 
                                    VALUES (@name, @lastName, @age)";

                using (var connection = dapper.GetDbconnection())
                {
                    connection.Open();

                    using (var tran = connection.BeginTransaction())
                    {
                        ObjectEntity obj = new ObjectEntity();

                        for (int i = 2; i <= rows; i++)
                        {
                            try
                            {
                                obj.name = excelRange.Cells[i, 1].Value2.ToString();
                                obj.lastName = excelRange.Cells[i, 2].Value2.ToString();
                                obj.age = int.Parse(excelRange.Cells[i, 3].Value2.ToString());
                            }
                            catch (Exception)
                            {
                                logger.LogError($"Error a convertir datos en la fila: {i}");
                                throw new Exception();
                            }

                            connection.Execute(sql, obj, transaction: tran);

                            SetObjectEntityDefaultValues(obj);
                        }

                        tran.Commit();
                        logger.LogInformation($"Archivo {FileName} mapeado con éxito. Lineas procesadas: {rows}.");
                    }
                }


            }
            catch (Exception)
            {
                throw new Exception($"Error al mapear {FileName} en la base de datos");
            }

        }

        private void SetObjectEntityDefaultValues(ObjectEntity obj)
        {
            obj.name = string.Empty;
            obj.lastName = string.Empty;
            obj.age = null;
        }

        public class ObjectEntity
        {
            public string name { get; set; }
            public string lastName { get; set; }
            public int? age { get; set; }
        }
    }
}
