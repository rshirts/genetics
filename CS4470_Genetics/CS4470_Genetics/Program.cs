using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4470_Genetics
{
    class Program
    {
        static void Main(string[] args)
        {
            string targetString = "Match this string";
            int maxPopulation = 200;
            double mutationRate = 0.01;
            Stopwatch stopwatch = new Stopwatch();

            Population population = new Population(maxPopulation, targetString, mutationRate);

            stopwatch.Start();

            while(population.targetFound == false)
            {
                population.calculateFitness();
                population.performNatrualSelection();
                population.generateNextPopulation();
                Console.WriteLine(population.bestFitnessDNA.ToString());
            }

            stopwatch.Stop();

            Console.WriteLine("Target String of : {0}, Max Population: {1}, Mutation Rate: {2}\nUsing double point crossover and 102 chars per gene.", targetString, maxPopulation, mutationRate);
            Console.WriteLine("Found target in {0} generations.", population.generations);
            Console.WriteLine("Total time to find solution: {0}", stopwatch.Elapsed);
        }
    }
}
