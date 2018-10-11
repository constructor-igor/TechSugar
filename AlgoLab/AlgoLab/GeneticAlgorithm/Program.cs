using System;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace GeneticAlgorithm
{
    public static class Extensions
    {
        public static string ToUserFriendly(this IChromosome chromosome)
        {
            return string.Join(",", chromosome.GetGenes().Select(gene => Convert.ToInt32(gene.Value).ToString()));
        }
    }
    public class MyProblemFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            double fitness = -chromosome.GetGenes().Select(gene => Convert.ToInt32(gene.Value)).Sum();
            Console.WriteLine($"[calculation] {fitness} for {chromosome.ToUserFriendly()}");
            return fitness;
        }
    }
    public class MyProblemChromosome : ChromosomeBase
    {
        // Change the argument value passed to base construtor to change the length 
        // of your chromosome.
        private readonly Random m_random;
        public MyProblemChromosome() : base(10)
        {
            m_random = new Random(12345);
            CreateGenes();
        }

        // Generate a gene base on my problem chromosome representation.
        public override Gene GenerateGene(int geneIndex)
        {
            Gene gene = new Gene(m_random.Next(0, 2));
            return gene;
        }

        public override IChromosome CreateNew()
        {
            return new MyProblemChromosome();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var selection = new EliteSelection();
            var crossover = new OnePointCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new MyProblemFitness();
            var chromosome = new MyProblemChromosome();
            var population = new Population(20, 20, chromosome);

            var ga = new GeneticSharp.Domain.GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = new GenerationNumberTermination(100)
            };            

            Console.WriteLine("GA running...");
            ga.Start();            

            if (ga.BestChromosome.Fitness != null)
            {
                double fitnessValue = ga.BestChromosome.Fitness.Value;
                string solution = ga.BestChromosome.ToUserFriendly();
                Console.WriteLine($"Best solution {solution} found has {fitnessValue} fitness for {ga.GenerationsNumber} generations.");
            }
        }
    }
}
