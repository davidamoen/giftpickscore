using System;
using System.Collections.Generic;

namespace giftpickscore
{
    public class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static string divider = "*****************************************";

        public static void Run()
        {
            var family = "m";

            while(family.ToLower() != "m" && family.ToLower() != "k") 
            {
                Console.WriteLine("Which family are you creating a list for?");
                Console.Write("Type \"m\" for Moen or \"k\" for Kremer: ");
                family = Console.ReadLine(); 
            }

            var people = family.ToLower() == "k" ? GetFamily(Families.Kremers) : GetFamily(Families.Moens);

            var results = new Dictionary<string, string>();

            // load the hat
            var hat = GetRandomizedClone(people);            

            // create random draw order
            var drawOrder = GetRandomizedClone(people);

            var r = new Random();
            foreach (var drawer in drawOrder)
            {
                var doneDrawing = false;
                var drawCount = 0;
                Console.WriteLine($"{drawer.Name.ToString()} is drawing.");
                while (!doneDrawing)
                {
                    var idx = r.Next(0, hat.Count);
                    if (OKPick(drawer,hat[idx])) 
                    {
                        results.Add(drawer.Name.ToString(), hat[idx].Name.ToString());
                        hat.RemoveAt(idx);
                        doneDrawing = true;
                    }
                    else
                    {
                        drawCount++;
                        if (drawCount > 10)
                        {
                            Console.WriteLine();
                            Console.WriteLine();                            
                            Console.WriteLine("That drawing failed!!! Trying again...");
                            Console.WriteLine();
                            Console.WriteLine();                            
                            Run();
                        }
                    }
                }
            }        
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The results are: ");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Key} buys for {result.Value}");
            }

        }

        public static bool OKPick(Person drawer, Person giftee)
        {
            Console.WriteLine(divider);
            Console.WriteLine($"Trying {drawer.Name} and {giftee.Name}");

            if (drawer.Name == giftee.Name)
            {
                Console.WriteLine($"{drawer.Name} drew their own name from the hat. Try again.");
                Console.WriteLine(divider);
                return false;
            }

            if (AreMarried(drawer, giftee))
            {
                Console.WriteLine($"{drawer.Name} and {giftee.Name} are married!! Duh.");
                Console.WriteLine(divider);

                return false;
            }

            if (IsChild(drawer, giftee))
            {
                Console.WriteLine($"{drawer.Name} is {giftee.Name}'s child.");
                Console.WriteLine(divider);

                return false;
            }

            if (IsParent(drawer, giftee))
            {
                Console.WriteLine($"{drawer.Name} is {giftee.Name}'s parent.");
                Console.WriteLine(divider);

                return false;
            }

            if (IsSibling(drawer, giftee))
            {
                Console.WriteLine($"{drawer.Name} is {giftee.Name}'s sibling.");
                Console.WriteLine(divider);

                return false;
            }

            Console.WriteLine($"{drawer.Name} can give to {giftee.Name}! That works!");
            Console.WriteLine(divider);
            return true;
        }      

        private static bool AreMarried(Person person1, Person person2)
        {
            return person1.Spouse == person2.Name;
        }

        private static bool IsChild(Person person1, Person person2)
        {
            return person2.Children.Contains(person1.Name);
        }

        private static bool IsParent(Person person1, Person person2)
        {
            return person1.Children.Contains(person2.Name);
        }

        private static bool IsSibling(Person person1, Person person2)
        {
            return person1.Siblings.Contains(person2.Name);
        }          

        private static List<Person> GetRandomizedClone(List<Person> list)
        {
            var r = new Random();
            var choices = new List<Person>(list);
            var newList = new List<Person>();
            while (choices.Count > 0)
            {
                var idx = r.Next(0, choices.Count);
                newList.Add(choices[idx]);
                choices.RemoveAt(idx);
            }
            return newList;
        }

        private static List<Person> GetFamily(Families family)
        {
            switch(family) 
            {
                case Families.Kremers:
                    return GetKremerFamily();
                case Families.Moens:
                    return GetMoenFamily();
                default:
                    return new List<Person>();
            }
        }

        private static List<Person> GetMoenFamily()
        {
            return new List<Person>()
            {
                new Person(Members.Julie)
                {
                    Spouse = Members.Mark,
                    Children = { Members.Quinn }
                },
                new Person(Members.Mark)
                {
                    Spouse = Members.Julie,
                    Children = { Members.Quinn }
                },
                new Person(Members.Quinn) 
                {
                    Spouse = Members.None
                },
                new Person(Members.Cindy)
                {
                    Spouse = Members.David,
                    Children = { Members.Garrett, Members.Nicholas }
                },
                new Person(Members.David)
                {
                    Spouse = Members.Cindy,
                    Children = { Members.Garrett, Members.Nicholas }
                },
                new Person(Members.Garrett)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Nicholas }
                },
                new Person(Members.Nicholas)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Garrett }
                },
                new Person(Members.Sally) 
                {
                    Spouse = Members.None
                },
                new Person(Members.Jacque)
                {
                    Spouse = Members.None
                }
            };
        }

        private static List<Person> GetKremerFamily()
        {
            return new List<Person>()
            {
                new Person(Members.Cari)
                {
                    Spouse = Members.Kevin,
                    Children = { Members.Aidan, Members.Thomas, Members.Cecelia }
                },
                new Person(Members.Kevin)
                {
                    Spouse = Members.Cari,
                    Children = { Members.Aidan, Members.Thomas, Members.Cecelia }
                },
                new Person(Members.Aidan)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Thomas, Members.Cecelia }
                },
                new Person(Members.Cecelia)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Aidan, Members.Thomas }
                },
                new Person(Members.Cindy)
                {
                    Spouse = Members.David,
                    Children = { Members.Garrett, Members.Nicholas }
                },
                new Person(Members.David)
                {
                    Spouse = Members.Cindy,
                    Children = { Members.Garrett, Members.Nicholas }
                },
                new Person(Members.Garrett)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Nicholas }
                },
                new Person(Members.Nicholas)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Garrett }
                },
                new Person(Members.Lorna)
                {
                    Spouse = Members.Jack,
                },
                new Person(Members.Jack)
                {
                    Spouse = Members.Lorna,
                },
            };

        }

    }

    public enum Families
    {
        Kremers,
        Moens
    }
    public enum Members
        {
            Cari,
            Kevin,
            Aidan,
            Thomas,
            Cecelia,
            Cindy,
            David,
            Garrett,
            Nicholas,
            Lorna,
            Jack,
            Julie,
            Mark,
            Sally,
            Jacque,
            Quinn,
            None
        }

    public class Person
    {
        public Person(Members name)
        {
            Name = name;
            Siblings = new List<Members>();
            Children = new List<Members>();
        }
        public Members Name { get; set; }
        public Members Spouse { get; set; }
        public List<Members> Siblings { get; set; }
        public List<Members> Children { get; set; }
    }
}
