﻿#region license
// Copyright (c) 2003, 2004, 2005 Rodrigo B. de Oliveira (rbo@acm.org)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Rodrigo B. de Oliveira nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion


namespace Boo.Lang.Extensions

import Boo.Lang.Compiler
import Boo.Lang.Compiler.Ast

class MacroMacro(AbstractAstMacro):

	override def Expand(macro as MacroStatement):
		if macro.Arguments.Count != 1 or macro.Arguments[0].NodeType != NodeType.ReferenceExpression:
			raise "macro <reference>"
		
		EnclosingModule(macro).Members.Add(CreateMacroType(macro))
		
	def PascalCase(name as string):
		return char.ToUpper(name[0]) + name[1:]

	private def CreateMacroType(macro as MacroStatement):
		macroName = (macro.Arguments[0] as ReferenceExpression).Name

		klass = [|
			class $(PascalCase(macroName) + "Macro")(Boo.Lang.Compiler.LexicalInfoPreservingMacro):
				override protected def ExpandImpl($macroName as Boo.Lang.Compiler.Ast.MacroStatement):
					$(macro.Block)
		|]
		klass.LexicalInfo = macro.LexicalInfo
		return klass

	private def EnclosingModule(macro as Node) as Module:
		return macro.GetAncestor(NodeType.Module)
