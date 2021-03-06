﻿#region license
// Copyright (c) 2004, Rodrigo B. de Oliveira (rbo@acm.org)
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

using System;
using System.Collections;
using Boo.Lang.Compiler.TypeSystem;
using Assembly = System.Reflection.Assembly;

namespace Boo.Lang.Compiler
{
	/// <summary>
	/// Referenced assemblies collection.
	/// </summary>
	[Boo.Lang.EnumeratorItemType(typeof(Assembly))]
	public class AssemblyCollection : CollectionBase
	{
		public AssemblyCollection()
		{
		}
	
		public void Add(Assembly assembly)
		{
			if (!Contains(assembly))
			{
				InnerList.Add(assembly);
			}
		}

		public void Extend(IEnumerable assemblies)
		{
			foreach (Assembly assembly in assemblies)
			{
				Add(assembly);
			}
		}
		
		public bool Contains(Assembly assembly)
		{
			AssertNotNull(assembly);

			foreach (Assembly existing in InnerList)
			{
				if (AssemblyEqualityComparer.Default.Equals(existing, assembly))
				{
					return true;
				}
			}
			return false;
		}
		
		public Assembly Find(string simpleName)
		{
			foreach (Assembly assembly in InnerList)
			{
				if (assembly.GetName().Name == simpleName)
				{
					return assembly;
				}
			}
			
			return null;
		}

		private static void AssertNotNull(Assembly assembly)
		{
			if (null == assembly) throw new ArgumentNullException("assembly");
		}
	}
}
