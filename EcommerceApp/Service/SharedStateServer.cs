public static class SharedStateServer
{
  public static int TotalItems { get; set; } = 0;

  public static int GetTotalItems(int numItems)
  {
    TotalItems = numItems;
    return TotalItems;
  }
}