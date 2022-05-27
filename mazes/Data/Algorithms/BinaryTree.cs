namespace mazes.Data.Algorithms;

public class BinaryTree
{
  /// <summary>
  /// Takes in a grid reference and returns that grid with the binary tree algorithm applied to it,
  /// creating a procedurally generated maze.
  /// </summary>
  /// <param name="grid">Reference to a grid (i.e. ref grid)</param>
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
