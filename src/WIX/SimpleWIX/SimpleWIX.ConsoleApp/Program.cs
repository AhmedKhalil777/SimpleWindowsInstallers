// See https://aka.ms/new-console-template for more information
using SimpleWIX.ConsoleApp;

while (true)
{
    Console.Write("Please Enter Operation (+, - , * , /) : ");
    var op = CalculatorProvider.BuildOperation<double>(op: Console.ReadLine());
    Console.Write("Enter the first num : ");
    var num1 = double.Parse(Console.ReadLine());
    Console.Write("Enter the second num : ");
    var num2 = double.Parse(Console.ReadLine());
    Console.WriteLine(op(num1,num2));
}
