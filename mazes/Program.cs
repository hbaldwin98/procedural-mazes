using mazes.Data;
using mazes.Data.Algorithms;

Grid grid = new Grid(10, 10);
// BinaryTree.On(ref grid); 
Sidewinder.On(ref grid);
grid.DrawGrid();
Console.WriteLine(grid.ToString());