using System.Collections;
using System.Data;

namespace mazes.Data;

public class Cell
{
  public int Row { get; set; }
  public int Column { get; set; }
  Dictionary<Cell, bool> Links { get; set; }
  Dictionary<string, Cell> Neighbors { get; set; }
  
  public Cell(int row, int column)
  {
    Row = row;
    Column = column;
    Links = new Dictionary<Cell, bool>();
    Neighbors = new Dictionary<string, Cell>();
  }

  // get and set neighbors with indexer
  public Cell this[string key]
  {
    get => Neighbors[key];
    set => Neighbors[key] = value;
  }

  // add a link between this cell and another cell
  public void Link(Cell cell, bool bidirection = true)
  {
    Links.Add(cell, bidirection);
    if (bidirection) {cell.Link(this, false);}
  }

  // remove a link between this cell and another cell
  void UnLink(Cell cell, bool bidirection = true)
  {
    Links.Remove(cell);
    if (bidirection) {cell.UnLink(this, false);}
  }
  
  // get a list of all linked cells
  List<Cell> CellLinks()
  {
    return Links.Keys.ToList();
  }
  
  // check if this cell is linked to another cell
  public bool Linked(Cell cell)
  {
    return Links.ContainsKey(cell);
  }
  
}
