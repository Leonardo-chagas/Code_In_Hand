using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Antlr4.Runtime;
using System.IO;

public class ErrorListener : BaseErrorListener
{
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        using(var writer = File.AppendText("Assets/Resources/log.txt")){
            writer.WriteLine(line);
            writer.WriteLine(output);
        }
        base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
    }
}
