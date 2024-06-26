using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using ExcelDataReader;

namespace BLL.Proyect.WebAPI_NGK
{
    public class Excel
    {
        public static DataTable ReadExcel_SimpleSheet_ToDataSet(string path)
        {
            DataTable dt = new DataTable();

            try
            {

                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(@path, true))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();
                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell).Trim().Replace(" #", "").Trim().Replace(" ", "_").Trim().Replace("/", "_").Trim().Replace(" #", "_").Trim().Replace("#", "_").Trim().Replace(".", "").Trim().Replace("%", ""));
                    }
                    int minimRows = rows.Count() > 10 ? (rows.Count() / 10) - 1 : 1;
                    int numberRows = 0;

                    foreach (Row row in rows) //this will also include your header row...
                    {
                        if (numberRows > 0)
                        {
                            DataRow tempRow = dt.NewRow();
                            int columnIndex = 0;
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                // Gets the column index of the cell with data
                                int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                                cellColumnIndex--; //zero based index
                                if (columnIndex < cellColumnIndex)
                                {
                                    do
                                    {
                                        tempRow[columnIndex] = ""; //Insert blank data here;
                                        columnIndex++;
                                    }
                                    while (columnIndex < cellColumnIndex);
                                }
                                tempRow[columnIndex] = GetCellValue(spreadSheetDocument, cell);

                                columnIndex++;
                            }
                            dt.Rows.Add(tempRow);
                            numberRows = 0;
                        }
                        numberRows++;
                    }

                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Given a cell name, parses the specified cell to get the column name.
        /// </summary>
        /// <param name="cellReference">Address of the cell (ie. B2)</param>
        /// <returns>Column Name (ie. B)</returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

        /// <summary>
        /// Given just the column name (no row index), it will return the zero based column index.
        /// Note: This method will only handle columns with a length of up to two (ie. A to Z and AA to ZZ). 
        /// A length of three can be implemented when needed.
        /// </summary>
        /// <param name="columnName">Column Name (ie. A or AB)</param>
        /// <returns>Zero based index if the conversion was successful; otherwise null</returns>
        public static int? GetColumnIndexFromName(string columnName)
        {

            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }

        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            return cell.InnerText.Trim();
        }

        public static List<Dictionary<string, string>> GetDataTableDictionaryList(DataTable dt)
        {
            return dt.AsEnumerable().Select(
                row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column].ToString()
                )).ToList();
        }










        private static string GetCellValue(WorkbookPart wbPart, List<Cell> theCells, string cellColumnReference)
        {
            Cell theCell = null;
            string value = "";
            foreach (Cell cell in theCells)
            {
                if (cell.CellReference.Value.StartsWith(cellColumnReference))
                {
                    theCell = cell;
                    break;
                }
            }
            if (theCell != null)
            {
                value = theCell.InnerText;
                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code looks up the corresponding value in the shared string table. For Booleans, the code converts the value into the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            // For shared strings, look up the value in the shared strings table.
                            var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                            // If the shared string table is missing, something is wrong. Return the index that is in the cell. Otherwise, look up the correct text in the table.
                            if (stringTable != null)
                            {
                                value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                            }
                            break;
                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }
            return value;
        }

        private static string GetCellValue(WorkbookPart wbPart, List<Cell> theCells, int index)
        {
            return GetCellValue(wbPart, theCells, GetExcelColumnName(index));
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;
            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }

        //Only xlsx files
        public static DataTable GetDataTableFromExcelFile(string filePath, string sheetName = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart wbPart = document.WorkbookPart;
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string sheetId = sheetName != "" ? sheets.Where(q => q.Name == sheetName).First().Id.Value : sheets.First().Id.Value;
                    WorksheetPart wsPart = (WorksheetPart)wbPart.GetPartById(sheetId);
                    SheetData sheetdata = wsPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                    int totalHeaderCount = sheetdata.Descendants<Row>().ElementAt(0).Descendants<Cell>().Count();
                    //Get the header                    
                    for (int i = 1; i <= totalHeaderCount; i++)
                    {
                        dt.Columns.Add(GetCellValue(wbPart, sheetdata.Descendants<Row>().ElementAt(0).Elements<Cell>().ToList(), i));
                    }
                    foreach (Row r in sheetdata.Descendants<Row>())
                    {
                        if (r.RowIndex > 1)
                        {
                            DataRow tempRow = dt.NewRow();

                            //Always get from the header count, because the index of the row changes where empty cell is not counted
                            for (int i = 1; i <= totalHeaderCount; i++)
                            {
                                tempRow[i - 1] = GetCellValue(wbPart, r.Elements<Cell>().ToList(), i);
                            }
                            dt.Rows.Add(tempRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        //Only xlsx files
        public static DataTable GetDataTableFromExcelFile(string filePath, string sheetName, bool allRows, int totalrows)
        {
            DataTable dt = new DataTable();
            try
            {
                if (allRows)
                {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
                    {
                        WorkbookPart wbPart = document.WorkbookPart;
                        IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                        string sheetId = sheetName != "" ? sheets.Where(q => q.Name == sheetName).First().Id.Value : sheets.First().Id.Value;
                        WorksheetPart wsPart = (WorksheetPart)wbPart.GetPartById(sheetId);
                        SheetData sheetdata = wsPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                        int totalHeaderCount = totalrows; // sheetdata.Descendants<Row>().ElementAt(0).Descendants<Cell>().Count();
                                                          //Get the header                    
                        for (int i = 1; i <= totalrows; i++)
                        {
                            //dt.Columns.Add(GetCellValue(wbPart, sheetdata.Descendants<Row>().ElementAt(0).Elements<Cell>().ToList(), i));
                            dt.Columns.Add("columna" + i);
                        }
                        foreach (Row r in sheetdata.Descendants<Row>())
                        {
                            if (r.RowIndex > 1)
                            {
                                DataRow tempRow = dt.NewRow();

                                //Always get from the header count, because the index of the row changes where empty cell is not counted
                                for (int i = 1; i <= totalHeaderCount; i++)
                                {
                                    tempRow[i - 1] = GetCellValue(wbPart, r.Elements<Cell>().ToList(), i);
                                }
                                dt.Rows.Add(tempRow);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

















    }
}
