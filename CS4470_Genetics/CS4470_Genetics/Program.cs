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
            string targetString = "this";
            int maxPopulation = 200;
            double mutationRate = 0.05;

            Population population = new Population(maxPopulation, targetString, mutationRate);

            Console.WriteLine("Target String: {0}, Max Population: {1}, Mutation Rate: {2}", targetString, maxPopulation, mutationRate);

            while(population.targetFound == false)
            {
                population.calculateFitness();
                population.performNatrualSelection();
                population.generateNextPopulation();
                Console.WriteLine(population.bestFitnessDNA.ToString());
            }

            Console.WriteLine("Found target in {0} generations.", population.generations);

        }
    }
}
