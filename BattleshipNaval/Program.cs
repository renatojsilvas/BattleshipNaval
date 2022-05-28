using System.Text.RegularExpressions;

namespace BattleshipNaval
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> listaDePosicoesPossiveisParaOJogador1 = new List<string>();
            List<string> listaDePosicoesQueNaoPodemSerUsadasParaOJogador1 = new List<string>();
            Dictionary<string, int> listaDePosicionamentosDeNaviosPossiveisParaOJogador1 = new Dictionary<string, int>();

            List<string> listaDePosicoesPossiveisParaOJogador2 = new List<string>();
            List<string> listaDePosicoesQueNaoPodemSerUsadasParaOJogador2 = new List<string>();
            Dictionary<string, int> listaDePosicionamentosDeNaviosPossiveisParaOJogador2 = new Dictionary<string, int>();

            GeraListaDePosicoesPossiveis(listaDePosicoesPossiveisParaOJogador1);
            GeraListaDePosicionamentosDeNaviosPossiveis(listaDePosicionamentosDeNaviosPossiveisParaOJogador1);

            GeraListaDePosicoesPossiveis(listaDePosicoesPossiveisParaOJogador2);
            GeraListaDePosicionamentosDeNaviosPossiveis(listaDePosicionamentosDeNaviosPossiveisParaOJogador2);

            Dictionary<string, int> quantidadeDeNaviosDoJogador1 = InicializaQuantidadeDeNavios();
            Dictionary<string, int> quantidadeDeNaviosDoJogador2 = InicializaQuantidadeDeNavios();

            char[,] tabuleiroJogador1 = InicializaTabuleiro();
            char[,] tabuleiroJogador2 = InicializaTabuleiro();

            int numeroDeNogadoresReaisNoJogo = PerguntaQuantosJogadoresReaisTemNoJogo();

            bool jogador1SeriaComputador = false, jogador2SeriaComputador = false;
            if (numeroDeNogadoresReaisNoJogo == 0)
                jogador1SeriaComputador = jogador2SeriaComputador = true;
            if (numeroDeNogadoresReaisNoJogo == 1)
                jogador2SeriaComputador = true;

            string nomeDoJogador1 = PerguntaNomeDoJogador(1, jogador1SeriaComputador);
            string nomeDoJogador2 = PerguntaNomeDoJogador(2, jogador2SeriaComputador);

            ConfiguraJogador(tabuleiroJogador1, nomeDoJogador1, quantidadeDeNaviosDoJogador1, jogador1SeriaComputador);
            ConfiguraJogador(tabuleiroJogador2, nomeDoJogador2, quantidadeDeNaviosDoJogador2, jogador2SeriaComputador);

            var vencedor = Jogar(tabuleiroJogador1, tabuleiroJogador2, nomeDoJogador1, nomeDoJogador2,
                jogador1SeriaComputador, jogador2SeriaComputador,
                listaDePosicoesPossiveisParaOJogador1, listaDePosicoesQueNaoPodemSerUsadasParaOJogador1,
                listaDePosicoesPossiveisParaOJogador2, listaDePosicoesQueNaoPodemSerUsadasParaOJogador2);

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
                Console.WriteLine($"Quantos jogadores reais tem no jogo? 0, 1 ou 2?");
                var resultado = Console.ReadLine();
                if (int.TryParse(resultado, out numeroDeJogadoresReaisTemNoJogo))
                {
                    if (numeroDeJogadoresReaisTemNoJogo == 0 || numeroDeJogadoresReaisTemNoJogo == 1 || numeroDeJogadoresReaisTemNoJogo == 2)
                        break;
                }
                Console.WriteLine($"Número Inválido!");
            }
            while (true);

            Console.Clear();

            return numeroDeJogadoresReaisTemNoJogo;
        }

        static string PerguntaNomeDoJogador(int numeroDoJogador, bool computador)
        {
            if (computador)
            {
                Random random = new Random();
                Console.WriteLine($"Escolhendo nome do jogador {numeroDoJogador}...");
                Thread.Sleep(2000);
                var nomeDoComputador = $"Jogador-{random.Next(1, 500)}";
                Console.WriteLine($"Nome escolhido pelo computador: {nomeDoComputador}");
                Thread.Sleep(2000);
                Console.Clear();
                return $"Jogador-{random.Next(1, 500)}";
            }

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

        static void ConfiguraJogador(char[,] tabuleiro, string nomeDoJogador, Dictionary<string, int> quantidadeDeNavios, bool computador)
        {
            if (computador)
            {
                Console.WriteLine($"{nomeDoJogador} posicionando navios...");

                List<string> listaDePosicionamentosQueNaoPodemSerUsadas = new List<string>();
                Dictionary<string, int> listaDePosicionamentosDeNaviosPossiveis = new Dictionary<string, int>();

                GeraListaDePosicionamentosDeNaviosPossiveis(listaDePosicionamentosDeNaviosPossiveis);

                int quantidade;

                quantidade = quantidadeDeNavios["PS"];
                for (int i = 0; i < quantidade; i++)
                {
                    do
                    {
                        var posicionamento = ObtemPosicionamentoDeNavioAleatorio(listaDePosicionamentosDeNaviosPossiveis, 5, listaDePosicionamentosQueNaoPodemSerUsadas);
                        if (AdicionaNavio(tabuleiro, "PS", posicionamento))
                            break;
                    }
                    while (true);
                }

                quantidade = quantidadeDeNavios["NT"];
                for (int i = 0; i < quantidade; i++)
                {
                    do
                    {
                        var posicionamento = ObtemPosicionamentoDeNavioAleatorio(listaDePosicionamentosDeNaviosPossiveis, 4, listaDePosicionamentosQueNaoPodemSerUsadas);
                        if (AdicionaNavio(tabuleiro, "NT", posicionamento))
                            break;
                    }
                    while (true);
                }

                quantidade = quantidadeDeNavios["DS"];
                for (int i = 0; i < quantidade; i++)
                {
                    do
                    {
                        var posicionamento = ObtemPosicionamentoDeNavioAleatorio(listaDePosicionamentosDeNaviosPossiveis, 3, listaDePosicionamentosQueNaoPodemSerUsadas);
                        if (AdicionaNavio(tabuleiro, "DS", posicionamento))
                            break;
                    }
                    while (true);
                }

                quantidade = quantidadeDeNavios["SB"];
                for (int i = 0; i < quantidade; i++)
                {
                    do
                    {
                        var posicionamento = ObtemPosicionamentoDeNavioAleatorio(listaDePosicionamentosDeNaviosPossiveis, 2, listaDePosicionamentosQueNaoPodemSerUsadas);
                        if (AdicionaNavio(tabuleiro, "SB", posicionamento))
                            break;
                    }
                    while (true);
                }

                Thread.Sleep(5000);
                Console.Clear();

                return;
            }

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

        static string Jogar(char[,] tabuleiroJogador1, char[,] tabuleiroJogador2, string nomeDoJogador1, string nomeDoJogador2,
            bool jogador1SeriaComputador,
            bool jogador2SeriaComputador,
            List<string> listaDePosicoesPossiveisParaOJogador1,
            List<string> listaDePosicoesQueNaoPodemSerUsadasParaOJogador1,
            List<string> listaDePosicoesPossiveisParaOJogador2,
            List<string> listaDePosicoesQueNaoPodemSerUsadasParaOJogador2)
        {
            Console.Clear();
            Console.WriteLine("Começar? Quando estiver pronto pressione qualquer tecla para continuar...");
            Console.Clear();

            int jogadorAtual = 1;

            do
            {
                if (jogadorAtual == 1)
                {
                    JogadorJoga(tabuleiroJogador2, nomeDoJogador1, jogador1SeriaComputador, listaDePosicoesPossiveisParaOJogador1, listaDePosicoesQueNaoPodemSerUsadasParaOJogador1);
                    jogadorAtual = 2;
                }
                else if (jogadorAtual == 2)
                {
                    JogadorJoga(tabuleiroJogador1, nomeDoJogador2, jogador2SeriaComputador, listaDePosicoesPossiveisParaOJogador2, listaDePosicoesQueNaoPodemSerUsadasParaOJogador2);
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

        static void JogadorJoga(char[,] tabuleiro, string nomeDoJogador, bool computador,
            List<string> listaDePosicoesPossiveis,
            List<string> listaDePosicoesQueNaoPodemSerUsadas)
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Jogador {nomeDoJogador} joga");
                Console.WriteLine();

                Console.WriteLine(ImprimeTabuleiro(tabuleiro, true));

                Console.WriteLine($"{nomeDoJogador}, dispare!");
                string posicao;
                if (computador)
                {
                    Thread.Sleep(2000);
                    posicao = ObtemPosicaoAleatoria(listaDePosicoesPossiveis, listaDePosicoesQueNaoPodemSerUsadas);
                    Console.WriteLine(posicao);
                }
                else
                {
                    Console.WriteLine("Escolha a posição onde deseja atirar.");
                    posicao = Console.ReadLine();
                }

                var resultado = Dispara(tabuleiro, posicao);

                if (resultado == 2)
                {
                    Console.WriteLine("Posição não existe!!");
                    if (computador)
                    {
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadLine();
                    }
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

                    if (computador)
                    {
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadLine();
                    }

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

        static void GeraListaDePosicoesPossiveis(List<string> listaDePosicoesPossiveis)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    listaDePosicoesPossiveis.Add(((char)('A' + i)).ToString() + (j + 1).ToString());
                }
            }
        }

        static string ObtemPosicaoAleatoria(List<string> listaDePosicoesPossiveis, List<string> listaDePosicoesQueNaoDevemSerUsadas)
        {
            Random random = new Random();
            List<string> listaDePosicionamentosPelaQuantidadeDePosicoes = new List<string>();

            foreach (var posicionamento in listaDePosicoesPossiveis.ToList())
            {
                foreach (var posicionamentoQueNaoDeveSerUsado in listaDePosicoesQueNaoDevemSerUsadas)
                {
                    if (posicionamento == posicionamentoQueNaoDeveSerUsado)
                    {
                        listaDePosicoesPossiveis.Remove(posicionamento);
                    }
                }
            }

            var posicao = listaDePosicoesPossiveis[random.Next(0,
                listaDePosicoesPossiveis.Count)];

            listaDePosicoesQueNaoDevemSerUsadas.Add(posicao);

            return posicao;
        }

        static string ObtemPosicionamentoDeNavioAleatorio(Dictionary<string, int> listaDePosicionamentosDeNaviosPossiveis, int quantidadeDePosicoes,
            List<string> listaDePosiconamentoDeNaviosQueNaoDevemSerUsados)
        {
            Random random = new Random();
            List<string> listaDePosicionamentosPelaQuantidadeDePosicoes = new List<string>();
            foreach (var posicionamento in listaDePosicionamentosDeNaviosPossiveis)
            {
                if (posicionamento.Value == quantidadeDePosicoes)
                    listaDePosicionamentosPelaQuantidadeDePosicoes.Add(posicionamento.Key);
            }

            foreach (var posicionamento in listaDePosicionamentosPelaQuantidadeDePosicoes.ToList())
            {
                foreach (var posicionamentoQueNaoDeveSerUsado in listaDePosiconamentoDeNaviosQueNaoDevemSerUsados)
                {
                    if (posicionamento == posicionamentoQueNaoDeveSerUsado)
                    {
                        listaDePosicionamentosPelaQuantidadeDePosicoes.Remove(posicionamento);
                    }
                }
            }

            var posicao = listaDePosicionamentosPelaQuantidadeDePosicoes[random.Next(0,
                listaDePosicionamentosPelaQuantidadeDePosicoes.Count)];

            listaDePosiconamentoDeNaviosQueNaoDevemSerUsados.Add(posicao);

            return posicao;
        }

        static void GeraListaDePosicionamentosDeNaviosPossiveis(Dictionary<string, int> listaDePosicionamentosDeNaviosPossiveis)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 2; k <= 5; k++)
                    {
                        string posicaoInicial = ((char)('A' + i)).ToString() + (j + 1).ToString();
                        int linhaInicial, linhaFinal, colunaInicial, colunaFinal;
                        if (ConvertePosicaoParaCoordenadas(posicaoInicial, out linhaInicial, out linhaFinal, out colunaInicial, out colunaFinal))
                        {
                            if (linhaInicial + k - 1 < 10)
                            {
                                linhaFinal = linhaInicial + k - 1;
                                colunaFinal = colunaInicial;
                                string posicaoFinal = ((char)('A' + colunaFinal)).ToString() + (linhaFinal + 1).ToString();
                                listaDePosicionamentosDeNaviosPossiveis.Add(posicaoInicial + posicaoFinal, k);
                            }

                            if (colunaInicial + k - 1 < 10)
                            {
                                colunaFinal = colunaInicial + k - 1;
                                linhaFinal = linhaInicial;
                                string posicaoFinal = ((char)('A' + colunaFinal)).ToString() + (linhaFinal + 1).ToString();
                                listaDePosicionamentosDeNaviosPossiveis.Add(posicaoInicial + posicaoFinal, k);
                            }
                        }
                    }
                }
            }
        }
    }
}