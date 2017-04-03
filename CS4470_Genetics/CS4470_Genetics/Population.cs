using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4470_Genetics
{
    /// <summary>
    /// This is the collection of all DNA inside a population,
    /// including methods to manipulate it.
    /// </summary>
    class Population
    {
        List<DNA> currentPopulation;
        String targetGoal;
        double mutationRate;
        List<DNA> matingPool;
        public int generations = 0;
        public DNA bestFitnessDNA = null;
        double bestFitnessLevel = 0;
        public bool targetFound = false;
        public static Random r = new Random();

        //Create the initial population.
        public Population(int populationSize, string targetGoal, double mutationRate)
        {
            this.targetGoal = targetGoal;
            this.mutationRate = mutationRate;
            createNewPopulation(populationSize);
        }

        void createNewPopulation(int populationSize)
        {
            currentPopulation = new List<DNA>();
            int currentSize = 0;
            
            //create population with random genes.
            while (currentSize++ < populationSize)
            {
                currentPopulation.Add(new DNA(targetGoal.Length, false));
            }
        }

        public override string ToString()
        {
            string returnMe = null;
            foreach (var item in bestFitnessDNA.genes)
            {
                returnMe += item;
            }
            return returnMe;
        }

        //calculate the fitness of each DNA in the population.
        public void calculateFitness()
        {
            //reset values
            bestFitnessDNA = null;
            bestFitnessLevel = 0;
            //foreach (var dna in currentPopulation)
            for(int i = 0; i < currentPopulation.Count; i++)
            {
                currentPopulation[i].calculateFitness(ref targetGoal);
                //find the best fitness 
                if (currentPopulation[i].fitness >= bestFitnessLevel)
                {
                    bestFitnessDNA = currentPopulation[i];
                    if (currentPopulation[i].fitness == 1)
                    {
                        targetFound = true;
                    }
                }
            }
        }

        //This creates a mating pool and then adds that DNA to the mating Pool a numer of times based on it's fitness level.
        public void performNatrualSelection()
        {
            //crete a new mating pool.
            matingPool = new List<DNA>();

            //Add each gene to mating pool based off fitness.
            foreach (var dna in currentPopulation)
            {
                //if (dna.fitness > 0)
                //{
                //    Console.WriteLine("fitness found: \n\t{0}\n\t{1} ", dna.ToString(), targetGoal);
                //}
                int n = (int)Math.Round(dna.fitness * 100);
                for (int i = 0; i < n; i++)
                {
                    matingPool.Add(dna);
                }
            }
        }

        //Go through the mating pool and create a new population.
        public void generateNextPopulation()
        {
            if (matingPool.Count > 0)
            {
                for (int i = 0; i < currentPopulation.Count; i++)
                {
                    int a = r.Next(0, matingPool.Count - 1);
                    int b = r.Next(0, matingPool.Count - 1);
                    DNA parentA = matingPool[a];
                    DNA parentB = matingPool[b];
                    DNA child = parentA.crossover(parentB);
                    child.mutate(mutationRate);
                    currentPopulation[i] = child;
                }
            }
            //Population had no good genes create a new population.
            else
            {
                createNewPopulation(currentPopulation.Count);
            }

            generations++;
        }

        void evalutePopulation() { }
    }
}
