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
            bool quit = false;
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    quit = true;
                }
                else if (command == "load")
                {
                    try
                    {
                        if (argument.Length > 1) LoadFileWithWords(argument[1]);
                        else LoadFileWithWords(defaultFile);
                    }
                    catch (System.IO.FileNotFoundException ex)
                    {
                        Console.WriteLine("The file was not found, please enter another file.\n" + ex.Message);
                    }
                }
                else if (command == "list")
                {
                    if (dictionary.Count > 0) // FIXME: Lägg till felhantering med try-catch
                    {
                        foreach (SweEngGloss gloss in dictionary) Console.WriteLine($"{gloss.word_swe,-15}  - {gloss.word_eng,-15}");

                    } else Console.WriteLine("The dictionary is empty, please load a file first!");
                }
                else if (command == "new")
                { 
                    try
                    {
                        if (argument.Length > 1) AddNewGlossToDictionary(argument[1], argument[2]);
                        else
                        {
                            string swedishWord = AskForSwedishWord();
                            string englishWord = AskForEnglishWord();
                            AddNewGlossToDictionary(swedishWord, englishWord);
                        }
                    }
                    catch (System.IndexOutOfRangeException ex)
                    {
                        Console.WriteLine("The input was not valid, please enter one swdish word and one english word.\n" + ex.Message);
                    }
                }
                else if (command == "delete") // FIXME: System.ArgumentOutOfRangeException - om delete innan load
                                              // System.ArgumentOutOfRangeException - om ordet inte finns
                { // TBD: Känn av motsvarande ord automatiskt
                    try
                    {
                        if (argument.Length > 1) DeleteMatchingWords(argument[1], argument[2]);
                        else
                        {
                            string swedishWord = AskForSwedishWord();
                            string englishWord = AskForEnglishWord();
                            DeleteMatchingWords(swedishWord, englishWord);
                        }
                    }
                    catch (System.ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine("The word was not found, please enter another word\n" + ex.Message);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length > 1) TranslateWord(argument[1]);
                    else
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string wordToBeTranslated = Console.ReadLine();
                        TranslateWord(wordToBeTranslated);
                    }
                }
                else Console.WriteLine($"Unknown command: '{command}'");
            }
            while (!quit);
        }

        private static void TranslateWord(string wordToBeTranslated)
        {
            foreach (SweEngGloss gloss in dictionary) // FIXME: lägg till en utskrift som gäller om ordet heter samma på engelska och svenska
            {
                if (gloss.word_swe == wordToBeTranslated) 
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == wordToBeTranslated)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        private static void DeleteMatchingWords(string swedishWord, string englishWord)
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == swedishWord && gloss.word_eng == englishWord) index = i;
            }
            dictionary.RemoveAt(index);
            Console.WriteLine($"Words '{swedishWord}' and '{englishWord}' was deleted");
        }

        private static void AddNewGlossToDictionary(string swedishWord, string englishWord)
        {
            dictionary.Add(new SweEngGloss(swedishWord, englishWord));
            Console.WriteLine($"Words '{swedishWord}' and '{englishWord}' was added in the dictionary");
        }

        private static void LoadFileWithWords(string fileName)
        {
            using (StreamReader streamReader = new StreamReader($"..\\..\\..\\dict\\{fileName}")) 
                // FIXME: Ändra tillbaka så användaren själv får välja vart filen ligger
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

        private static string AskForEnglishWord() // FIXME: Ändra så att det inte går att lägga till siffror i orden
        {
            Console.Write("Write word in English: ");
            return Console.ReadLine().Trim();
        }

        private static string AskForSwedishWord() // FIXME: Ändra så att det inte går att lägga till siffror i orden
        {
            Console.WriteLine("Write word in Swedish: ");
            return Console.ReadLine().Trim();
        }
    }
}