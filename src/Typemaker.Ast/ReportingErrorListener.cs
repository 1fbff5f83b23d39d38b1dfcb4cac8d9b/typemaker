﻿using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class ReportingErrorListener : IAntlrErrorListener<IToken>, IAntlrErrorListener<int>
	{
		readonly List<ParseError> output;

		readonly IVocabulary vocabulary;

		public ReportingErrorListener(List<ParseError> output, IVocabulary vocabulary)
		{
			this.output = output ?? throw new ArgumentNullException(nameof(output));
			this.vocabulary = vocabulary ?? throw new ArgumentNullException(nameof(vocabulary));
		}

		void AddParseError(int line, int column, string message) => output.Add(new ParseError((ulong)line, (ulong)column, message));

		public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e) => AddParseError(line, charPositionInLine, offendingSymbol == null ? msg : String.Format("Unexpected token {0} ({1})!", offendingSymbol.Text, vocabulary.GetSymbolicName(offendingSymbol.Type)));

		public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] int offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
		{
			string message;
			switch (offendingSymbol)
			{
				case TypemakerLexer.BANNED_MOJO_OPERATOR:
					message = "The 'operator' keyword is not allowed!";
					break;
				default:
					message = String.Format("Unexpected token {0} ({1})!", (char)offendingSymbol, offendingSymbol);
					break;
			}
			AddParseError(line, charPositionInLine, message);
		}
	}
}