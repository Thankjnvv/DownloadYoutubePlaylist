﻿using System;
using System.Threading;
using System.Collections.Generic;
using DownloadYoutubePlaylist.FileManagement;

namespace DownloadYoutubePlaylist
{
    public static class UIManager
    {
        private const int MAX_THREADS = 4;

        public static void Menu(out Thread [] threadArray)
        {
            try
            {
                ReadInputType();
                if (Resources.InputType == 1)
                {
                    ReadArtistAndLimit();
                }
                else
                {
                    ReadArtists();
                }
            }
            catch
            {
                Console.WriteLine("Oops, something went wrong :'(");
            }
            finally
            {
                threadArray = new Thread[ReadNumOfThreads()];
            }
        }

        private static void ReadInputType()
        {
            Console.WriteLine("Press 1 to insert an artist's name");
            Console.WriteLine("Press 2 to insert a file path");
            int inputType;
            try
            {
                inputType = Convert.ToInt32(Console.ReadLine());
                if (inputType < 1 || inputType > 2)
                {
                    inputType = 1;
                }
            }
            catch
            {
                inputType = 1;
            }

            Resources.InputType = inputType;
        }

        private static void ReadArtistAndLimit()
        {
            Console.WriteLine("Enter artist's name");
            Resources.Artists = new string[] { Console.ReadLine().Trim() };
        }

        private static void ReadArtists()
        {
            Console.WriteLine("Enter File's Path");
            
            Resources.Artists =  FileReader.ReadFromFile(Console.ReadLine())
                .Split(new string[] { "\r\n" }, StringSplitOptions.None);
        }

        private static int ReadNumOfThreads()
        {
            Console.WriteLine("How many instances of chrome would you like to work on the job? (Max {0})", MAX_THREADS);
            int numOfThreads;
            try
            {
                numOfThreads = Convert.ToInt32(Console.ReadLine());
                if (numOfThreads < 1 || numOfThreads > MAX_THREADS)
                {
                    numOfThreads = 1;
                }
            }
            catch
            {
                numOfThreads = 1;
            }

            return numOfThreads;
        }
    }
}