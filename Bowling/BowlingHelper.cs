namespace Bowling
{
  public static class BowlingHelper
  {
    public static bool IsStrike(this Frame frame)
    {
      return frame.Throw1 == 10;
    }

    public static bool IsSpare(this Frame frame)
    {
      return frame.Throw1 != 10 && frame.Throw1 + frame.Throw2 == 10;
    }

    public static int CalculateFrameScore(this Frame[] frames, int frame)
    {
      var score = 0;

      for (var i = 0; i <= frame; i++)
      {
        var currentFrame = frames[i];

        if (currentFrame.IsSpare()) score += frames.CalculateSpareScore(i);
        else if (currentFrame.IsStrike()) score += frames.CalculateStrikeScore(i);
        else score += currentFrame.Total;
      }

      return score;
    }

    public static bool CanCalculateScore(this Frame[] frames, int frameIndex, int currentFrame, int currentThrow)
    {
      if (frames[frameIndex].IsSpare())
      {
        if (currentFrame == frameIndex + 1) return currentThrow == 1;
        return currentFrame > frameIndex;
      }

      if (frames[frameIndex].IsStrike())
      {
        if (currentFrame >= frameIndex + 2) return true;

        if (currentFrame == frameIndex + 1 && !frames[frameIndex + 1].IsStrike() && currentThrow == 2) return true;

        return false;
      }

      return currentFrame > frameIndex || currentThrow == 2;
    }

    public static int GetNextPlayerIndex(int total, int current)
    {
      var index = current+1;
      if (index >= total) return 0;
      return index;
    }

    public static bool IsGameOver(int current, bool isDone)
    {
      if (current != 0) return false;

      return isDone;
    }

    private static int CalculateSpareScore(this Frame[] frames, int i)
    {
      return i == 9 ? frames[i].Total + frames[i + 1].Total : frames[i].Total + frames[i + 1].Throw1;
    }

    private static int CalculateStrikeScore(this Frame[] frames, int i)
    {
      var score = frames[i].Total;

      if (frames[i + 1].IsStrike()) score += i == 9 ? frames[i+1].Total : frames[i+1].Throw1 + frames[i+2].Throw1;
      else score += frames[i + 1].Total;

      return score;
    }
  }
}
