using System;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[bold yellow]Calculadora de Préstamos[/]");
        AnsiConsole.WriteLine();

        // Entrada de datos
        decimal monto = AnsiConsole.Ask<decimal>("Ingrese el [green]monto del préstamo[/]:");
        decimal interesAnual = AnsiConsole.Ask<decimal>("Ingrese la [green]tasa de interés anual (%) [/]:");
        int meses = AnsiConsole.Ask<int>("Ingrese el [green]plazo en meses[/]:");

        // Tasa mensual en decimal
        decimal i = (interesAnual / 12) / 100;

        // Cálculo de cuota fija
        decimal cuota = monto * (i * (decimal)Math.Pow(1 + (double)i, meses)) /
                        ((decimal)Math.Pow(1 + (double)i, meses) - 1);

        decimal saldo = monto;

        // Crear tabla
        var tabla = new Table();
        tabla.Border(TableBorder.Rounded);
        tabla.AddColumn("No.");
        tabla.AddColumn("Pago de cuota");
        tabla.AddColumn("Interés");
        tabla.AddColumn("Abono capital");
        tabla.AddColumn("Saldo");

        // Bucle de amortización
        for (int n = 1; n <= meses; n++)
        {
            decimal interes = saldo * i;
            decimal abonoCapital = cuota - interes;
            saldo -= abonoCapital;

            // Evitar residuos decimales al final
            if (n == meses)
                saldo = 0;

            tabla.AddRow(
                n.ToString(),
                cuota.ToString("N2"),
                interes.ToString("N2"),
                abonoCapital.ToString("N2"),
                saldo.ToString("N2")
            );
        }

        // Mostrar tabla
        AnsiConsole.Write(tabla);

        AnsiConsole.MarkupLine("\n[bold green]Cálculo finalizado correctamente.[/]");
        Console.ReadKey();
    }
}
