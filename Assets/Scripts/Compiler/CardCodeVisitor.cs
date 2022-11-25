using System;
using System.IO;
using System.Collections;
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
    }

    public override object? VisitAssignment([NotNull] CardCodeParser.AssignmentContext context)
    {
        var line = context.Start.Line;
        var varName = context.IDENTIFIER().GetText();

        if(reservedWords.Contains(varName) || reservedOperators.Contains(varName)){
            errorHandler.HandleError(line, $"variável não pode ser chamada de {varName}");
        }

        var value = Visit(context.expression());

        Variables[varName] = value;

        return null;
    }

    public override object? VisitPrintCall([NotNull] CardCodeParser.PrintCallContext context)
    {
        var line = context.Start.Line;
        var exp = Visit(context.expression());
        if(exp == null){
            errorHandler.HandleError(line, "Nenhum valor foi passado para o print");
        }
        
        using(var writer = File.AppendText("Assets/Resources/output.txt")){
            writer.WriteLine(Visit(context.expression())?.ToString() ?? "");
        }

        return null;
    }

    public override object? VisitConstant([NotNull] CardCodeParser.ConstantContext context)
    {
        var line = context.Start.Line;
        if(context.INTEGER() is {} i){
            return int.Parse(i.GetText());
        }
        if(context.FLOAT() is {} f){
            return float.Parse(f.GetText());
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

    public override object? VisitIdentifierExpression([NotNull] CardCodeParser.IdentifierExpressionContext context)
    {
        var line = context.Start.Line;
        var varName = context.IDENTIFIER().GetText();

        if(reservedWords.Contains(varName) || reservedOperators.Contains(varName)){
            errorHandler.HandleError(line, $"variável não pode ser chamada de {varName}");
        }

        if(!Variables.ContainsKey(varName)){
            //ocorreu erro: variável não definida
            errorHandler.HandleError(line, $"variável {varName} não foi definida");
        }
        
        return Variables[varName];
    }

    public override object? VisitAdditiveExpression([NotNull] CardCodeParser.AdditiveExpressionContext context)
    {
        var line = context.Start.Line;
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

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

    public override object? VisitIfBlock([NotNull] CardCodeParser.IfBlockContext context)
    {
        var line = context.Start.Line;
        var exp = Visit(context.expression());
        if(exp == null){
            errorHandler.HandleError(line, "Nenhuma expressão foi passada para o if");
        }
        if(IsTrue(exp, line)){
            Visit(context.block(0));
        }
        else{
            Visit(context.block(1));
        }
        return null;
    }

    public override object? VisitComparisonExpression([NotNull] CardCodeParser.ComparisonExpressionContext context)
    {
        var line = context.Start.Line;
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

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

    public override object? VisitMultiplicativeExpression([NotNull] CardCodeParser.MultiplicativeExpressionContext context)
    {
        var line = context.Start.Line;
        var left = context.expression(0);
        var right = context.expression(1);

        var op = context.multOp().GetText();

        return op switch{
            "*" => Multiply(left, right, line),
            "/" => Divide(left, right, line),
            "%" => Remainder(left, right, line),
            _ => throw new Exception("operador não foi implementado")
        };
    }

    public override object? VisitBooleanExpression([NotNull] CardCodeParser.BooleanExpressionContext context)
    {
        var line = context.Start.Line;
        var left = context.expression(0);
        var right = context.expression(1);

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
