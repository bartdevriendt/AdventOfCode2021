// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode2021;

Dictionary<int, Dictionary<int, ExcerciseBase>> LoadExcercies()
{

    Dictionary<int, Dictionary<int, ExcerciseBase>> excercises = new ();
    
    var types = Assembly.GetAssembly(typeof(ExcerciseBase))?.GetTypes().Where(t => t.BaseType == typeof(ExcerciseBase)).ToList();
    foreach (Type t in types)
    {
        ExcerciseBase baseType = (ExcerciseBase)Activator.CreateInstance(t);

        if (!excercises.ContainsKey(baseType.Day))
        {
            excercises[baseType.Day] = new Dictionary<int, ExcerciseBase>();
        }

        excercises[baseType.Day][baseType.Part] = baseType;
    }

    return excercises;
}

var excercises = LoadExcercies();

int day = Convert.ToInt32(args[0]);
int part = Convert.ToInt32(args[1]);

Console.WriteLine($"Running day {day} - part {part}");

excercises[day][part].Run();
