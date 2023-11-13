using System;

namespace Game.Helpers
{
public static class MathExt
{
	public static bool IsZero(this double v) => Math.Abs(v) < double.Epsilon;
}
}