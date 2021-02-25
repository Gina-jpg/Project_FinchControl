using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Description: Robot performs trics using light, sound, and movement commands
    // Application Type: Console
    // Author: Anger, Gina and Velis, John
    // Dated Created: 1/22/2020
    // Last Modified: 2/24/2020
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            SetTheme();
            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            string menuChoice;
            Finch finchRobot = new Finch();
            bool quitApplication = false;


            DisplayScreenHeader("Main Menu");

            do
            {

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice (remember, first must be connected to finch)
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                       DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        Console.WriteLine("\tWIP. Please choose another option");
                        break;

                    case "e":
                        Console.WriteLine("\tWIP. Please choose another option");
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region DATA RECORDER

        /// <summary>
        /// *****************************************************************
        /// *                     Data Recorder Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitDataRecorderMenu = false;
            string menuChoice;

            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;

            do
            {
                DisplayScreenHeader("Data Recorder");


                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of data points");
                Console.WriteLine("\tb) Frequency of data points");
                Console.WriteLine("\tc) Get data"); //TODO: change to temps
                Console.WriteLine("\td) Show data");
                Console.WriteLine("\te) Get light values");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":

                        temperatures = DataRecorderDisplayData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":

                        DataRecorderDisplayDataTable(temperatures);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitDataRecorderMenu);
        }
            /// <summary>
        /// Display Data Table of Values
        /// </summary>
        /// <param name="temperatures"></param>
            static void DataRecorderDisplayDataTable(double[] temperatures)
        {
            DisplayScreenHeader("Temperatures");

            //display table of temperatures


            Console.WriteLine();
            Console.WriteLine(
                "Reading #".PadLeft(20) +
                "Temperature.PadLeft(15)"
                );
            Console.WriteLine(
                "_______".PadLeft(20) +
                "___________.PadLeft(15)"
                );

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(20) +
                    temperatures[index].ToString().PadLeft(15)
                    );
            }

            DisplayContinuePrompt();
        }


            /// <summary>
        /// Get temperatures from robot
        /// </summary>
        /// <param name="numberOfDataPoints">number of data points</param>
        /// <param name="dataPointFrequency">data point frequency</param>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>temperatures</returns>
            static double[] DataRecorderDisplayData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints];


            DisplayScreenHeader("Temperatures");

            //echo the values
            Console.WriteLine($"The Finch robot will now record {numberOfDataPoints} temperatures {dataPointFrequency} seconds apart");
            DisplayContinuePrompt();

            

            //Display Table of temps
            DataRecorderDisplayDataTable(temperatures);

            //TODO: add sum or average

            DisplayMenuPrompt("Data Recorder");

            return temperatures;
        }

            /// <summary>
            /// Get data point frequancy from user
            /// </summary>
            /// <returns>data point frequency</returns>
            static double DataRecorderDisplayGetDataPointFrequency()
            {
                double dataPointFrequency;

                DisplayScreenHeader("Number Of Data Points");

                //TODO Validate the number!
                Console.WriteLine("Data Point Frequency");
                dataPointFrequency = double.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine($"You chose {dataPointFrequency} as the frequency of data points");

                DisplayMenuPrompt("Data Recorder");

                return dataPointFrequency;
            }
            /// <summary>
            /// Get Number of Data Points From User
            /// </summary>
            /// <returns>number of data points</returns>
            static int DataRecorderDisplayGetNumberOfDataPoints()
            {
                int numberOfDataPoints;

                DisplayScreenHeader("Number Of Data Points");

                //TODO validate the number!
                Console.WriteLine("Number of Data Points");
                numberOfDataPoints = int.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine($"You chose {numberOfDataPoints} as the number of data points");

                DisplayMenuPrompt("Data Recorder");

                return numberOfDataPoints;
            }

            #endregion

        #region TALENT SHOW

            /// <summary>
            /// *****************************************************************
            /// *                     Talent Show Menu                          *
            /// *****************************************************************
            /// </summary>
            static void TalentShowDisplayMenuScreen(Finch finchRobot)
            {
                Console.CursorVisible = true;

                bool quitTalentShowMenu = false;
                string menuChoice;

                do
                {
                    DisplayScreenHeader("Talent Show Menu");

                    //
                    // get user menu choice
                    //
                    Console.WriteLine("\ta) Light and Sound");
                    Console.WriteLine("\tb) Dance");
                    Console.WriteLine("\tc) Mixing it Up");
                    //Console.WriteLine("\td) Play Song");
                    Console.WriteLine("\tq) Main Menu");
                    Console.Write("\t\tEnter Choice:");
                    menuChoice = Console.ReadLine().ToLower();

                    //
                    // process user menu choice
                    //
                    switch (menuChoice)
                    {
                        case "a":
                            TalentShowDisplayLightAndSound(finchRobot);
                            break;

                        case "b":
                            TalentShowDance(finchRobot);
                            break;

                        case "c":
                            TalentShowMixingItUp(finchRobot);
                            break;

                        case "d":
                        Console.WriteLine("Module is still under development.");
                            break;

                        case "q":
                            quitTalentShowMenu = true;
                            break;

                        default:
                            Console.WriteLine();
                            Console.WriteLine("\tPlease enter a letter for the menu choice.");
                            DisplayContinuePrompt();
                            break;
                    }

                } while (!quitTalentShowMenu);
            }

            /// <summary>
            /// *****************************************************************
            /// *               Talent Show > Light and Sound                   *
            /// *****************************************************************
            /// </summary>
            /// <param name="finchRobot">finch robot object</param>
            static void TalentShowDisplayLightAndSound(Finch finchRobot)
            {
                Console.CursorVisible = false;

                DisplayScreenHeader("Light and Sound");

                Console.WriteLine("\tThe Finch robot will now show off its glowing talent!");
                Console.WriteLine("Enter a value for a note frequency");

                string userResponse = "600";
                userResponse = Console.ReadLine();

            if (Int32.TryParse(userResponse, out int note))
            {
                Console.WriteLine($"\tYou entered {note}.");
            }
            else
            {
                Console.WriteLine("\tYou did not enter a numerical value. 600 will be selected as the default.");
            }

                DisplayContinuePrompt();

            for (double r = 200; r < note; r++)
            {
                finchRobot.noteOn((int)(Math.PI * r));
            }

            for (double theta = 0; theta < (Math.PI / 2) * 255; theta++)
                {
                    finchRobot.setLED((int)Math.Cos(theta) * 255, (int)Math.Sin(theta) * 255, (int)theta); 
                }


            DisplayContinuePrompt();
            ResetFinchRobot(finchRobot);

                DisplayMenuPrompt("Talent Show Menu");
            }
            /// <summary>
            /// *****************************************************************
            /// *               Talent Show > Dance                  *
            /// *****************************************************************
            /// </summary>
            /// <param name="finchRobot"></param>
        static void TalentShowDance(Finch finchRobot)
            {
                DisplayScreenHeader("Dance");
                Console.WriteLine("Cornelius will now show off his new dance. Make sure he has planty of room.");
                DisplayContinuePrompt();

                finchRobot.setMotors(0, 100);
                finchRobot.wait(1000);
                finchRobot.setMotors(0, -100);
                finchRobot.wait(1000);
                finchRobot.setMotors(100, 0);
                finchRobot.wait(1000);
                finchRobot.setMotors(-100, 0);
                finchRobot.wait(100);

                finchRobot.setMotors(200, 0);
                finchRobot.wait(500);
                finchRobot.setMotors(0, -200);
                finchRobot.wait(1000);
                finchRobot.setMotors(0, 0);

                Console.WriteLine("Dance complete :)");
                ResetFinchRobot(finchRobot);

                DisplayMenuPrompt("Talent Show Menu");
            }
            /// <summary>
            /// *****************************************************************
            /// *               Talent Show > Mixing it Up                 *
            /// *****************************************************************
            /// </summary>
            /// <param name="finchRobot"></param>
        static void TalentShowMixingItUp(Finch finchRobot)
            {
                DisplayScreenHeader("Mixing It Up");
                Console.WriteLine("Little CornBread is going to show you how he keeps things interesting. Please make room and prepare for some noise");
                DisplayContinuePrompt();
                
                finchRobot.setLED(255, 0, 0);
                finchRobot.setMotors(200, 250);
                finchRobot.wait(1000);
                finchRobot.setLED(0, 255, 0);
                finchRobot.setMotors(250, 200);
                finchRobot.wait(1000);
                finchRobot.setMotors(0, 0);

                finchRobot.setLED(0, 0, 0);

                for (int i = 0; i < 400; i++)
                {
                    finchRobot.noteOn(i + 100);
                }
                for (int n = 500; n > 0; n--)
                {
                    finchRobot.noteOn(n + 100);
                }

                ResetFinchRobot(finchRobot);
                DisplayMenuPrompt("Talent Show Menu");

            }

            #endregion

        #region FINCH ROBOT MANAGEMENT

            /// <summary>
            /// *****************************************************************
            /// *               Disconnect the Finch Robot                      *
            /// *****************************************************************
            /// </summary>
            /// <param name="finchRobot">finch robot object</param>
            static void DisplayDisconnectFinchRobot(Finch finchRobot)
            {
                Console.CursorVisible = false;

                DisplayScreenHeader("Disconnect Finch Robot");

                Console.WriteLine("\tAbout to disconnect from the Finch robot.");
                DisplayContinuePrompt();

                finchRobot.disConnect();

                Console.WriteLine("\tThe Finch robot is now disconnected.");

                DisplayMenuPrompt("Main Menu");
            }

            /// <summary>
            /// *****************************************************************
            /// *                  Connect the Finch Robot                      *
            /// *****************************************************************
            /// </summary>
            /// <param name="finchRobot">finch robot object</param>
            /// <returns>notify if the robot is connected</returns>
            static bool DisplayConnectFinchRobot(Finch finchRobot)
            {
                Console.CursorVisible = false;

                bool robotConnected;

                DisplayScreenHeader("Connect Finch Robot");

                Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
                DisplayContinuePrompt();


            do {
                robotConnected = finchRobot.connect();

                if (robotConnected)
                {
                    finchRobot.setLED(0, 0, 255);
                    finchRobot.wait(1500);
                    Console.WriteLine();
                    Console.WriteLine("\tCornelius is connected");
                    robotConnected = true;
                }
                else
                {
                    finchRobot.setLED(255, 0, 0);
                    finchRobot.wait(1500);
                    Console.WriteLine("\tCornelius cannot connect");
                    robotConnected = false;
                }
            }while(!robotConnected);

                DisplayContinuePrompt();
                ResetFinchRobot(finchRobot);

                DisplayMenuPrompt("Main Menu");

            return robotConnected;
            }
        /// <summary>
        /// Reset Finch Robot
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void ResetFinchRobot(Finch finchRobot)
        {

            finchRobot.setMotors(0,0);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
            {
                Console.CursorVisible = false;

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\tFinch Control");
                Console.WriteLine("\t\tIntroducting Cornelius");
                Console.WriteLine();

                DisplayContinuePrompt();
            }

            /// <summary>
            /// *****************************************************************
            /// *                     Closing Screen                            *
            /// *****************************************************************
            /// </summary>
            static void DisplayClosingScreen()
            {
                Console.CursorVisible = false;

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\tThank you for using Finch Control!");
                Console.WriteLine();

                DisplayContinuePrompt();
            }

            /// <summary>
            /// display continue prompt
            /// </summary>
            static void DisplayContinuePrompt()
            {
                Console.WriteLine();
                Console.WriteLine("\tPress any key to continue.");
                Console.ReadKey();
            }

            /// <summary>
            /// display menu prompt
            /// </summary>
            static void DisplayMenuPrompt(string menuName)
            {
                Console.WriteLine();
                Console.WriteLine($"\tPress any key to return to the {menuName}.");
                Console.ReadLine();
            }

            /// <summary>
            /// display screen header
            /// </summary>
            static void DisplayScreenHeader(string headerText)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\t" + headerText);
                Console.WriteLine();
            }

            #endregion
        }
    }
