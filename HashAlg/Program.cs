using HashAlg.Classes;

Console.Clear();

Console.WriteLine("Example: the \"Hello, World!\" hash is: '" + CustomHasher.GetStringHash("Hello, World!") + "'.\n");
                  
Console.Write("Try using the workings of this algorithm to calculate the HASH value of a file!" +
                  "\nEnter the path to the file: ");

string filePath = Console.ReadLine();
Console.WriteLine(@$"File '{filePath}' hashing result: '" + CustomHasher.GetFileHash(filePath) + "'.");     
             