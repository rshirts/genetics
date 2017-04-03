using System;
using System.Collections.Generic;
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

            Population population = new Population(maxPopulation, targetString, mutationRate);

            while(population.targetFound == false)
            {
                population.calculateFitness();
                population.performNatrualSelection();
                population.generateNextPopulation();
                Console.WriteLine(population.bestFitnessDNA.ToString());
            }

            Console.WriteLine("Target String: {0}, Max Population: {1}, Mutation Rate: {2}", targetString, maxPopulation, mutationRate);
            Console.WriteLine("Found target in {0} generations.", population.generations);

        }
    }
}
