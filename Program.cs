namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new List<SweEngGloss> { };
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
                if (command == "quit") // FIXME: gör så programmet stängs av om kommando är quit
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    if(argument.Length > 1)
                    {
                        LoadFileWithWords(argument[1]);
                    }
                    else
                    {
                        LoadFileWithWords(defaultFile);
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
                    if (argument.Length > 1)
                    {
                        AddNewGlossToDictionary(argument[1], argument[2]);
                    }
                    else
                    {
                        string swedishWord = AskForSwedishWord();
                        string englishWord = AskForEnglishWord();
                        AddNewGlossToDictionary(swedishWord, englishWord);
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length > 1)
                    {
                        DeleteMatchingWords(argument[1], argument[2]);
                    }
                    else
                    {
                        string swedishWord = AskForSwedishWord();
                        string englishWord = AskForEnglishWord();
                        DeleteMatchingWords(swedishWord, englishWord);
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

        private static void DeleteMatchingWords(string swedishWord, string englishWord)
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == swedishWord && gloss.word_eng == englishWord)
                    index = i;
            }
            Console.WriteLine($"Words '{swedishWord}' and '{englishWord}' was deleted");
            dictionary.RemoveAt(index);
        }

        private static void AddNewGlossToDictionary(string swedishWord, string englishWord)
        {
            dictionary.Add(new SweEngGloss(swedishWord, englishWord));
            Console.WriteLine($"Words '{swedishWord}' and '{englishWord}' was added in the dictionary");
        }

        private static void LoadFileWithWords(string fileName)
        {
            using (StreamReader streamReader = new StreamReader(fileName)) // FIXME ändra så agrumentet läses ifrån dict-mappen
            {
                dictionary.Clear();
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    SweEngGloss gloss = new SweEngGloss(line);
                    dictionary.Add(gloss);
                    line = streamReader.ReadLine();
                }
            }
            Console.WriteLine("File was readed correctly!");
        }

        private static string AskForEnglishWord()
        {
            Console.Write("Write word in English: ");
            string englishWord = Console.ReadLine().Trim();
            return englishWord;
        }

        private static string AskForSwedishWord()
        {
            Console.WriteLine("Write word in Swedish: ");
            string swedishWord = Console.ReadLine().Trim();
            return swedishWord;
        }
    }
}