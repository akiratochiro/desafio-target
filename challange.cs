using System;
using System.Collections.Generic;
using System.Text.Json;

class Venda
{
    public string vendedor { get; set; }
    public double valor { get; set; }
}

class Estoque
{
    public int codigoProduto { get; set; }
    public string descricaoProduto { get; set; }
    public int estoque { get; set; }
}

class Program
{
    static void Main()
    {
        // QUESTÃO 1 - COMISSÃO

        string json = @"{
            ""vendas"": [
                { ""vendedor"": ""João Silva"", ""valor"": 1200.50 },
                { ""vendedor"": ""João Silva"", ""valor"": 950.75 },
                { ""vendedor"": ""João Silva"", ""valor"": 1800.00 },
                { ""vendedor"": ""João Silva"", ""valor"": 1400.30 },
                { ""vendedor"": ""João Silva"", ""valor"": 1100.90 },
                { ""vendedor"": ""João Silva"", ""valor"": 1550.00 },
                { ""vendedor"": ""João Silva"", ""valor"": 1700.80 },
                { ""vendedor"": ""João Silva"", ""valor"": 250.30 },
                { ""vendedor"": ""João Silva"", ""valor"": 480.75 },
                { ""vendedor"": ""João Silva"", ""valor"": 320.40 },

                { ""vendedor"": ""Maria Souza"", ""valor"": 2100.40 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 1350.60 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 950.20 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 1600.75 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 1750.00 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 1450.90 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 400.50 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 180.20 },
                { ""vendedor"": ""Maria Souza"", ""valor"": 90.75 },

                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 800.50 },
                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1200.00 },
                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1950.30 },
                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1750.80 },
                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1300.60 },
                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 300.40 },
                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 500.00 },
                { ""vendedor"": ""Carlos Oliveira"", ""valor"": 125.75 },

                { ""vendedor"": ""Ana Lima"", ""valor"": 1000.00 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 1100.50 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 1250.75 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 1400.20 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 1550.90 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 1650.00 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 75.30 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 420.90 },
                { ""vendedor"": ""Ana Lima"", ""valor"": 315.40 }
            ]
        }";

        var dados = JsonSerializer.Deserialize<Dictionary<string, List<Venda>>>(json);

        Dictionary<string, double> comissoes = new Dictionary<string, double>();

        foreach (var venda in dados["vendas"])
        {
            double percentual = 0;

            if (venda.valor >= 500)
            {
                percentual = 0.05;
            }
            else if (venda.valor >= 100)
            {
                percentual = 0.01;
            }

            double comissao = venda.valor * percentual;

            if (!comissoes.ContainsKey(venda.vendedor))
            {
                comissoes[venda.vendedor] = 0;
            }

            comissoes[venda.vendedor] += comissao;
        }

        Console.WriteLine("COMISSÕES:");

        foreach (var vendedor in comissoes)
        {
            Console.WriteLine(
                vendedor.Key + ": R$ " + vendedor.Value.ToString("F2")
            );
        }


        // QUESTÃO 2 - ESTOQUE

        List<Estoque> estoque = new List<Estoque>
        {
            new Estoque { codigoProduto = 101, descricaoProduto = "Caneta Azul", estoque = 150 },
            new Estoque { codigoProduto = 102, descricaoProduto = "Caderno Universitário", estoque = 75 },
            new Estoque { codigoProduto = 103, descricaoProduto = "Borracha Branca", estoque = 200 },
            new Estoque { codigoProduto = 104, descricaoProduto = "Lápis Preto HB", estoque = 320 },
            new Estoque { codigoProduto = 105, descricaoProduto = "Marcador de Texto Amarelo", estoque = 90 }
        };

        Console.WriteLine("\nMOVIMENTAÇÃO DE ESTOQUE");

        Console.Write("Código do produto: ");
        int codigo = int.Parse(Console.ReadLine());

        Console.Write("Entrada ou saída: ");
        string tipo = Console.ReadLine();

        Console.Write("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());

        Estoque produto = estoque.Find(p => p.codigoProduto == codigo);

        if (produto != null)
        {
            if (tipo.ToLower() == "entrada")
            {
                produto.estoque += quantidade;
            }
            else if (tipo.ToLower() == "saída" || tipo.ToLower() == "saida")
            {
                produto.estoque -= quantidade;
            }

            Console.WriteLine(
                "Estoque final: " + produto.estoque
            );
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }


        // QUESTÃO 3 - JUROS

        Console.WriteLine("\nCÁLCULO DE JUROS");

        Console.Write("Valor: R$ ");
        double valor = double.Parse(Console.ReadLine());

        Console.Write("Data de vencimento (dd/MM/yyyy): ");
        DateTime vencimento = DateTime.Parse(Console.ReadLine());

        int diasAtraso = (DateTime.Today - vencimento).Days;

        if (diasAtraso > 0)
        {
            double juros = valor * 0.025 * diasAtraso;

            Console.WriteLine("Dias de atraso: " + diasAtraso);
            Console.WriteLine("Juros: R$ " + juros.ToString("F2"));
            Console.WriteLine("Valor total: R$ " + (valor + juros).ToString("F2"));
        }
        else

            Console.WriteLine("Não há juros.");
        }
    }
}