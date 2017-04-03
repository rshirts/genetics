using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4470_Genetics
{
    /// <summary>
    /// This class is an individual DNA set in the population.
    /// </summary>
    class DNA
    {
        //This is a char list that will comprise each gene of the DNA
        public char[] genes;
        //This tells us how close we are to the actual target goal.
        public double fitness = 0;
        Random r = new Random();

        public override string ToString()
        {
            string returnMe = null;
            foreach (var item in genes)
            {
                returnMe += item;
            }
            return returnMe;
        }


        /// <summary>
        /// Constructor that assigns random values to each gene.
        /// </summary>
        /// <param name="numberOfGenes">How many genes each DNA should have.</param>
        public DNA(int numberOfGenes, bool child)
        {
            genes = new char[numberOfGenes];

            if(!child)
            {
                for (int i = 0; i < numberOfGenes; i++)
                {
                    genes[i] = randomChar();
                }
            }
        }

        /// <summary>
        /// Generates a random char from the ascii table includes symbols.
        /// </summary>
        /// <returns>Ascii Char</returns>
        char randomChar()
        {
            //only use standard chars and space and period
            //int randomChar = r.Next(22, 126);
            int randomChar = r.Next(63, 122);
            //add space
            if (randomChar == 63) randomChar = 32;
            //add period
            if (randomChar == 64) randomChar = 46;
            return (char)randomChar;
        }

        /// <summary>
        /// Compare each gene with the target and assess a fitness score.
        /// </summary>
        /// <param name="targetString"></param>
        public void calculateFitness(ref string targetString)
        {
            double score = 0;
            for( int i = 0; i < targetString.Length; i++)
            {
                char a = genes[i];
                char b = targetString[i];
                if (a == b)
                {
                    score++;
                }
            }
            
            //Calculate a percentage of how many genes are correct.
            fitness = score / targetString.Length;
        }

        //This will take another DNA and do a two point crossover then return a child.
        public DNA crossover(DNA partner)
        {
            DNA child = new DNA(genes.Length, true);

            //select the two points of crossover and put them in order.
            // always keep the left and right side the same.
            int selection1 = r.Next(1, genes.Length - 1);
            int selection2 = r.Next(1, genes.Length - 2);
            int leftSlice = 0;
            int rightSlice = 0;
            if (selection1 <= selection2)
            {
                leftSlice = selection1;
                rightSlice = selection2;
            }
            else
            {
                leftSlice = selection2;
                rightSlice = selection1;
            }

            //perform the crossover of genes between the two DNA
            for(int i = 0; i < genes.Length; i++)
            {
                if( i <= leftSlice || i > rightSlice )
                {
                    child.genes[i] = genes[i]; 
                }
                else
                {
                    child.genes[i] = partner.genes[i];
                }
            }

            return child;
        }

        public void mutate(double mutationRate)
        {
            int applyMutation = (int)(mutationRate * 100);
            int randomValue = 0;
            for (int i = 0; i < genes.Length; i++)
            {
                randomValue = r.Next(1, 100);
                if (randomValue <= applyMutation)
                {
                    genes[i] = randomChar();
                }
            }
        }
    }
}
