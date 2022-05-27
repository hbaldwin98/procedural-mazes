namespace mazes.Data.Algorithms;

public class Sidewinder
{
  /// <summary>
  /// Takes in a grid reference and returns that grid with the sidewinder algorithm applied to it,
  /// creating a procedurally generated maze.
  /// </summary>
  /// <param name="grid">Reference to a grid (i.e. ref grid)</param>
  public static void On(ref Grid grid)
  {
    foreach (Cell[] cells in grid.RetrieveEachRow())
    {
      List<Cell> run = new List<Cell>();

      foreach (Cell cell in cells)
      {
        run.Add(cell);
        
        bool atEasternBoundary = cell["east"] == null;
        bool atNorthernBoundary = cell["north"] == null;
        
        // if we're at the eastern boundary, then we need to
        // open up a new run to the north.
        // if we're at the northern boundary, then we need to
        // open up a new run to the east.
        bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && new Random().Next(2) == 0);
        
        if (shouldCloseOut)
        {
          // pick a random cell from the run and link its
          // northern neighbor.
          Cell member = run[new Random().Next(run.Count)];
          if (member["north"] != null) member.Link(member["north"]);
          run.Clear();
        }
        else
        {
          // link the cell to the cell to the east.
          cell.Link(cell["east"]);
        }
      }
    }
  }
}
