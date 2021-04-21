using System;

namespace SharedCodeLibrary
{
  public class PagingParameters
  {
    public int FirstElementPosition { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int Offset { get; set; }

    public PagingParameters(int page, int pageSize = 10, int offset = 0)
    {  
      bool infPage = Math.Abs(Page) == int.MaxValue;
      bool infPageSize = Math.Abs(PageSize) == int.MaxValue;
      bool infOffset = Math.Abs(Offset) == int.MaxValue;

      if (infPage || infPageSize || infOffset)
      {
        return;
      }
      else
      {
        Page = page;
        PageSize = pageSize;
        Offset = offset;
        FirstElementPosition = (page - 1) * PageSize + offset;
      }     
    }
  }
}
