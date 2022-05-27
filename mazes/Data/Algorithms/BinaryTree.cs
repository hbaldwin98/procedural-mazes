namespace mazes.Data.Algorithms;

public class BinaryTree
{
  public static void On(ref Grid grid)
  {
    foreach (Cell cell in grid.RetrieveEachCell())
    {
      // create an empty array of cells
      List<Cell> neighbors = new List<Cell>();

      // save the neighbors in the array
      Cell north = cell["north"];
      Cell east = cell["east"];
      if (north != null) neighbors.Add(north);
      if (east != null) neighbors.Add(east);

      int index = new Random().Next(neighbors.Count());
      
      Cell neighbor = null!;
      if (neighbors.Count > 0) { neighbor = neighbors[index]; }
      
      if (neighbor != null) { cell.Link(neighbor); }
    }

  }
}
