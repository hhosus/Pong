using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pong
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Campo
            const int fieldLength = 50, fieldWidth = 15;
            const char fieldTile = '#';
            string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));

            //Raquete
            const int racketLength = fieldWidth / 4;
            const char racketTile = '|';

            int leftRacketHeight = 0;
            int rightRacketHeight = 0;

            //Bola
            int ballX = fieldLength / 2;
            int ballY = fieldWidth / 2;
            const char ballTile = 'O';

            bool isBallGoingDown = true;
            bool isBallGoingRight = true;

            //Pontos Jogador
            int leftPlayerPoints = 0;
            int rightPlayerPoints = 0;

            //Placar
            int scoreboardX = fieldLength / 2 - 2;
            int scoreboardY = fieldWidth + 1;
            int winScore = 1;

            //Loop de Jogo
            while (true)
            {
                //Desenha o Campo
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(line);

                Console.SetCursorPosition(0, fieldWidth);
                Console.WriteLine(line);

                //Desenha a Raquete
                for (int i = 0; i < racketLength; i++)
                {
                    Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                    Console.WriteLine(racketTile);
                    Console.SetCursorPosition(fieldLength - 1, i + 1 + rightRacketHeight);
                    Console.WriteLine(racketTile);
                }

                //Atualiza posição da bola
                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(ballTile);
                    Thread.Sleep(100); //Adiciona um delay para tempo de reação do jogador

                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(" "); //Limpa a última posição da bola

                    //Atualiza posição da bola;
                    if (isBallGoingDown)
                    {
                        ballY++;
                    } else
                    {
                        ballY--;
                    }
                    if (isBallGoingRight)
                    {
                        ballX++;
                    } else
                    {
                        ballX--;
                    }

                    if (ballY == 1 || ballY == fieldWidth - 1)
                    {
                        isBallGoingDown = !isBallGoingDown; // Muda direção da bola revertendo estado da variável
                    }

                    if (ballX == 1)
                    {
                        //Raquete da esquerda rebate bola
                        if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + racketLength)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else //Bola sai do campo, Direita marca;
                        {
                            rightPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                            if (rightPlayerPoints == winScore)
                            {
                                goto outer;
                            }
                        }
                    }

                    if (ballX == fieldLength - 2)
                    {
                        //Raquete da direita rebate bola
                        if(ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + racketLength)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else //Bola sai do campo; Esquerda marca.
                        {
                            leftPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                            if(leftPlayerPoints == winScore)
                            {
                                goto outer;
                            }
                        }
                    }
                }

                //Lê qual tecla foi apertada
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (rightRacketHeight > 0)
                        {
                            rightRacketHeight--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (rightRacketHeight < fieldWidth - racketLength - 1)
                        {
                            rightRacketHeight++;
                        }
                        break;

                    case ConsoleKey.W:
                        if (leftRacketHeight > 0)
                        {
                            leftRacketHeight--;
                        }
                        break;

                    case ConsoleKey.S:
                        if (leftRacketHeight < fieldWidth - racketLength - 1)
                        {
                            leftRacketHeight++;
                        }
                        break;
                }

                //Limpa as antigas posições da raquete
                for(int i = 1; i < fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(fieldLength - 1, i);
                    Console.WriteLine(" ");
                }

            
            
            }

        outer:;
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            if(rightPlayerPoints == winScore)
            {
                Console.WriteLine("Jogador 2 venceu!");
            }
            else
            {
                Console.WriteLine("Jogador 1 venceu!");
            }

            Console.WriteLine("\n\nAperte qualquer tecla para terminar o processo.");
            Console.ReadKey();

           
        }
    }
}
