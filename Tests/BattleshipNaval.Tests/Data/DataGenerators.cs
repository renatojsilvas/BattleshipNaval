using BattleshipNaval.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleshipNaval.Tests.Data
{
    internal static class DataGenerators
    {
        internal static List<Coordinate> GetAllPossibleCoordinates(int width, int length)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    coordinates.Add(new Coordinate(((char)('A' + i)).ToString() + (j + 1).ToString()));                    
                }
            }

            return coordinates;
        }

        internal static List<CoordinateRangeLinear> GetAllPossibleCoordinatesRange(int width, int length, int minSize, int maxSize)
        {
            List<CoordinateRangeLinear> possibleCoordinateRanges = new List<CoordinateRangeLinear>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = minSize; k <= maxSize; k++)
                    {
                        string posicaoInicial = ((char)('A' + i)).ToString() + (j + 1).ToString();
                        int linhaInicial, linhaFinal, colunaInicial, colunaFinal;
                        if (ConvertePosicaoParaCoordenadas(posicaoInicial, out linhaInicial, out linhaFinal, out colunaInicial, out colunaFinal))
                        {
                            if (linhaInicial + k - 1 < width)
                            {
                                linhaFinal = linhaInicial + k - 1;
                                colunaFinal = colunaInicial;
                                string posicaoFinal = ((char)('A' + colunaFinal)).ToString() + (linhaFinal + 1).ToString();
                                possibleCoordinateRanges.Add(new CoordinateRangeLinear(posicaoInicial + posicaoFinal));
                            }

                            if (colunaInicial + k - 1 < length)
                            {
                                colunaFinal = colunaInicial + k - 1;
                                linhaFinal = linhaInicial;
                                string posicaoFinal = ((char)('A' + colunaFinal)).ToString() + (linhaFinal + 1).ToString();
                                possibleCoordinateRanges.Add(new CoordinateRangeLinear(posicaoInicial + posicaoFinal));
                            }
                        }
                    }
                }
            }

            return possibleCoordinateRanges;
        }

        private static bool ConvertePosicaoParaCoordenadas(string posicao, out int linhaInicial, out int linhaFinal, out int colunaInicial, out int colunaFinal)
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
    }
}
