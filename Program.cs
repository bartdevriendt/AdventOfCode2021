// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using AdventOfCode2021;
using AdventOfCode2021.Excercises;

string day = args[0];
string part = args[1];

Console.WriteLine($"Running day {day} - part {part}");

Dictionary<string, Dictionary<string, ExcerciseBase>> excercises =
    new Dictionary<string, Dictionary<string, ExcerciseBase>>();

excercises["1"] = new Dictionary<string, ExcerciseBase>();
excercises["1"]["1"] = new Day1Part1();
excercises["1"]["2"] = new Day1Part2();


excercises[day][part].Run();
