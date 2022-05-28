using System.Text.RegularExpressions;

namespace BattleshipNaval
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> quantidadeDeNaviosDoJogador1 = InicializaQuantidadeDeNavios();
            Dictionary<string, int> quantidadeDeNaviosDoJogador2 = InicializaQuantidadeDeNavios();

            char[,] tabuleiroJogador1 = InicializaTabuleiro();
            char[,] tabuleiroJogador2 = InicializaTabuleiro();

            int numeroDeNogadoresReaisNoJogo = PerguntaQuantosJogadoresReaisTemNoJogo();            

            string nomeDoJogador1 = PerguntaNomeDoJogador(1);
            string nomeDoJogador2 = PerguntaNomeDoJogador(2);

            ConfiguraJogador(tabuleiroJogador1, nomeDoJogador1, quantidadeDeNaviosDoJogador1);
            ConfiguraJogador(tabuleiroJogador2, nomeDoJogador2, quantidadeDeNaviosDoJogador2);

            var vencedor = Jogar(tabuleiroJogador1, tabuleiroJogador2, nomeDoJogador1, nomeDoJogador2);
            
            ExibeVencedor(vencedor);
        }

        static Dictionary<string, int> InicializaQuantidadeDeNavios()
        {
            Dictionary<string, int> quantidadeDeNavios = new Dictionary<string, int>();

            quantidadeDeNavios.Add("PS", 1);
            quantidadeDeNavios.Add("NT", 2);
            quantidadeDeNavios.Add("DS", 3);
            quantidadeDeNavios.Add("SB", 4);

            return quantidadeDeNavios;
        }

        static char[,] InicializaTabuleiro()
        {
            char[,] tabuleiro = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tabuleiro[i, j] = ' ';
                }
            }
            return tabuleiro;
        }

        static int PerguntaQuantosJogadoresReaisTemNoJogo()
        {
            int numeroDeJogadoresReaisTemNoJogo = 0;
            
            do
            {
                Console.WriteLine($"Quantos jogadores reais tem no jogo? 1 ou 2?");
                var resultado = Console.ReadLine();
                if (int.TryParse(resultado, out numeroDeJogadoresReaisTemNoJogo))
                {
                    if (numeroDeJogadoresReaisTemNoJogo == 1)
                    {
                        Console.WriteLine($"Em breve!!");
                        Console.WriteLine($"Pressione qualquer tecla para continuar...");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                    if (numeroDeJogadoresReaisTemNoJogo == 2)
                    {
                        break;
                    }
                }
                Console.WriteLine($"Número Inválido!");
            } 
            while (true);

            Console.Clear();

            return numeroDeJogadoresReaisTemNoJogo;
        }

        static string PerguntaNomeDoJogador(int numeroDoJogador)
        {
            string nome;
            do
            {
                Console.WriteLine($"Qual o nome do jogador {numeroDoJogador}?");
                nome = Console.ReadLine();
                if (!string.IsNullOrEmpty(nome))
                    break;
                Console.WriteLine("Nome Inválido!");
            }
            while (true);

            Console.Clear();

            return nome;
        }

        static void ConfiguraJogador(char[,] tabuleiro, string nomeDoJogador, Dictionary<string, int> quantidadeDeNavios)
        {
            do
            {
                Console.WriteLine($"{nomeDoJogador}, posicione seus navios!");
                Console.WriteLine();
                Console.WriteLine($"Navios disponíveis: {ImprimeDisponibilidadeDeNavios(quantidadeDeNavios)}");
                Console.WriteLine();
                Console.WriteLine(ImprimeTabuleiro(tabuleiro, false));

                var navio = SelecionaNavio(quantidadeDeNavios);
                var posicao = PerguntaPosicao();

                if (AdicionaNavio(tabuleiro, navio, posicao))
                {
                    quantidadeDeNavios[navio]--;
                }
                else
                {
                    Console.WriteLine("Não foi possível adicionar um navio nessa posição. Digite qualquer tecla para continuar...");
                    Console.ReadLine();
                }

                Console.Clear();
            }
            while (ExisteNaviosDisponiveis(quantidadeDeNavios));
        }

        static string ImprimeDisponibilidadeDeNavios(Dictionary<string, int> quantidadeDeNavios)
        {
            return $"PS = {quantidadeDeNavios["PS"]}, NT = {quantidadeDeNavios["NT"]}, DS = {quantidadeDeNavios["DS"]}, SB = {quantidadeDeNavios["SB"]}";
        }

        static string SelecionaNavio(Dictionary<string, int> quantidadeDeNavios)
        {
            string tipoDeNavio;
            do
            {
                Console.WriteLine("Qual o tipo de embarcação?");
                tipoDeNavio = Console.ReadLine();
                if (tipoDeNavio == "PS" ||
                    tipoDeNavio == "NT" ||
                    tipoDeNavio == "DS" ||
                    tipoDeNavio == "SB")
                {
                    if (quantidadeDeNavios[tipoDeNavio] > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Não existem mais {tipoDeNavio} disponíveis");
                    }
                }
                Console.WriteLine("Essa embarcação não existe!");
            }
            while (true);

            return tipoDeNavio;
        }

        static string PerguntaPosicao()
        {
            string posicao;
            do
            {
                Console.WriteLine("Qual sua posição?");
                posicao = Console.ReadLine();

                Regex regex = new Regex(@"\b([A-J])([1-9]|10)(([A-J])([1-9]|10))?\b");
                var resultado = regex.Match(posicao);
                if (resultado.Success)
                    break;

                Console.WriteLine("Posição inválida!");
            }
            while (true);

            return posicao;
        }

        static bool AdicionaNavio(char[,] tabuleiro, string tipoDeNavio, string posicao)
        {
            int linhaInicial, linhaFinal, colunaInicial, colunaFinal;
            if (ConvertePosicaoParaCoordenadas(posicao, out linhaInicial, out linhaFinal, out colunaInicial, out colunaFinal))
            {
                int numeroDePosicoes = CalculaNumeroDePosicoesAPartirDasCoordenadas(linhaInicial, linhaFinal, colunaInicial, colunaFinal);
                if (TipoDeNavioBateComAQuantidadeDePosicoesDesejadas(tipoDeNavio, numeroDePosicoes))
                {
                    if (ExistePossibilidadeDeAdicionarNavio(tabuleiro, linhaInicial, linhaFinal, colunaInicial, colunaFinal))
                        return AdicionaNavioNasCoordenadas(tabuleiro, linhaInicial, linhaFinal, colunaInicial, colunaFinal);
                }
            }
            return false;
        }

        static bool ConvertePosicaoParaCoordenadas(string posicao, out int linhaInicial, out int linhaFinal, out int colunaInicial, out int colunaFinal)
        {
            Regex regex = new Regex(@"\b([A-J])([1-9]|10)(([A-J])([1-9]|10))?\b");
            var resultado = regex.Match(posicao);
            if (resultado.Success)
            {
                linhaInicial = Convert.ToInt32(resultado.Groups[2].Value) - 1;
                colunaInicial = resultado.Groups[1].Value[0] - 'A';
                linhaFinal = resultado.Groups[5].Success ? Convert.ToInt32(resultado.Groups[5].Value) - 1 : linhaInicial;
                colunaFinal = resultado.Groups[4].Success ? resultado.Groups[4].Value[0] - 'A' : colunaInicial;

                if ((linhaInicial == linhaFinal || colunaInicial == colunaFinal) &&
                    linhaFinal >= linhaInicial && colunaFinal >= colunaInicial)
                    return true;
            }
            linhaInicial = linhaFinal = colunaInicial = colunaFinal = -1;
            return false;
        }

        static int CalculaNumeroDePosicoesAPartirDasCoordenadas(int linhaInicial, int linhaFinal, int colunaInicial, int colunaFinal)
        {
            if (linhaInicial == linhaFinal)
                return colunaFinal - colunaInicial + 1;

            if (colunaInicial == colunaFinal)
                return linhaFinal - linhaInicial + 1;

            return -1;
        }

        static bool TipoDeNavioBateComAQuantidadeDePosicoesDesejadas(string tipoDeNavio, int numeroDePosicoes)
        {
            switch (tipoDeNavio)
            {
                case "PS":
                    return numeroDePosicoes == 5;
                case "NT":
                    return numeroDePosicoes == 4;
                case "DS":
                    return numeroDePosicoes == 3;
                case "SB":
                    return numeroDePosicoes == 2;
                default:
                    return false;
            }
        }

        static bool ExistePossibilidadeDeAdicionarNavio(char[,] tabuleiro, int linhaInicial, int linhaFinal, int colunaInicial, int colunaFinal)
        {
            for (int i = linhaInicial; i <= linhaFinal; i++)
            {
                for (int j = colunaInicial; j <= colunaFinal; j++)
                {
                    if (tabuleiro[i, j] == 'O')
                        return false;
                }
            }
            return true;
        }

        static bool AdicionaNavioNasCoordenadas(char[,] tabuleiro, int linhaInicial, int linhaFinal, int colunaInicial, int colunaFinal)
        {
            for (int i = linhaInicial; i <= linhaFinal; i++)
            {
                for (int j = colunaInicial; j <= colunaFinal; j++)
                {
                    tabuleiro[i, j] = 'O';
                }
            }
            return true;
        }

        static bool ExisteNaviosDisponiveis(Dictionary<string, int> quantidadeDeNavios)
        {
            return quantidadeDeNavios.Values.Sum() != 0;
        }

        static string Jogar(char[,] tabuleiroJogador1, char[,] tabuleiroJogador2, string nomeDoJogador1, string nomeDoJogador2)
        {
            Console.Clear();
            Console.WriteLine("Começar? Quando estiver pronto pressione qualquer tecla para continuar...");
            Console.Clear();

            int jogadorAtual = 1;

            do
            {
                if (jogadorAtual == 1)
                {
                    JogadorJoga(tabuleiroJogador2, nomeDoJogador1);
                    jogadorAtual = 2;
                }
                else if (jogadorAtual == 2)
                {
                    JogadorJoga(tabuleiroJogador1, nomeDoJogador2);
                    jogadorAtual = 1;
                }
            }
            while (ExistirNaviosParaOsDoisJogadores(tabuleiroJogador1, tabuleiroJogador2));

            if (ContaQuantidadeDePartesDeNaviosAtingidas(tabuleiroJogador1) < 30)
                return nomeDoJogador1;

            if (ContaQuantidadeDePartesDeNaviosAtingidas(tabuleiroJogador2) < 30)
                return nomeDoJogador2;

            return string.Empty;
        }

        static void JogadorJoga(char[,] tabuleiro, string nomeDoJogador)
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Jogador {nomeDoJogador} joga");
                Console.WriteLine();

                Console.WriteLine(ImprimeTabuleiro(tabuleiro, true));

                Console.WriteLine($"{nomeDoJogador}, dispare! Escolha a posição onde deseja atirar.");
                var posicao = Console.ReadLine();
                var resultado = Dispara(tabuleiro, posicao);

                if (resultado == 2)
                {
                    Console.WriteLine("Posição não existe!!");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadLine();
                }
                else
                {
                    if (resultado == 0)
                    {
                        Console.WriteLine("Água!!");
                    }

                    if (resultado == 1)
                    {
                        Console.WriteLine("Acertou um navio!!");
                    }

                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadLine();
                    
                    break;
                }
            }
            while (true);
        }

        static int Dispara(char[,] tabuleiro, string posicao)
        {
            int linhaInicial, linhaFinal, colunaInicial, colunaFinal;
            if (ConvertePosicaoParaCoordenadas(posicao, out linhaInicial, out linhaFinal, out colunaInicial, out colunaFinal))
            {
                if (tabuleiro[linhaInicial, colunaInicial] == ' ')
                {
                    tabuleiro[linhaInicial, colunaInicial] = 'A';
                    return 0;
                }
                if (tabuleiro[linhaInicial, colunaInicial] == 'O')
                {
                    tabuleiro[linhaInicial, colunaInicial] = 'X';
                    return 1;
                }
            }
            return 2;
        }

        static bool ExistirNaviosParaOsDoisJogadores(char[,] tabuleiroJogador1, char[,] tabuleiroJogador2)
        {
            int quantidadeDePartesDeNaviosAtingidasNoJogador1 = ContaQuantidadeDePartesDeNaviosAtingidas(tabuleiroJogador1);
            int quantidadeDePartesDeNaviosAtingidasNoJogador2 = ContaQuantidadeDePartesDeNaviosAtingidas(tabuleiroJogador2);

            return (quantidadeDePartesDeNaviosAtingidasNoJogador1 < 30 && quantidadeDePartesDeNaviosAtingidasNoJogador2 < 30);
        }

        private static int ContaQuantidadeDePartesDeNaviosAtingidas(char[,] tabuleiroJogador)
        {
            int quantidadeDePartesDeNaviosAtingidasNoJogador = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (tabuleiroJogador[i, j] == 'X')
                    {
                        quantidadeDePartesDeNaviosAtingidasNoJogador++;
                    }
                }
            }
            return quantidadeDePartesDeNaviosAtingidasNoJogador;
        }

        static void ExibeVencedor(string vencedor)
        {
            Console.Clear();
            Console.WriteLine($"O grande vencedor é {vencedor}");
        }

        static string ImprimeTabuleiro(char[,] tabuleiro, bool jogando)
        {
            string resultado = "     A   B   C   D   E   F   G   H   I   J  \r\n";
            resultado += "   -----------------------------------------\r\n";
            for (int i = 0; i < 10; i++)
            {
                resultado += ($"{i + 1,2} | ");
                for (int j = 0; j < 10; j++)
                {
                    resultado += jogando && tabuleiro[i, j] == 'O' ? ' ' : (tabuleiro[i, j]);
                    resultado += ($" | ");
                }
                resultado += "\r\n";
                resultado += "   -----------------------------------------\r\n";
            }
            return resultado;
        }
    }
}