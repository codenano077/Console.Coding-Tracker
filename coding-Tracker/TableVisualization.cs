using ConsoleTableExt;// this is to visualize the data in a table format

namespace coding_Tracker
{
    internal class TableVisualization
    {
        internal static void ShowTable<T>(List<T> tableData) where T : class // this is to show the data in a table format using the ConsoleTableExt library
        {
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("Coding")
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }  
    }
}