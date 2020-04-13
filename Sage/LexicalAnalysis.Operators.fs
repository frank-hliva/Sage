module Sage.LexicalAnalysis.Operators

open StdLib
open Sage
open Sage.LexicalAnalysis.Elements
open Parsing.ParserImpl

let operators = [
    "|>" => OP_FORWARD_PIPE
    "<|" => OP_BACKWARD_PIPE
    ">>" => OP_FORWARD_COMPOSITOR
    "<<" => OP_BACKWARD_COMPOSITOR

    "~=" => OP_SPECIAL_ASSIGN

    "^^" => OP_XOR
    "xor" => OP_XOR

    "||" => OP_OR
    "or" => OP_OR

    "&&" => OP_AND
    "and" => OP_AND

    ":xor" => OP_BITWISE_XOR
    ":or" => OP_BITWISE_OR
    ":and" => OP_BITWISE_AND
    ":not" => OP_BITWISE_NOT

    "<" => OP_LESS_THAN
    "<=" => OP_LESS_THAN_OR_EQUALS
    ">" => OP_GREATER_THAN
    ">=" => OP_GREATER_THAN_OR_EQUALS
    "<>" => OP_NOT_EQUALS
    "!=" => OP_NOT_EQUALS
    "=" => OP_EQUALS_OR_ASSIGN
    "==" => OP_EQUALS

    "<<:" => OP_BITWISE_ZERO_FILL_LEFT_SHIFT
    ":>>" => OP_BITWISE_SIGNED_RIGHT_SHIFT
    ":>>>" => OP_BITWISE_ZERO_FILL_RIGHT_SHIFT

    "%%" => OP_APPEND

    "+" => OP_ADDITION
    "-" => OP_SUBTRACT //OP_NEGATE

    "*" => OP_MULTIPLY //OP_STAR
    "/" => OP_DIVIDE
    "%" => OP_MODULO

    "!" => OP_NOT
    "not" => OP_NOT
    "++" => OP_SUCCESSOR
    "--" => OP_PREDECESSOR

    "**" => OP_POWER

    "<-" => OP_LARROW
    "←" => OP_LARROW
    "->" => OP_RARROW
    "→" => OP_RARROW

    "=>" => OP_DOUBLE_RARROW // unused
    "+>" => OP_PLUS_RARROW // unused
    "." => OP_DOT // unused
    "#" => OP_SHARP // unused
    "&" => OP_JOIN // unused
    "\\" => OP_BACK_SLASH // unused
]

let operatorSet = operators |> Set.ofList
let operatorTokenSet = operatorSet |> Set.map snd