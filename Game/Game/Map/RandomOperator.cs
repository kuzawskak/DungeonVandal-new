using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Map
{

    /// <summary>
    /// Enum na oznaczenie typu pola 
    /// ( wykorzystywany przy zapisie i identyfikacji pól mapy)
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        /// Cieżki Mur
        /// </summary>
        CIEZKIMUR,
        /// <summary>
        /// Magiczny Mur
        /// </summary>
        MAGICZNYMUR, 
        /// <summary>
        /// Marmurowy Kamień
        /// </summary>
        MARMUROWYKAMIEN,
        /// <summary>
        /// Puste pole
        /// </summary>
        PUSTE, 
        /// <summary>
        /// Radioaktywny głaz
        /// </summary>
        RADIOAKTYWNYGLAZ,
        /// <summary>
        /// Dynamit
        /// </summary>
        DYNAMIT, 
        /// <summary>
        /// Pęk dynamitów
        /// </summary>
        PEKDYNAMITOW,
        /// <summary>
        /// Rakieta
        /// </summary>
        RACKET,
        /// <summary>
        /// Pole siłowe
        /// </summary>
        POLESILOWE,
        /// <summary>
        /// Ameba
        /// </summary>
        AMEBA,
        /// <summary>
        /// Kamień
        /// </summary>
        KAMIEN,
        /// <summary>
        /// Lekki Mur
        /// </summary>
        LEKKIMUR,
        /// <summary>
        /// Ziemia
        /// </summary>
        ZIEMIA, 
        /// <summary>
        /// Blob
        /// </summary>
        BLOB, 
        /// <summary>
        /// Vandal
        /// </summary>
        VANDAL, 
        /// <summary>
        /// Goblin
        /// </summary>
        GOBLIN, 
        /// <summary>
        /// Gigantyczny Szczur
        /// </summary>
        GIGANTYCZNYSZCZUR, 
        /// <summary>
        /// Chodzaca Bomba
        /// </summary>
        CHODZACABOMBA,
        /// <summary>
        /// Cel Misji
        /// </summary>
        CELMISJI
    };


    /// <summary>
    /// Klasa losowego generowania mapy na podstawie poziomu
    /// </summary>
    public class RandomOperator
    {
        /// <summary>
        /// Czynnik losowy w konstrukcji mapy
        /// </summary>
        Random rand = new Random();
        /// <summary>
        /// Liczba komrek mapy horyzontalnie
        /// </summary>
        private int map_width;
        /// <summary>
        /// Liczba komorek mapy wertykalnie
        /// </summary>
        private int map_height;

        /// <summary>
        /// mapa reprezentowana jako tablica integerów
        /// </summary>
        public ElementType[,] Map { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="map_width">ilosc komorek w poziomie mapy</param>
        /// <param name="map_height">ilosc komorek w pionie mapy</param>
        public RandomOperator(int map_width, int map_height)
        {
            this.map_width = map_width;
            this.map_height = map_height;
            Map = new ElementType[map_width, map_height];

        }



        /// <summary>
        /// Tworzenie losowej mapy
        /// </summary>
        /// <param name="width">liczba komórek mapoy w poziomie</param>
        /// <param name="height">liczba komórek mapy w pionie</param>
        /// <param name="gameLevel">etap gry</param>
        /// <param name="difficultyLevel">poziom trudności gry</param>
        public void GenerateRandomMap(int width, int height,int gameLevel,int difficultyLevel)
        {
            
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                    {
                        Map[i, j] = ElementType.CIEZKIMUR;
                    }
                    else
                    {
                        Map[i, j] = ElementType.ZIEMIA;
                    }
                }

            int pos_x;
            int pos_y;

            for (int i = 0; i < 4 - difficultyLevel; i++)
            {
                pos_x = rand.Next(10, width - 2);
                pos_y = rand.Next(1, height - 2);
            }



                for (int i = 0; i < gameLevel; i++)
                {

                    pos_x = rand.Next(10, width - 2);
                    pos_y = rand.Next(1, height - 2);
                    for (int j = 0; j < 2* gameLevel; j++)
                    {
                        try
                        {
                            Map[pos_x + j, pos_y] = ElementType.MARMUROWYKAMIEN;
                        }
                        catch { //wyjscie poza tablice  - nie wstawiaj
                        }
                    }
                  
                }
            

        
            //losowanie 10 dynamitow
       
            for (int i = 0; i < 10; i++)
            {
                pos_x = rand.Next(1, width - 2);
                pos_y = rand.Next(1, height - 2);

                Map[pos_x, pos_y] = ElementType.DYNAMIT;
            }
            //losowanie 2 pekow dynamitow

            for (int i = 0; i < 2; i++)
            {
                pos_x = rand.Next(1, width - 2);
                pos_y = rand.Next(1, height - 2);

                Map[pos_x, pos_y] = ElementType.PEKDYNAMITOW;
            }
            //10 rakiet
            for (int i = 0; i < 10; i++)
            {
                pos_x = rand.Next(1, width - 2);
                pos_y = rand.Next(1, height - 2);

                Map[pos_x, pos_y] = ElementType.RACKET;
            }
            //magiczny mur
            for (int i = 0; i < 10; i++)
            {
                pos_x = rand.Next(1, width - 2);
                pos_y = rand.Next(1, height - 2);

                Map[pos_x, pos_y] = ElementType.MAGICZNYMUR;
            }
            //kamien
            for (int i = 0; i < 10; i++)
            {
                pos_x = rand.Next(1, width - 2);
                pos_y = rand.Next(1, height - 2);

                Map[pos_x, pos_y] = ElementType.KAMIEN;
            }
            //radioaktywny glaz
            for (int i = 0; i < 2*difficultyLevel; i++)
            {
                if (gameLevel == 1)
                {
                    pos_x = rand.Next(7, width - 4);
                    pos_y = rand.Next(1, height - 2);
                }
                else if( gameLevel <= 3)
                {
                    pos_x = rand.Next(12, width - 2);
                    pos_y = rand.Next(8, height - 2);
                }
                else{
                     pos_x = rand.Next(5, (int)(width/2)-2);
                    pos_y = rand.Next(8, height - 2);
                }
                try
                {
                    Map[pos_x, pos_y] = ElementType.RADIOAKTYWNYGLAZ;
                }
                catch { }
            }
            //ciezki mur
            for (int i = 0; i < gameLevel*5; i++)
            {
                pos_x = rand.Next(1, width - 2);
                pos_y = rand.Next(1, height - 2);
                try
                {
                    Map[pos_x, pos_y] = ElementType.CIEZKIMUR;
                }
                catch { }
            }

            //lekki mur
            for (int i = 0; i < gameLevel*4; i++)
            {
                pos_x = rand.Next(1, width - 2);
                pos_y = rand.Next(1, height - 2);
                try
                {
                    Map[pos_x, pos_y] = ElementType.LEKKIMUR;
                }
                catch { }
            }

            //ameba 
            pos_x = rand.Next(4, width - 2);
            pos_y = rand.Next(4, height - 2);

            Map[pos_x, pos_y] = ElementType.GIGANTYCZNYSZCZUR;


            for (int i = 0; i < difficultyLevel; i++)
            {
                
                pos_x = 5;
                pos_y = 5;
                try
                {
                    Map[pos_x, pos_y] = ElementType.BLOB;
                }
                catch { }
            }

            pos_x = 8;
            pos_y = 8;

            Map[pos_x, pos_y] = ElementType.GOBLIN;

            pos_x = 7;
            pos_y = 7;

            Map[pos_x, pos_y] = ElementType.POLESILOWE;

            for (int i = 0; i < difficultyLevel; i++)
            {
                pos_x = rand.Next(6, width - 2);
                pos_y = rand.Next(6, height - 2);
                try
                {
                    Map[pos_x, pos_y] = ElementType.CHODZACABOMBA;
                }
                catch { }
            }
            pos_x = 10;
            pos_y = 10;

            Map[pos_x, pos_y] = ElementType.CHODZACABOMBA;


            if (difficultyLevel >= 1)
                try
                {
                    Map[8 + gameLevel, 6 + gameLevel] = ElementType.AMEBA;
                }
                catch { }
            if (difficultyLevel >= 2)
                try
                {
                    Map[9 + gameLevel, 6 * gameLevel] = ElementType.AMEBA;
                }
                catch { }
            if (difficultyLevel >= 3)
                try
                {
                    Map[10 + gameLevel, 6 * gameLevel] = ElementType.AMEBA;
                }
                catch { }


            Map[1, 1] = ElementType.VANDAL;
            Map[1, 2] = ElementType.ZIEMIA;
            for(int i=2;i<10;i++)
            Map[i, 1] = ElementType.ZIEMIA;

            if(gameLevel<2)
            Map[width - 2,height - 2 ] = ElementType.CELMISJI;
            else
                Map[(int)(width/2),(int)( height/2)] = ElementType.CELMISJI;



        }
    }
}
