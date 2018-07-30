using System;
using System.Windows.Forms;

namespace Bowling
{
  public partial class PlayerControl : UserControl
  {
    public int CurrentFrameIndex;
    public int CurrentThrow;
    public int CurrentFrameTotal
    {
      get
      {
        if (CurrentFrameIndex == Max && _frames[9].IsStrike()) return 0;
        return _frames[CurrentFrameIndex].Total;
      }
    }

    private Frame[] _frames;
    private Label[] _firstThrowLabels;
    private Label[] _secondThrowLabels;
    private Label[] _scoreLabels;

    private const string Strike = "X";
    private const string Spare = "/";
    private const int Max = 10;

    public PlayerControl()
    {
      InitializeComponent();
      InitializeControls();
      ClearView();
    }

    public void Throw(int pins)
    {
      if (CurrentThrow == 1) _frames[CurrentFrameIndex].Throw1 = pins;
      else _frames[CurrentFrameIndex].Throw2 = pins;

      UpdateThrowLabels();
      UpdateThrow();
    }

    public void ClearView()
    {
      InitializeControls();
      CurrentFrameIndex = 0;
      CurrentThrow = 1;

      for (var i = 0; i < Max; i++)
      {
        _firstThrowLabels[i].Text = string.Empty;
        _secondThrowLabels[i].Text = string.Empty;
        _scoreLabels[i].Text = string.Empty;
        bonusThrowLabel.Text = string.Empty;
      }
    }

    public void UpdateFinalScore()
    {
      frame10Score.Text = _frames.CalculateFrameScore(9).ToString();
    }

    private void UpdateThrow()
    {
      if (CurrentFrameIndex != Max && (_frames[CurrentFrameIndex].IsStrike() || CurrentThrow == 2))
      {
        CurrentFrameIndex++;
        CurrentThrow = 1;
      }
      else
      {
        CurrentThrow++;
      }
    }

    public bool IsDone()
    {
      var lastFrame = _frames[9];

      if (lastFrame.IsSpare())
      {
        return CurrentFrameIndex == Max && CurrentThrow == 2;
      }

      if (lastFrame.IsStrike())
      {
        return CurrentFrameIndex == Max && CurrentThrow == 3;
      }

      return CurrentFrameIndex >= Max;
    }

    private void UpdateThrowLabels()
    {
      for (var i = 0; i <= Math.Min(CurrentFrameIndex, 8); i++)
      {
        var frame = _frames[i];

        if (frame.IsStrike())
        {
          _secondThrowLabels[i].Text = Strike;
        }
        else if (frame.IsSpare())
        {
          _firstThrowLabels[i].Text = frame.Throw1.ToString();
          _secondThrowLabels[i].Text = Spare;
        }
        else
        {
          _firstThrowLabels[i].Text = frame.Throw1.ToString();
          if (CurrentThrow == 2) _secondThrowLabels[i].Text = frame.Throw2.ToString();
        }

        if (_frames.CanCalculateScore(i, CurrentFrameIndex, CurrentThrow))
        {
          _scoreLabels[i].Text = _frames.CalculateFrameScore(i).ToString();
        }
      }

      if (CurrentFrameIndex >= 9) UpdateLastFrame();
    }

    private void UpdateLastFrame()
    {
      var lastFrame = _frames[9];
      var bonusFrame = _frames[Max];

      if (lastFrame.IsStrike())
      {
        frame10throw1.Text = Strike;
        if (CurrentFrameIndex == Max)
        {
          frame10throw2.Text = bonusFrame.Throw1 == Max ? Strike : bonusFrame.Throw1.ToString();
          if (CurrentThrow == 2) bonusThrowLabel.Text = bonusFrame.Throw2 == Max ? Strike : bonusFrame.Throw2.ToString();
        }
      }
      else if (_frames[9].IsSpare())
      {
        frame10throw1.Text = lastFrame.Throw1.ToString();
        frame10throw2.Text = Spare;
        if (CurrentFrameIndex == Max) bonusThrowLabel.Text = bonusFrame.Throw1.ToString();
      }
      else
      {
        frame10throw1.Text = lastFrame.Throw1.ToString();
        if (CurrentThrow == 2) frame10throw2.Text = lastFrame.Throw2.ToString();
      }
    }

    private void InitializeControls()
    {
      CurrentThrow = 1;
      _frames = new Frame[11];
      for (var i = 0; i < 11; i++)
      {
        _frames[i] = new Frame();
      }

      _firstThrowLabels = new Label[10];
      _firstThrowLabels[0] = frame1throw1;
      _firstThrowLabels[1] = frame2throw1;
      _firstThrowLabels[2] = frame3throw1;
      _firstThrowLabels[3] = frame4throw1;
      _firstThrowLabels[4] = frame5throw1;
      _firstThrowLabels[5] = frame6throw1;
      _firstThrowLabels[6] = frame7throw1;
      _firstThrowLabels[7] = frame8throw1;
      _firstThrowLabels[8] = frame9throw1;
      _firstThrowLabels[9] = frame10throw1;

      _secondThrowLabels = new Label[10];
      _secondThrowLabels[0] = frame1throw2;
      _secondThrowLabels[1] = frame2throw2;
      _secondThrowLabels[2] = frame3throw2;
      _secondThrowLabels[3] = frame4throw2;
      _secondThrowLabels[4] = frame5throw2;
      _secondThrowLabels[5] = frame6throw2;
      _secondThrowLabels[6] = frame7throw2;
      _secondThrowLabels[7] = frame8throw2;
      _secondThrowLabels[8] = frame9throw2;
      _secondThrowLabels[9] = frame10throw2;

      _scoreLabels = new Label[10];
      _scoreLabels[0] = frame1Score;
      _scoreLabels[1] = frame2Score;
      _scoreLabels[2] = frame3Score;
      _scoreLabels[3] = frame4Score;
      _scoreLabels[4] = frame5Score;
      _scoreLabels[5] = frame6Score;
      _scoreLabels[6] = frame7Score;
      _scoreLabels[7] = frame8Score;
      _scoreLabels[8] = frame9Score;
      _scoreLabels[9] = frame10Score;
    }
  }
}
