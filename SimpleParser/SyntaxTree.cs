namespace SimpleParser;

internal class SyntaxTree{
    private readonly Node _root;
    private readonly string[] _tokens;
    private int readPosition;

    internal SyntaxTree(string parsedText){
        this._root = new Node(){
            Left = null,
            Right = null
        };
        this._tokens = parsedText.Split(' ');
        this.readPosition = _tokens.Count() - 1;
        
        BuildTree(_root);
    }

    private Token GetToken(){
        string p_text = _tokens[readPosition];
        readPosition--;
        string name = string.Empty;
        string value = string.Empty;

        if(p_text.Equals('+') || p_text.Equals('-') || p_text.Equals('*') || p_text.Equals('/')){
            name = "Operator";
            value = p_text;
        }
        else{
            string[] str = p_text.Split('.');
            name = str[0];
            value = str[1];
        }

        return new Token(){
            Name = name,
            Value = value
        };
    }

    private void BuildTree(Node parent){;
        while(readPosition >= 0){
            if(parent.Data == null){//base case empty tree root node
                parent.Data = GetToken();
                parent.Right = new Node(){
                    Data = GetToken()
                };
                parent.Left = new Node(){
                    Data = GetToken()
                };
            }
            else{//non root node
                parent.Right = new Node(){
                    Data = GetToken()
                };
                parent.Left = new Node(){
                    Data = GetToken()
                };
            }
            //expand if children are not leafs, Token.Name == "Operator"
            if(parent.Right.Data.Name.Equals("Operator")){
                BuildTree(parent.Right);
            }
            if(parent.Left.Data.Name.Equals("Operator")){
                BuildTree(parent.Left);
            }
        }

    }

    private void Optimize(){

    }


    internal void PrintTree(){

    }

}