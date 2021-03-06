// Signature file for parser generated by fsyacc
module Sage.Parsing.ParserImpl
type token = 
  | EOF
  | LAMBDA
  | OP_POWER
  | OP_PREDECESSOR
  | OP_SUCCESSOR
  | OP_NEGATE
  | OP_NOT
  | OP_MODULO
  | OP_DIVIDE
  | OP_MULTIPLY
  | OP_SUBTRACT
  | OP_ADDITION
  | OP_APPEND
  | OP_BITWISE_ZERO_FILL_RIGHT_SHIFT
  | OP_BITWISE_SIGNED_RIGHT_SHIFT
  | OP_BITWISE_ZERO_FILL_LEFT_SHIFT
  | OP_EQUALS
  | OP_EQUALS_OR_ASSIGN
  | OP_NOT_EQUALS
  | OP_GREATER_THAN_OR_EQUALS
  | OP_GREATER_THAN
  | OP_LESS_THAN_OR_EQUALS
  | OP_LESS_THAN
  | OP_BITWISE_NOT
  | OP_BITWISE_AND
  | OP_BITWISE_OR
  | OP_BITWISE_XOR
  | OP_AND
  | OP_OR
  | OP_XOR
  | OP_SPECIAL_ASSIGN
  | OP_BACKWARD_COMPOSITOR
  | OP_FORWARD_COMPOSITOR
  | OP_BACKWARD_PIPE
  | OP_FORWARD_PIPE
  | KW_STACKALLOC
  | KW_ALLOC
  | KW_PACKAGE
  | KW_MODULE
  | KW_SELF
  | KW_PROTOCOL
  | KW_KIND
  | KW_MATCH
  | KW_FUNC
  | KW_ELIF_NOT
  | KW_ELIF
  | KW_ELSE
  | KW_THEN
  | KW_IF_NOT
  | KW_IF
  | KW_LET
  | INDENTATION of (string * int)
  | WHITESPACES of (string * int)
  | MULTILINE_COMMENT of (string)
  | SINGLELINE_COMMENT of (string)
  | OPERATOR of (string)
  | IDENTIFIER of (string)
  | SYMBOL of (string)
  | NUMBER of (decimal)
  | STRING of (string)
  | OP_BACK_SLASH
  | OP_JOIN
  | OP_SHARP
  | OP_DOT
  | OP_PLUS_RARROW
  | OP_DOUBLE_RARROW
  | OP_DOUBLE_STAR
  | OP_STAR
  | OP_RARROW
  | OP_LARROW
  | NON_CURRIED
  | ANYTHING
  | BAR
  | COLON
  | POWER
  | SEPARATOR
  | COMMA
  | RBRACE
  | LBRACE
  | RBRACKET
  | LBRACKET
  | RPAREN
  | LPAREN
type tokenId = 
    | TOKEN_EOF
    | TOKEN_LAMBDA
    | TOKEN_OP_POWER
    | TOKEN_OP_PREDECESSOR
    | TOKEN_OP_SUCCESSOR
    | TOKEN_OP_NEGATE
    | TOKEN_OP_NOT
    | TOKEN_OP_MODULO
    | TOKEN_OP_DIVIDE
    | TOKEN_OP_MULTIPLY
    | TOKEN_OP_SUBTRACT
    | TOKEN_OP_ADDITION
    | TOKEN_OP_APPEND
    | TOKEN_OP_BITWISE_ZERO_FILL_RIGHT_SHIFT
    | TOKEN_OP_BITWISE_SIGNED_RIGHT_SHIFT
    | TOKEN_OP_BITWISE_ZERO_FILL_LEFT_SHIFT
    | TOKEN_OP_EQUALS
    | TOKEN_OP_EQUALS_OR_ASSIGN
    | TOKEN_OP_NOT_EQUALS
    | TOKEN_OP_GREATER_THAN_OR_EQUALS
    | TOKEN_OP_GREATER_THAN
    | TOKEN_OP_LESS_THAN_OR_EQUALS
    | TOKEN_OP_LESS_THAN
    | TOKEN_OP_BITWISE_NOT
    | TOKEN_OP_BITWISE_AND
    | TOKEN_OP_BITWISE_OR
    | TOKEN_OP_BITWISE_XOR
    | TOKEN_OP_AND
    | TOKEN_OP_OR
    | TOKEN_OP_XOR
    | TOKEN_OP_SPECIAL_ASSIGN
    | TOKEN_OP_BACKWARD_COMPOSITOR
    | TOKEN_OP_FORWARD_COMPOSITOR
    | TOKEN_OP_BACKWARD_PIPE
    | TOKEN_OP_FORWARD_PIPE
    | TOKEN_KW_STACKALLOC
    | TOKEN_KW_ALLOC
    | TOKEN_KW_PACKAGE
    | TOKEN_KW_MODULE
    | TOKEN_KW_SELF
    | TOKEN_KW_PROTOCOL
    | TOKEN_KW_KIND
    | TOKEN_KW_MATCH
    | TOKEN_KW_FUNC
    | TOKEN_KW_ELIF_NOT
    | TOKEN_KW_ELIF
    | TOKEN_KW_ELSE
    | TOKEN_KW_THEN
    | TOKEN_KW_IF_NOT
    | TOKEN_KW_IF
    | TOKEN_KW_LET
    | TOKEN_INDENTATION
    | TOKEN_WHITESPACES
    | TOKEN_MULTILINE_COMMENT
    | TOKEN_SINGLELINE_COMMENT
    | TOKEN_OPERATOR
    | TOKEN_IDENTIFIER
    | TOKEN_SYMBOL
    | TOKEN_NUMBER
    | TOKEN_STRING
    | TOKEN_OP_BACK_SLASH
    | TOKEN_OP_JOIN
    | TOKEN_OP_SHARP
    | TOKEN_OP_DOT
    | TOKEN_OP_PLUS_RARROW
    | TOKEN_OP_DOUBLE_RARROW
    | TOKEN_OP_DOUBLE_STAR
    | TOKEN_OP_STAR
    | TOKEN_OP_RARROW
    | TOKEN_OP_LARROW
    | TOKEN_NON_CURRIED
    | TOKEN_ANYTHING
    | TOKEN_BAR
    | TOKEN_COLON
    | TOKEN_POWER
    | TOKEN_SEPARATOR
    | TOKEN_COMMA
    | TOKEN_RBRACE
    | TOKEN_LBRACE
    | TOKEN_RBRACKET
    | TOKEN_LBRACKET
    | TOKEN_RPAREN
    | TOKEN_LPAREN
    | TOKEN_end_of_input
    | TOKEN_error
type nonTerminalId = 
    | NONTERM__startstart
    | NONTERM_start
    | NONTERM_prog
    | NONTERM_expr
    | NONTERM_stat
    | NONTERM_optSep
    | NONTERM_exprList
    | NONTERM_primitiveLiteral
    | NONTERM_literal
    | NONTERM_identifierOrPath
    | NONTERM_path
    | NONTERM_literalOrIdentifier
    | NONTERM_listLiteral
    | NONTERM_list
    | NONTERM_recordLiteral
    | NONTERM_record
    | NONTERM_key
    | NONTERM_moduleDef
    | NONTERM_sendMessages
    | NONTERM_receiver
    | NONTERM_messageList
    | NONTERM_message
    | NONTERM_binaryMessage
    | NONTERM_keywordMessage
    | NONTERM_keyword
    | NONTERM_matchingList
    | NONTERM_matching
    | NONTERM_case
    | NONTERM_optBar
    | NONTERM_func
    | NONTERM_funcKw
    | NONTERM_argNames
    | NONTERM_functionEval
    | NONTERM_args
    | NONTERM_arg
    | NONTERM_op
    | NONTERM_op0
    | NONTERM_logicalOp1
    | NONTERM_logicalOp2
    | NONTERM_logicalOp3
    | NONTERM_bitwiseOp1
    | NONTERM_bitwiseOp2
    | NONTERM_bitwiseOp3
    | NONTERM_bitwiseOp4
    | NONTERM_op4
    | NONTERM_bitwise_bit_shift
    | NONTERM_op5
    | NONTERM_op6
    | NONTERM_op7
    | NONTERM_opNegate
    | NONTERM_op8
    | NONTERM_op9
    | NONTERM_wrappedExpr
    | NONTERM_block
    | NONTERM_exprMain
    | NONTERM_opFunctional
/// This function maps tokens to integer indexes
val tagOfToken: token -> int

/// This function maps integer indexes to symbolic token ids
val tokenTagToTokenId: int -> tokenId

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
val prodIdxToNonTerminal: int -> nonTerminalId

/// This function gets the name of a token as a string
val token_to_string: token -> string
val start : (FSharp.Text.Lexing.LexBuffer<'cty> -> token) -> FSharp.Text.Lexing.LexBuffer<'cty> -> (ast) 
