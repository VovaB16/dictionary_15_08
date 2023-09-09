using System;
using System.Collections.Generic;
using System.IO;

class DictionaryApp
{
    static void CreateDictionary(Dictionary<string, List<string>> dictionaries)
    {
        Console.Write("Введіть назву нового словника: ");
        string dictionaryName = Console.ReadLine();

        if (!dictionaries.ContainsKey(dictionaryName))
        {
            dictionaries[dictionaryName] = new List<string>();
            Console.WriteLine($"Створено словник {dictionaryName}");
        }
        else
        {
            Console.WriteLine($"Словник з назвою {dictionaryName} вже існує.");
        }
    }

    static void AddWordTranslation(Dictionary<string, List<string>> dictionaries)
    {
        Console.Write("Введіть назву словника: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введіть слово: ");
            string word = Console.ReadLine();

            Console.Write("Введіть переклад слова: ");
            string translation = Console.ReadLine();

            dictionaries[dictionaryName].Add($"{word} - {translation}");

            Console.WriteLine("Слово та його переклад успішно додані до словника.");
        }
        else
        {
            Console.WriteLine($"Словник {dictionaryName} не знайдено.");
        }
    }

    static void ReplaceWordTranslation(Dictionary<string, List<string>> dictionaries)
    {
        Console.Write("Введіть назву словника: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введіть слово, яке потрібно замінити: ");
            string word = Console.ReadLine();

            Console.Write("Введіть новий переклад слова: ");
            string newTranslation = Console.ReadLine();

            bool wordFound = false;

            for (int i = 0; i < dictionaries[dictionaryName].Count; i++)
            {
                if (dictionaries[dictionaryName][i].StartsWith(word + " - "))
                {
                    dictionaries[dictionaryName][i] = $"{word} - {newTranslation}";
                    wordFound = true;
                    break;
                }
            }

            if (wordFound)
            {
                Console.WriteLine("Переклад слова успішно замінено.");
            }
            else
            {
                Console.WriteLine($"Слово {word} не знайдено у словнику {dictionaryName}.");
            }
        }
        else
        {
            Console.WriteLine($"Словник {dictionaryName} не знайдено.");
        }
    }

    static void DeleteWordTranslation(Dictionary<string, List<string>> dictionaries)
    {
        Console.Write("Введіть назву словника: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введіть слово, яке потрібно видалити: ");
            string word = Console.ReadLine();

            bool wordFound = false;

            for (int i = dictionaries[dictionaryName].Count - 1; i >= 0; i--)
            {
                if (dictionaries[dictionaryName][i].StartsWith(word + " - "))
                {
                    dictionaries[dictionaryName].RemoveAt(i);
                    wordFound = true;
                }
            }

            if (wordFound)
            {
                Console.WriteLine("Слово та його переклад успішно видалені зі словника.");
            }
            else
            {
                Console.WriteLine($"Слово {word} не знайдено у словнику {dictionaryName}.");
            }
        }
        else
        {
            Console.WriteLine($"Словник {dictionaryName} не знайдено.");
        }
    }

    static void SearchTranslation(Dictionary<string, List<string>> dictionaries)
    {
        Console.Write("Введіть назву словника: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введіть слово для пошуку перекладу: ");
            string word = Console.ReadLine();

            bool wordFound = false;

            foreach (string translation in dictionaries[dictionaryName])
            {
                if (translation.StartsWith(word + " - "))
                {
                    Console.WriteLine(translation);
                    wordFound = true;
                }
            }

            if (!wordFound)
            {
                Console.WriteLine($"Слово {word} не знайдено у словнику {dictionaryName}.");
            }
        }
        else
        {
            Console.WriteLine($"Словник {dictionaryName} не знайдено.");
        }
    }

    static void ExportDictionary(Dictionary<string, List<string>> dictionaries)
    {
        Console.Write("Введіть назву словника: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введіть шлях до файлу для експорту: ");
            string filePath = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string translation in dictionaries[dictionaryName])
                {
                    writer.WriteLine(translation);
                }
            }

            Console.WriteLine($"Словник {dictionaryName} експортовано до файлу {filePath}.");
        }
        else
        {
            Console.WriteLine($"Словник {dictionaryName} не знайдено.");
        }
    }

    static void SaveDictionaries(Dictionary<string, List<string>> dictionaries, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (KeyValuePair<string, List<string>> dictionary in dictionaries)
            {
                writer.WriteLine(dictionary.Key);

                foreach (string translation in dictionary.Value)
                {
                    writer.WriteLine(translation);
                }

                writer.WriteLine();
            }
        }

        Console.WriteLine($"Словники збережено у файлі {filePath}.");
    }

    static void LoadDictionaries(Dictionary<string, List<string>> dictionaries, string filePath)
    {
        dictionaries.Clear();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            string currentDictionary = null;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    if (currentDictionary == null)
                    {
                        currentDictionary = line;
                        dictionaries[currentDictionary] = new List<string>();
                    }
                    else
                    {
                        dictionaries[currentDictionary].Add(line);
                    }
                }
                else
                {
                    currentDictionary = null;
                }
            }
        }

        Console.WriteLine($"Словники завантажено з файлу {filePath}.");
    }

    static void Main()
    {
        bool exit = false;
        Dictionary<string, List<string>> dictionaries = new Dictionary<string, List<string>>();

        while (!exit)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Створити словник");
            Console.WriteLine("2. Додати слово і його переклад");
            Console.WriteLine("3. Замінити слово або його переклад");
            Console.WriteLine("4. Видалити слово або переклад");
            Console.WriteLine("5. Шукати переклад слова");
            Console.WriteLine("6. Експортувати словник");
            Console.WriteLine("7. Зберегти словники у файл");
            Console.WriteLine("8. Завантажити словники з файлу");
            Console.WriteLine("9. Вийти з програми");
            Console.Write("Введіть номер пункту меню: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateDictionary(dictionaries);
                    break;
                case "2":
                    AddWordTranslation(dictionaries);
                    break;
                case "3":
                    ReplaceWordTranslation(dictionaries);
                    break;
                case "4":
                    DeleteWordTranslation(dictionaries);
                    break;
                case "5":
                    SearchTranslation(dictionaries);
                    break;
                case "6":
                    ExportDictionary(dictionaries);
                    break;
                case "7":
                    Console.Write("Введіть шлях до файлу для збереження: ");
                    string saveFilePath = Console.ReadLine();
                    SaveDictionaries(dictionaries, saveFilePath);
                    break;
                case "8":
                    Console.Write("Введіть шлях до файлу для завантаження: ");
                    string loadFilePath = Console.ReadLine();
                    LoadDictionaries(dictionaries, loadFilePath);
                    break;
                case "9":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

//C: \Users\Users\c\my_dictionaries.txt
