using System;
using System.Collections.Generic;


namespace RefactoringGuru.DesignPatterns.AbstractFactory.Conceptual
{
    class Program
    {
        
        public abstract class Herbivores : IAnimal
        {
            public bool Live { get; set; }
            public double Weight { get; set; }
            public abstract double Eat();
            public abstract string Print();
        }
        public interface IAnimal
        {
            bool Live { get; set; }
        }
        public class Wild : Herbivores
        {
            public Wild()
            { }
            public Wild(double weight)
            {
                Weight = weight;
                Live = true;
            }
            public override double Eat()
            {
                if (Weight < 0)
                {
                    Live = false;
                    return Weight;
                }
                return Weight += 10;
            }

            public override string Print()
            {
                return $"Live:{Live}--->Weight:{Weight}";
            }
        }
        public class Bison : Herbivores
        {
            public Bison()
            {
                Live = true;
                Weight = 0;
            }
            public Bison(double weight)
            {
                Weight = weight;
                Live = true;
            }
            public override double Eat()
            {
                if (Weight < 0)
                {
                    Live = false;
                    return Weight;
                }
                return Weight += 10;
            }
            public override string Print()
            {
                return $"Bison Live - {Live}    Weight - {Weight}";
            }
        }
        public abstract class Predator : IAnimal
        {
            public bool Live { get; set; }
            public double Power { get; set; }
            public abstract double Eat(Herbivores herbivores);
            public abstract string ToString();
        }
        public class Lion : Predator
        {
            public Lion()
            { }
            public Lion(double power)
            {
                Power = power;
                Live = true;
            }
            public override double Eat(Herbivores herbivores)
            {
                if (Power < 0)
                {
                    Live = false;
                    return Power;
                }
                if (Power > herbivores.Weight)
                {
                    herbivores.Live = false;
                    return Power += 10;
                }
                if (Power == herbivores.Weight)
                {
                    return Power;
                }
                return Power -= 10;
            }
            public override string ToString()
            {
                return $"Lion Live - {Live}    Weight - {Power}";
            }
        }
        public class Wolf : Predator
        {
            public Wolf()
            { }
            public Wolf(double power)
            {
                Power = power;
                Live = true;
            }
            public override double Eat(Herbivores herbs)
            {
                if (Power < 0)
                {
                    Live = false;
                    return Power;
                }
                if (Power > herbs.Weight)
                {
                    herbs.Live = false;
                    return Power += 10;
                }
                if (Power == herbs.Weight)
                {
                    return Power;
                }
                return Power -= 10;
            }
            public override string ToString()
            {
                return $"Wolf Live:{Live}---->Weight:{Power}";
            }
        }
        public abstract class Country
        {
            public List<Herbivores> herb { get; set; }
            public List<Predator> predator { get; set; }
            public abstract void CreatHerb(Herbivores herb);
            public abstract void CreatPredators(Predator predators);
        }
        public class Africa : Country
        {
            public Africa()
            {
                herb = new List<Herbivores>();
                predator = new List<Predator>();
            }
            public override void CreatHerb(Herbivores herb)
            {
                base.herb.Add(herb);
            }
            public override void CreatPredators(Predator pred)
            {
                predator.Add(pred);
            }
        }
        public class NorthAmerica : Country
        {
            public override void CreatHerb(Herbivores herb)
            {
                base.herb.Add(herb);
            }
            public override void CreatPredators(Predator pred)
            {
                predator.Add(pred);
            }
        }
        public class AnimalWorld
        {
            public void ShowHerbivores(Country country)
            {
                for (int i = 0; i < country.herb.Count; i++)
                {
                    Console.WriteLine(country.herb[i].Print());
                }
                   
            }
            public void ShowPredators(Country country)
            {
                for (int i = 0; i < country.predator.Count; i++)
                {
                    Console.WriteLine(country.predator[i].ToString());
                }
                   
            }
            public void Herbivores(Country country)
            {
                for (int i = 0; i < country.herb.Count; i++)
                {
                    country.herb[i].Eat();
                } 
            }
            public void Nutrition(Country country)
            {
                int j = 0;

                for (int i = 0; i < country.predator.Count || j < country.herb.Count; i++, j++)
                {
                    country.predator[i].Eat(country.herb[j]);
                }
                   
            }
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Africa africa = new Africa();
            Wolf wolf = new Wolf(50);
            Lion lion = new Lion(140);
            africa.CreatPredators(wolf);
            africa.CreatPredators(lion);
            AnimalWorld world = new AnimalWorld();
            world.ShowHerbivores(africa);
            world.ShowPredators(africa);
            world.Nutrition(africa);
            world.ShowHerbivores(africa);
            world.ShowPredators(africa);
        }
        
    }

}
