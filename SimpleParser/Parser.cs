using System;

namespace SimpleParser;

class Parser{
    private readonly StreamReader _streamReader;
    private readonly SymbolTable _symbolTable;
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

    private void Term(){
        Factor();
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
                Name = "Word",
                Value = value
            };
            _symbolTable.Table.Add(t);
            Console.Write(t.Name + "." + t.Value + " ");
        }
        else if(Char.IsDigit((char)lookahead)){//num
            string value = string.Empty;
            while(Char.IsDigit((char)lookahead)){
                value += (char)lookahead;
                Match(lookahead);               
            }
            
            Token t = new Token(){
                Name = "Num",
                Value = value
            };
            _symbolTable.Table.Add(t);
            Console.Write(t.Name + "." + t.Value + " ");
        }
        else if(lookahead == '('){
            Match('(');
            Expression();
            if(lookahead == ')'){
                Match(lookahead);
            }
            else{
                throw new Exception("Syntax error");
            }
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