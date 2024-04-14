// See https://aka.ms/new-console-template for more information
using zipxmlreplace;

string templ = args[0];
string from = args[1];
string to= args[2];
ZipReplacer.Replace(Directory.GetCurrentDirectory(),templ,from, to);
Console.WriteLine(args[0]);
