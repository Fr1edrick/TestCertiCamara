using System;

namespace TestCertiCamara.CustomExtension
{
  public static class CustomExtension
  {
    public static int CheckNumber(this int value)
    {
      return Convert.ToInt32(value);
    }
  }
}
