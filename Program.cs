using System;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = "Task1.xlsx";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("The specified Excel file does not exist.");
            return;
        }

        Dictionary<string, List<string>> lines = new Dictionary<string, List<string>>();
        List<List<int>> originalList = new List<List<int>>();

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];

            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            for (int row = 2; row < rowCount; row++)
            {
                List<int> rowValues = new List<int>();

                for (int col = 2; col < colCount; col++)
                {
                    try
                    {
                        int cellValue = Convert.ToInt32(worksheet.Cells[row, col].Text);
                        rowValues.Add(cellValue);
                    }
                    catch{
                        Console.WriteLine("error val = " + row + " " + col);
                    }
                }
                originalList.Add(rowValues);
            }
        }

        // foreach (var kvp in lines)
        // {
        //     Console.WriteLine($"{kvp.Key}: {string.Join(", ", kvp.Value)}");
        // }

        // Print the result of originalList
 
        // Console.WriteLine("Contents of originalList:");

        // foreach (var row in originalList)
        // {
        //     Console.WriteLine($"[{string.Join(", ", row)}]");
        // }
        //PrintList(originalList);

        List<List<int>> resultList = new List<List<int>> { originalList[0] };

        foreach (var innerList in originalList.GetRange(1, originalList.Count - 1))
        {
            resultList.Add(new List<int> { resultList[resultList.Count - 1][1], innerList[1] });
        }

        //List<List<List<int>>> result = GroupArrays(resultList);

        // foreach (var group in result)
        // {
        //     Console.WriteLine("[");
        //     foreach (var subArray in group)
        //     {
        //         Console.WriteLine($"    [{subArray[0]}, {subArray[1]}],");
        //     }
        //     Console.WriteLine("]");
        // }

        //Console.WriteLine(result.Count);
        PrintList(originalList);
        //PrintList(resultList);

    }

    static void PrintList(List<List<int>> list)
    {
        foreach (var innerList in list)
        {
            Console.WriteLine($"[{innerList[0]}, {innerList[1]}]");
        }
        Console.WriteLine(list.Count);
    } 
    
    static List<List<List<int>>> GroupArrays(List<List<int>> arr)
    {
        List<List<List<int>>> groupedArrays = new List<List<List<int>>>();
        List<List<int>> currentGroup = new List<List<int>>();

        foreach (var subArray in arr)
        {
            if (currentGroup.Count == 0 || currentGroup[currentGroup.Count - 1][1] == subArray[0])
            {
                currentGroup.Add(subArray);
                Console.WriteLine(currentGroup[currentGroup.Count - 1][0] + " " + currentGroup[currentGroup.Count - 1][1] + " == " + subArray[0] + " " + subArray[1]);
            }
            else
            {
                groupedArrays.Add(new List<List<int>>(currentGroup));
                currentGroup = new List<List<int>> { subArray };
                Console.WriteLine(subArray);
            }
        }

        // if (currentGroup.Count > 0)
        // {
        //     groupedArrays.Add(currentGroup);
        // }

        return groupedArrays;
    }
}
