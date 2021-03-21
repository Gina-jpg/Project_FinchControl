using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Description: Robot uses light, sound, and movement commands and shows sensor readings with user input
    // Application Type: Console
    // Author: Anger, Gina and Velis, John
    // Dated Created: 1/22/2020
    // Last Modified: 2/28/2020
    //
    // **************************************************


    /// <summary>
    /// USER COMMANDS
    /// </summary>
    public enum Command
    {
        NONE, //Default
        SONG_AND_DANCE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        DONE
    }

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
                        AlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(finchRobot);
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
            Console.WriteLine("\tPlease enter a value for a note frequency");

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

            for (double theta = 0; theta < (Math.PI / 2) * 250; theta++)
            {
                finchRobot.setLED((int)Math.Cos(theta) * 100, (int)Math.Sin(theta) * 100, (int)Math.Cos(theta) * 100);
            }


            DisplayContinuePrompt();
            ResetFinchRobot(finchRobot);

            Console.Clear();
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
            Console.WriteLine("\tCornelius will now show off his new dance. Make sure he has planty of room.");
            DisplayContinuePrompt();

            finchRobot.setMotors(0, 500);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, -500);
            finchRobot.wait(1000);
            finchRobot.setMotors(500, 0);
            finchRobot.wait(1000);
            finchRobot.setMotors(-500, 0);
            finchRobot.wait(100);

            finchRobot.setMotors(600, 0);
            finchRobot.wait(500);
            finchRobot.setMotors(0, -600);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);

            Console.WriteLine("\tDance complete :)");
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
            Console.WriteLine("\t CornBread is going to show you how he keeps things interesting. " +
                "\tPlease make room and prepare for some noise");
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

            Console.Clear();
            DisplayMenuPrompt("Talent Show Menu");

        }

        #endregion

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
            double[] lightValues = null;
            do
            {
                DisplayScreenHeader("Data Recorder");


                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of data points");
                Console.WriteLine("\tb) Frequency of data points");
                Console.WriteLine("\tc) Get temperatures"); 
                Console.WriteLine("\td) Get light values");
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
                        temperatures = DataRecorderDisplayTemps(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        lightValues = DataRecorderDisplayLightValues(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        Console.Clear();
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
        /// Get light values from robot
        /// </summary>
        /// <param name="numberOfDataPoints"></param>
        /// <param name="dataPointFrequency"></param>
        /// <param name="finchRobot"></param>
        /// <returns>light values</returns>
            static double[] DataRecorderDisplayLightValues(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] lightValues = new double[numberOfDataPoints];

            DisplayScreenHeader("\tLight Values");

            Console.WriteLine($"\tThe Finch robot will now record {numberOfDataPoints} sensor readings {dataPointFrequency} seconds apart");

            DataRecorderDisplayLightDataTable(lightValues, dataPointFrequency, finchRobot);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        

            DisplayMenuPrompt("Data Recorder");

            return lightValues;
        }


            /// <summary>
        /// Display table of values for light sensor readings
        /// </summary>
        /// <param name="lightValues">light values</param>
        /// <param name="dataPointFrequency">data point frequency</param>
        /// <param name="finchRobot">finch robot object</param>
            static void DataRecorderDisplayLightDataTable(double[] lightValues, double dataPointFrequency, Finch finchRobot)
        {
            DisplayScreenHeader("Light Values");

            Console.WriteLine(
                "Reading #".PadLeft(20) +
                "Left light value".PadLeft(25) 
                );


            Console.WriteLine(
                 "_________".PadLeft(20) +
                 "________________".PadLeft(25));

                for (int index = 0; index < lightValues.Length; index++)
                {
                    //left light values
                    lightValues[index] = finchRobot.getLeftLightSensor();

                    Console.WriteLine(
                       (index + 1).ToString().PadLeft(20) +
                       lightValues[index].ToString("N2").PadLeft(25)
                       );

                finchRobot.wait((int)dataPointFrequency * 1000);
                }

            Console.WriteLine(
                "Reading #".PadLeft(20) +
                "Right light value".PadLeft(25)
                );

            Console.WriteLine(
                "_________".PadLeft(20) +
                "________________".PadLeft(25)
                );

                for (int index = 0; index < lightValues.Length; index++)
                {
                     //right light values
                        lightValues[index] = finchRobot.getRightLightSensor();

                        Console.WriteLine(
                        (index + 1).ToString().PadLeft(20) +
                        lightValues[index].ToString("N2").PadLeft(25)
                        );

                finchRobot.wait((int)dataPointFrequency * 1000);
                }

          Console.WriteLine(
              "Reading #".PadLeft(20) +
              "Average light values".PadLeft(30)
              );

            Console.WriteLine(
                "_________".PadLeft(20) +
                "____________________".PadLeft(25)
                );


            double[] averageLightValues = new double[lightValues.Length];


            for (int index = 0; index < lightValues.Length; index++)
            {
                //average light values
                int[]bothSensors = finchRobot.getLightSensors();
                averageLightValues[index] = bothSensors.Average();


                Console.WriteLine(
                        (index + 1).ToString().PadLeft(20) +
                        averageLightValues[index].ToString().PadLeft(25)
                        );
                finchRobot.wait((int)dataPointFrequency * 1000);
            }

            DisplayContinuePrompt();
        }


            /// <summary>
            ///  Display table of values for temperature sensor readings
            /// </summary>
            /// <param name="temperatures">temperatures</param>
            /// <param name="dataPointFrequency">data point frequency</param>
            /// <param name="finchRobot">finch robot object</param>
            static void DataRecorderDisplayTempsDataTable(double[] temperatures, double dataPointFrequency, Finch finchRobot)
        {
            DisplayScreenHeader("Temperatures");

            //display headers for table of temperatures


            Console.WriteLine();
            Console.WriteLine(
                "Reading #".PadLeft(20) +
                "Temperature (C)".PadLeft(25)+
                "Temperature (F)".PadLeft(30)
                );

            Console.WriteLine(
                "_________".PadLeft(20) +
                "________________".PadLeft(25) +
                "________________".PadLeft(30)
                );

            /*******************************************************/

           
            //Display Celcius and Fahrenheit values in table
            for (int index = 0; index < temperatures.Length; index++)
            {
                
                double celciusTemp = finchRobot.getTemperature();
                temperatures[index] = celciusTemp;
                double fahrenheitTemp = ConvertCelciusToFahrenheit(celciusTemp);

                Console.WriteLine(
                    (index + 1).ToString().PadLeft(20) +
                    temperatures[index].ToString("N2").PadLeft(25) +
                    fahrenheitTemp.ToString("N2").PadLeft(30)
                   );

                finchRobot.wait((int)dataPointFrequency * 1000);

              
            }


            DisplayContinuePrompt();
        }


            /// <summary>
            /// Converts Celcius to Fahrenheit for temperature inputs
            /// </summary>
            /// <param name="celciusTemp"></param>
            /// <returns>temperature in Fahrenheit</returns>
            static double ConvertCelciusToFahrenheit(double celciusTemp)
        {

            double fahrenheitTemp;
            fahrenheitTemp = ((9 * celciusTemp) / 5) + 32;
            return fahrenheitTemp;
        }    


            /// <summary>
        /// Get temperatures from robot
        /// </summary>
        /// <param name="numberOfDataPoints">number of data points</param>
        /// <param name="dataPointFrequency">data point frequency</param>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>temperatures</returns>
            static double[] DataRecorderDisplayTemps(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints];


            DisplayScreenHeader("Temperatures");

            //echo the values
            Console.WriteLine($"\tThe Finch robot will now record {numberOfDataPoints} temperatures {dataPointFrequency} second/(s) apart");
            DisplayContinuePrompt();


            //Display Table of temps
            DataRecorderDisplayTempsDataTable(temperatures, dataPointFrequency, finchRobot);
            Console.Clear();

            DisplayScreenHeader("Data Analysis");

            Console.WriteLine($"\tAverage Temperature:{temperatures.Average().ToString("N2")}");
            Console.WriteLine($"\tSum of Temperatures:{temperatures.Sum().ToString("N2")}");
            Console.WriteLine();
            DisplayContinuePrompt();
            Console.Clear();
            Console.WriteLine("\t\tData recording is complete.");

            DisplayMenuPrompt("Main Menu");

            return temperatures;
        }


            /// <summary>
            /// Get data point frequency from user
            /// </summary>
            /// <returns>data point frequency</returns>
            static double DataRecorderDisplayGetDataPointFrequency()
            {
                double dataPointFrequency;
                bool validResponse = true;
                string userResponse;

                DisplayScreenHeader("Data Point Frequency");

                Console.WriteLine("\tPlease enter frequency of data points in seconds");

            do
            {
                userResponse = Console.ReadLine();
                if (double.TryParse(userResponse, out dataPointFrequency))
                {
                    Console.WriteLine($"\tYou chose {dataPointFrequency} as the frequency of data points");
                    validResponse = true;
                }
                else if (double.TryParse(userResponse, out dataPointFrequency) && dataPointFrequency < 0)
                {
                    Console.WriteLine("\tPlease enter a positive real number");
                    validResponse = false;
                }
                else 
                {
                    Console.WriteLine("\tPlease enter a positive real number.");
                    validResponse = false;
                }
               
            } while (!validResponse);

                DisplayMenuPrompt("Data Recorder");

                return dataPointFrequency;
            }


            /// <summary>
            /// Get Number of Data Points From User
            /// </summary>
            /// <returns>number of data points</returns>
            static int DataRecorderDisplayGetNumberOfDataPoints()
            {
                string userResponse;
                int numberOfDataPoints;
                bool validResponse;

                DisplayScreenHeader("\tNumber Of Data Points");

                Console.WriteLine("\tPlease enter number of data points");
            do
            {
                validResponse = true;
                userResponse = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(userResponse, out numberOfDataPoints))
                {
                    Console.WriteLine($"\tYou chose {numberOfDataPoints} as the number of data points");
                }
                else if (int.TryParse(userResponse, out numberOfDataPoints) && numberOfDataPoints < 0)
                {
                    Console.WriteLine("\tPlease enter a positive real number");
                    validResponse = false;
                }
                else
                {
                    Console.WriteLine("\tPlease enter a positive real number");
                    validResponse = false;
                }
            }while (!validResponse);

                DisplayMenuPrompt("Data Recorder");

                return numberOfDataPoints;}


        #endregion

        #region ALARM SYSTEM

        /// <summary>
        /// *****************************************************************
        /// *                     Alarm System Menu                         *        
        /// ***************************************************************** 
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitAlarmSystemMenu = false;
            string menuChoice;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;
            int tempThreshold = 0;

            do
            {
                DisplayScreenHeader("Alarm System Menu");


                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Get light sensors to monitor");
                Console.WriteLine("\tb) Set range type");
                Console.WriteLine("\tc) Set minimum/maximum light threshold value");
                Console.WriteLine("\td) Set minimum/maximum temperature threshold value");
                Console.WriteLine("\te) Set time to monitor");
                Console.WriteLine("\tf) Set light alarm");
                Console.WriteLine("\tg) Set temperature alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = AlarmSystemDisplayGetSensors();
                        break;

                    case "b":
                        rangeType = AlarmSystemDisplayGetRangeType(finchRobot);
                        break;

                    case "c":
                        minMaxThresholdValue = AlarmSystemDisplayGetLightThresholdValue(finchRobot, sensorsToMonitor);
                        break;
                    case "d":
                        tempThreshold = AlarmSystemDisplayGetTemperatureThresholdValue(finchRobot);
                        break;

                    case "e":
                        timeToMonitor = AlarmSystemDisplayGetTimeToMonitor(finchRobot);
                        break;

                    case "f":
                        AlarmSystemDisplaySetLightAlarm(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "g":
                        AlarmSystemDisplaySetTempAlarm(finchRobot, rangeType, tempThreshold, timeToMonitor);
                        break;

                    case "q":
                        quitAlarmSystemMenu = true;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitAlarmSystemMenu);
        }

     
        /// <summary>
        /// Get temperature threshold value from user
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>temperature threshold value</returns>
        static int AlarmSystemDisplayGetTemperatureThresholdValue(Finch finchRobot)
        {
            int tempThreshold;

            DisplayScreenHeader("Temperature Threshold");

            Console.WriteLine($"Current temperature: {finchRobot.getTemperature().ToString("N2")}");
            //get threshold from user
            Console.WriteLine("Enter Temperature Threshold");


            bool validResponse = false;

            do
            {
                string userResponse = Console.ReadLine();

                if (!int.TryParse(userResponse, out tempThreshold))
                {
                    Console.WriteLine("Please enter a positive whole number");
                    validResponse = false;
                }
                else
                {
                    validResponse = true;
                    Console.WriteLine($"You entered {tempThreshold} as your threshold to alarm");
                }

            } while (!validResponse);


            DisplayMenuPrompt("Alarm System Menu");


            return tempThreshold;
        }


        /// <summary>
        /// Monitor temperature values and alarm if temperature threshold is exceeded
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <param name="rangeType">minimum or maximum range type</param>
        /// <param name="tempThreshold">temperature threshold</param>
        /// <param name="timeToMonitor">time to monitor</param>
        static void AlarmSystemDisplaySetTempAlarm(Finch finchRobot, string rangeType, int tempThreshold, int timeToMonitor)
        {
            DisplayScreenHeader("Temperature Alarm");

            Console.WriteLine("\tStart");
            DisplayContinuePrompt();

            //Create temp array
            double[] tempValues = new double[timeToMonitor];
            bool thresholdExceeded = false;
            int secondsElapsed = 0;

            do {


                for (int index = 0; index < tempValues.Length; index++)
                {

                    tempValues[index] = finchRobot.getTemperature();
                    double temperature = tempValues[index];

                    if (!thresholdExceeded && secondsElapsed < timeToMonitor)
                    {
                        //loop through temperature values
                        Console.WriteLine($"Temperature reads {temperature} in degrees Celcius");

                        //wait 1 second & increment
                        finchRobot.wait(1000);
                        secondsElapsed++;
                    }
                    

                    //set thresholds
                    switch (rangeType)
                    {
                        case "minimum":
                            thresholdExceeded = (temperature <= tempThreshold);
                            break;

                        case "maximum":
                            thresholdExceeded = (temperature >= tempThreshold);
                            break;

                        default:
                            Console.WriteLine("Cannot determine whether value was exceeded");
                            break;
                    }
                }
                    
                
            } while (!thresholdExceeded && secondsElapsed < timeToMonitor);


            if (thresholdExceeded)
            {
                Console.WriteLine("Threshold Exceeded");

                if (thresholdExceeded)
                {
                    //alarm

                    finchRobot.noteOn(500);
                    finchRobot.wait(2000);
                    finchRobot.noteOff();
                }

            }
            else
            {
                Console.WriteLine("Threshold Not Exceeded Within Time Limit");
            }


            DisplayMenuPrompt("Alarm System");

        }


        /// <summary>
        /// Monitor light values and alarm if light threshold is exceeded
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <param name="sensorsToMonitor">sensors to monitor</param>
        /// <param name="rangeType">minimum or maximum range type</param>
        /// <param name="minMaxThresholdValue">light min or max threshold value</param>
        /// <param name="timeToMonitor">time to monitor</param>
        static void AlarmSystemDisplaySetLightAlarm(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {

            bool thresholdExceeded = false;
            int secondsElapsed = 1;
            int leftLightSensorValue;
            int rightLightSensorValue;

            DisplayScreenHeader("Set Alarm");

            //echo values to user
            Console.WriteLine("\tStart");
            DisplayContinuePrompt();

            do
            {
                //get current light levels
                leftLightSensorValue = finchRobot.getLeftLightSensor();
                rightLightSensorValue = finchRobot.getRightLightSensor();

                switch (sensorsToMonitor)
                {
                    case "left":
                        Console.WriteLine($"Current left light sensor reads {leftLightSensorValue}");
                        break;
                    case "right":
                        Console.WriteLine($"Current right light sensor reads {rightLightSensorValue}");
                        break;
                        
                    case "both":
                        Console.WriteLine($"Current left light sensor reads {leftLightSensorValue}");
                        Console.WriteLine($"Current right light sensor reads {rightLightSensorValue}");
                        break;

                    default:
                        Console.WriteLine("Cannot find light sensor values");
                        break;
                }
                //wait 1 second and increment
                finchRobot.wait(1000);
                secondsElapsed++;

                //test for threshold exceeded
                switch (sensorsToMonitor)
                {
                    case "left":
                        if (rangeType == "minimum")
                        {
                            thresholdExceeded = (leftLightSensorValue <= minMaxThresholdValue); //if threshold exceeded T or F - same as below
                        }
                        else //maximum
                        {
                            thresholdExceeded = (leftLightSensorValue >= minMaxThresholdValue);
                        }
                        break;

                    case "right":
                        if (rangeType == "minimum")
                        {
                            thresholdExceeded = (rightLightSensorValue <= minMaxThresholdValue);

                        }
                        else //maximum
                        {
                            thresholdExceeded = (rightLightSensorValue >= minMaxThresholdValue);
                        }
                        break;

                    case "both":
                        if (rangeType == "minimum")
                        {
                            thresholdExceeded = ((leftLightSensorValue <= minMaxThresholdValue) || (rightLightSensorValue <= minMaxThresholdValue));
                        }
                        else //maximum
                        {
                            thresholdExceeded = (leftLightSensorValue >= minMaxThresholdValue) || (rightLightSensorValue >= minMaxThresholdValue);
                        }
                        break;

                    default:
                        Console.WriteLine("Cannot determine whether value was exceeded");
                        break;

                }


            } while (!thresholdExceeded && secondsElapsed <= timeToMonitor);

            //Display Results

            if (thresholdExceeded)
            {
                Console.WriteLine("Threshold Exceeded");

                if (thresholdExceeded)
                {
                    //alarm

                    finchRobot.noteOn(500);
                    finchRobot.wait(2000);
                    finchRobot.noteOff();
                }

            }
            else
            {
                Console.WriteLine("Threshold Not Exceeded Within Time Limit");
            }



            DisplayMenuPrompt("Alarm System Menu");
        }


        /// <summary>
        /// Get time to monitor from user
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>time to monitor</returns>
        static int AlarmSystemDisplayGetTimeToMonitor(Finch finchRobot)
        {
            int timeToMonitor = 0;

            DisplayScreenHeader("Time to Monitor");

            Console.WriteLine("Enter time to monitor in seconds");

            bool validResponse = false;

            //validate input in seconds
            do
            {
                string userResponse = Console.ReadLine();

                if (!int.TryParse(userResponse, out timeToMonitor))
                {
                    Console.WriteLine("Please enter a positive whole number");
                    validResponse = false;
                }
                else
                {
                    validResponse = true;
                    Console.WriteLine($"You entered {timeToMonitor} seconds as your time to monitor");
                }

            } while (!validResponse);


            DisplayMenuPrompt("Alarm System Menu");

            return timeToMonitor;
        }


        /// <summary>
        /// Get light threshold value from user
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <param name="sensorsToMonitor">sensors to monitor</param>
        /// <returns>light threshold value</returns>
        static int AlarmSystemDisplayGetLightThresholdValue(Finch finchRobot, string sensorsToMonitor)
        {
            int thresholdValue = 0;
            int currentLeftSensorValue = finchRobot.getLeftLightSensor();
            int currentRightSensorValue = finchRobot.getRightLightSensor();
            DisplayScreenHeader("Threshold Value");

            //display ambient values
            switch (sensorsToMonitor)
            {
                case "left":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                    break;

                case "right":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");
                    break;

                case "both":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");
                    break;

                default:
                    Console.WriteLine("\tUnkown sensor reference");
                    break;
            }

            //get threshold from user
            Console.WriteLine("Enter Light Threshold");


            bool validResponse = false;

            //validate threshold
            do
            {
                string userResponse = Console.ReadLine();

                if (!int.TryParse(userResponse, out thresholdValue)) 
                {
                    Console.WriteLine("Please enter a positive whole number");
                    validResponse = false;
                }
                else
                {
                    validResponse = true;
                    Console.WriteLine($"You entered {thresholdValue} as your threshold to alarm");
                }

            } while (!validResponse);


            DisplayMenuPrompt("Alarm System Menu");

            return thresholdValue;
        }


        /// <summary>
        /// Get range type from user
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>minimum or maximum range type</returns>
         static string AlarmSystemDisplayGetRangeType(Finch finchRobot)
        {
            string rangeType = "";

            DisplayScreenHeader("Range Type");

            Console.WriteLine("Enter range type [minimum, maximum]:");
            

            bool validResponse = false;

            //validate range type
            do
            {
                rangeType = Console.ReadLine();

                if (rangeType != "minimum" && rangeType != "maximum")
                {
                    validResponse = false;
                    Console.WriteLine("Please enter 'minimum' or 'maximum'");
                }
                else
                {
                    validResponse = true;
                    Console.WriteLine($"You entered {rangeType} as the range type");
                }

            } while (!validResponse);


            DisplayMenuPrompt("Alarm System Menu");

            return rangeType;
        }


         /// <summary>
         /// Get sensors to monitor (left, right, both)
         /// </summary>
         /// <returns>sensors to monitor</returns>
         static string AlarmSystemDisplayGetSensors()
        {
            string sensorsToMonitor = "";

            DisplayScreenHeader("Sensors to Monitor");

            Console.WriteLine("Choose a sensor value: (left, right, both)");
            
            bool validResponse = false;

            //validate sensor choice
            do
            {
                sensorsToMonitor = Console.ReadLine();

                if (sensorsToMonitor != "left" && sensorsToMonitor != "right" && sensorsToMonitor != "both")
                {
                    validResponse = false;
                    Console.WriteLine("Please choose 'left', 'right', or 'both' as a sensor value");
                }
                else
                {
                    validResponse = true;
                    Console.WriteLine($"You chose {sensorsToMonitor} as the sensor(s) to monitor");
                }

            } while (!validResponse);

            DisplayMenuPrompt("Alarm System");

            return sensorsToMonitor;
        }


        #endregion

        #region USER PROGRAMMING

        /// <summary>
        /// *****************************************************************
        /// *                     User Programming Menu                     *
        /// *****************************************************************
        /// </summary>
        static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitAlarmSystemMenu = false;
            string menuChoice;

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters = (0,0,0);  //putting assignment in tuple
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;
            List<Command> commands = null;


            do
            {
                DisplayScreenHeader("User Programming Menu");


                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = userProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        commands = UserProgrammingDisplayGetFinchCommands();
                        break;

                    case "c":
                        UserProgrammingDisplayViewCommands(commands);
                        break;
                    case "d":
                        UserProgrammingDisplayExecuteCommands(finchRobot, commands, commandParameters);
                        break;

                    case "q":
                        quitAlarmSystemMenu = true;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitAlarmSystemMenu);
        }


        /// <summary>
        /// Get Command Parameters from User
        ///<returns>commandParameters</returns>
        static (int motorSpeed, int ledBrightness, double waitSeconds) userProgrammingDisplayGetCommandParameters()
        {
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            int minThreshold = 0;
            int maxThreshold = 0;
            string userResponse = "";


            DisplayScreenHeader("Command Parameters");
        /*************************************************/

            //Motors
            Console.Write("\tMotor Speed: ");
            commandParameters.motorSpeed = UserProgrammingValidationMethod(userResponse, -255, 255);
            

            //LED
            Console.Write("\tLED Brightness: ");
            commandParameters.ledBrightness = UserProgrammingValidationMethod(userResponse, 0, 255);


            //Wait
            Console.Write("\tWait Time Seconds: ");
            commandParameters.waitSeconds = UserProgrammingValidationMethod(userResponse, 0, 180);

            DisplayContinuePrompt();

            return commandParameters;
        }


        /// <summary>
        /// Validation Method
        /// </summary>
        /// <param name="userResponse">user response</param>
        /// <param name="minThreshold">minimum accepted value</param>
        /// <param name="maxThreshold">maximum accepted value</param>
        /// <returns>validated value</returns>
        static int UserProgrammingValidationMethod(string userResponse, int minThreshold, int maxThreshold)
        {
            int validValue = 0;
            bool validResponse = false;

            do
            {
                userResponse = Console.ReadLine();

                if (int.TryParse(userResponse, out validValue) && (minThreshold <= validValue && validValue <= maxThreshold))
                {

                        Console.WriteLine($"\tValue is {validValue}");
                        Console.WriteLine();
                        validResponse = true;
                    
                }
                else
                {
                    
                    Console.WriteLine($"\t\tPlease enter a value between {minThreshold} and {maxThreshold}");
                }

            } while (!validResponse);

            return validValue;
        }


        /// <summary>
        /// Get commands from user
        /// </summary>
        /// <returns>List<Command>commands</returns>
        static List<Command> UserProgrammingDisplayGetFinchCommands()
        {
            List<Command> commands = new List<Command>();
            bool isDone = false;
            string userResponse;

            DisplayScreenHeader("Add Commands"); 

            DisplayContinuePrompt();
            Console.Write("Please enter one of the following: ");
            //Prints all commands in enum to console
            foreach (Command command in Enum.GetValues(typeof(Command)))
            {
                if (command == Command.NONE)
                {
                    Console.Write("| ");
                }
                else
                {
                    Console.Write(command + " | ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Enter 'done' when finished");
            Console.WriteLine();

            do
            {

                Console.Write("Command:");
                userResponse = Console.ReadLine();



                if (userResponse != "done")
                {
                    if (Enum.TryParse(userResponse.ToUpper(), out Command command))
                    {
                        commands.Add(command);
                    }
                    else
                    {
                        Console.WriteLine("\tThat one is not in the list. Please enter one of the commands: ");
                    }
                }
                else
                {
                    isDone = true;
                }


            } while (!isDone);


            DisplayContinuePrompt();

            return commands;
        }


        /// <summary>
        /// Display a list of entered commands
        /// </summary>
        /// <param name="commands">List<Command> commands</param>
        static void UserProgrammingDisplayViewCommands(List<Command> commands)
        {
            DisplayScreenHeader("View Commands");

            Console.WriteLine("\tCommand List:");
            Console.WriteLine("\t____________");


            foreach (Command command in commands)
            {
                Console.WriteLine("\t" + command);
            }


            DisplayContinuePrompt();
        }


        /// <summary>
        /// Execute commands chosen by user
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <param name="commands">List<Command> commands</param>
        /// <param name="commandParameters">command parameters tuple</param>
        static void UserProgrammingDisplayExecuteCommands(Finch finchRobot, List<Command> commands, (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            //The following initializations make commandParameters to variables for ease of calling
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            double waitSeconds = commandParameters.waitSeconds;

            //List<(Command command, int duration)> commandTuple = new List<(Command command, int duration)>(); //TODO: Beyond step

            DisplayScreenHeader("Execute Commands");

            Console.WriteLine("The Finch Robot will now execute all commands");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                int waitMilliseconds = (int)(waitSeconds * 1000);

                switch (command)
                {
                    case Command.NONE:
                        Console.WriteLine();
                        Console.WriteLine("\tDefault Value Error: you entered a word that was not an acceptable value"); //if they type something wrong
                        Console.WriteLine();
                        break;
                    case Command.SONG_AND_DANCE:

                                finchRobot.setLED(0, 150, 150);

                                //sing and spin left
                                finchRobot.setMotors((motorSpeed * 3)/4 , motorSpeed);

                                finchRobot.noteOn(880);
                                finchRobot.wait(1000);

                                finchRobot.noteOff();
                                finchRobot.wait(500 / 2);

                                finchRobot.noteOn(932);
                                finchRobot.wait(500);

                                //change light to red and spin right
                                finchRobot.setMotors(0, 0);
                                finchRobot.setLED(150, 0, 100);
                                finchRobot.setMotors(motorSpeed, (motorSpeed * 3)/4);

                                finchRobot.noteOn(988);
                                finchRobot.wait(1500);

                            //stop and play finale
                            finchRobot.setMotors(0,0);

                            finchRobot.noteOn(1047);
                            finchRobot.wait(500);
                            finchRobot.noteOff();

                        break;
                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        break;
                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        break;
                    case Command.WAIT:
                        finchRobot.wait(waitMilliseconds);
                        break;
                    case Command.TURNRIGHT:
                        finchRobot.setMotors(motorSpeed / 4, motorSpeed);
                        break;
                    case Command.TURNLEFT:
                        finchRobot.setMotors(motorSpeed, motorSpeed / 4);
                        break;
                    case Command.LEDON:
                        for (int i = 0; i < waitSeconds; i++)
                        {
                            finchRobot.setLED(255, 0, 0);
                            finchRobot.setLED(0, 255, 0);
                            finchRobot.setLED(0, 0, 255);
                        }
                        break;
                    case Command.LEDOFF:
                        finchRobot.setLED(0,0,0);
                        break;
                    case Command.GETTEMPERATURE:

                        double dataPointFrequency = 1;
                        int numberOFDataPoints = (int)waitSeconds;

                        Console.WriteLine($"The Finch will now record temperatures for {waitSeconds} seconds");

                        DataRecorderDisplayTemps(numberOFDataPoints, dataPointFrequency, finchRobot);

                        break;
                    case Command.DONE:
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tUnknown command error; there is a command that does not have a switch/case");
                        Console.WriteLine();
                        break;

                }

                Console.WriteLine($"\t\tCommand: {command}");
            }

            DisplayContinuePrompt();
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
                Console.Clear();

                Console.WriteLine("\tThe Finch robot is now disconnected.");
                Console.Clear();
                
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

                Console.Clear();
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
