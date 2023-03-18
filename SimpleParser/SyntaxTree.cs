namespace SimpleParser;

internal class SyntaxTree{
    private readonly Node _root;
    private readonly string[] _tokens;
    private int readPosition;

    internal string TreeString {get; set;} = string.Empty;

    internal SyntaxTree(string parsedText){
        this._root = new Node(){
            Left = null,
            Right = null
        };
        this._tokens = parsedText.Split(' ');
        
        this.readPosition = _tokens.Count() - 1;
        
        BuildTree(_root);

        Optimize(_root);

        PrintTree(_root);
    }

    private Token GetToken(){
        string p_text = _tokens[readPosition];
        readPosition--;
        string name = string.Empty;
        string value = string.Empty;

        if(p_text.Equals("+") || p_text.Equals("-") || p_text.Equals("*") || p_text.Equals("/")){
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
            if(IsEmptySpaceOrEOF(_tokens[readPosition])){//split is adding a white string at the last one
                readPosition--;
                continue;
            }
            if(parent.Data == null){//base case, empty tree root node
                parent.Data = GetToken();
                BuildTree(parent);
            }
            else if(parent.Right == null){
                parent.Right = new Node(){
                    Data = GetToken()
                };
                if(parent.Right.Data.Name == "Operator"){
                    BuildTree(parent.Right);
                }
            }
            else if(parent.Left == null){
                parent.Left = new Node(){
                    Data = GetToken()
                };
                if(parent.Left.Data.Name == "Operator"){
                    BuildTree(parent.Left);
                }
                
            }
            else{
                return;
            }
        }

    }

    private bool IsEmptySpaceOrEOF(string str){
        if(str == "" || str == " " || str == "\n"){
            return true;
        }
        else{
            return false;
        }
    }

    private void Optimize(Node node){
        //establish operator order
        OptimizeMultAndDiv(node);
        OptimizeAddAndSub(node);
    }

    private void OptimizeMultAndDiv(Node node){
        //if node with both leafs with number execute operation
        if(node.Left.Data.Name == "Operator"){
            OptimizeMultAndDiv(node.Left);
        }
        if(node.Right.Data.Name == "Operator"){
            OptimizeMultAndDiv(node.Right);
        }
        if(node.Left.Data.Name == "Num" && node.Right.Data.Name == "Num"){//both are leaf nodes
            ExecuteMultOrDiv(node);
        }
    }

    private void OptimizeAddAndSub(Node node){
        //if node with both leafs with number execute operation
        if(node.Left.Data.Name == "Operator"){
            OptimizeAddAndSub(node.Left);
        }
        if(node.Right.Data.Name == "Operator"){
            OptimizeAddAndSub(node.Right);
        }
        if(node.Left.Data.Name == "Num" && node.Right.Data.Name == "Num"){//both are leaf nodes
            ExecuteAddOrSub(node);
        }
    }

    private void ExecuteMultOrDiv(Node node){
        int left = int.Parse(node.Left.Data.Value);
        int right = int.Parse(node.Right.Data.Value);
        
        if(node.Data.Value == "*"){
            int value = left * right;
            node.Data.Value = value.ToString();
        }
        else if(node.Data.Value == "/"){
            if(right == 0){
                throw new Exception("Division by Zero");
            }
            else{
                int value = left / right;
                node.Data.Value = value.ToString();
            }
        }
        //updating node
        node.Data.Name = "Num";
        node.Left= null;
        node.Right = null;
    }

    private void ExecuteAddOrSub(Node node){
        int left = int.Parse(node.Left.Data.Value);
        int right = int.Parse(node.Right.Data.Value);
        if(node.Data.Value == "+"){
            int value = left + right;
            node.Data.Value = value.ToString();
        }
        else if(node.Data.Value == "-"){
            int value = left - right;
            node.Data.Value = value.ToString();
        }
        //updating node
        node.Data.Name = "Num";
        node.Left= null;
        node.Right = null;
    }


    private void PrintTree(Node node){
        if(node.Data.Name != "Operator"){//one leaf tree
            TreeString += node.Data.Value;
            return;
        }
        
        if(node.Left.Data.Name == "Operator"){//next node is not a leaf
            PrintTree(node.Left);
        }
        else{
            TreeString += node.Left.Data.Value + " ";
        }

        if(node.Right.Data.Name == "Operator"){//next node is not a leaf
            PrintTree(node.Right);
        }
        else{
            TreeString += node.Right.Data.Value + " ";
        }
        TreeString += node.Data.Value + " ";
    }

}