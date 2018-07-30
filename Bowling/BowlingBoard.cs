using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bowling
{
  public partial class BowlingBoard : Form
  {
    private List<PlayerControl> _players;
    private int _currentPlayerIndex;
    private Button[] _buttons;

    public BowlingBoard()
    {
      InitializeComponent();
      InitializeControls();
      ClearView();
    }

    private void Throw(int pins)
    {
      addPlayerButton.Enabled = false;

      var player = _players[_currentPlayerIndex];
      player.Throw(pins);

      UpdateCurrentPlayer();
      UpdateButtonsVisible();
    }

    private void UpdateCurrentPlayer()
    {
      var player = _players[_currentPlayerIndex];

      if (player.CurrentFrameIndex > 9 && player.IsDone())
      {
        _currentPlayerIndex = BowlingHelper.GetNextPlayerIndex(_players.Count, _currentPlayerIndex);
        player.UpdateFinalScore();
      }
      else if (player.CurrentFrameIndex <= 9 && player.CurrentThrow == 1)
      {
        _currentPlayerIndex = BowlingHelper.GetNextPlayerIndex(_players.Count, _currentPlayerIndex);
      }
    }

    private void UpdateButtonsVisible()
    {
      var player = _players[_currentPlayerIndex];

      if (BowlingHelper.IsGameOver(_currentPlayerIndex, player.IsDone()))
      {
        HideButtons();
        return;
      } 
      
      for (var i = 0; i < 11; i++)
      {
        _buttons[i].Visible = player.CurrentFrameTotal + i <= 10;
      }
    }

    private void HideButtons()
    {
      for (var i = 0; i < 11; i++)
      {
        _buttons[i].Visible = false;
      }
    }

    private void ClearView()
    {
      _players.Where(x => x != _players.First()).ToList().ForEach(x => Controls.Remove(x));
      _players = new List<PlayerControl> { playerControl1 };
      playerControl1.ClearView();

      addPlayerButton.Enabled = true;

      Size = new Size(559, 193);
      foreach (var button in _buttons)
      {
        button.Visible = true;
      }
    }

    private void InitializeControls()
    {
      _players = new List<PlayerControl> {playerControl1};
      _currentPlayerIndex = 0;

      _buttons = new Button[11];
      _buttons[0] = button0;
      _buttons[1] = button1;
      _buttons[2] = button2;
      _buttons[3] = button3;
      _buttons[4] = button4;
      _buttons[5] = button5;
      _buttons[6] = button6;
      _buttons[7] = button7;
      _buttons[8] = button8;
      _buttons[9] = button9;
      _buttons[10] = button10;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Throw(1);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Throw(2);
    }

    private void button3_Click(object sender, EventArgs e)
    {
      Throw(3);
    }

    private void button4_Click(object sender, EventArgs e)
    {
      Throw(4);
    }

    private void button5_Click(object sender, EventArgs e)
    {
      Throw(5);
    }

    private void button6_Click(object sender, EventArgs e)
    {
      Throw(6);
    }

    private void button7_Click(object sender, EventArgs e)
    {
      Throw(7);
    }

    private void button8_Click(object sender, EventArgs e)
    {
      Throw(8);
    }

    private void button9_Click(object sender, EventArgs e)
    {
      Throw(9);
    }

    private void button10_Click(object sender, EventArgs e)
    {
      Throw(10);
    }

    private void button0_Click(object sender, EventArgs e)
    {
      Throw(0);
    }

    private void newGameButton_Click(object sender, EventArgs e)
    {
      ClearView();
    }

    private void addPlayerButton_Click(object sender, EventArgs e)
    {
      var newPlayer = new PlayerControl {Location = new Point(12, _players.Last().Top + 78)};
      _players.Add(newPlayer);
      Controls.Add(newPlayer);
      Size = new Size(Width, Height + 78);
    }
  }
}
