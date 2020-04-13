module Sage.LexicalAnalysis.Token'

let isSeparator = function
| Token.SEPARATOR _ -> true
| _ -> false

let isBar = function
| Token.BAR _ -> true
| _ -> false

let isSpace = function
| Token.INDENTATION _ -> true
| Token.WHITESPACES _ -> true
| _ -> false

let isSubstract = function
| Token.OP_SUBTRACT _ -> true
| _ -> false

let isLineBreakOperators = function
| Token.BAR

| Token.OP_FORWARD_PIPE
| Token.OP_BACKWARD_PIPE
| Token.OP_FORWARD_COMPOSITOR
| Token.OP_BACKWARD_COMPOSITOR

| Token.OP_JOIN
| Token.OP_DOUBLE_RARROW -> true
| _ -> false

let isOperator = Operators.operatorTokenSet.Contains