using BusinessLogic.DTOs.Dependent;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public static class DnlqWebCrawling
    {
        public static List<ExternalDependentDTO> ProcessDNLQData()
        {
            List<string> errors = new List<string>();
            bool successful = false;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www2.loteria.gub.uy/agencias_quiniela.php?xAgencia=2&vBanca=17");

            var data = doc.DocumentNode.SelectSingleNode("//table").InnerText;

            var splitData = data.Split("AGENCIA&nbsp;&nbsp;6")[1].Split("AGENCIA&nbsp;&nbsp;7")[0].Split("SUBA");

            var listSubAgentes = new List<string>();

            foreach (var item in splitData)
            {
                var textItem = item.Replace("&nbsp;", String.Empty);

                var formatedText = textItem.Replace("\t", String.Empty);
                formatedText = formatedText.Replace("\n", String.Empty);
                formatedText = formatedText.Trim();
                var splitText = formatedText.Split("CORR");


                if (splitText.Length == 1)
                {
                    string result = String.Empty;
                    string subAgenteString = splitText[0];
                    var sp = subAgenteString.Split(" ");

                    foreach (var i in sp)
                    {
                        if (i.Length > 0)
                        {
                            result = result + i + " ";
                        }
                    }
                    listSubAgentes.Add(result);
                }
                if (splitText.Length > 1)
                {
                    var corredorString = splitText[1];
                    var corrResult = String.Empty;
                    var sp = corredorString.Split(" ");
                    foreach (var i in sp)
                    {
                        if (i.Length > 0)
                        {
                            corrResult = corrResult + i + " ";
                        }
                    }

                    listSubAgentes.Add(corrResult);
                }
            }
            List<ExternalDependentDTO> finalFormatedList = new List<ExternalDependentDTO>();

            for (int i = 0; i < listSubAgentes.Count; i++)
            {
                var x = listSubAgentes[i].Trim().Split("Maldonado");
                var x3 = x[1].Split(" ");
                var nombre = string.Join(" ", x3.Take(x3.Length - 1)).Trim();

                if(i == listSubAgentes.Count - 1) 
                    nombre = string.Join(" ", x3.Take(x3.Length)).Trim();

                string[] x2 = new string[0];

                if (i > 0)
                    x2 = listSubAgentes[i - 1].Trim().Split(" ");

                int number = 0;
                if (x2.Length > 0)
                {
                    string c = x2.Last();
                    bool haveNumber = int.TryParse(c, out number);
                }

                ExternalDependentDTO obj = new ExternalDependentDTO();
                obj.Number = number;
                obj.Name = nombre;
                obj.Address = x[0];

                finalFormatedList.Add(obj);
            }
            return finalFormatedList;
           
        }
    }
}
