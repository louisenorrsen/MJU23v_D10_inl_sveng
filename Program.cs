namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    // FIXME: lägg till beskräftelse att filen laddats in korrekt
                    if(argument.Length == 2)
                    {
                        using (StreamReader streamReader = new StreamReader(argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // FIXME: Töm listan istället för att skapa en ny
                            string line = streamReader.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = streamReader.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader streamReader = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // FIXME: Töm listan istället för att skapa en ny
                            string line = streamReader.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = streamReader.ReadLine();
                            }
                        }
                    }
                }
                else if (command == "list")
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    // FIXME: lägg till bekräftelse vilket ord som lagts till
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2])); // FIXME: bryt ut till AddNewGloss(string swedishWord, string englisWord)
                    }
                    else if(argument.Length == 1)
                    {
                        string swedishWord = AskForSwedishWord();
                        string englishWord = AskForEnglishWord();
                        dictionary.Add(new SweEngGloss(swedishWord, englishWord));
                    }
                }
                else if (command == "delete")
                {
                    // FIXME: lägg till bekräftelse vilket ord som tagits bort
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        string swedishWord = AskForSwedishWord();
                        string englishWord = AskForEnglishWord();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) // FIXME: bryt ut till LookForMatchingWords( string swedishWord, string englishWord)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swedishWord && gloss.word_eng == englishWord)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == s)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == s)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static string AskForEnglishWord()
        {
            Console.Write("Write word in English: ");
            string englishWord = Console.ReadLine();
            return englishWord;
        }

        private static string AskForSwedishWord()
        {
            Console.WriteLine("Write word in Swedish: ");
            string swedishWord = Console.ReadLine();
            return swedishWord;
        }
    }
}