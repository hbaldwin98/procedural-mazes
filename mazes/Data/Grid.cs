using System.Data;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;

namespace mazes.Data;

public class Grid
{
  int Rows { get; set; }
  int Columns { get; set; }
  Cell[,] Cells { get; set; }

  public Grid(int rows, int columns)
  {
    Rows = rows;
    Columns = columns;
    Cells = new Cell[rows, columns];

    PrepareGrid();
    ConfigureCells();
  }

  public Cell this[int row, int col]
  {
    get
    {
      if (row < 0 || row >= Rows || col < 0 || col >= Columns) return null!;
      return Cells[row, col];
    }
    set
    {
      if (row >= 0 && row < Rows && col >= 0 && col < Columns) Cells[row, col] = value;
    }
  }

  // sets each cell in the grid to a new cell
  void PrepareGrid()
  {
    for (int i = 0; i < Rows; i++)
    {
      for (int j = 0; j < Columns; j++)
      {
        this[i, j] = new Cell(i, j);
      }
    }
  }

  // sets the neighbors of each cell in the grid
  void ConfigureCells()
  {
    foreach (Cell cell in Cells)
    {
      int row = cell.Row;
      int col = cell.Column;

      cell["north"] = this[row - 1, col];
      cell["south"] = this[row + 1, col];
      cell["west"] = this[row, col - 1];
      cell["east"] = this[row, col + 1];
    }
  }

  // retrieve a random cell from the grid
  public Cell RandomCell()
  {
    Random rnd = new Random();
    int row = rnd.Next(0, Rows);
    int col = rnd.Next(0, Columns);

    return this[row, col];
  }

  int Size()
  {
    return Rows * Columns;
  }

  // iterate over the each row of the grid
  public IEnumerable<Cell[]> RetrieveEachRow()
  {
    return Cells.Cast<Cell>().GroupBy(cell => cell.Row).Select(group => group.ToArray());
    // for (int i = 0; i < Rows; i++)
    // {
    //   yield return Enumerable.Range(0, Rows).Select(x => Cells[i, x]).ToArray();
    // }
  }

  // retrieve each cell in the grid
  public IEnumerable<Cell> RetrieveEachCell()
  {
    return RetrieveEachRow().SelectMany(row => row);
  }

  public override string ToString()
  {
    string output = "+" + string.Concat(Enumerable.Repeat("---+", Columns)) + "\n";
    // for each row in the grid
    foreach (Cell[] row in RetrieveEachRow())
    {
      string top = "|";
      string bottom = "+";

      foreach (Cell cell in row)
      {
        string body = "   ";
        string eastWall = cell["east"] != null ? (cell.Linked(cell["east"]) ? " " : "|") : "|";
        top += body + eastWall;

        string southWall = cell["south"] != null ? (cell.Linked(cell["south"]) ? "   " : "---") : "---";
        string corner = "+";
        bottom += southWall + corner;
      }

      output += top + "\n";
      output += bottom + "\n";
    }

    return output;
  }

  public void DrawGrid(int cellSize = 50)
  {
    int width = cellSize * Columns;
    int height = cellSize * Rows;
    int penWidth = 2;
    Bitmap bitmap = new Bitmap(width + penWidth, height + penWidth, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
    // create a new png image
    Pen wallPen = new Pen(Color.Black, penWidth);
    Graphics graphics = Graphics.FromImage(bitmap);
    using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255, 255)))
    {
      graphics.FillRectangle(brush, -penWidth, -penWidth, width, height);
    }
    foreach (Cell cell in RetrieveEachCell())
    {
      var x1 = cell.Column * cellSize;
      var y1 = cell.Row * cellSize;
      var x2 = (cell.Column + 1) * cellSize;
      var y2 = (cell.Row + 1) * cellSize;

      if (cell["north"] == null)
      {
        graphics.DrawLine(wallPen, x1, y1, x2, y1);
      }

      if (cell["west"] == null)
      {
        graphics.DrawLine(wallPen, x1, y1, x1, y2);
      }

      if ((cell["east"] != null && !cell.Linked(cell["east"])) || cell["east"] == null)
      {
        graphics.DrawLine(wallPen, x2, y1, x2, y2);
      }

      if ((cell["south"] != null && !cell.Linked(cell["south"])) || cell["south"] == null)
      {
        graphics.DrawLine(wallPen, x1, y2, x2, y2);
      }

    }

    bitmap.Save("maze.png");
  }
}
