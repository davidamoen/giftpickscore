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
            var people = GetPeople();

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
                            Console.WriteLine("Sorry, this list isn't working.  Please try again. ");
                            return;
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

            Console.WriteLine($"{drawer.Name} can give to {giftee.Name}! Hooray!");
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

        private static List<Person> GetPeople()
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
                new Person(Members.Thomas)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Aidan, Members.Cecelia}
                },
                new Person(Members.Cecelia)
                {
                    Spouse = Members.None,
                    Siblings = { Members.Aidan, Members.Thomas }
                },
                new Person(Members.Cindy)
                {
                    Spouse = Members.Dave,
                    Children = { Members.Garrett, Members.Nicholas }
                },
                new Person(Members.Dave)
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
    public enum Members
        {
            Cari,
            Kevin,
            Aidan,
            Thomas,
            Cecelia,
            Cindy,
            Dave,
            Garrett,
            Nicholas,
            Lorna,
            Jack,
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
