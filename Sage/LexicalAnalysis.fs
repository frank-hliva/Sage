namespace Sage.LexicalAnalysis

open Sage
open Sage.Parsing

#if NOYACC
type Token =

| LPAREN
| RPAREN
| COMMA
| SEPARATOR
| OP_LARROW
| OP_RARROW

| STRING of string
| NUMBER of decimal
| SYMBOL of string
| IDENTIFIER of string
| SINGLELINE_COMMENT of string 
| MULTILINE_COMMENT of string
| WHITESPACES of string * int
| INDENTATION of string * int

| KW_LET
| KW_IF
| KW_THEN
| KW_ELSE
| KW_FUNC

| OP_EQUALS_OR_ASSIGN

| OP_ADD
| OP_SUB
| OP_MUL
| OP_DIV
| OP_MOD

| OP_EQUALS
| OP_NOT_EQUALS
| OP_GREATER
| OP_LESS
| OP_GREATER_EQ
| OP_LESS_EQ

| OP_AND
| OP_OR
| OP_XOR
| OP_NOT

| LAMBDA

| EOF
#else
type Token = ParserImpl.token
#endif

type lexElement = position * Token
type lexBuff = lexElement list