namespace SimpleParser;

class SymbolTable{
    internal List<Token> Table {get; set;}

    internal SymbolTable(){
        this.Table = new List<Token>();
    } 
}