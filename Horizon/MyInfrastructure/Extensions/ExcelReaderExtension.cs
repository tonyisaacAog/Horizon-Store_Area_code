using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyInfrastructure.Extensions
{
    public static class ExcelReaderExtension
    {
        public static DataTable ReadExcelSheet(IFormFile file, string SheetName,int FromRow=1)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(file.OpenReadStream());
            ExcelWorksheet workSheet = package.Workbook.Worksheets.Single(x=>x.Name == SheetName);

            DataTable dt = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[FromRow, 1, 1, workSheet.Dimension.End.Column])
            {
                dt.Columns.Add(firstRowCell.Text);
            }
            for (var rowNumber = FromRow+1; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                if (!string.IsNullOrEmpty(workSheet.Cells[rowNumber, 1].Text))
                {
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }
    }
}