using Microsoft.Office.Interop.Excel;
using OptimizationFiltering.Models;
using OptimizationFiltering.Parametres;
using System;
using System.Reflection;

namespace OptimizationFiltering
{
    internal static class ExportExcel
    {
        private static Microsoft.Office.Interop.Excel.Application? _Excel;
        private static Workbook? _Workbook;
        private static Worksheet? _Worksheet;

        public static void Export(Method selectedMethod, Task selectedTask, InputParameters inputParameter, Limitations limitation, OutputParameters output)
        {
            _Excel = new Microsoft.Office.Interop.Excel.Application();
            _Workbook = _Excel.Workbooks.Add();
            _Worksheet = (Worksheet)_Workbook.ActiveSheet;
            _Worksheet.Columns.AutoFit();
            _Worksheet.Name = "Отчет";

            _Worksheet.Cells[1, 1] = "Метод оптимизации";
            _Worksheet.Cells[1, 1].Font.Bold = true;
            _Worksheet.Cells[1, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[1, 2] = selectedMethod.Name;
            _Worksheet.Cells[1, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[3, 1] = "Задание";
            _Worksheet.Cells[3, 1].Font.Bold = true;
            _Worksheet.Cells[3, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[3, 2] = selectedTask.Name;
            _Worksheet.Cells[3, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[5, 1] = "Входные параметры";
            _Worksheet.Cells[5, 1].Font.Bold = true;
            _Worksheet.Range[_Worksheet.Cells[5, 1], _Worksheet.Cells[5, 2]].Merge();
            _Worksheet.Cells[5, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[5, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[6, 1] = "Количество фильтрационных перегородок, шт";
            _Worksheet.Cells[6, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[6, 2] = inputParameter.CountPartitions;
            _Worksheet.Cells[6, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[7, 1] = "Величина перепада давлений на первой перегородке, КПа";
            _Worksheet.Cells[7, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[7, 2] = inputParameter.DifferenceMagnitude1;
            _Worksheet.Cells[7, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[8, 1] = "Величина перепада давлений на второй перегородке, КПа";
            _Worksheet.Cells[8, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[8, 2] = inputParameter.DifferenceMagnitude2;
            _Worksheet.Cells[8, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[10, 1] = "Ограничения";
            _Worksheet.Cells[10, 1].Font.Bold = true;
            _Worksheet.Cells[10, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[11, 1] = limitation.LeftT1 + " < T1 < " + limitation.RightT1;
            _Worksheet.Cells[12, 1] = limitation.LeftT2 + " < T2 < " + limitation.RightT2;
            _Worksheet.Cells[13, 1] = "0.5T1 + T2 <= " + limitation.RightT1T2;
            _Worksheet.Cells[11, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[12, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[13, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[1, 4] = "Выходные параметры";
            _Worksheet.Cells[1, 4].Font.Bold = true;
            _Worksheet.Range[_Worksheet.Cells[1, 4], _Worksheet.Cells[1, 5]].Merge();
            _Worksheet.Cells[1, 4].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[1, 5].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[2, 4] = "Себестоимость фильтрата, у.е.";
            _Worksheet.Cells[2, 4].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[2, 5] = Math.Round(output.VolumeFlowFiltrResult, 2);
            _Worksheet.Cells[2, 5].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[3, 4] = "Температура на первой перегородке, C";
            _Worksheet.Cells[3, 4].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[3, 5] = Math.Round(output.Temperature1Result, 2);
            _Worksheet.Cells[3, 5].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[4, 4] = "Температура на второй перегородке, C";
            _Worksheet.Cells[4, 4].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[4, 5] = Math.Round(output.Temperature2Result, 2);
            _Worksheet.Cells[4, 5].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Cells[1, 7] = "Температура 1, C";
            _Worksheet.Cells[1, 7].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[1, 8] = "Температура 2, C";
            _Worksheet.Cells[1, 8].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Cells[1, 9] = "Себестоимость фильтрата, у.е.";
            _Worksheet.Cells[1, 9].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            for (int i = 0; i < output.OutputParametersArray.Length; i++)
            {
                _Worksheet.Cells[i+2, 7] = output.OutputParametersArray[i].Temperature1;
                _Worksheet.Cells[i+2, 8] = output.OutputParametersArray[i].Temperature2;
                _Worksheet.Cells[i+2, 9] = output.OutputParametersArray[i].VolumeFlowFiltr;

                _Worksheet.Cells[i + 2, 7].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
                _Worksheet.Cells[i + 2, 8].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
                _Worksheet.Cells[i + 2, 9].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            }
        }

        public static void SaveAndClose(string fileName)
        {
            _Excel.Visible = false;
            _Workbook.Activate();
            _Workbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
    Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
    Missing.Value, Missing.Value, Missing.Value);
            _Workbook.Close();
            _Excel.Quit();
        }

        public static void Save(string fileName)
        {
            _Excel.Visible = true;
            _Workbook.Activate();
            _Workbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
    Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
    Missing.Value, Missing.Value, Missing.Value);
        }
    }
}
