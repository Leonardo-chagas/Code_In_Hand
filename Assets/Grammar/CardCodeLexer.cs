//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:\Jogos Criados\TCC\Code_In_Hand\Assets\Grammar\CardCode.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public partial class CardCodeLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, INTEGER=24, 
		FLOAT=25, STRING=26, BOOL=27, NULL=28, WS=29, IDENTIFIER=30;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "INTEGER", "FLOAT", 
		"STRING", "BOOL", "NULL", "WS", "IDENTIFIER"
	};


	public CardCodeLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public CardCodeLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "';'", "'('", "','", "')'", "'IF'", "'ELSE'", "'!'", "'*'", "'/'", 
		"'%'", "'+'", "'-'", "'=='", "'!='", "'>'", "'<'", "'>='", "'<='", "'and'", 
		"'or'", "'{'", "'}'", "'='", null, null, null, null, "'null'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		"INTEGER", "FLOAT", "STRING", "BOOL", "NULL", "WS", "IDENTIFIER"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "CardCode.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static CardCodeLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', ' ', '\xB8', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', 
		'\x3', '\b', '\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', 
		'\n', '\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', '\r', 
		'\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', '\x11', '\x3', 
		'\x11', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x13', '\x3', 
		'\x13', '\x3', '\x13', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', 
		'\x14', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x16', '\x3', 
		'\x16', '\x3', '\x17', '\x3', '\x17', '\x3', '\x18', '\x3', '\x18', '\x3', 
		'\x19', '\x6', '\x19', 'z', '\n', '\x19', '\r', '\x19', '\xE', '\x19', 
		'{', '\x3', '\x1A', '\x6', '\x1A', '\x7F', '\n', '\x1A', '\r', '\x1A', 
		'\xE', '\x1A', '\x80', '\x3', '\x1A', '\x3', '\x1A', '\x6', '\x1A', '\x85', 
		'\n', '\x1A', '\r', '\x1A', '\xE', '\x1A', '\x86', '\x3', '\x1B', '\x3', 
		'\x1B', '\a', '\x1B', '\x8B', '\n', '\x1B', '\f', '\x1B', '\xE', '\x1B', 
		'\x8E', '\v', '\x1B', '\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1B', '\a', 
		'\x1B', '\x93', '\n', '\x1B', '\f', '\x1B', '\xE', '\x1B', '\x96', '\v', 
		'\x1B', '\x3', '\x1B', '\x5', '\x1B', '\x99', '\n', '\x1B', '\x3', '\x1C', 
		'\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', 
		'\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x5', '\x1C', '\xA4', '\n', 
		'\x1C', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', 
		'\x1D', '\x3', '\x1E', '\x6', '\x1E', '\xAC', '\n', '\x1E', '\r', '\x1E', 
		'\xE', '\x1E', '\xAD', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1F', '\x3', 
		'\x1F', '\a', '\x1F', '\xB4', '\n', '\x1F', '\f', '\x1F', '\xE', '\x1F', 
		'\xB7', '\v', '\x1F', '\x2', '\x2', ' ', '\x3', '\x3', '\x5', '\x4', '\a', 
		'\x5', '\t', '\x6', '\v', '\a', '\r', '\b', '\xF', '\t', '\x11', '\n', 
		'\x13', '\v', '\x15', '\f', '\x17', '\r', '\x19', '\xE', '\x1B', '\xF', 
		'\x1D', '\x10', '\x1F', '\x11', '!', '\x12', '#', '\x13', '%', '\x14', 
		'\'', '\x15', ')', '\x16', '+', '\x17', '-', '\x18', '/', '\x19', '\x31', 
		'\x1A', '\x33', '\x1B', '\x35', '\x1C', '\x37', '\x1D', '\x39', '\x1E', 
		';', '\x1F', '=', ' ', '\x3', '\x2', '\b', '\x3', '\x2', '\x32', ';', 
		'\x3', '\x2', '$', '$', '\x3', '\x2', ')', ')', '\x5', '\x2', '\v', '\f', 
		'\xF', '\xF', '\"', '\"', '\x5', '\x2', '\x43', '\\', '\x61', '\x61', 
		'\x63', '|', '\x6', '\x2', '\x32', ';', '\x43', '\\', '\x61', '\x61', 
		'\x63', '|', '\x2', '\xC0', '\x2', '\x3', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x5', '\x3', '\x2', '\x2', '\x2', '\x2', '\a', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\t', '\x3', '\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\r', '\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x13', '\x3', '\x2', '\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x17', '\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x1D', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '!', '\x3', '\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '%', '\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', 
		'\x2', '\x2', '\x2', '\x2', ')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'/', '\x3', '\x2', '\x2', '\x2', '\x2', '\x31', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x33', '\x3', '\x2', '\x2', '\x2', '\x2', '\x35', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x37', '\x3', '\x2', '\x2', '\x2', '\x2', '\x39', 
		'\x3', '\x2', '\x2', '\x2', '\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'=', '\x3', '\x2', '\x2', '\x2', '\x3', '?', '\x3', '\x2', '\x2', '\x2', 
		'\x5', '\x41', '\x3', '\x2', '\x2', '\x2', '\a', '\x43', '\x3', '\x2', 
		'\x2', '\x2', '\t', '\x45', '\x3', '\x2', '\x2', '\x2', '\v', 'G', '\x3', 
		'\x2', '\x2', '\x2', '\r', 'J', '\x3', '\x2', '\x2', '\x2', '\xF', 'O', 
		'\x3', '\x2', '\x2', '\x2', '\x11', 'Q', '\x3', '\x2', '\x2', '\x2', '\x13', 
		'S', '\x3', '\x2', '\x2', '\x2', '\x15', 'U', '\x3', '\x2', '\x2', '\x2', 
		'\x17', 'W', '\x3', '\x2', '\x2', '\x2', '\x19', 'Y', '\x3', '\x2', '\x2', 
		'\x2', '\x1B', '[', '\x3', '\x2', '\x2', '\x2', '\x1D', '^', '\x3', '\x2', 
		'\x2', '\x2', '\x1F', '\x61', '\x3', '\x2', '\x2', '\x2', '!', '\x63', 
		'\x3', '\x2', '\x2', '\x2', '#', '\x65', '\x3', '\x2', '\x2', '\x2', '%', 
		'h', '\x3', '\x2', '\x2', '\x2', '\'', 'k', '\x3', '\x2', '\x2', '\x2', 
		')', 'o', '\x3', '\x2', '\x2', '\x2', '+', 'r', '\x3', '\x2', '\x2', '\x2', 
		'-', 't', '\x3', '\x2', '\x2', '\x2', '/', 'v', '\x3', '\x2', '\x2', '\x2', 
		'\x31', 'y', '\x3', '\x2', '\x2', '\x2', '\x33', '~', '\x3', '\x2', '\x2', 
		'\x2', '\x35', '\x98', '\x3', '\x2', '\x2', '\x2', '\x37', '\xA3', '\x3', 
		'\x2', '\x2', '\x2', '\x39', '\xA5', '\x3', '\x2', '\x2', '\x2', ';', 
		'\xAB', '\x3', '\x2', '\x2', '\x2', '=', '\xB1', '\x3', '\x2', '\x2', 
		'\x2', '?', '@', '\a', '=', '\x2', '\x2', '@', '\x4', '\x3', '\x2', '\x2', 
		'\x2', '\x41', '\x42', '\a', '*', '\x2', '\x2', '\x42', '\x6', '\x3', 
		'\x2', '\x2', '\x2', '\x43', '\x44', '\a', '.', '\x2', '\x2', '\x44', 
		'\b', '\x3', '\x2', '\x2', '\x2', '\x45', '\x46', '\a', '+', '\x2', '\x2', 
		'\x46', '\n', '\x3', '\x2', '\x2', '\x2', 'G', 'H', '\a', 'K', '\x2', 
		'\x2', 'H', 'I', '\a', 'H', '\x2', '\x2', 'I', '\f', '\x3', '\x2', '\x2', 
		'\x2', 'J', 'K', '\a', 'G', '\x2', '\x2', 'K', 'L', '\a', 'N', '\x2', 
		'\x2', 'L', 'M', '\a', 'U', '\x2', '\x2', 'M', 'N', '\a', 'G', '\x2', 
		'\x2', 'N', '\xE', '\x3', '\x2', '\x2', '\x2', 'O', 'P', '\a', '#', '\x2', 
		'\x2', 'P', '\x10', '\x3', '\x2', '\x2', '\x2', 'Q', 'R', '\a', ',', '\x2', 
		'\x2', 'R', '\x12', '\x3', '\x2', '\x2', '\x2', 'S', 'T', '\a', '\x31', 
		'\x2', '\x2', 'T', '\x14', '\x3', '\x2', '\x2', '\x2', 'U', 'V', '\a', 
		'\'', '\x2', '\x2', 'V', '\x16', '\x3', '\x2', '\x2', '\x2', 'W', 'X', 
		'\a', '-', '\x2', '\x2', 'X', '\x18', '\x3', '\x2', '\x2', '\x2', 'Y', 
		'Z', '\a', '/', '\x2', '\x2', 'Z', '\x1A', '\x3', '\x2', '\x2', '\x2', 
		'[', '\\', '\a', '?', '\x2', '\x2', '\\', ']', '\a', '?', '\x2', '\x2', 
		']', '\x1C', '\x3', '\x2', '\x2', '\x2', '^', '_', '\a', '#', '\x2', '\x2', 
		'_', '`', '\a', '?', '\x2', '\x2', '`', '\x1E', '\x3', '\x2', '\x2', '\x2', 
		'\x61', '\x62', '\a', '@', '\x2', '\x2', '\x62', ' ', '\x3', '\x2', '\x2', 
		'\x2', '\x63', '\x64', '\a', '>', '\x2', '\x2', '\x64', '\"', '\x3', '\x2', 
		'\x2', '\x2', '\x65', '\x66', '\a', '@', '\x2', '\x2', '\x66', 'g', '\a', 
		'?', '\x2', '\x2', 'g', '$', '\x3', '\x2', '\x2', '\x2', 'h', 'i', '\a', 
		'>', '\x2', '\x2', 'i', 'j', '\a', '?', '\x2', '\x2', 'j', '&', '\x3', 
		'\x2', '\x2', '\x2', 'k', 'l', '\a', '\x63', '\x2', '\x2', 'l', 'm', '\a', 
		'p', '\x2', '\x2', 'm', 'n', '\a', '\x66', '\x2', '\x2', 'n', '(', '\x3', 
		'\x2', '\x2', '\x2', 'o', 'p', '\a', 'q', '\x2', '\x2', 'p', 'q', '\a', 
		't', '\x2', '\x2', 'q', '*', '\x3', '\x2', '\x2', '\x2', 'r', 's', '\a', 
		'}', '\x2', '\x2', 's', ',', '\x3', '\x2', '\x2', '\x2', 't', 'u', '\a', 
		'\x7F', '\x2', '\x2', 'u', '.', '\x3', '\x2', '\x2', '\x2', 'v', 'w', 
		'\a', '?', '\x2', '\x2', 'w', '\x30', '\x3', '\x2', '\x2', '\x2', 'x', 
		'z', '\t', '\x2', '\x2', '\x2', 'y', 'x', '\x3', '\x2', '\x2', '\x2', 
		'z', '{', '\x3', '\x2', '\x2', '\x2', '{', 'y', '\x3', '\x2', '\x2', '\x2', 
		'{', '|', '\x3', '\x2', '\x2', '\x2', '|', '\x32', '\x3', '\x2', '\x2', 
		'\x2', '}', '\x7F', '\t', '\x2', '\x2', '\x2', '~', '}', '\x3', '\x2', 
		'\x2', '\x2', '\x7F', '\x80', '\x3', '\x2', '\x2', '\x2', '\x80', '~', 
		'\x3', '\x2', '\x2', '\x2', '\x80', '\x81', '\x3', '\x2', '\x2', '\x2', 
		'\x81', '\x82', '\x3', '\x2', '\x2', '\x2', '\x82', '\x84', '\a', '\x30', 
		'\x2', '\x2', '\x83', '\x85', '\t', '\x2', '\x2', '\x2', '\x84', '\x83', 
		'\x3', '\x2', '\x2', '\x2', '\x85', '\x86', '\x3', '\x2', '\x2', '\x2', 
		'\x86', '\x84', '\x3', '\x2', '\x2', '\x2', '\x86', '\x87', '\x3', '\x2', 
		'\x2', '\x2', '\x87', '\x34', '\x3', '\x2', '\x2', '\x2', '\x88', '\x8C', 
		'\a', '$', '\x2', '\x2', '\x89', '\x8B', '\n', '\x3', '\x2', '\x2', '\x8A', 
		'\x89', '\x3', '\x2', '\x2', '\x2', '\x8B', '\x8E', '\x3', '\x2', '\x2', 
		'\x2', '\x8C', '\x8A', '\x3', '\x2', '\x2', '\x2', '\x8C', '\x8D', '\x3', 
		'\x2', '\x2', '\x2', '\x8D', '\x8F', '\x3', '\x2', '\x2', '\x2', '\x8E', 
		'\x8C', '\x3', '\x2', '\x2', '\x2', '\x8F', '\x99', '\a', '$', '\x2', 
		'\x2', '\x90', '\x94', '\a', ')', '\x2', '\x2', '\x91', '\x93', '\n', 
		'\x4', '\x2', '\x2', '\x92', '\x91', '\x3', '\x2', '\x2', '\x2', '\x93', 
		'\x96', '\x3', '\x2', '\x2', '\x2', '\x94', '\x92', '\x3', '\x2', '\x2', 
		'\x2', '\x94', '\x95', '\x3', '\x2', '\x2', '\x2', '\x95', '\x97', '\x3', 
		'\x2', '\x2', '\x2', '\x96', '\x94', '\x3', '\x2', '\x2', '\x2', '\x97', 
		'\x99', '\a', ')', '\x2', '\x2', '\x98', '\x88', '\x3', '\x2', '\x2', 
		'\x2', '\x98', '\x90', '\x3', '\x2', '\x2', '\x2', '\x99', '\x36', '\x3', 
		'\x2', '\x2', '\x2', '\x9A', '\x9B', '\a', 'v', '\x2', '\x2', '\x9B', 
		'\x9C', '\a', 't', '\x2', '\x2', '\x9C', '\x9D', '\a', 'w', '\x2', '\x2', 
		'\x9D', '\xA4', '\a', 'g', '\x2', '\x2', '\x9E', '\x9F', '\a', 'h', '\x2', 
		'\x2', '\x9F', '\xA0', '\a', '\x63', '\x2', '\x2', '\xA0', '\xA1', '\a', 
		'n', '\x2', '\x2', '\xA1', '\xA2', '\a', 'u', '\x2', '\x2', '\xA2', '\xA4', 
		'\a', 'g', '\x2', '\x2', '\xA3', '\x9A', '\x3', '\x2', '\x2', '\x2', '\xA3', 
		'\x9E', '\x3', '\x2', '\x2', '\x2', '\xA4', '\x38', '\x3', '\x2', '\x2', 
		'\x2', '\xA5', '\xA6', '\a', 'p', '\x2', '\x2', '\xA6', '\xA7', '\a', 
		'w', '\x2', '\x2', '\xA7', '\xA8', '\a', 'n', '\x2', '\x2', '\xA8', '\xA9', 
		'\a', 'n', '\x2', '\x2', '\xA9', ':', '\x3', '\x2', '\x2', '\x2', '\xAA', 
		'\xAC', '\t', '\x5', '\x2', '\x2', '\xAB', '\xAA', '\x3', '\x2', '\x2', 
		'\x2', '\xAC', '\xAD', '\x3', '\x2', '\x2', '\x2', '\xAD', '\xAB', '\x3', 
		'\x2', '\x2', '\x2', '\xAD', '\xAE', '\x3', '\x2', '\x2', '\x2', '\xAE', 
		'\xAF', '\x3', '\x2', '\x2', '\x2', '\xAF', '\xB0', '\b', '\x1E', '\x2', 
		'\x2', '\xB0', '<', '\x3', '\x2', '\x2', '\x2', '\xB1', '\xB5', '\t', 
		'\x6', '\x2', '\x2', '\xB2', '\xB4', '\t', '\a', '\x2', '\x2', '\xB3', 
		'\xB2', '\x3', '\x2', '\x2', '\x2', '\xB4', '\xB7', '\x3', '\x2', '\x2', 
		'\x2', '\xB5', '\xB3', '\x3', '\x2', '\x2', '\x2', '\xB5', '\xB6', '\x3', 
		'\x2', '\x2', '\x2', '\xB6', '>', '\x3', '\x2', '\x2', '\x2', '\xB7', 
		'\xB5', '\x3', '\x2', '\x2', '\x2', '\f', '\x2', '{', '\x80', '\x86', 
		'\x8C', '\x94', '\x98', '\xA3', '\xAD', '\xB5', '\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
