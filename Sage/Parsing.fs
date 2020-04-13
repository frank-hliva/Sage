namespace Sage.Parsing

type ast =
| Number of decimal
| Str of string
| Bool of bool
| Nil
| Unit
| Identifier of string
| IdentifiersPath of string list
| Block of ast list
| ExprList of ast list
| Root of ast list
| Parens of ast
| Let of left : string * right : ast
| Function of isCurried : bool * args : string list * body : ast
| FunctionEval of isCurried : bool * identifier : ast * evalArgs : ast list
| SendMessages of receiver : ast * messages : messageWrapper list
| ExprEval of expr : ast * evalArgs : ast list
| If of cond : ast * then' : ast * else' : ast option
| IfNot of cond : ast * then' : ast * else' : ast option
| Match of expr : ast * matchingList : matching list
| BinaryOperator of operator : string * left : ast * right : ast
| UnaryOperator of operator : string * right : ast
| Module of name : ast * moduleType : moduleType * body : ast
| List of ast list
| Record of (ast * ast) list

and matching = case * (ast option)

and case =
| Expr of ast
| CaseIdentifier of string
| Anything

and messageWrapper =
| Msg of msg
| Join
| Pipe

and msg =
| UnaryMessage of key : ast
| BinaryMessage of binaryMessage
| KeywordMessage of keywords

and keywords = keyword list

and binaryMessage =
| Value of key : ast * value : ast
| Undefined of key : ast

and keyword =
| Value of key : ast * value : ast
| Undefined of key : ast
| Placeholder of key : ast
| Delimiter

and moduleType =
| Normal = 0
| Root = 1
| Assign = 2

module KeywordMessage =

    let private (|IsPlaceholder|_|) = function
    | keyword.Placeholder placeholder -> Some placeholder
    | _ -> None

    let toMessage : keywords -> msg = function
    | IsPlaceholder key :: [] -> UnaryMessage(key)
    | kw -> KeywordMessage(kw)