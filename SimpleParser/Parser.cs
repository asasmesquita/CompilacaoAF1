namespace SimpleParser;

class Parser{
    private static StreamReader _streamReader;
    private int lookahead;
    internal Parser(StreamReader streamReader){
        _streamReader = streamReader;
        if(!_streamReader.EndOfStream){
            lookahead = _streamReader.Read();
        }
    }

    internal void Expression(){
        while(!_streamReader.EndOfStream){
            Term();
            while(true){
                if(lookahead == '+'){
                    Match('+');
                    Term();
                    Console.Write('+');
                }
                else if(lookahead == '-'){
                    Match('-');
                    Term();
                    Console.Write('-');
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
            Console.Write('*');
            }
        else if(lookahead == '/'){
            Match('/');
            Factor();
            Console.Write('/');
        }
        else{
            return;
        }
    }


    private void Factor(){
        if(Char.IsDigit((char)lookahead) || Char.IsAsciiLetter((char)lookahead)){
            Console.Write((char)lookahead);
            Match(lookahead);
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