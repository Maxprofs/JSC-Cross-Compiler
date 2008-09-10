using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsc.Languages.JavaScript.legacy
{
	class DelegateImplementationProvider
	{
		static string GetLambadaTitle(MethodInfo invoke)
		{
			StringBuilder w = new StringBuilder();

			ParameterInfo[] p = invoke.GetParameters();

			w.Append("(");

			for (int i = 0; i < p.Length; i++)
			{
				if (i > 0)
					w.Append(", ");

				w.Append(p[i].Name);
			}

			w.Append(") => ");

			w.Append(invoke.ReturnType.Name);

			return w.ToString();
		}

		static T Single<T>(Func<T[]> e)
		{
			return Single(e());
		}

		static T Single<T>(T[] e)
		{
			if (e.Length != 1)
				throw new ArgumentException();

			return e[0];
		}

		static string MethodParamsAsString(MethodBase e)
		{
			StringBuilder w = new StringBuilder();

			ParameterInfo[] p = e.GetParameters();

			for (int i = 0; i < p.Length; i++)
			{
				if (i > 0)
					w.Append(", ");

				w.Append(IdentWriter.GetDecoratedParameterInfo(p[i]));
			}

			return w.ToString();
		}

		static IEnumerable<KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo>> ToArray(Type e)
		{

			foreach (FieldInfo v in e.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				object[] vhint = v.GetCustomAttributes(typeof(ScriptCoreLib.ScriptDelegateDataHintAttribute), false);

				if (vhint.Length == 1)
				{
					yield return new KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo>((ScriptCoreLib.ScriptDelegateDataHintAttribute)vhint[0], v);
				}
			}
		}

		const string IsExtensionMethod = "IsExtensionMethod";
		public const string AsExtensionMethod = "AsExtensionMethod";


		/// <summary>
		/// writes the implementation for delegates that the Excecution Engine is responsible for
		/// </summary>
		/// <param name="w"></param>
		/// <param name="z"></param>
		public static void Write(IdentWriter w, Type z)
		{

			Type MulticastDelegate = w.Session.ResolveImplementation(z.BaseType);
			Type Delegate = w.Session.ResolveImplementation(z.BaseType.BaseType);

			FieldInfo FieldList = null;
			FieldInfo FieldTarget = null;
			FieldInfo FieldMethod = null;

			DelegateHint.Resolve(MulticastDelegate, Delegate,
				out FieldList,
				out FieldTarget,
				out FieldMethod
			);

			ConstructorInfo Constructor = Single<ConstructorInfo>(z.GetConstructors);
			MethodInfo Invoke = z.GetMethod("Invoke");

			w.WriteCommentLine("delegate: " + GetLambadaTitle(Invoke));


			w.Helper.DOMDefineNamedType(z, null);
			w.Helper.DefineAndAssignPrototype(z);

			// delegate ctor

			// events cannot be inherited
			// w.Helper.DefineTypeInheritanceConstructor(z, MulticastDelegate);

			#region IsExtensionMethod
			w.WriteIdent();
			w.Helper.WritePrototypeAlias(z);
			w.Helper.WriteAccessor();
			w.Write(IsExtensionMethod);
			w.Helper.WriteAssignment();
			w.Write("false");
			w.Write(";");
			w.WriteLine();
			#endregion

			#region AsExtensionMethod
			w.WriteIdent();
			w.Helper.WritePrototypeAlias(z);
			w.Helper.WriteAccessor();
			w.Write(AsExtensionMethod);
			w.Helper.WriteAssignment();
			w.Write("function ");
			w.Write("(");
			w.Write(")");
			w.WriteLine();

			w.WriteBeginScope();

			#region IsExtensionMethod
			w.WriteIdent();
			w.Write("this");
			w.Helper.WriteAccessor();
			w.Write(IsExtensionMethod);
			w.Helper.WriteAssignment();
			w.Write("true");
			w.Write(";");
			w.WriteLine();
			#endregion

			#region return this;
			w.WriteIdent();
			w.Write("return");
			w.WriteSpace();
			w.Write("this");
			w.Write(";");
			w.WriteLine();
			#endregion


			w.EndScopeAndTerminate();

			#endregion

			ConstructorInfo MulticastDelegateConstructor = Single<ConstructorInfo>(MulticastDelegate.GetConstructors);

			w.Helper.DefineTypeMemberMethodAs(z, Constructor, MulticastDelegateConstructor);
			w.Helper.DefineTypeInheritanceConstructor(z, Constructor, MulticastDelegate);

			// we need to provide support for extension methods


			w.Helper.DefineTypeMemberMethodHeader(z, Invoke);
			w.WriteBeginScope();

			const string Local_a = "_arguments";
			const string Local_target = "_target";

			#region WriteForEach
			Action<Action> WriteForEach = delegate(Action f)
			{
				w.WriteIdent();
				w.WriteLine(string.Format("for (var i = 0; i < this.{0}.length; i++)", FieldList.Name));

				w.WriteBeginScope();

				w.WriteIdent();
				w.WriteLine(string.Format("var f = this.{0}[i];", FieldList.Name));

				// we need to update arguments
				#region var Local_a
				w.WriteIdent();
				w.Write("var");
				w.WriteSpace();
				w.Write(Local_a);
				w.Helper.WriteAssignment();


				// http://ninghui48.blogspot.com/2007/09/javascript-arguments.html

				w.Write("Array.prototype.slice.call(arguments)");
				w.Write(".");
				w.Write("slice(0)");
				w.Write(";");
				w.WriteLine();
				#endregion

				#region add this pointer to argumetns for extension methods
				w.WriteIdent();
				w.Write("if");

				w.Write("(");
				w.Write("f");
				w.Helper.WriteAccessor();
				w.Write(IsExtensionMethod);
				w.Write(")");
				w.WriteSpace();


				w.Write(Local_a);

				w.Helper.WriteAccessor();
				w.Write("splice(0, 0, ");
				w.Write("f");
				w.Helper.WriteAccessor();
				w.Write(FieldTarget.Name);
				w.Write(")");

				w.Write(";");
				w.WriteLine();
				#endregion


				#region select function owner

				w.WriteIdent();
				w.Write("var");
				w.WriteSpace();
				w.Write(Local_target);
				w.Helper.WriteAssignment();


				w.Write("f");
				w.Helper.WriteAccessor();
				w.Write(IsExtensionMethod);

				w.WriteSpace();
				w.Write("?");
				w.WriteSpace();


				// static functions live in the window at the moment cuz it is the global object
				w.Write("window");

				w.WriteSpace();
				w.Write(":");
				w.WriteSpace();

				w.Write("f");
				w.Helper.WriteAccessor();
				w.Write(FieldTarget.Name);

				w.Write(";");
				w.WriteLine();

				#endregion


				w.WriteIdent();
				f();


				w.Write(Local_target);
				w.Write("[");
				w.Write("f");
				w.Helper.WriteAccessor();
				w.Write(FieldMethod.Name);
				w.Write("]");
				w.Helper.WriteAccessor();
				w.Write("apply");
				w.Write("(");
				w.Write(Local_target);
				w.Write(", ");
				w.Write(Local_a);

				w.Write(")");
				w.Write(";");
				w.WriteLine();

				w.WriteEndScope();
			};
			#endregion


			if (Invoke.ReturnType == typeof(void))
			{
				WriteForEach(
					delegate
					{
					}
				);


			}
			else
			{
				w.WriteIdent();
				w.WriteLine("var _ = void(0);");

				WriteForEach(
				   delegate
				   {
					   w.Write("_");
					   w.Helper.WriteAssignment();
				   }
				);

				w.WriteIdent();
				w.WriteLine("return _;");
			}



			w.EndScopeAndTerminate();


			w.WriteLine();
		}
	}
}
