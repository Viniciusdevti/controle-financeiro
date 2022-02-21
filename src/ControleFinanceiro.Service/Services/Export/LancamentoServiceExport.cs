using ClosedXML.Excel;
using ControleFinanceiro.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ControleFinanceiro.Service.Services.Export
{
    public class LancamentoServiceExport
    {
        public ServiceMessage<Byte[]> Export(List<Lancamento> bar)
        {
            ServiceMessage<Byte[]> serviceMessage = new();
            try
            {

                DataTable dt = new DataTable();

                dt.TableName = "Categoria";
                //Add Columns  
                dt.Columns.Add("ID", typeof(long));
                dt.Columns.Add("Valor", typeof(string));
                dt.Columns.Add("Data", typeof(DateTime));
                dt.Columns.Add("Comentario", typeof(string));
                dt.Columns.Add("IdSubCategoria", typeof(long));


                foreach (var item in bar)
                {
                    //Add Rows in DataTable  
                    dt.Rows.Add(item.IdLancamento, item.Valor.ToString("C"), item.Data, item.Comentario, item.IdSubCategoria );
                }
                dt.AcceptChanges();

                using XLWorkbook wb = new XLWorkbook();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new MemoryStream();
                wb.SaveAs(stream);

                serviceMessage.Result = stream.ToArray();
                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.Successfull = false;
                serviceMessage.Errors = ex.Message;
                return serviceMessage;

            }
        }
    }
}
