using System;

namespace SimpleParser;

class Parser{
    private static StreamReader _streamReader;
    private static SymbolTable _symbolTable;
    private int lookahead;
    internal Parser(StreamReader streamReader, SymbolTable symbolTable){
        _streamReader = streamReader;
        if(!_streamReader.EndOfStream){
            lookahead = _streamReader.Read();
        }
        _symbolTable = symbolTable;
    }

    internal void Expression(){
        while(!_streamReader.EndOfStream){
            if(lookahead == '('){
                Match('(');
                Term();
                while(lookahead != ')'){
                    if(lookahead == '+'){
                        Match('+');
                        Term();
                        Console.Write('+' + " ");
                    }
                    else if(lookahead == '-'){
                        Match('-');
                        Term();
                        Console.Write('-' + " ");
                    }
                    else{
                        return;
                    }
                }
            }
            else{
                Term();
                while(true){
                    if(lookahead == '+'){
                        Match('+');
                        Term();
                        Console.Write('+' + " ");
                    }
                    else if(lookahead == '-'){
                        Match('-');
                        Term();
                        Console.Write('-' + " ");
                    }
                    else{
                        return;
                    }
                }
            }
            
        }
    }

    private void Term(){
        bool closedTerm = false;
        if(lookahead == ')'){
            Match(')');
            closedTerm = true;
        }
        if(!closedTerm){
            Factor();
        }
        if(lookahead == '*'){
            Match('*');
            Factor();
            Console.Write('*' + " ");
            }
        else if(lookahead == '/'){
            Match('/');
            Factor();
            Console.Write('/' + " ");
        }
        else{
            return;
        }
    }


    private void Factor(){
        if(Char.IsAsciiLetter((char)lookahead)){//id
            string value = string.Empty;
            while(Char.IsAsciiLetter((char)lookahead)){
                value += (char)lookahead;
                Match(lookahead);               
            }
            Token t = new Token(){
                Name = "Id" + _symbolTable.Table.Count.ToString(),
                Value = value
            };
            _symbolTable.Table.Add(t);
            Console.Write(t.Name + " ");
        }
        else if(Char.IsDigit((char)lookahead)){//num
            string value = string.Empty;
            while(Char.IsDigit((char)lookahead)){
                value += (char)lookahead;
                Match(lookahead);               
            }
            
            Token t = new Token(){
                Name = "Num",
                Value = value.ToString()
            };
            _symbolTable.Table.Add(t);
            Console.Write(t.Name + "." + t.Value + " ");
        }
        else{
            throw new Exception("Syntax error");
        }
    }

    private void Match(int token){
        if(lookahead == token){
            lookahead = _streamReader.Read();
        }
        else{
            throw new Exception("Syntax error");
        }

    }
}