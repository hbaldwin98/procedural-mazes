namespace mazes.Data.Algorithms;

public class Sidewinder
{
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
        
        bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && new Random().Next(2) == 0);

        if (shouldCloseOut)
        {
          Cell member = run[new Random().Next(run.Count)];
          if (member["north"] != null) member.Link(member["north"]);
          run.Clear();
        }
        else
        {
          cell.Link(cell["east"]);
        }
      }
    }
  }
}
