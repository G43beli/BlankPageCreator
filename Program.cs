using iTextSharp.text.pdf; //library: https://itextpdf.com/en

Console.Write("PDF source file location (full path): ");
var src = Console.ReadLine();
src = src?.Trim().Replace("\"", "").Replace("'", "");
if (string.IsNullOrWhiteSpace(src) || Path.GetExtension(src) != ".pdf")
{
    Console.WriteLine("PDF cannot be found or is invalid!");
    return;
}

Console.Write("New PDF destination file location (full path or leave empty for standard name): ");
var dest = Console.ReadLine();
dest = dest?.Trim().Replace("\"", "").Replace("'", "");
if (string.IsNullOrWhiteSpace(dest) || Path.GetExtension(dest) != ".pdf")
{
    var filename = Path.GetFileNameWithoutExtension(src) + "_new.pdf";
    var directory = Path.GetDirectoryName(src);
    if (directory == null)
    {
        Console.WriteLine("New PDF cannot be saved!");
        return;
    }
    dest = Path.Combine(directory, filename);
}

var reader = new PdfReader(src);
var stamper = new PdfStamper(reader, new FileStream(dest, FileMode.Create));
for (var pageNumber = reader.NumberOfPages; pageNumber > 0; pageNumber--)
{
    var pagesize = reader.GetPageSize(pageNumber);
    stamper.InsertPage(pageNumber + 1, pagesize);
}
stamper.Close();
reader.Close();

Console.WriteLine($"\nNew PDF generated: {dest}");
Console.WriteLine($"Click any key to close the terminal.");
Console.ReadKey();
Environment.Exit(0);