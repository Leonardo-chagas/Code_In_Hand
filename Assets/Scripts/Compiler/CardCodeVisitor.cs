using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;

public class CardCodeVisitor : CardCodeBaseVisitor<object?>
{
    private Dictionary<string, object?> Variables { get; } = new();
    private List<string> reservedWords = new List<string>{"PRINT", "IF", "ELSE", "VAR", "AND", "OR", "TRUE", "FALSE"};
    private List<string>reservedOperators = new List<string>{"=", "-", "+", "*", "/", "==", ">", ">=", "<", "<="};
    private ErrorListener errorHandler;

    public CardCodeVisitor(ErrorListener errorListener){
        errorHandler = errorListener;

        Variables["PRINT"] = new Func<int, object?[], object?>(PRINT);
    }

    private object? PRINT(int line, object?[] args){
        if(args.Length <= 0){
            errorHandler.HandleError(line, "Nenhuma expressão foi passada para o print");
            return null;
        }
        else{
            foreach(var arg in args){
                using(var writer = File.AppendText("Assets/Resources/output.txt")){
                    writer.WriteLine(arg.ToString());
                }
            }
        }
        return null;
    }

    public override object VisitFunctionCall(CardCodeParser.FunctionCallContext context)
    {
        var line = context.Start.Line;
        var name = context.IDENTIFIER().GetText();
        var args = context.expression().Select(Visit).ToArray();

        if(!Variables.ContainsKey(name)){
            errorHandler.HandleError(line, $"função {name} não foi definida");
            return null;
        }

        if(Variables[name] is not Func<int, object?[], object?> func){
            errorHandler.HandleError(line, $"variável {name} não é uma função");
            return null;
        }
        return func(line, args);
    }

    public override object? VisitAssignment(CardCodeParser.AssignmentContext context)
    {
        var line = context.Start.Line;
        var varName = context.IDENTIFIER().GetText();
        
        

        if(reservedWords.Contains(varName) || reservedOperators.Contains(varName)){
            errorHandler.HandleError(line, $"variável não pode ser chamada de {varName}");
            return null;
        }

        var value = Visit(context.expression());
        if(value == null){
            errorHandler.HandleError(line, $"nenhuma expressão foi passada para a variável {varName}");
            return null;
        }
        

        Variables[varName] = value;
        

        return null;
    }


    public override object? VisitConstant(CardCodeParser.ConstantContext context)
    {
        var line = context.Start.Line;
        if(context.INTEGER() is {} i){
            return int.Parse(i.GetText());
        }
        if(context.FLOAT() is {} f){
            return float.Parse(f.GetText(), CultureInfo.InvariantCulture.NumberFormat);
        }
        if(context.STRING() is {} s){
            return s.GetText()[1..^1];
        }
        if(context.BOOL() is {} b){
            return b.GetText() == "true";
        }
        if(context.NULL() is {}){
            return null;
        }
        //ocorreu erro: não reconheceu o tipo
        errorHandler.HandleError(line, "tipo não reconhecido");
        return null;
    }

    public override object? VisitIdentifierExpression(CardCodeParser.IdentifierExpressionContext context)
    {
        var line = context.Start.Line;
        var varName = context.IDENTIFIER().GetText();

        if(reservedWords.Contains(varName) || reservedOperators.Contains(varName)){
            errorHandler.HandleError(line, $"variável não pode ser chamada de {varName}");
            return null;
        }

        if(!Variables.ContainsKey(varName)){
            //ocorreu erro: variável não definida
            errorHandler.HandleError(line, $"variável {varName} não foi definida");
            return null;
        }
        
        return Variables[varName];
    }

    public override object? VisitAdditiveExpression(CardCodeParser.AdditiveExpressionContext context)
    {
        var line = context.Start.Line;
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        if(left == null || right == null){
            errorHandler.HandleError(line, "um dos valores da espressão de adição ou subtração não foi definido");
            return null;
        }

        var op = context.addOp().GetText();

        return op switch{
            "+" => Add(left, right, line),
            "-" => Subtract(left, right, line),
            _ => throw new Exception("operador não foi implementado")
        };
    }

    private object? Add(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l + r;
        if(left is float lf && right is float rf)
            return lf + rf;
        if(left is int lInt && right is float rFloat)
            return lInt + rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat + rInt;
        if(left is string || right is string)
            return $"{left}{right}";
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível adicionar valores de tipo {left.GetType()} e {right.GetType()}");
        return null;
    }

    private object? Subtract(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l - r;
        if(left is float lf && right is float rf)
            return lf - rf;
        if(left is int lInt && right is float rFloat)
            return lInt - rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat - rInt;
        /* if(left is string || right is string){
            return $"{left}{right}";
        } */
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível subtrair valores de tipo {left.GetType()} e {right.GetType()}");
        return null;
    }

    public override object? VisitIfBlock(CardCodeParser.IfBlockContext context)
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/info.txt");
        writer.Write("visitou if");
        var line = context.Start.Line;
        var exp = Visit(context.expression());
        

        if(exp == null){
            errorHandler.HandleError(line, "Nenhuma expressão foi passada para o if");
            return null;
        }

        if(IsTrue(exp, line)){
            //throw new Exception("passou pelo if");
            writer.WriteLine("passou pelo if");
            Visit(context.block(0));
        }
        else{
            //throw new Exception("passou pelo else");
            writer.WriteLine("passou pelo else");
            if(context.ELSE() != null){
                Visit(context.block(1));
            }
        }
        writer.Close();
        return null;
    }

    public override object? VisitComparisonExpression(CardCodeParser.ComparisonExpressionContext context)
    {
        var line = context.Start.Line;
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        if(left == null || right == null){
            errorHandler.HandleError(line, "um dos valores da expressão de comparação não foi definido");
            return null;
        }

        var op = context.compareOp().GetText();

        return op switch{
            "==" => IsEquals(left, right, line),
            "!=" => NotEquals(left, right, line),
            ">" => GreaterThan(left, right, line),
            "<" => LessThan(left, right, line),
            ">=" => GreaterThanOrEqual(left, right, line),
            "<=" => LessThanOrEqual(left, right, line),
            _ => throw new Exception("operador não foi implementado")
        };
    }

    public override object? VisitMultiplicativeExpression(CardCodeParser.MultiplicativeExpressionContext context)
    {
        var line = context.Start.Line;
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        if(left == null || right == null){
            errorHandler.HandleError(line, "um dos valores da expressão de multiplicação ou divisão não foi definido");
            return null;
        }

        var op = context.multOp().GetText();

        return op switch{
            "*" => Multiply(left, right, line),
            "/" => Divide(left, right, line),
            "%" => Remainder(left, right, line),
            _ => throw new Exception("operador não foi implementado")
        };
    }

    public override object? VisitBooleanExpression(CardCodeParser.BooleanExpressionContext context)
    {
        var line = context.Start.Line;
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        if(left == null || right == null){
            errorHandler.HandleError(line, "um dos valores da expressão booleana não foi definido");
            return null;
        }

        var op = context.boolOp().GetText();

        return op switch{
            "and" => AndOperation(left, right, line),
            "or" => OrOperation(left, right, line),
            _ => throw new Exception("operador não implementado")
        };
    }

    private bool AndOperation(object? left, object? right, int line){
        if(left is bool l && right is bool r)
            return l && r;
        
        //ocorreu erro: valores de tipo inválido
        errorHandler.HandleError(line, $"valores de tipo {left.GetType()} e {right.GetType()} são inválidos para esta operação");
        throw new Exception();
    }

    private bool OrOperation(object? left, object? right, int line){
        if(left is bool l && right is bool r)
            return l || r;
        
        //ocorreu erro: valores de tipo inválido
        errorHandler.HandleError(line, $"valores de tipo {left.GetType()} e {right.GetType()} são inválidos para esta operação");
       throw new Exception();
    }

    private object? Multiply(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l * r;
        if(left is float lf && right is float rf)
            return lf * rf;
        if(left is int lInt && right is float rFloat)
            return lInt * rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat * rInt;
        
        errorHandler.HandleError(line, $"não foi possível multiplicar valores de tipo {left.GetType()} e {right.GetType()}");
        return null;
    }

    private object? Divide(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l / r;
        if(left is float lf && right is float rf)
            return lf / rf;
        if(left is int lInt && right is float rFloat)
            return lInt / rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat / rInt;
        
        errorHandler.HandleError(line, $"não foi possível dividir valores de tipo {left.GetType()} e {right.GetType()}");
        return null;
    }

    private object? Remainder(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l % r;
        if(left is float lf && right is float rf)
            return lf % rf;
        if(left is int lInt && right is float rFloat)
            return lInt % rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat % rInt;
        
        errorHandler.HandleError(line, $"não foi possível achar o resto dos valores de tipo {left.GetType()} e {right.GetType()}");
        return null;
    }

    private bool IsEquals(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l == r;
        if(left is float lf && right is float rf)
            return lf == rf;
        if(left is int lInt && right is float rFloat)
            return lInt == rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat == rInt;
        if(left is string && right is string)
            return left == right;
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível comparar valores de tipo {left.GetType()} e {right.GetType()}");
        throw new Exception();
    }

    private bool NotEquals(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l != r;
        if(left is float lf && right is float rf)
            return lf != rf;
        if(left is int lInt && right is float rFloat)
            return lInt != rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat != rInt;
        if(left is string && right is string)
            return left != right;
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível comparar valores de tipo {left.GetType()} e {right.GetType()}");
        throw new Exception();
    }

    private bool GreaterThan(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l > r;
        if(left is float lf && right is float rf)
            return lf > rf;
        if(left is int lInt && right is float rFloat)
            return lInt > rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat > rInt;
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível comparar valores de tipo {left.GetType()} e {right.GetType()}");
        throw new Exception();
    }

    private bool LessThan(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l < r;
        if(left is float lf && right is float rf)
            return lf < rf;
        if(left is int lInt && right is float rFloat)
            return lInt < rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat < rInt;
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível comparar valores de tipo {left.GetType()} e {right.GetType()}");
        throw new Exception();
    }

    private bool GreaterThanOrEqual(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l >= r;
        if(left is float lf && right is float rf)
            return lf >= rf;
        if(left is int lInt && right is float rFloat)
            return lInt >= rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat >= rInt;
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível comparar valores de tipo {left.GetType()} e {right.GetType()}");
        throw new Exception();
    }

    private bool LessThanOrEqual(object? left, object? right, int line){
        if(left is int l && right is int r)
            return l <= r;
        if(left is float lf && right is float rf)
            return lf <= rf;
        if(left is int lInt && right is float rFloat)
            return lInt <= rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat <= rInt;
        
        //ocorreu erro: não implementado
        errorHandler.HandleError(line, $"não foi possível comparar valores de tipo {left.GetType()} e {right.GetType()}");
        throw new Exception();
    }

    private bool IsTrue(object? value, int line){
        if(value is bool b){
            return b;
        }
        //ocorreu erro: valor não é booleano
        errorHandler.HandleError(line, "valor não é booleano");
        throw new Exception();
    }

    public bool IsFalse(object? value, int line) => !IsTrue(value, line);
}
