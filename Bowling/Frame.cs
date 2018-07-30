namespace Bowling
{
  public class Frame
  {
    public int Throw1 { get; set; }
    public int Throw2 { get; set; }
    public int Total => Throw1 + Throw2;
  }
}
