﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace SleepData
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            // specify path for data file
            //string file = "/users/jgrissom/downloads/data.txt";
            string file = "C:\\Users\\JerryChiu\\Documents\\Fall 2019\\Net Database Programming 156-101-10114-20 Jeff Grissom\\Lesson 02\\data.txt";

            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter(file);
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                ArrayList sleep = new ArrayList();

                if (File.Exists(file))
                {
                    // read data from file
                    StreamReader sr = new StreamReader(file);

                    while (!sr.EndOfStream)
                    {
                        //start to read data file
                        string line = sr.ReadLine();
                        // convert string to array
                        // first splite the line between Start Date of Week and the hours slept each day of week
                        string[] arr = line.Split(',');
                        string[] dateArr = arr[0].Split('/');
                        string[] hours = arr[1].Split('|');

                        int year = Convert.ToInt16(dateArr[2]);
                        int month = Convert.ToInt16(dateArr[0]);
                        int date = Convert.ToInt16(dateArr[1]);
                        DateTime dt = new DateTime(year, month, date);

                        int total = 0;

                        for (int i = 0; i < hours.Length; i++)
                        {
                            total = total + Convert.ToInt16(hours[i]);
                        }

                        float average = (float) total / hours.Length;

                        Console.WriteLine("Week of {0}, {1}, {2}", dt.ToString("MMM"), date, year);
                        Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
                        Console.WriteLine(" -- -- -- -- -- -- -- --- ---");
                        Console.WriteLine(" {0,2} {1,2} {2,2} {3,2} {4,2} {5,2} {6,2} {7,3} {8:n1}", hours[0], hours[1], hours[2], hours[3], hours[4], hours[5], hours[6], total, average);
                        Console.WriteLine();

                    }

                    sr.Close();

                }
            }
        }
    }
}
