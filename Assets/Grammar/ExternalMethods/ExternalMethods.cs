using System;
using System.Collections;
using System.Collections.Generic;

namespace ExternalMethods
{
    public class Operations
    {
        public object? Add(object? left, object? right){
            if(left is int l && right is int r){
                return l + r;
            }
            if(left is float lf && right is float rf){
                return lf + rf;
            }
            if(left is int lInt && right is float rFloat){
                return lInt + rFloat;
            }
            if(left is float lFloat && right is int rInt){
                return lFloat + rInt;
            }
            if(left is string || right is string){
                return $"{left}{right}";
            }
            
            //ocorreu erro: não implementado
            throw new Exception("Cannot add these values");
        }

        public object? Subtract(object? left, object? right){
            if(left is int l && right is int r){
                return l - r;
            }
            if(left is float lf && right is float rf){
                return lf - rf;
            }
            if(left is int lInt && right is float rFloat){
                return lInt - rFloat;
            }
            if(left is float lFloat && right is int rInt){
                return lFloat - rInt;
            }
            /* if(left is string || right is string){
                return $"{left}{right}";
            } */
            
            //ocorreu erro: não implementado
            throw new Exception("Cannot subtract these values");
        }
    }
}