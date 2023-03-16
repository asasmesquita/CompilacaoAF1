namespace SimpleParser;

class Program{
    static void Main(string[] args){
        if(args.Length == 1){
            try{
                using(FileStream stream = File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.None)){
                    try{
                        StreamReader reader = new StreamReader(stream);
                        Parser parser = new Parser(reader);
                        parser.Expression();

                        Console.WriteLine();
                        Console.WriteLine("Operation terminated with success");
                    }
                    catch(Exception e){
                        Console.WriteLine("Error: cannot open file");
                        Console.WriteLine(e.Message);
                    }  
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: file not found");
                Console.WriteLine(e.Message);
            }
            
        }
        else{
            Console.WriteLine("Error: mssing argument, insert file to compile");
            Console.WriteLine("Usage: simpleparser [file with code to compile]");
        }
    }
}

