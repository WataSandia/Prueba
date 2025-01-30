using SolutionTwo;

Begin:

Console.ForegroundColor = ConsoleColor.White;
Console.Write("Ingrese un número [1 - 100]: ");

var userInput = Console.ReadLine();
bool succesfullParse = int.TryParse(userInput, out int number);

Console.WriteLine();

if (succesfullParse is false)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Only interger numbers are valid.");
    Console.WriteLine();

    goto End;
}


try
{
    var numbers = new Numbers();
    numbers.Extract(number);

    int missingNumber = numbers.Calculate();

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"The missing number is {missingNumber}");

    Environment.Exit(0);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.WriteLine();

    goto End;
}

End:
goto Begin;