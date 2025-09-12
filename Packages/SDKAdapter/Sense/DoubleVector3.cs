using System;
using UnityEngine;

namespace GDK
{
	[Serializable]
	public struct DoubleVector3
	{
		public double x;
		public double y;
		public double z;

		public DoubleVector3(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3 ToVector3()
		{
			return new Vector3((float)x, (float)y, (float)z);
		}

		public override string ToString()
		{
			return $"{x},{y},{z}";
		}
	}
}