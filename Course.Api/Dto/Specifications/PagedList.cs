using System.Linq;

namespace CourseApi.Dto.Specifications;


public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    public PagedList(List<T> items, int  count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData()
        {
            TotalCount = count,
            PageSzie = pageSize,
            Total = (int)Math.Ceiling(count / (double)pageSize)
        };
        AddRange(items);
    }
  


    public static PagedList<T> ToPagedList(IEnumerable<T> entidad, int pageNumber, int pageSize)
    {
        var count = entidad.Count();
        var items = entidad.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }



}