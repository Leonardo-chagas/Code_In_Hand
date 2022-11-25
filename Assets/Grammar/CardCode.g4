grammar CardCode;

program: line* EOF;

line: statement | printCall | ifBlock;

statement: (assignment) ';';

//ifBlock: 'if' expression block ('else' block)?;
ifBlock: 'IF' expression block ('ELSE' block)?;

//printCall: 'print(' expression ')';
printCall: 'PRINT' expression ';';

expression
    : constant                              #constantExpression
    | IDENTIFIER                            #identifierExpression
    | '(' expression ')'                    #parenthesizedExpression
    | '!' expression                        #notExpression
    | expression multOp expression          #multiplicativeExpression
    | expression addOp expression           #additiveExpression
    | expression compareOp expression       #comparisonExpression
    | expression boolOp expression          #booleanExpression
    ;

multOp: '*' | '/' | '%';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: 'and' | 'or';

//BOOL_OPERATOR: 'and' | 'or';

constant: INTEGER | FLOAT | STRING | BOOL | NULL;

INTEGER: [0-9]+;
FLOAT: [0-9]+ '.' [0-9]+;
STRING: ('"' ~'"'* '"') | ('\'' ~'\''* '\'');
BOOL: 'true' | 'false';
NULL: 'null';

block: '{' line* '}';

assignment: IDENTIFIER '=' expression;

WS: [ \t\r\n]+ -> skip;

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_];