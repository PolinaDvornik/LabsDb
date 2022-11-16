using lab3_School.Models;
using Microsoft.Extensions.Caching.Memory;

namespace lab3_School.CachedModels
{
    public class CachedClassType
    {
        private readonly SchoolContext _context;
        private readonly IMemoryCache _cache;
        private double time = 2 * 8 + 240;
        public CachedClassType(SchoolContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public void AddClassTypes(string cacheKey, int rowNumber)
        {
            IEnumerable<ClassType> storages = _context.ClassTypes.Take(20).ToList();
            if (storages != null)
            {
                _cache.Set(cacheKey, storages, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(time)
                });
            }
        }

        public IEnumerable<ClassType> GetClassTypes(int rowNumber)
        {
            return _context.ClassTypes.Take(rowNumber).ToList();
        }

        public IEnumerable<ClassType> GetClassTypes(string cacheKey, int rowNumber)
        {
            IEnumerable<ClassType> storages;
            if (!_cache.TryGetValue(cacheKey, out storages))
            {
                storages = _context.ClassTypes.Take(rowNumber).ToList();
                if (storages != null)
                {
                    _cache.Set(cacheKey, storages, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(time)));
                }
            }
            return storages;
        }

        public string GetTable(IEnumerable<ClassType> classTypes)
        {
            string HtmlString = "<html><head><title>Типы классов</title>" +
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/></head><body>" +
                        "<a href = '/'>На главную</a><br>" +
                        "<p>Списко типов классов:</p>" +
                        "<table border=1>" +
                        "<tr>" +
                        "<th>Id</th>" +
                        "<th>Название</th>" +
                        "<th>Описание</th>" +
                        "</tr>";
            foreach (var classType in classTypes)
            {
                HtmlString += "<tr>" +
                $"<td>{classType.Id}</td>" +
                $"<td>{classType.Surname}</td>" +
                $"<td>{classType.Description}</td>" +
                "</tr>";
            }
            HtmlString += "</table></body></html>";
            return HtmlString;
        }

        public string GetTable(string name)
        {
            var classTypes = _context.ClassTypes.ToList()
                .Where(x => x.Surname.Trim().ToLower().Contains(name.Trim().ToLower()));
            return GetTable(classTypes);
        }
    }
}
