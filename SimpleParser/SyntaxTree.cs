namespace SimpleParser;

internal class SyntaxTree{
    private readonly Token _head;

    public SyntaxTree(Token token){
        this._head = token;
    }

}