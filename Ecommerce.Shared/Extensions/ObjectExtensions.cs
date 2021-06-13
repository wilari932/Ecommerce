using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public static class ObjectExtensions
    {

		public static bool IsEquals(this object obj, object input)
		{
			if (obj != null)
			{
				return obj.Equals(input);
			}
			else
			{
				return (obj == input);
			}

		}
	}
}
