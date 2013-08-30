using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{

	[Script(Implements = typeof(global::System.Text.StringBuilder))]
	internal class __StringBuilder
	{
		public __StringBuilder()
		{

		}

		string _Value = "";

        public int Length
        {
            get
            {
                return this._Value.Length;
            }
        }

		public __StringBuilder Append(bool e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(double e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(uint e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(byte e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(int e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(string e)
		{
			_Value += e;

			return this;
		}

        public __StringBuilder Append(char e)
        {
            _Value += new string( e, 1);

            return this;
        }

        public __StringBuilder Append(string value, int startIndex, int count)
        {
            return Append(value.Substring(startIndex, count));
        }


		public __StringBuilder Append(object value)
		{
			if (value != null)
			{
				_Value += value.ToString();
			}

			return this;
		}

		public __StringBuilder AppendLine()
		{
			return Append(Environment.NewLine);
		}

		public __StringBuilder AppendLine(string value)
		{
			return Append(value).AppendLine();
		}

        public __StringBuilder AppendLine(string value, int startIndex, int count)
        {
            return Append(value.Substring(startIndex, count)).AppendLine();
        }

		public override string ToString()
		{
			return _Value;
		}
	}

}
