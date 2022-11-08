using System;
using System.Collections;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;

public class CardCodeVisitor : CardCodeBaseVisitor<object?>
{
    private Dictionary<string, object?> Variables { get; } = new();
    public override object? VisitAssignment([NotNull] CardCodeParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();

        var value = Visit(context.expression());

        Variables[varName] = value;

        return null;
    }

    public override object? VisitPrintCall([NotNull] CardCodeParser.PrintCallContext context)
    {
        /* if(context.expression() == null){
            Error("Nenhum argumento foi repassado para o print");
        } */

        return null;
    }

    public override object? VisitConstant([NotNull] CardCodeParser.ConstantContext context)
    {
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
        return null;
    }

    public override object? VisitIdentifierExpression([NotNull] CardCodeParser.IdentifierExpressionContext context)
    {
        var varName = context.IDENTIFIER().GetText();

        if(!Variables.ContainsKey(varName)){
            //ocorreu erro: variável não definida
            throw new Exception($"variable {varName} is not defined");
        }
        
        return Variables[varName];
    }

    public override object? VisitAdditiveExpression([NotNull] CardCodeParser.AdditiveExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.addOp().GetText();

        return op switch{
            "+" => Add(left, right),
            "-" => Subtract(left, right),
            _ => throw new NotImplementedException()
        };
    }

    private object? Add(object? left, object? right){
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
        throw new Exception("Cannot add these values");
    }

    private object? Subtract(object? left, object? right){
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
        throw new Exception("Cannot subtract these values");
    }

    public override object? VisitIfBlock([NotNull] CardCodeParser.IfBlockContext context)
    {
        if(IsTrue(Visit((context.expression())))){
            Visit(context.block(0));
        }
        else{
            Visit(context.block(1));
        }
        return null;
    }

    public override object? VisitComparisonExpression([NotNull] CardCodeParser.ComparisonExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.compareOp().GetText();

        return op switch{
            "==" => IsEquals(left, right),
            "!=" => NotEquals(left, right),
            ">" => GreaterThan(left, right),
            "<" => LessThan(left, right),
            ">=" => GreaterThanOrEqual(left, right),
            "<=" => LessThanOrEqual(left, right),
            _ => throw new Exception("not implemented")
        };
    }

    public override object? VisitMultiplicativeExpression([NotNull] CardCodeParser.MultiplicativeExpressionContext context)
    {
        var left = context.expression(0);
        var right = context.expression(1);

        var op = context.multOp().GetText();

        return op switch{
            "*" => Multiply(left, right),
            "/" => Divide(left, right),
            "%" => Remainder(left, right),
            _ => throw new Exception("not implemented")
        };
    }

    public override object? VisitBooleanExpression([NotNull] CardCodeParser.BooleanExpressionContext context)
    {
        var left = context.expression(0);
        var right = context.expression(1);

        var op = context.boolOp().GetText();

        return op switch{
            "and" => AndOperation(left, right),
            "or" => OrOperation(left, right),
            _ => throw new Exception("not implemented")
        };
    }

    private bool AndOperation(object? left, object? right){
        if(left is bool l && right is bool r)
            return l && r;
        
        //ocorreu erro: valores de tipo inválido
        throw new Exception("values of type invalid for this operation");
    }

    private bool OrOperation(object? left, object? right){
        if(left is bool l && right is bool r)
            return l || r;
        
        //ocorreu erro: valores de tipo inválido
        throw new Exception("values of type invalid for this operation");
    }

    private object? Multiply(object? left, object? right){
        if(left is int l && right is int r)
            return l * r;
        if(left is float lf && right is float rf)
            return lf * rf;
        if(left is int lInt && right is float rFloat)
            return lInt * rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat * rInt;
        
        throw new Exception("cannot add these values");
    }

    private object? Divide(object? left, object? right){
        if(left is int l && right is int r)
            return l / r;
        if(left is float lf && right is float rf)
            return lf / rf;
        if(left is int lInt && right is float rFloat)
            return lInt / rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat / rInt;
        
        throw new Exception("cannot add these values");
    }

    private object? Remainder(object? left, object? right){
        if(left is int l && right is int r)
            return l % r;
        if(left is float lf && right is float rf)
            return lf % rf;
        if(left is int lInt && right is float rFloat)
            return lInt % rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat % rInt;
        
        throw new Exception("cannot add these values");
    }

    private bool IsEquals(object? left, object? right){
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
        throw new Exception("Cannot compare these values");
    }

    private bool NotEquals(object? left, object? right){
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
        throw new Exception("Cannot compare these values");
    }

    private bool GreaterThan(object? left, object? right){
        if(left is int l && right is int r)
            return l > r;
        if(left is float lf && right is float rf)
            return lf > rf;
        if(left is int lInt && right is float rFloat)
            return lInt > rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat > rInt;
        
        //ocorreu erro: não implementado
        throw new Exception("Cannot compare these values");
    }

    private bool LessThan(object? left, object? right){
        if(left is int l && right is int r)
            return l < r;
        if(left is float lf && right is float rf)
            return lf < rf;
        if(left is int lInt && right is float rFloat)
            return lInt < rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat < rInt;
        
        //ocorreu erro: não implementado
        throw new Exception("Cannot compare these values");
    }

    private bool GreaterThanOrEqual(object? left, object? right){
        if(left is int l && right is int r)
            return l >= r;
        if(left is float lf && right is float rf)
            return lf >= rf;
        if(left is int lInt && right is float rFloat)
            return lInt >= rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat >= rInt;
        
        //ocorreu erro: não implementado
        throw new Exception("Cannot compare these values");
    }

    private bool LessThanOrEqual(object? left, object? right){
        if(left is int l && right is int r)
            return l <= r;
        if(left is float lf && right is float rf)
            return lf <= rf;
        if(left is int lInt && right is float rFloat)
            return lInt <= rFloat;
        if(left is float lFloat && right is int rInt)
            return lFloat <= rInt;
        
        //ocorreu erro: não implementado
        throw new Exception("Cannot compare these values");
    }

    private bool IsTrue(object? value){
        if(value is bool b){
            return b;
        }
        //ocorreu erro: valor não é booleano
        throw new Exception("value is not boolean");
    }

    public bool IsFalse(object? value) => !IsTrue(value);

    private void Error(string error){
        throw new Exception(error);
    }
}
